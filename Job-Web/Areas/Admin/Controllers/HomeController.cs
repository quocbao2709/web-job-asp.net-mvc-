using Microsoft.AspNetCore.Mvc;

namespace Job_Web.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}