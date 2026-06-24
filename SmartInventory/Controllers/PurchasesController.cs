using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using SmartInventory.Services;

namespace SmartInventory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchasesController : ControllerBase
{
    private readonly IPurchaseService _service;

    public PurchasesController(IPurchaseService service) => _service = service;

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpPost]
    public IActionResult Create(int productId, int quantity, decimal unitCost)
    {
        try
        {
            var purchase = _service.Create(productId, quantity, unitCost);
            return Ok(purchase);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
