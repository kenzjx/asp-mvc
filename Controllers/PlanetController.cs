using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using redux.Services;

namespace redux.Controllers
{
    [Route("he-mat-troi/[action]")]
    public class PlanetController : Controller
    {
        private readonly ILogger<PlanetController> _logger;
        private readonly PlanetService _planetService;

        public PlanetController(ILogger<PlanetController> logger, PlanetService planetService )
        {
            _logger = logger;
            _planetService = planetService;
        
        }

        [Route("Danh-sach-cac-hanh-tinh.html")]
        public IActionResult Index()
        {
            return View();
        }
        [BindProperty(SupportsGet = true, Name ="action")]
        public string VnName {set;get;}


        public IActionResult Mercury()
        {
           var planet = _planetService.Where(p=>p.VnName == VnName).FirstOrDefault();
            return View("Detail", planet);
        }

        [HttpGet("traidat")]
         public IActionResult Earth()
        {
           var planet = _planetService.Where(p=>p.VnName == VnName).FirstOrDefault();
            return View("Detail", planet);
        }
         public IActionResult Mars()
        {
           var planet = _planetService.Where(p=>p.VnName == VnName).FirstOrDefault();
            return View("Detail", planet);
        }
         public IActionResult Venus()
        {
           var planet = _planetService.Where(p=>p.VnName == VnName).FirstOrDefault();
            return View("Detail", planet);
        }
         public IActionResult Neptune()
        {
           var planet = _planetService.Where(p=>p.VnName == VnName).FirstOrDefault();
            return View("Detail", planet);
        }
         public IActionResult Jupiter()
        {
           var planet = _planetService.Where(p=>p.VnName == VnName).FirstOrDefault();
            return View("Detail", planet);
        }
         public IActionResult Uranus()
        {
           var planet = _planetService.Where(p=>p.VnName == VnName).FirstOrDefault();
            return View("Detail", planet);
        }

        [Route("[controller]-[action].html", Order =3, Name ="saturn1" ), ]
        
        [Route("[controller]/[action].html", Order =2)]
        
        
        [Route("sao/[action].html", Order =1)] // sao/Saturn
         public IActionResult Saturn()
        {
           var planet = _planetService.Where(p=>p.VnName == VnName).FirstOrDefault();
            return View("Detail", planet);
        }


        [Route("hanhtinh/{id:int}")] // hanh tinh /1

         public IActionResult PlanetInfo(int id)
        {
           var planet = _planetService.Where(p=>p.Id == id).FirstOrDefault();
            return View("Detail", planet);
        }
    }
}