

using System;
using Dal;
using DO;

public class Program
{
    private static DalOrder s_dalOrder = new DalOrder();
    private static DalOrderItem s_dalOrderItem = new DalOrderItem();
    private static DalProduct s_dalProduct = new DalProduct();
    public static int Main(string[] args)
    {
        int choice;
        PrintMenu();
        choice = int.Parse(Console.ReadLine());

        while (choice != 0)
        {
            switch (choice)
            {
                case 1:
                    PrintOrderMenu();
                    break;

                case 2:
                    PrintItemMenu();
                    break;
                
                case 3:
                    PrintProductMenu();
                    break;

                default:
                    Console.WriteLine("ERROR");
                    break;
            }
            PrintMenu();
            choice = Convert.ToInt32(Console.ReadLine());

        }


        return 0;

    }

    public static void PrintMenu()
    {
        Console.WriteLine("For exist - 0\n" +
                          "For check order - 1\n" +
                          "For check product - 2\n" +
                          "For check item - 3\n");
       
    }
    public static void PrintOrderMenu()
    {
        Console.WriteLine("To add a new order - a\n" +
                          "Check your order by id - b\n" +
                          "To see all your orders - c\n" +
                          "To update your order - d\n" +
                          "To delete order from your list- e\n");
        char choose = char.Parse(Console.ReadLine());
        int id;
        Order o = new Order();
        switch (choose)
        {
            case 'a':
                Console.WriteLine("Enter your name, email and adress\n");
              
                o.CustomerName = Console.ReadLine();
                o.CustomerEmail = Console.ReadLine();
                o.CustomerAdress = Console.ReadLine();
                s_dalOrder.AddOrder(o);
                break;
            case 'b':
                Console.WriteLine("Enter order id:");
                id = int.Parse(Console.ReadLine());
                try
                { 
                    o = s_dalOrder.GetOrder(id);
                    Console.WriteLine(o);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;
            case 'c':
                Order[] orders = s_dalOrder.GetAllOrders();

                foreach (Order i in orders)
                {
                    Console.WriteLine(i);
                }
                break;
            case 'd':
                Console.WriteLine("Enter order id");
                o.Id = int.Parse(Console.ReadLine());
                try
                {
                    o = s_dalOrder.GetOrder(o.Id);
                    Console.WriteLine(o);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }
                Console.WriteLine("Enter name, email and adrass:");
                o.CustomerName = Console.ReadLine();
                o.CustomerEmail = Console.ReadLine();
                o.CustomerAdress = Console.ReadLine();
                s_dalOrder.UpdateOrder(o);
                break;
            case 'e':
                Console.WriteLine("Enter order id:");
                id = int.Parse(Console.ReadLine());
                try
                {
                     s_dalOrder.DeleteOrder(id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
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
            case 'a':
                Console.WriteLine("Enter your name, price, in stock and category\n");

                p.Name = Console.ReadLine();
                p.Price = double.Parse(Console.ReadLine());
                p.InStock =  int.Parse(Console.ReadLine());
               // p.Category = Console.ReadLine();
              
                break;
            /*
            case 'b':
                Console.WriteLine("Enter order item id:");
                id = int.Parse(Console.ReadLine());
                try
                {
                    p = s_dalOrderItem.GetOrderItem(id);
                    Console.WriteLine(p);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                break;
            case 'c':
                OrderItem[] orders = s_dalOrderItem.GetAllOrdersItems();

                foreach (OrderItem i in orders)
                {
                    Console.WriteLine(i);
                }

                break;
            case 'd':
                Console.WriteLine("Enter order id");
                o.Id = int.Parse(Console.ReadLine());
                try
                {
                    o = s_dalOrderItem.GetOrderItem(o.Id);
                    Console.WriteLine(o);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }

                Console.WriteLine("Enter  product id, order id, price and amount\n");

                o.ProductID = int.Parse(Console.ReadLine());
                o.OrderID = int.Parse(Console.ReadLine());
                o.Price = double.Parse(Console.ReadLine());
                o.Amount = int.Parse(Console.ReadLine());
                s_dalOrderItem.AddOrderItem(o);
                break;
            case 'e':
                Console.WriteLine("Enter order id:");
                id = int.Parse(Console.ReadLine());
                try
                {
                    s_dalOrder.DeleteOrder(id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                break;
            */
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
                          "To delete order item from your list- e\n");
        char choose = char.Parse(Console.ReadLine());
        int id;
        OrderItem o = new OrderItem();
        switch (choose)
        {
            case 'a':
                Console.WriteLine("Enter  product id, order id, price and amount\n");

                o.ProductID = int.Parse(Console.ReadLine());
                o.OrderID = int.Parse(Console.ReadLine());
                o.Price = double.Parse(Console.ReadLine());
                o.Amount = int.Parse(Console.ReadLine());
                s_dalOrderItem.AddOrderItem(o);
                break;
            case 'b':
                Console.WriteLine("Enter order item id:");
                id = int.Parse(Console.ReadLine());
                try
                {
                    o = s_dalOrderItem.GetOrderItem(id);
                    Console.WriteLine(o);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;
            case 'c':
                OrderItem[] orders = s_dalOrderItem.GetAllOrdersItems();

                foreach (OrderItem i in orders)
                {
                    Console.WriteLine(i);
                }
                break;
            case 'd':
                Console.WriteLine("Enter order id");
                o.Id = int.Parse(Console.ReadLine());
                try
                {
                    o = s_dalOrderItem.GetOrderItem(o.Id);
                    Console.WriteLine(o);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }
             
                Console.WriteLine("Enter  product id, order id, price and amount\n");

                o.ProductID = int.Parse(Console.ReadLine());
                o.OrderID = int.Parse(Console.ReadLine());
                o.Price = double.Parse(Console.ReadLine());
                o.Amount = int.Parse(Console.ReadLine());
                s_dalOrderItem.AddOrderItem(o);
                break;
            case 'e':
                Console.WriteLine("Enter order id:");
                id = int.Parse(Console.ReadLine());
                try
                {
                    s_dalOrder.DeleteOrder(id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;
            default:
                Console.WriteLine("ERROR INPUT");
                break;
        }
    }
}
