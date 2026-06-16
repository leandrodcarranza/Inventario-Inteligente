using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SmartInventory.Data;
using SmartInventory.Models;

namespace SmartInventory.Services;

public class PostgresProductService : IProductService
{
    private readonly AppDbContext _db;

    public PostgresProductService(AppDbContext db)
    {
        _db = db;
    }

    public List<Product> GetAll() => _db.Products.ToList();

    public Product? GetById(int id) => _db.Products.Find(id);

    public Product Add(Product product)
    {
        product.InitialStock = product.Stock;

        _db.Products.Add(product);
        _db.SaveChanges();

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
        _db.SaveChanges();
        return existing;
    }

    public bool Delete(int id)
    {
        var product = GetById(id);
        if (product is null) return false;
        _db.Products.Remove(product);
        _db.SaveChanges();
        return true;
    }
}
