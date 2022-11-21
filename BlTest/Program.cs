using BlApi;
using BlImplementation;
using BO;


namespace BlTest
{
    internal class Program
    {
        static IBl bl = new Bl();

        private static Cart C = new Cart();
        static void Main(string[] args)
        {

          
            Console.WriteLine("For exist - 0\n" +
                              "For check order - 1\n" +
                              "For check product - 2\n" +
                              "For add product to the cart - 3\n");
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
                        PrintCartMenu();
                        break;

                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
                PrintMenu();
                choice = int.Parse(Console.ReadLine());

            }



        }

        public static void PrintMenu()//print the menu for the user
        {
            Console.WriteLine("For exist - 0\n" +
                              "For check order - 1\n" +
                              "For check product - 2\n" +
                              "For add product to the cart - 3\n");

        }
        public static void PrintOrderMenu()//print order menu and the option for the user
        {
            Console.WriteLine("Get order - a\n" +
                              "Get all orders - b\n" +
                              "To update delivery - c\n" +
                              "To track order - d\n" +
                              "To update shipping- e\n");
            char choose = char.Parse(Console.ReadLine());//read the choose
            int id;
            switch (choose)
            {
                case 'a'://to add new order
                    Console.WriteLine("Enter order id");
                    id = int.Parse(Console.ReadLine());//read choice.
                    var order = bl.Order.GetOrder(id);
                    Console.WriteLine(order);
                    break;
                case 'b'://check the order
                    IEnumerable<OrderForList> ordersForLists = bl.Order.GetAllOrders();
                    foreach (var i in ordersForLists)
                        Console.WriteLine(i);
                    break;
                case 'c':
                    Console.WriteLine("Enter order id");
                    id = int.Parse(Console.ReadLine());//read choice.
                    var updateOrder = bl.Order.UpdateDelivery(id);
                    Console.WriteLine(updateOrder);
                    break;
                case 'd'://update the order
                    Console.WriteLine("Enter order id");
                    id = int.Parse(Console.ReadLine());//read choice.
                    var trackOrder = bl.Order.TrackOrder(id);
                    Console.WriteLine(trackOrder);
                    break;
                case 'e'://delete order from the array
                    Console.WriteLine("Enter order id:");
                    id = int.Parse(Console.ReadLine());//read the order id
                    var updateShipping = bl.Order.UpdateShipping(id);
                    Console.WriteLine(updateShipping);
                    break;
                default:
                    Console.WriteLine("ERROR INPUT");
                    break;
            }
        }

        public static void PrintProductMenu()
        {
            Console.WriteLine("Get all products - a\n" +
                            "Get product for admin - b\n" +
                            "Get product for user - c\n" +
                            "Add Product - d\n" +
                            "Update Product- e\n" +
                            "Remove product - f" );
            char choose = char.Parse(Console.ReadLine());//read the choose
            int id;
            switch (choose)
            {
                case 'a'://to add new order
                    var products = bl.Product.GetAlProducts();
                    foreach (var Product in products)
                        Console.WriteLine(Product);
                    break;
                case 'b'://check the order
                    Console.WriteLine("Enter order id");
                    id = int.Parse(Console.ReadLine());//read choice.
                    var p = bl.Product.GetProductForAdmin(id);
                    Console.WriteLine(p);
                    break;
                case 'c':
                    Console.WriteLine("Enter order id");
                    id = int.Parse(Console.ReadLine());//read choice.
                    var product = bl.Product.GetProductForUser(id,C);
                    Console.WriteLine(product);
                    break;
                case 'd'://update the order
                    BO.Product itemProduct = new Product();
                    
                    ////need to fix it
                    break;
                case 'e'://delete order from the array
                    Console.WriteLine("Enter order id:");
                    id = int.Parse(Console.ReadLine());//read the order id
                    //bl.Product.UpdateProduct(C);
                    break;
                case 'f':
                    Console.WriteLine("Enter order id:");
                    id = int.Parse(Console.ReadLine());//read the order id
                    bl.Product.RemoveProduct(id);
                    break;
                default:
                    Console.WriteLine("ERROR INPUT");
                    break;
            }
        }

        public static void PrintCartMenu()
        {
            Console.WriteLine("To make a new order - a\n" +
                              "Add to the cart - b\n" +
                              "To update the cart - c\n");
            char choose = char.Parse(Console.ReadLine());
            int orderId;
            switch (choose)
            {
                case 'a'://add new order item
                    Console.WriteLine("Enter your name,address and email\n");
                    string name = Console.ReadLine();
                    string address = Console.ReadLine();
                    string email = Console.ReadLine();
                    bl.Cart.MakeAnOrder(C,name,address,email);
                    break;
                case 'b'://get order from the list 
                    Console.WriteLine("Enter order id");
                    orderId = int.Parse(Console.ReadLine());//read choice.
                    C = bl.Cart.Add(orderId, C);
                    break;
                case 'c'://print all item orders
                    Console.WriteLine("Enter order id and amount\n");
                    orderId = int.Parse(Console.ReadLine());//read choice.
                    var amount = int.Parse(Console.ReadLine());
                    C = bl.Cart.Update(orderId, amount, C);
                    break;
                default:
                    Console.WriteLine("ERROR INPUT");
                    break;
            }
        }
    }
}
