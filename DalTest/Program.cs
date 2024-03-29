﻿using System;
using System.Reflection.Emit;
using Dal;
using DalApi;
using DO;

public class Program
{
    /// <summary>
    /// define del order, del order item, del product
    /// </summary>
    private static IDal s_dalList = DalApi.Factory.Get();
    
    public static int Main(string[] args)
    {
        init();
        int choice;
        PrintMenu();//print the menu 
        choice = int.Parse(Console.ReadLine());//read choice.

        while (choice != 0)// if choice not equal to zero
        {
            switch (choice)
            {
                case 1://for order
                    PrintOrderMenu();
                    break;

                case 2://for item
                    PrintProductMenu();
                    break;
                
                case 3://for product
                    
                    PrintItemMenu();
                    break;

                default:
                    Console.WriteLine("ERROR");
                    break;
            }
            PrintMenu();
            choice = int.Parse(Console.ReadLine());

        }


        return 0;

    }

    public static void init()
    {
        Random s_generator = new Random();
        List<Order> s_orders = new List<Order>();
        //date that from him the orders start
        DateTime startOfOrders = DateTime.Now - new TimeSpan(365, 0, 0, 0);
        //adding 20 order
        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            order.Id = i + 1;
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
        foreach (var product in s_orders)
        {
            s_dalList.Order.Add(product);
        }
        /////////////////////////
        List<Product> s_products = new List<Product>();
        s_products.Add(new Product { Category = Enums.Category.Pants, ID = 944737, InStock = 0, Name = "Simon Pants 48", Price = 200.0 });
        s_products.Add(new Product { Category = Enums.Category.Pants, ID = 189456, InStock = 7, Name = "Jeans Pants 45", Price = 250.0 });
        s_products.Add(new Product { Category = Enums.Category.Coat, ID = 242897, InStock = 6, Name = "Outdoor Coat 42", Price = 300.0 });
        s_products.Add(new Product { Category = Enums.Category.Coat, ID = 347348, InStock = 12, Name = "The North Face Coat 43", Price = 340.0 });
        s_products.Add(new Product { Category = Enums.Category.Shoe, ID = 365462, InStock = 3, Name = "Adidas Shoes 40", Price = 280.0 });
        s_products.Add(new Product { Category = Enums.Category.Shoe, ID = 298765, InStock = 18, Name = "Nike Shoes 38", Price = 290.0 });
        s_products.Add(new Product { Category = Enums.Category.Shirt, ID = 867452, InStock = 10, Name = "Castro T-Shirt L", Price = 150.0 });
        s_products.Add(new Product { Category = Enums.Category.Shirt, ID = 398475, InStock = 20, Name = "MJ Sport Shirt S", Price = 450.0 });
        s_products.Add(new Product { Category = Enums.Category.Sock, ID = 899384, InStock = 35, Name = "Nike training Socks 38-42", Price = 80.0 });
        s_products.Add(new Product { Category = Enums.Category.Sock, ID = 656475, InStock = 35, Name = "Kumi Sneakers Socks 32-36", Price = 45.0 });
        foreach (var product in s_products)
        {
            s_dalList.Product.Add(product);
        }
        ////////////////////////
        List<OrderItem> s_ordersItems = new List<OrderItem>();

        OrderItem orderItem;

        //adding 40 - 80 orders items (2-4 for each order
        for (int i = 0; i < s_orders.Count; i++)
        {
            for (int j = 0; j < s_generator.Next(2, 4); j++)
            {
                orderItem = new OrderItem();
                orderItem.OrderID = (int)s_orders[i].Id;

                //random product
                Product product = (Product)s_products[s_generator.Next(0, s_products.Count)];
                orderItem.ProductID = product.ID;
                orderItem.Price = product.Price;
                orderItem.Amount = s_generator.Next(1, 10);
                s_ordersItems.Add(orderItem);

            }
        }

        foreach (var product in s_ordersItems)
        {
            s_dalList.OrderItem.Add(product);
        }
    }

