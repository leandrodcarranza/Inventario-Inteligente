using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Models;
using SmartInventory.Services;

namespace SmartInventory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [Authorize(Roles = "admin,seller")]
    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [Authorize(Roles = "admin,seller")]
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _service.GetById(id);
        return product is null ? NotFound() : Ok(product);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Create(Product product)
    {
        var created = _service.Add(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, Product product)
    {
        var updated = _service.Update(id, product);
        return updated is null ? NotFound() : Ok(updated);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _service.Delete(id) ? NoContent() : NotFound();
    }
}