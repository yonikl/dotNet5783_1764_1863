namespace Dal;
using DO;

internal static class DataSource
{
    static readonly Random s_generator = new Random();
    internal static Order[] s_orders = new Order[100];
    internal static OrderItem[] s_ordersItems = new OrderItem[200];
    internal static Product[] s_products = new Product[50];
    
    public static void AddNewOrder(Order order)
    {
        
        s_orders[Config.ordersSize++] = order;
        
    }

    public static void AddNewOrderItem(OrderItem orderItem)
    {
        
        s_ordersItems[Config.ordersItemsSize++] = orderItem;
        
    }

    public static void AddNewProduct(Product product)
    {
        
        s_products[Config.productsSize++] = product;
        
    }

    static DataSource()
    {
        s_Initialize();
    }
    private static void s_Initialize()
    {
        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            order.CustomerName = ((Enums.ClientsNames)(i % 10)).ToString();
            order.CustomerAdress = ((Enums.ClientsAddresses)(i % 10)).ToString();
            order.CustomerEmail = ((Enums.ClientsNames)(i % 10)).ToString() + "@gmail.com";
            order.OrderDate = DateTime.Now - new TimeSpan();
        }
    }



    internal static class Config
    {
        internal static int ordersSize = 0;
        internal static int ordersItemsSize = 0;
        internal static int productsSize = 0;

        internal static int IdForOrdersItems = 1;
        internal static int IdForOrders = 1;

        internal static int GetIdForOrdersItems => IdForOrdersItems++;
        internal static int GetIdForOrders => IdForOrders++;
    }
} 
