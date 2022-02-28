
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using redux.Models.Contacts;

namespace redux.Data {
    
    // Kế thừa từ IdentityDbContext nên có sẵn các DbSet
    // UserRoles Roles RoleClaimsUsers UserClaims UserLogins UserTokens
    public class AppDbContext : DbContext {

        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder builder) {

            // base.OnModelCreating (builder); 
            // // Bỏ tiền tố AspNet của các bảng: mặc định các bảng trong IdentityDbContext có
            // // tên với tiền tố AspNet như: AspNetUserRoles, AspNetUser ...
            // // Đoạn mã sau chạy khi khởi tạo DbContext, tạo database sẽ loại bỏ tiền tố đó
            // foreach (var entityType in builder.Model.GetEntityTypes ()) {
            //     var tableName = entityType.GetTableName ();
            //     if (tableName.StartsWith ("AspNet")) {
            //         entityType.SetTableName (tableName.Substring (6));
            //     }
            // }
        }

        public DbSet<Contact> Contacts {set;get;}

    }
}