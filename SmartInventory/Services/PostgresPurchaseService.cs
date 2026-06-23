using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmartInventory.Data;
using SmartInventory.Models;

namespace SmartInventory.Services;

public class PostgresPurchaseService : IPurchaseService
{
    private readonly AppDbContext _db;

    public PostgresPurchaseService(AppDbContext db) => _db = db;

    public List<Purchase> GetAll() => _db.Purchases.ToList();

    public Purchase Create(int productId, int quantity, decimal unitCost)
    {
        var product = _db.Products.Find(productId);
        if (product == null) throw new Exception("Producto no encontrado");

        product.Stock += quantity;
        product.InitialStock += quantity;

        var purchase = new Purchase
        {
            ProductId = productId,
            Quantity = quantity,
            UnitCost = unitCost
        };

        _db.Purchases.Add(purchase);
        _db.SaveChanges();
        return purchase;
    }
}
