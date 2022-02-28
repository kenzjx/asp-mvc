using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using redux.Services;

namespace redux.Controllers
{
    
    [Area("ProductManage")]
    public class ProductController : Controller
    {

        private readonly ProductService _productService;

         private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, ProductService productService)

        {
            _productService = productService;
            _logger = logger;
        }

        [Route("/cac-san-pham")]
        public IActionResult Index()
        {
            var products = _productService.OrderBy(p=>p.Name).ToList();
            return View(products);
        }
    }
}