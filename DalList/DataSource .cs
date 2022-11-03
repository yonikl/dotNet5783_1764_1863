namespace Dal;
using DO;

internal static class DataSource
{
    static readonly Random Generator = new Random();
    internal static Order[] Orders = new Order[200];
    internal static OrderItem[] OrdersItems = new OrderItem[200];
    internal static Product[] Products = new Product[50];


    internal static class Config
    {
        internal static int OrdersSize = 0;
        internal static int OrdersItemsSize = 0;
        internal static int ProductsSize = 0;

        internal static int IdForOrdersItems = 1;
        internal static int IdForOrders = 1;

        internal static int GetIdForOrdersItems => ++IdForOrdersItems;
        internal static int GetIdForOrders => ++IdForOrders;
    }
} 
