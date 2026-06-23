using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartInventory.Models;

namespace SmartInventory.Services;

public interface IPurchaseService
{
    List<Purchase> GetAll();
    Purchase Create(int productId, int quantity, decimal unitCost);
}
