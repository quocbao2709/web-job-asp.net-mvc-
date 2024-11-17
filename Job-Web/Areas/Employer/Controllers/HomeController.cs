using Microsoft.AspNetCore.Mvc;

namespace Job_Web.Areas.Employer.Controllers;

[Area("Employer")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}