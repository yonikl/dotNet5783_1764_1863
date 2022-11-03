using DO;
namespace Dal;
using DO;

internal static class DataSource
{
    static readonly int Random;
    internal static Order[] Orders = new Order[200];
    internal static OrderItem[] OrdersItems = new OrderItem[200];
    internal static Product[] Products = new Product[50];
}