using Microsoft.AspNetCore.Mvc;

namespace Job_Web.Areas.JobSeeker.Controllers;

[Area("JobSeeker")]
public class HomeControllers : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}