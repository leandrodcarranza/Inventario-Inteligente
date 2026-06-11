using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmartInventory.Models;

namespace SmartInventory.Services;

public class InMemoryProductService : IProductService
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Teclado Mecánico", Description = "Switch Blue", Price = 45000, Stock = 10, Category = "Periféricos" },
        new Product { Id = 2, Name = "Monitor 24\"", Description = "Full HD 144Hz", Price = 180000, Stock = 5, Category = "Monitores" },
        new Product { Id = 3, Name = "Mouse Gamer", Description = "RGB 12000 DPI", Price = 22000, Stock = 20, Category = "Periféricos" },
    };

    private int _nextId = 4;

    public List<Product> GetAll() => _products;

    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public Product Add(Product product)
    {
        product.Id = _nextId++;
        _products.Add(product);
        return product;
    }

    public Product? Update(int id, Product product)
    {
        var existing = GetById(id);
        if (existing is null) return null;

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.Stock = product.Stock;
        existing.Category = product.Category;
        return existing;
    }

    public bool Delete(int id)
    {
        var product = GetById(id);
        if (product is null) return false;
        _products.Remove(product);
        return true;
    }
}
