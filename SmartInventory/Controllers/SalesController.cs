using Microsoft.AspNetCore.Mvc;
using SmartInventory.Services;

namespace SmartInventory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SalesController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_saleService.GetAllSales());
    }

    [HttpPost]
    public IActionResult CreateSale(int productId, int quantity)
    {
        try
        {
            var sale = _saleService.CreateSale(productId, quantity);
            return Ok(sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}