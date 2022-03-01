using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using redux.Data;
using redux.Models;

namespace redux.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _dbcontext;
        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public DbManageController(AppDbContext dbcontext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
            _roleManager = roleManager;
            
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult DeleteDb()
        {
            return View();
        }

        [TempData]
        public string StatusMessage {set;get;}
        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
           var success = await _dbcontext.Database.EnsureDeletedAsync();

           StatusMessage = success ? "Xoa Database thanh cong" : "Khong xoa duoc";

           return RedirectToAction(nameof(Index));

        }

        [HttpPost]
         public async Task<IActionResult> Migrate()
        {
           await _dbcontext.Database.MigrateAsync();

           StatusMessage =  "Tao Database thanh cong";

           return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> SeedDataAsync()
        {
            var rolenames = typeof(RoleName).GetFields().ToList();
            foreach(var r in rolenames)
            {
                var rolename = (string)r.GetRawConstantValue();
                var rfound = await _roleManager.FindByNameAsync(rolename);
                if (rfound == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(rolename));
                }

            }

            //admin pass=admin123, admin@example.com

            var useradmin = await _userManager.FindByEmailAsync("admin");
            if(useradmin == null)
            {
                useradmin = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(useradmin, "admin123");
                await _userManager.AddToRoleAsync(useradmin, RoleName.Administrator);

            }
            StatusMessage = "Vừa seed Database";
            return RedirectToAction("Index");
        }
    }
}