    public static void PrintMenu()//print the menu for the user
    {
        Console.WriteLine("For exist - 0\n" +
                          "For check order - 1\n" +
                          "For check product - 2\n" +
                          "For check item - 3\n");
       
    }
    public static void PrintOrderMenu()//print order menu and the option for the user
    {
        Console.WriteLine("To add a new order - a\n" +
                          "Check your order by id - b\n" +
                          "To see all your orders - c\n" +
                          "To update your order - d\n" +
                          "To delete order from your list- e\n");
        char choose = char.Parse(Console.ReadLine());//read the choose
        int id;
        Order o = new Order();
        switch (choose)
        {
            case 'a'://to add new order
                Console.WriteLine("Enter your name, email and adress\n");
              
                o.CustomerName = Console.ReadLine();//read the name of the user
                o.CustomerEmail = Console.ReadLine();//read the emial of the user
                o.CustomerAddress = Console.ReadLine();//read the adress of the user
                s_dalList.Order.Add(o);//add the order 
                break;
            case 'b'://check the order
                Console.WriteLine("Enter order id:");
                id = int.Parse(Console.ReadLine());//read the order id
                try
                { 
                    o = s_dalList.Order.Get(id);//get the order using the id
                    Console.WriteLine(o);//print the order
                }
                catch (DalItemNotFoundException)//in case the order id mot found
                {
                    Console.WriteLine("ERROR");
                    break;
                }
                break;
            case 'c':
                IEnumerable<Order> orders = s_dalList.Order.GetAll();//get all the orders

                foreach (Order i in orders)//print all the orders
                {
                    Console.WriteLine(i);
                }
                break;
            case 'd'://update the order
                Console.WriteLine("Enter order id");
                o.Id = int.Parse(Console.ReadLine());//read the id order
                try
                {
                    o = s_dalList.Order.Get(o.Id);//get the order by using the id
                    Console.WriteLine(o);//print the order
                }
                catch (DalItemNotFoundException)//if the order id not found
                {
                    break;
                }
                break;
                //update the order
                Console.WriteLine("Enter name, email and adrass:");
                //read the details for the new order
                o.CustomerName = Console.ReadLine();
                o.CustomerEmail = Console.ReadLine();
                o.CustomerAddress = Console.ReadLine();
                s_dalList.Order.Update(o);//update the order
                break;
            case 'e'://delete order from the array
                Console.WriteLine("Enter order id:");
                id = int.Parse(Console.ReadLine());//read the order id
                try
                {
                     s_dalList.Order.Delete(id);//delete the order
                }
                catch (DalItemNotFoundException)//if the order id not found
                {
                    break;
                }
                break;
            default:
                Console.WriteLine("ERROR INPUT");
                break;
        }
    }

    public static void PrintProductMenu()
    {
        Console.WriteLine("To add a new product - a\n" +
                          "Check your product by id - b\n" +
                          "To see all your products - c\n" +
                          "To update your product - d\n" +
                          "To delete product from your list- e\n");
        char choose = char.Parse(Console.ReadLine());
        int id;
        Product p = new Product();
        switch (choose)
        {
            case 'a'://add new product
                Console.WriteLine("Enter product name, price, in stock and category\n");
                //read the details of the product
                p.Name = Console.ReadLine();
                p.Price = double.Parse(Console.ReadLine());
                p.InStock =  int.Parse(Console.ReadLine());
                p.Category = (DO.Enums.Category)System.Enum.Parse(typeof(DO.Enums.Category), Console.ReadLine());
                s_dalList.Product.Add(p);//add the new product
                break;
            
            case 'b'://check the product
                Console.WriteLine("Enter product id:");
                id = int.Parse(Console.ReadLine());//read the product id
                try
                {
                    p = s_dalList.Product.Get(id);
                    Console.WriteLine(p);//print the product
                }
                catch (DalItemNotFoundException)//if the product id not found
                {
                    break;
                }

                break;
            case 'c'://print all the products
                IEnumerable<Product> products = s_dalList.Product.GetAll();

                foreach (Product i in products)//print
                {
                    Console.WriteLine(i);
                }

                break;
            case 'd'://update product
                Console.WriteLine("Enter product id");
                p.ID = int.Parse(Console.ReadLine());//read product id
                try
                {
                    p = s_dalList.Product.Get(p.ID);
                    Console.WriteLine(p);//print the product
                }
                catch (DalItemNotFoundException)//if the product id not found
                {
                    break;
                }
                Console.WriteLine("Enter product name, price, in stock and category\n");
                //update the product
                p.Name = Console.ReadLine();
                p.Price = double.Parse(Console.ReadLine());
                p.InStock = int.Parse(Console.ReadLine());
                p.Category = (Enums.Category)Convert.ToInt32(Console.ReadLine());
                s_dalList.Product.Update(p);
                break;
            case 'e'://delete product from the array
                Console.WriteLine("Enter product id:");
                id = int.Parse(Console.ReadLine());//read product id
                try
                {
                    s_dalList.Product.Delete(id);//delete the product
                }
                catch (DalItemNotFoundException)//if the product not found
                {
                    break;
                }

                break;
            default:
                Console.WriteLine("ERROR INPUT");
                break;
        }
    }

