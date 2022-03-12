
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using redux.Models;


using redux.Models.Contacts;

namespace redux.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
             builder.Entity<PostCategory>().HasKey(p => new {p.PostID, p.CategoryID});
            
             builder.Entity<Product>().HasData(
                new Product() {
                    ProductId = 1,
                    Name = "Đá phong thuỷ tự nhiên",
                    Description = "Số 1 cao 40cm rộng 20cm dày 20cm màu xanh lá cây đậm",
                    Price = 1000000
                },
                new Product() {
                    ProductId = 2,
                    Name = "Đèn đá muối hình tròn",
                    Description = "Trang trí trong nhà Chất liệu : • Đá muối",
                    Price = 1500000
                },
                new Product() {
                    ProductId = 3,
                    Name = "Tranh sơn mài",
                    Description = "Tranh sơn mài loại nhỏ 15x 15 giá 50.000",
                    Price = 50000
                } ,
                new Product() {
                    ProductId = 4,
                    Name = "Tranh sơn dầu - Ngựa",
                    Description = "Nguyên liệu thể hiện :    Sơn dầu",
                    Price = 450000
                }
        );
            // Bỏ tiền tố AspNet của các bảng: mặc định các bảng trong IdentityDbContext có
            // tên với tiền tố AspNet như: AspNetUserRoles, AspNetUser ...
            // Đoạn mã sau chạy khi khởi tạo DbContext, tạo database sẽ loại bỏ tiền tố đó
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
             base.OnModelCreating(builder);
          
        }
        public DbSet<Category> Categories {set;get;}
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<Product> Products {set;get;}
        
    public DbSet<Post> Posts {set; get;}
    public DbSet<PostCategory> PostCategories {set; get;}
    }
}