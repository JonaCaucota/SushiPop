using Microsoft.AspNetCore.Mvc;

namespace SushiPopG5.Controllers;

public class LegalController: Controller
{
    public IActionResult Index()
    {
        return View();
    }
}