    public static void PrintItemMenu()
    {
        Console.WriteLine("To add a new order item - a\n" +
                          "Check your order item by id - b\n" +
                          "To see all your order items - c\n" +
                          "To update your order item - d\n" +
                          "To delete order item from your list- e\n"+
                          "check your order item by order id and product id - f"+
                          "print all orders whit the same product id - g");
        char choose = char.Parse(Console.ReadLine());
        int orderId;
        OrderItem o = new OrderItem();
        switch (choose)
        {
            case 'a'://add new order item
                Console.WriteLine("Enter  product id, order id, price and amount\n");
                //read the details of the order
                o.ProductID = int.Parse(Console.ReadLine());
                o.OrderID = int.Parse(Console.ReadLine());
                o.Price = double.Parse(Console.ReadLine());
                o.Amount = int.Parse(Console.ReadLine());
                s_dalList.OrderItem.Add(o);//add the order item to the list
                break;
            case 'b'://get order from the list 
                Console.WriteLine("Enter order item id:");
                orderId = int.Parse(Console.ReadLine());//read thr item id
                try
                {
                    o = s_dalList.OrderItem.Get(orderId);
                    Console.WriteLine(o);//print the item
                }
                catch (DalItemNotFoundException)//if the item id not found
                {
                    break;
                }
                break;
            case 'c'://print all item orders
                IEnumerable <OrderItem> items = s_dalList.OrderItem.GetAll();

                foreach (OrderItem i in items)//print all items
                {
                    Console.WriteLine(i);
                }
                break;
            case 'd'://update the item
                Console.WriteLine("Enter order item id");
                o.Id = int.Parse(Console.ReadLine());//read the item id
                try
                {
                    o = s_dalList.OrderItem.Get(o.Id);
                    Console.WriteLine(o);//print the item to update
                }
                catch (DalItemNotFoundException)
                {
                    break;
                }
             
                Console.WriteLine("Enter  product id, order id, price and amount\n");
                //read the details to update
                o.ProductID = int.Parse(Console.ReadLine());
                o.OrderID = int.Parse(Console.ReadLine());
                o.Price = double.Parse(Console.ReadLine());
                o.Amount = int.Parse(Console.ReadLine());
                s_dalList.OrderItem.Update(o);//update item
                break;
            case 'e'://delete item from the list
                Console.WriteLine("Enter order item id:");
                orderId = int.Parse(Console.ReadLine());//read the id
                try
                {
                    s_dalList.OrderItem.Delete(orderId);//delete the item
                }
                catch (DalItemNotFoundException)//if the item id not found
                {
                    break;
                }
                break;
            case 'f'://delete item from the list
                Console.WriteLine("Enter order id and product id:");
                orderId = int.Parse(Console.ReadLine());//read the id
                int productId = int.Parse(Console.ReadLine());//read product id
                try
                {
                    o = s_dalList.OrderItem.GetByCondition(x => x.OrderID == orderId && x.ProductID == productId);
                    //get the item
                    Console.WriteLine(o);//print the item
                }
                catch (DalItemNotFoundException)//if the item id not found
                {
                    break;
                }
                break;
            case 'g'://delete item from the list
                Console.WriteLine("Enter order id:");
                orderId = int.Parse(Console.ReadLine());//read order id
                try
                {
                    IEnumerable<OrderItem> orders = s_dalList.OrderItem.GetAll(x => x.OrderID == orderId);//get the items
                    foreach (OrderItem i in orders)//print the items
                    {
                        Console.WriteLine(i);
                    } 
                }
                catch (DalItemNotFoundException)//if the order id not found
                {
                    break;
                }
                break;
            default:
                Console.WriteLine("ERROR INPUT");
                break;
        }
    }
}
