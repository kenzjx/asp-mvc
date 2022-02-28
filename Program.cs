using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using redux.Data;
using redux.ExtendMethods;
using redux.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
