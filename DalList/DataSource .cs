namespace Dal;
using DO;

internal static class DataSource
{
    
    internal static readonly Random s_generator = new Random();

    //lists to store the entities
    internal static List<Order?> s_orders = new List<Order?>();
    internal static List<OrderItem?> s_ordersItems = new List<OrderItem?>();
    internal static List<Product?> s_products = new List<Product?>();
    /// <summary>
    /// adding orders to the list
    /// </summary>
    public static void AddNewOrders()
    {
        //date that from him the orders start
        DateTime startOfOrders = DateTime.Now - new TimeSpan(365, 0, 0, 0);
        //adding 20 order
        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            order.Id = Config.GetIdForOrders;
            order.CustomerName = ((Enums.ClientsNames)(i % 10)).ToString();
            order.CustomerAddress = ((Enums.ClientsAddresses)(i % 10)).ToString();
            order.CustomerEmail = ((Enums.ClientsNames)(i % 10)).ToString() + "@gmail.com";

            //adding max of 180 days to the start orders date until order date
            order.OrderDate = startOfOrders + new TimeSpan(s_generator.Next(180), 0, 0, 0);
            //adding 16 orders with shipping dates
            if (i <= 16)
            {
                //adding max of 90 days to the order date until shipping date
                order.ShipDate = order.OrderDate + new TimeSpan(s_generator.Next(90), 0, 0, 0);
            }
            else
            {
                order.ShipDate = null;
            }
            //adding 10 orders with delivery dates
            if (i <= 10)
            {
                //adding max of 90 days to the shipping date until delivery date
                order.DeliveryDate = order.ShipDate + new TimeSpan(90, 0, 0, 0);
            }
            else
            {
                order.DeliveryDate = null;
            }
            s_orders.Add(order);

        }

    }

    /// <summary>
    /// add orders items to the list
    /// </summary>
    public static void AddNewOrderItem()
    {
        OrderItem orderItem = new OrderItem();

        //adding 40 - 80 orders items (2-4 for each order
        for (int i = 0; i < s_orders.Count; i++)
        {
            for (int j = 0; j < s_generator.Next(2, 4); j++)
            {
                orderItem.Id = Config.GetIdForOrdersItems;
                orderItem.OrderID = (int)s_orders[i]?.Id;

                //random product
                Product product = (Product)s_products[s_generator.Next(0, s_products.Count)];
                orderItem.ProductID = product.ID;
                orderItem.Price = product.Price;
                orderItem.Amount = s_generator.Next(1, 10);
                s_ordersItems.Add(orderItem);
                
            }
        }
        
        
    }

    /// <summary>
    /// adding products to the list
    /// </summary>
    public static void AddNewProducts()
    {
        //adding 10 products
        s_products.Add(new Product { Category = Enums.Category.Pants, ID = 944737, InStock = 0, Name = "Simon Pants 48", Price = 200.0 });
        s_products.Add(new Product { Category = Enums.Category.Pants, ID = 189456, InStock = 7, Name = "Jeans Pants 45", Price = 250.0 });
        s_products.Add(new Product { Category = Enums.Category.Coat, ID = 242897, InStock = 6, Name = "Outdoor Coat 42", Price = 300.0 });
        s_products.Add(new Product { Category = Enums.Category.Coat, ID = 347348, InStock = 12, Name = "The North Face Coat 43", Price = 340.0 });
        s_products.Add(new Product { Category = Enums.Category.Shoe, ID = 365462, InStock = 3, Name = "Adidas Shoes 40", Price = 280.0 });
        s_products.Add(new Product { Category = Enums.Category.Shoe, ID = 298765, InStock = 18, Name = "Nike Shoes 38", Price = 290.0 });
        s_products.Add(new Product { Category = Enums.Category.Shirt, ID = 867452, InStock = 10, Name = "Castro T-Shirt L", Price = 150.0 }); 
        s_products.Add(new Product { Category = Enums.Category.Shirt, ID = 398475, InStock = 20, Name = "MJ Sport Shirt S", Price = 450.0 });
        s_products.Add(new Product { Category = Enums.Category.Sock, ID = 899384, InStock = 35, Name = "Nike training Socks 38-42", Price = 80.0 });
        s_products.Add(new Product { Category = Enums.Category.Sock, ID = 899384, InStock = 35, Name = "Kumi Sneakers Socks 32-36", Price = 45.0 });
        
        
    }

    /// <summary>
    /// empty constructor
    /// </summary>
    static DataSource()
    {
        s_Initialize();
    }
    /// <summary>
    /// calling the functions to fill the list
    /// </summary>
    private static void s_Initialize()
    {
        AddNewProducts();

        AddNewOrders();

        AddNewOrderItem();



    }

    


    internal static class Config
    {
        //id for entities
        internal static int IdForOrdersItems = 1;
        internal static int IdForOrders = 1;

        //getting id for entities
        internal static int GetIdForOrdersItems => IdForOrdersItems++;
        internal static int GetIdForOrders => IdForOrders++;
    }
} 
