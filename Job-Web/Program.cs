using Job_Web.Data;
using Job_Web.Middleware; // Thêm dòng này để chắc chắn `ApplicationDbContext` được nhận diện
using Microsoft.EntityFrameworkCore;
using Job_Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
// Cấu hình DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cấu hình Identity
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddRoles<IdentityRole>();


// Cấu hình phân quyền
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

// Cấu hình Authentication (Cookie Authentication)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Đường dẫn trang đăng nhập
        options.AccessDeniedPath = "/Admin/Home/AccessDenied"; // Đường dẫn trang AccessDenied
    });




// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<User>>();

    await SeedData.Initialize(roleManager, userManager);
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
// Sử dụng Session
app.UseSession();
// Đăng ký Middleware
app.UseMiddleware<UserSessionMiddleware>();


app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();