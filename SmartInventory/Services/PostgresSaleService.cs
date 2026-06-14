using SmartInventory.Data;
using SmartInventory.Models;

namespace SmartInventory.Services;

public class PostgresSaleService : ISaleService
{
    private readonly AppDbContext _db;

    public PostgresSaleService(AppDbContext db)
    {
        _db = db;
    }

    public List<Sale> GetAllSales()
    {
        return _db.Sales.ToList();
    }

    public Sale CreateSale(int productId, int quantity)
    {
        var product = _db.Products.Find(productId);

        if (product == null)
            throw new Exception("Producto no encontrado");

        if (product.Stock < quantity)
            throw new Exception("Stock insuficiente");

        product.Stock -= quantity;

        var sale = new Sale
        {
            ProductId = productId,
            Quantity = quantity
        };

        _db.Sales.Add(sale);
        _db.SaveChanges();

        return sale;
    }
}