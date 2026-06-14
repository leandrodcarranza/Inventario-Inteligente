using SmartInventory.Models;

namespace SmartInventory.Services;

public interface ISaleService
{
    Sale CreateSale(int productId, int quantity);
    List<Sale> GetAllSales();
}