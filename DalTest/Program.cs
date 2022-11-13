using System;
using Dal;
using DalApi;
using DO;

public class Program
{
    /// <summary>
    /// define del order, del order item, del product
    /// </summary>
    private static IDal dalList = new DalList();
    
    public static int Main(string[] args)
    {

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
                dalList.Order.Add(o);//add the order 
                break;
            case 'b'://check the order
                Console.WriteLine("Enter order id:");
                id = int.Parse(Console.ReadLine());//read the order id
                try
                { 
                    o = dalList.Order.Get(id);//get the order using the id
                    Console.WriteLine(o);//print the order
                }
                catch (Exception e)//in case the order id mot found
                {
                    Console.WriteLine(e);
                }
                break;
            case 'c':
                IEnumerable<Order> orders = dalList.Order.GetAll();//get all the orders

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
                    o = dalList.Order.Get(o.Id);//get the order by using the id
                    Console.WriteLine(o);//print the order
                }
                catch (Exception e)//if the order id not found
                {
                    Console.WriteLine(e);
                    break;
                }
                //update the order
                Console.WriteLine("Enter name, email and adrass:");
                //read the details for the new order
                o.CustomerName = Console.ReadLine();
                o.CustomerEmail = Console.ReadLine();
                o.CustomerAddress = Console.ReadLine();
                dalList.Order.Update(o);//update the order
                break;
            case 'e'://delete order from the array
                Console.WriteLine("Enter order id:");
                id = int.Parse(Console.ReadLine());//read the order id
                try
                {
                     dalList.Order.Delete(id);//delete the order
                }
                catch (Exception e)//if the order id not found
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
            case 'a'://add new product
                Console.WriteLine("Enter your name, price, in stock and category\n");
                //read the details of the product
                p.Name = Console.ReadLine();
                p.Price = double.Parse(Console.ReadLine());
                p.InStock =  int.Parse(Console.ReadLine());
                p.Category = (Enums.Category)Convert.ToInt32(Console.ReadLine());
                dalList.Product.Add(p);//add the new product
                break;
            
            case 'b'://check the product
                Console.WriteLine("Enter product id:");
                id = int.Parse(Console.ReadLine());//read the product id
                try
                {
                    p = dalList.Product.Get(id);
                    Console.WriteLine(p);//print the product
                }
                catch (Exception e)//if the product id not found
                {
                    Console.WriteLine(e);
                }

                break;
            case 'c'://print all the products
                IEnumerable<Product> products = dalList.Product.GetAll();

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
                    p = dalList.Product.Get(p.ID);
                    Console.WriteLine(p);//print the product
                }
                catch (Exception e)//if the product id not found
                {
                    Console.WriteLine(e);
                    break;
                }
                Console.WriteLine("Enter your name, price, in stock and category\n");
                //update the product
                p.Name = Console.ReadLine();
                p.Price = double.Parse(Console.ReadLine());
                p.InStock = int.Parse(Console.ReadLine());
                p.Category = (Enums.Category)Convert.ToInt32(Console.ReadLine());
                dalList.Product.Update(p);
                break;
            case 'e'://delete product from the array
                Console.WriteLine("Enter product id:");
                id = int.Parse(Console.ReadLine());//read product id
                try
                {
                    dalList.Product.Delete(id);//delete the product
                }
                catch (Exception e)//if the product not found
                {
                    Console.WriteLine(e);
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
                dalList.OrderItem.Add(o);//add the order item to the list
                break;
            case 'b'://get order from the list 
                Console.WriteLine("Enter order item id:");
                orderId = int.Parse(Console.ReadLine());//read thr item id
                try
                {
                    o = dalList.OrderItem.Get(orderId);
                    Console.WriteLine(o);//print the item
                }
                catch (Exception e)//if the item id not found
                {
                    Console.WriteLine(e);
                }
                break;
            case 'c'://print all item orders
                IEnumerable <OrderItem> items = dalList.OrderItem.GetAll();

                foreach (OrderItem i in items)//print all items
                {
                    Console.WriteLine(i);
                }
                break;
            case 'd'://update the item
                Console.WriteLine("Enter order id");
                o.Id = int.Parse(Console.ReadLine());//read the item id
                try
                {
                    o = dalList.OrderItem.Get(o.Id);
                    Console.WriteLine(o);//print the item to update
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }
             
                Console.WriteLine("Enter  product id, order id, price and amount\n");
                //read the details to update
                o.ProductID = int.Parse(Console.ReadLine());
                o.OrderID = int.Parse(Console.ReadLine());
                o.Price = double.Parse(Console.ReadLine());
                o.Amount = int.Parse(Console.ReadLine());
                dalList.OrderItem.Update(o);//update item
                break;
            case 'e'://delete item from the list
                Console.WriteLine("Enter order id:");
                orderId = int.Parse(Console.ReadLine());//read the id
                try
                {
                    dalList.OrderItem.Delete(orderId);//delete the item
                }
                catch (Exception e)//if the item id not found
                {
                    Console.WriteLine(e);
                }
                break;
            case 'f'://delete item from the list
                Console.WriteLine("Enter order id and product id:");
                orderId = int.Parse(Console.ReadLine());//read the id
                int productId = int.Parse(Console.ReadLine());//read product id
                try
                {   
                    o = dalList.OrderItem.GetOrderItemByProductIdAndOrderId(orderId,productId);//get the item
                    Console.WriteLine(o);//print the item
                }
                catch (Exception e)//if the item id not found
                {
                    Console.WriteLine(e);
                }
                break;
            case 'g'://delete item from the list
                Console.WriteLine("Enter product id:");
                productId = int.Parse(Console.ReadLine());//read product id
                try
                {
                    IEnumerable<OrderItem> orders = dalList.OrderItem.GetOrderItemsInSpecificOrder(productId);//get the items
                    foreach (OrderItem i in orders)//print the items
                    {
                        Console.WriteLine(i);
                    } 
                }
                catch (Exception e)//if the item id not found
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
