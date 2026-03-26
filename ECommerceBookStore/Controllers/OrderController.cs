using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using ECommereceBookStore.Data;
using ECommereceBookStore.Models;

[Authorize] // ✅ user must login
public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ CREATE PAGE
    public IActionResult Create()
    {
        return View();
    }

    // ✅ SAVE ORDER (FROM CART)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateOrder()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cartItems = _context.CartItems
            .Include(c => c.Book)
            .Where(c => c.UserId == userId)
            .ToList();

        if (cartItems == null || !cartItems.Any())
        {
            return RedirectToAction("Index", "Cart");
        }

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            Status = "Pending",
            TotalAmount = cartItems.Sum(c => c.Book.Price * c.Quantity),
            OrderItems = cartItems.Select(c => new OrderItem
            {
                BookId = c.BookId,
                Quantity = c.Quantity,
                Price = c.Book.Price
            }).ToList()
        };

        _context.Orders.Add(order);
        _context.CartItems.RemoveRange(cartItems);
        _context.SaveChanges();

        return RedirectToAction("MyOrders");
    }

    // ✅ USER ORDER LIST
    public IActionResult MyOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var orders = _context.Orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToList();

        return View(orders);
    }

    // ✅ ORDER DETAILS (IMPORTANT FIX)
    public IActionResult Details(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var order = _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Book)
            .FirstOrDefault(o => o.Id == id && o.UserId == userId);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }
}