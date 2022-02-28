using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using redux.Data;

namespace redux.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _dbcontext;

        public DbManageController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
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
    }
}