using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SushiPopG5.Models;
using System;
using System.Diagnostics;

namespace SushiPopG5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DateTime today = DateTime.Now;
            string horarioAtencion = ObtenerHorarioAtencion(today.DayOfWeek);

            ViewData["Day"] = today.ToString("dddd");
            ViewData["HorarioAtencion"] = horarioAtencion;

            return View();
        }

        private string ObtenerHorarioAtencion(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                    return "11 a 14 horas";
                case DayOfWeek.Friday:
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    return "11 a 14 horas y 19 a 23 horas";
                default:
                    return string.Empty;
            }
        }

        public IActionResult Privacy()
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
