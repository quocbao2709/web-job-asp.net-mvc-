using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Job_Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult AccessDenied()
    {
        return View();
    }
    
}