using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using SmartInventory.Data;

namespace SmartInventory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _db;

    public DashboardController(AppDbContext db) => _db = db;

    [HttpGet("summary")]
    public IActionResult GetSummary()
    {
        return Ok(new
        {
            totalProducts = _db.Products.Count(),
            totalStock = _db.Products.Sum(p => p.Stock),
            totalSales = _db.Sales.Count(),
            lowStock = _db.Products.Count(p => p.Stock <= 5)
        });
    }

    [HttpGet("sales-by-period")]
    public IActionResult GetSalesByPeriod([FromQuery] string period = "week")
    {
        var from = period == "month"
            ? DateTime.UtcNow.AddMonths(-6)
            : DateTime.UtcNow.AddDays(-7).Date;

        var sales = _db.Sales
            .Where(s => s.SaleDate >= from)
            .AsEnumerable()
            .GroupBy(s => period == "month"
                ? s.SaleDate.ToString("yyyy-MM")
                : s.SaleDate.Date.ToString("yyyy-MM-dd"))
            .Select(g => new { label = g.Key, total = g.Sum(s => s.Quantity) })
            .OrderBy(x => x.label)
            .ToList();

        return Ok(sales);
    }

    [HttpGet("top-products")]
    public IActionResult GetTopProducts()
    {
        var top = _db.Sales
            .GroupBy(s => s.ProductId)
            .Select(g => new { productId = g.Key, totalSold = g.Sum(s => s.Quantity) })
            .OrderByDescending(x => x.totalSold)
            .Take(5)
            .ToList()
            .Select(x => new
            {
                x.productId,
                x.totalSold,
                name = _db.Products.Find(x.productId)?.Name ?? "Desconocido"
            })
            .ToList();

        return Ok(top);
    }
}
