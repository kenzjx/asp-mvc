using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using redux.Services;

namespace redux.Controllers
{
    public class FirstController : Controller
    {
        private readonly ProductService productService;
        private readonly ILogger<FirstController> _logger;
        private readonly IWebHostEnvironment _env;

        public FirstController(ILogger<FirstController> logger, IWebHostEnvironment env, ProductService _productService)
        {
            productService = _productService;
            _logger = logger;
            _env = env;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Index action");
          return Content("Xin chao cac ban", "text/plain");
        }

        public void Nothing()
        {
            Response.Headers.Add("Hi", "xuan chao cac banj");
        }

        public IActionResult Bird()
        {
            string filePath = Path.Combine(_env.ContentRootPath, "Files", "1200.jpeg");
            var bytes = System.IO.File.ReadAllBytes(filePath);
          return  File(bytes,"image/jpg" );
        }

        public IActionResult IphonePrice()
        {
            return Json(
                new{
                    productName = "Iphone",
                    Price = 1000
                }
            );
        }

        public IActionResult Privacy()
        {
            var url = Url.Action("Pryivacy", "Home");
            return LocalRedirect(url);
        }

         public IActionResult Google()
        {
            var url = "https://google.com";
            return Redirect(url);
        }

        public IActionResult HelloView(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                username="kahch";
            }
             return View("xinchao3", username);
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult ViewProduct (int? id)
        {
            var product = productService.Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            
            this.ViewData["product"] = product;
            return View("ViewProduct2");
        }
    }
}