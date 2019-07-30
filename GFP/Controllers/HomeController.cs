using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GFP.Models;

namespace GFP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Elegible()
        {
            return View();
        }

        public IActionResult Reglas()
        {
            return View();
        }

        public IActionResult AdminReglas()
        {
            return View();
        }

        public IActionResult Consolidado()
        {
            return View();
        }

        public IActionResult EnvioSinpe()
        {
            return View();
        }

        public IActionResult Historico()
        {
            return View();
        }

        public IActionResult Tesoro()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
