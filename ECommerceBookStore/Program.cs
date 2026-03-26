using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ECommereceBookStore.Data;
using ECommereceBookStore.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Identity Setup
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ✅ Cookie Config
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

// ✅ MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ VERY IMPORTANT
app.UseAuthentication();
app.UseAuthorization();

// ✅ DEFAULT ROUTE (FIX FOR YOUR ERROR)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();