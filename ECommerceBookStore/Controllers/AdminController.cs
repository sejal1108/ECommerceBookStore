using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommereceBookStore.Models;
using ECommereceBookStore.Data;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ LIST ALL ORDERS
    public IActionResult Orders()
    {
        var orders = _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Book)
            .ToList();

        return View(orders);
    }

    // ✅ ORDER DETAILS (VERY IMPORTANT)
    public IActionResult Details(int id)
    {
        var order = _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Book)
            .FirstOrDefault(o => o.Id == id);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    // ✅ UPDATE STATUS
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateStatus(int orderId, string newStatus)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

        if (order != null)
        {
            order.Status = newStatus;
            _context.SaveChanges();
        }

        return RedirectToAction("Details", new { id = orderId });
    }

    // ✅ DELETE ORDER
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteOrder(int id)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == id);

        if (order != null)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        return RedirectToAction("Orders");
    }
}