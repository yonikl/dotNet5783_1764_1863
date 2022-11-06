namespace Dal;
using DO;

internal static class DataSource
{
    
    internal static readonly Random s_generator = new Random();

    //arrays to store the entities
    internal static Order[] s_orders = new Order[100];
    internal static OrderItem[] s_ordersItems = new OrderItem[200];
    internal static Product[] s_products = new Product[50];
    
    public static void AddNewOrders()
    {
        //date that from him the orders start
        DateTime startOfOrders = DateTime.Now - new TimeSpan(365, 0, 0, 0);
        //adding 20 orders
        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            order.Id = Config.GetIdForOrders;
            order.CustomerName = ((Enums.ClientsNames)(i % 10)).ToString();
            order.CustomerAdress = ((Enums.ClientsAddresses)(i % 10)).ToString();
            order.CustomerEmail = ((Enums.ClientsNames)(i % 10)).ToString() + "@gmail.com";

            //adding max of 180 days to the start orders date until order date
            order.OrderDate = startOfOrders + new TimeSpan(180, 0, 0, 0);
            //adding 16 orders with shipping dates
            if (i <= 16)
            {
                //adding max of 90 days to the order date until shipping date
                order.ShipDate = order.OrderDate + new TimeSpan(s_generator.Next(90), 0, 0, 0);
            }
            else
            {
                order.ShipDate = DateTime.MinValue;
            }
            //adding 10 orders with delivery dates
            if (i <= 10)
            {
                //adding max of 90 days to the shipping date until delivery date
                order.DeliveryDate = order.ShipDate + new TimeSpan(90, 0, 0, 0);
            }
            else
            {
                order.DeliveryDate = DateTime.MinValue;
            }
            s_orders[Config.ordersSize++] = order;

        }

    }

    public static void AddNewOrderItem()
    {
        OrderItem orderItem = new OrderItem();

        //adding 40 - 80 orders items (2-4 for each order
        for (int i = 0; i < Config.ordersSize; i++)
        {
            for (int j = 0; j < s_generator.Next(2, 4); j++)
            {
                orderItem.Id = Config.GetIdForOrdersItems;
                orderItem.OrderID = s_orders[i].Id;

                //random product
                Product product = s_products[s_generator.Next(0, Config.productsSize)];
                orderItem.ProductID = product.ID;
                orderItem.Price = product.Price;
                orderItem.Amount = s_generator.Next(1, 10);
                s_ordersItems[Config.ordersItemsSize++] = orderItem;
            }
        }
        
        
    }

    public static void AddNewProducts()
    {
        //adding 10 products
        s_products[Config.productsSize++] = new Product { Category = Enums.Category.Pants, ID = 944737, InStock = 0, Name = "Simon Pants 48", Price = 200.0 });
        s_products[Config.productsSize++] = new Product { Category = Enums.Category.Pants, ID = 189456, InStock = 7, Name = "Jeans Pants 45", Price = 250.0 });
        s_products[Config.productsSize++] = new Product { Category = Enums.Category.Coat, ID = 242897, InStock = 6, Name = "Outdoor Coat 42", Price = 300.0 });
        s_products[Config.productsSize++] = new Product { Category = Enums.Category.Coat, ID = 347348, InStock = 12, Name = "The North Face Coat 43", Price = 340.0 });
        s_products[Config.productsSize++] = new Product { Category = Enums.Category.Shoe, ID = 365462, InStock = 3, Name = "Adidas Shoes 40", Price = 280.0 });
        s_products[Config.productsSize++] = new Product { Category = Enums.Category.Shoe, ID = 298765, InStock = 18, Name = "Nike Shoes 38", Price = 290.0 });
        s_products[Config.productsSize++] = new Product { Category = Enums.Category.Shirt, ID = 867452, InStock = 10, Name = "Castro T-Shirt L", Price = 150.0 });
        s_products[Config.productsSize++] = new Product { Category = Enums.Category.Shirt, ID = 398475, InStock = 20, Name = "MJ Sport Shirt S", Price = 450.0 });
        s_products[Config.productsSize++] = new Product { Category = Enums.Category.Sock, ID = 899384, InStock = 35, Name = "Nike training Socks 38-42", Price = 80.0 });
        s_products[Config.productsSize++] = new Product { Category = Enums.Category.Sock, ID = 899384, InStock = 35, Name = "Kumi Sneakers Socks 32-36", Price = 45.0 });
        
        
    }

    static DataSource()
    {
        s_Initialize();
    }
    private static void s_Initialize()
    {
        AddNewProducts();

        AddNewOrders();

        AddNewOrderItem();



    }

    


    internal static class Config
    {
        //the arrays sizes
        internal static int ordersSize = 0;
        internal static int ordersItemsSize = 0;
        internal static int productsSize = 0;

        //id for entities
        internal static int IdForOrdersItems = 1;
        internal static int IdForOrders = 1;

        //getting id for entities
        internal static int GetIdForOrdersItems => IdForOrdersItems++;
        internal static int GetIdForOrders => IdForOrders++;
    }
} 
