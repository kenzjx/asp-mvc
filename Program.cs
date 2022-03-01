using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using redux.Data;
using redux.ExtendMethods;
using redux.Models;
using redux.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOptions();
var mailsettings = builder.Configuration.GetSection ("MailSettings");  // đọc config
        builder.Services.Configure<MailSettings> (mailsettings); 
               // đăng ký để Inject
builder.Services.AddTransient<IEmailSender, SendMailService>();
builder.Services.AddDbContext<AppDbContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppMvcConnectionString"));
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<RazorViewEngineOptions>(options=>{
    // default /View/Controller/Action.cshml
    options.ViewLocationFormats.Add("/MyView/{1}/{0}"+RazorViewEngine.ViewExtension);
});

builder.Services.AddSingleton<ProductService, ProductService>();
builder.Services.AddSingleton<PlanetService, PlanetService>();
builder.Services.AddIdentity<AppUser, IdentityRole> ()
                .AddEntityFrameworkStores<AppDbContext> ()
                .AddDefaultTokenProviders ();
builder.Services.AddAuthentication ()
                .AddGoogle (googleOptions => {
                    // Đọc thông tin Authentication:Google từ appsettings.json
                    IConfigurationSection googleAuthNSection = builder.Configuration.GetSection ("Authentication:Google");

                    // Thiết lập ClientID và ClientSecret để truy cập API google
                    googleOptions.ClientId = googleAuthNSection["ClientId"];
                    googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
                    // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
                    googleOptions.CallbackPath = "/dang-nhap-tu-google";

                })
                .AddFacebook (facebookOptions => {
                    // Đọc cấu hình
                    IConfigurationSection facebookAuthNSection = builder.Configuration.GetSection ("Authentication:Facebook");
                    facebookOptions.AppId = facebookAuthNSection["AppId"];
                    facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
                    // Thiết lập đường dẫn Facebook chuyển hướng đến
                    facebookOptions.CallbackPath = "/dang-nhap-tu-facebook";
                });
            // Truy cập IdentityOptions
builder.Services.Configure<IdentityOptions> (options => {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 50; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true; // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false; // Xác thực số điện thoại

            });
            
builder.Services.Configure<RouteOptions> (options => {
                options.LowercaseUrls = true;                   // url chữ thường
                options.LowercaseQueryStrings = false;          // không bắt query trong url phải in thường
            });
 builder.Services.ConfigureApplicationCookie (options => {
                // options.Cookie.HttpOnly = true;  
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  
                options.LoginPath = $"/login/"; 
                options.LogoutPath = $"/logout/";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            builder.Services.Configure<SecurityStampValidatorOptions>(options => 
            {
                // Trên 30 giây truy cập lại sẽ nạp lại thông tin User (Role)
                // SecurityStamp trong bảng User đổi -> nạp lại thông tinn Security
                options.ValidationInterval = TimeSpan.FromSeconds(5); 
            });
builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();


            builder.Services.AddAuthorization(options =>
            {
                // options.AddPolicy("MinimumAge", policy => {
                //     policy.Requirements.Add(new MinimumAgeRequirement(18) {
                //         OpenTime = 8,
                //         CloseTime = 22
                //     });
                // });

                options.AddPolicy("ViewManageMenu", builder =>{
                    builder.RequireAuthenticatedUser();
                    builder.RequireRole(RoleName.Administrator);
                });

                options.AddPolicy("AdminDropdown", policy => {
                    policy.RequireClaim("permission", "manage.user");
                });
                

                options.AddPolicy("MyPolicy1", policy => {
                    policy.RequireRole("Vip");
                });

                options.AddPolicy("CanViewTest", policy => {
                    policy.RequireRole("VipMember","Editor");
                });

                
                options.AddPolicy("CanView", policy => {
                    // policy.RequireRole("Editor");
                    policy.RequireClaim("permision", "post.view");
                });


            });
            // builder.Services.AddTransient<IAuthorizationHandler, MinimumAgeHandler>();
            // builder.Services.AddTransient<IAuthorizationHandler, CanUpdatePostAgeHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.AddStatusCodePage();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(enpoints =>{
    enpoints.MapGet("/sayhi", async (context)=>{
        await context.Response.WriteAsync($"Hello Aspnet {DateTime.Now}");
    });
    enpoints.MapRazorPages();
    enpoints.MapControllerRoute(
        name: "first",
        pattern: "{url}/{id}",
        defaults: new {
            controller = "First",
            action = "ViewProduct"
        
        },
        constraints: new {
            url = "xemsanpham",
            id = new RangeRouteConstraint(2,4)
        }
    );

    enpoints.MapAreaControllerRoute(
        name: "product",
        pattern: "{controller}/{action=Index}/{id?}",
        areaName : "ProductManage"
    );

    enpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
    
});

    
app.MapRazorPages();
app.Run();
