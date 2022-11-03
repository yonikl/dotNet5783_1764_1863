namespace Dal;
using DO;

internal static class DataSource
{
    static readonly Random Generator = new Random();
    internal static Order[] Orders = new Order[100];
    internal static OrderItem[] OrdersItems = new OrderItem[200];
    internal static Product[] Products = new Product[50];
    
    private static int OrdersSize = 0;
    private static int ProductsSize = 0;
    private static int OrdersItemSize = 0;

    public static void AddNewOrder(Order order)
    {
        if (OrdersSize < 100)
        {
            Orders[OrdersSize] = order;
            OrdersSize++;
        }
    }

    public static void AddNewOrderItem(OrderItem orderItem)
    {
        if (OrdersItemSize < 200)
        {
            OrdersItems[OrdersSize] = orderItem;
            OrdersItemSize++;
        }
    }

    public static void AddNewProduct(Product product)
    {
        if (ProductsSize < 50)
        {
            Products[ProductsSize] = product;
            ProductsSize++;
        }
    }

    static DataSource()
    {
        s_Initialize();
    }
    private static void s_Initialize()
    {

    }
}


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
