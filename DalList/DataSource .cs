﻿namespace Dal;
using DO;

internal static class DataSource
{
    internal static readonly Random s_generator = new Random();
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
        AddNewProduct(new Product { Category = Enums.Category.Pants, ID = 944737, InStock = 0, Name = "Simon Pants 48", Price = 200.0 });
        AddNewProduct(new Product { Category = Enums.Category.Pants, ID = 189456, InStock = 7, Name = "Jeans Pants 45", Price = 250.0 });
        AddNewProduct(new Product { Category = Enums.Category.Coat, ID = 242897, InStock = 6, Name = "Outdoor Coat 42", Price = 300.0 });
        AddNewProduct(new Product { Category = Enums.Category.Coat, ID = 347348, InStock = 12, Name = "The North Face Coat 43", Price = 340.0 });
        AddNewProduct(new Product { Category = Enums.Category.Shoe, ID = 365462, InStock = 3, Name = "Adidas Shoes 40", Price = 280.0 });
        AddNewProduct(new Product { Category = Enums.Category.Shoe, ID = 298765, InStock = 18, Name = "Nike Shoes 38", Price = 290.0 });
        AddNewProduct(new Product { Category = Enums.Category.Shirt, ID = 867452, InStock = 10, Name = "Castro T-Shirt L", Price = 150.0 });
        AddNewProduct(new Product { Category = Enums.Category.Shirt, ID = 398475, InStock = 20, Name = "MJ Sport Shirt S", Price = 450.0 });
        AddNewProduct(new Product { Category = Enums.Category.Sock, ID = 899384, InStock = 35, Name = "Nike training Socks 38-42", Price = 80.0 });
        AddNewProduct(new Product { Category = Enums.Category.Sock, ID = 899384, InStock = 35, Name = "Kumi Sneakers Socks 32-36", Price = 45.0 });

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
