using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace PL.Models;

internal class Shop
{
    private IBl? bl = Factory.Get();
    private Cart? cart { get; }
    public IEnumerable<OrderForList?> Orders => bl!.Order.GetAllOrders();
    public IEnumerable<ProductForList?> Products => bl!.Product.GetAllProducts();

}
