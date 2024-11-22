using System.Diagnostics;
using Job_Web.Data;
using Microsoft.AspNetCore.Mvc;
using Job_Web.Models;
using BCrypt.Net;

namespace Job_Web.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(UserRegisterModel model)
    {
        if (ModelState.IsValid)
        {
            // Kiểm tra xem email đã tồn tại chưa
            var existingUser = _context.Users.SingleOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(model);
            }

            // Tạo mới user và lưu vào database
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = HashPassword(model.Password), // Mã hóa mật khẩu
                Role = model.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
            return RedirectToAction("Login");
        }

        return View(model);
    }
    // GET: Account/Login
    // Hiển thị form đăng nhập
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(UserLoginModel model)
    {
        if (ModelState.IsValid)
        {
            // Tìm người dùng từ cơ sở dữ liệu
            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                // Lưu thông tin người dùng vào session
                HttpContext.Session.SetString("UserName", user.Username);
                HttpContext.Session.SetString("Role", user.Role);

                // Chuyển hướng tới trang chính hoặc trang quản lý
                return RedirectToAction("Dashboard", "Account");
            }
            else
            {
                // Thông báo lỗi nếu thông tin đăng nhập không đúng
                ModelState.AddModelError("", "Invalid email or password.");
            }
        }

        // Nếu model không hợp lệ, quay lại form đăng nhập
        return View(model);
    }
    
    public IActionResult Dashboard()
    {
        // Lấy thông tin người dùng từ session
        var userName = HttpContext.Session.GetString("UserName");
        var role = HttpContext.Session.GetString("Role");

        if (userName == null || role == null)
        {
            // Nếu session không tồn tại, chuyển hướng đến trang đăng nhập
            return RedirectToAction("Login", "Account");
        }

        // Hiển thị thông tin người dùng trên dashboard
        ViewData["UserName"] = userName;
        ViewData["Role"] = role;

        return View();
    }
    public IActionResult Logout()
    {
        // Xóa tất cả thông tin session
        HttpContext.Session.Clear();

        // Chuyển hướng về trang đăng nhập
        return RedirectToAction("Login", "Account");
    }


    private string HashPassword(string password)
    {
        // Sử dụng BCrypt để mã hóa mật khẩu
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    public IActionResult SomeAction()
    {
        ViewData["UserName"] = HttpContext.Session.GetString("UserName");
        ViewData["Role"] = HttpContext.Session.GetString("Role");

        return View();
    }

}