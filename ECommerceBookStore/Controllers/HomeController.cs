using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ECommereceBookStore.Data;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var books = _context.Books.ToList(); // ✅ load data
        return View(books);
    }
}