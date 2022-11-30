using BlApi;
using BlImplementation;
using BO;

namespace BlTest
{
    internal class Program
    {
        static IBl bl = new Bl();
        static Cart c = new Cart();
        static void Main(string[] args)
        {
            int choice;
            PrintMenu();//print the menu 
            choice = int.Parse(Console.ReadLine());//read choice.

            while (choice != 0)// if choice not equal to zero
            {
                try
                {

                    switch (choice)
                    {
                        case 1: //for order
                            PrintOrderMenu();
                            break;

                        case 2: //for product
                            PrintProductMenu();
                            break;

                        case 3: //for cart
                            PrintCartMenu();
                            break;

                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
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
                              "For check cart - 3\n");

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
                case 'a'://to get order by id
                    Console.WriteLine("Enter order id");
                    id = int.Parse(Console.ReadLine());//read choice.
                    var order = bl.Order.GetOrder(id);
                    Console.WriteLine(order);//print the order
                    break;
                case 'b'://to get all orders
                    IEnumerable<OrderForList> ordersForLists = bl.Order.GetAllOrders();//get all orders
                    foreach (var i in ordersForLists)//print all orders
                        Console.WriteLine(i);
                    break;
                case 'c'://update delivery
                    Console.WriteLine("Enter order id");
                    id = int.Parse(Console.ReadLine());//read choice.
                    var updateOrder = bl.Order.UpdateDelivery(id);//update
                    Console.WriteLine(updateOrder);//print the update delivery
                    break;
                case 'd'://track order
                    Console.WriteLine("Enter order id");
                    id = int.Parse(Console.ReadLine());//read choice.
                    var trackOrder = bl.Order.TrackOrder(id);
                    Console.WriteLine(trackOrder);//print the order
                    break;
                case 'e'://update shipping
                    Console.WriteLine("Enter order id:");
                    id = int.Parse(Console.ReadLine());//read the order id
                    var updateShipping = bl.Order.UpdateShipping(id);
                    Console.WriteLine(updateShipping);//print the order
                    break;
                default:
                    Console.WriteLine("ERROR INPUT");
                    break;
            }
        }

        public static void PrintProductMenu()//print product menu for user and admin
        {
            Console.WriteLine("Get all products - a\n" +
                            "Get product for admin - b\n" +
                            "Get product for user - c\n" +
                            "Add Product - d\n" +
                            "Update product- e\n" +
                            "Remove product - f" );
            char choose = char.Parse(Console.ReadLine());//read the choose
            int id;
            string category;
            switch (choose)
            {
                case 'a'://get all orders
                    var products = bl.Product.GetAllProducts();
                    foreach (var Product in products)//print all orders
                        Console.WriteLine(Product);
                    break;
                case 'b'://get product for admin
                    Console.WriteLine("Enter product id");
                    id = int.Parse(Console.ReadLine());//read choice.
                    var p = bl.Product.GetProductForAdmin(id);//get the product
                    Console.WriteLine(p);//print the product
                    break;
                case 'c'://get product for user
                    Console.WriteLine("Enter product id");
                    id = int.Parse(Console.ReadLine());//read choice.
                    var product = bl.Product.GetProductForUser(id,c);
                    Console.WriteLine(product);//print the product
                    break;
                case 'd'://add profuct
                    Product itemProduct = new Product();
                    Console.WriteLine("Enter product id,name,price,in stock and category");
                    //read id name price in stock and category
                    itemProduct.ID = int.Parse(Console.ReadLine());
                    itemProduct.Name = Console.ReadLine();
                    itemProduct.Price = int.Parse(Console.ReadLine());
                    itemProduct.InStock = int.Parse(Console.ReadLine()); 
                    category = Console.ReadLine();
                    itemProduct.Category = (Enums.Category)System.Enum.Parse(typeof(Enums.Category), category);//casting to enum
                    bl.Product.AddProduct(itemProduct);//add the product
                    break;
                case 'e'://update product
                    Product updateProduct = new Product();
                    Console.WriteLine("Enter product id,name,price,in stock and category");
                    updateProduct.ID = int.Parse(Console.ReadLine());
                    updateProduct.Name = Console.ReadLine();
                    updateProduct.Price = int.Parse(Console.ReadLine());
                    updateProduct.InStock = int.Parse(Console.ReadLine()); 
                    category = Console.ReadLine();
                    updateProduct.Category = (Enums.Category)Enum.Parse(typeof(Enums.Category), category);
                    bl.Product.UpdateProduct(updateProduct);//update
                    break;
                case 'f'://remove product
                    Console.WriteLine("Enter product id:");
                    id = int.Parse(Console.ReadLine());//read the order id
                    bl.Product.RemoveProduct(id);
                    break;
                default:
                    Console.WriteLine("ERROR INPUT");
                    break;
            }
        }

        public static void PrintCartMenu()//print cart menu for user
        {
            Console.WriteLine("To make a new order - a\n" +
                              "Add to the cart - b\n" +
                              "To update the cart - c\n"); 
            char choose = char.Parse(Console.ReadLine());
            int orderId;
            switch (choose)
            {
                case 'a'://make a new order
                    Console.WriteLine("Enter your name,address and email\n");
                    //read name address and email
                    string name = Console.ReadLine();
                    string address = Console.ReadLine();
                    string email = Console.ReadLine();
                    bl.Cart.MakeAnOrder(c,name,address,email);//make the order
                    break;
                case 'b'://add to the cart
                    Console.WriteLine("Enter product id");
                    orderId = int.Parse(Console.ReadLine());//read choice.
                    c = bl.Cart.Add(orderId, c);//get the new cart
                    Console.WriteLine(c);//print the cart
                    break;
                case 'c'://update cart
                    Console.WriteLine("Enter product id and amount\n");
                    orderId = int.Parse(Console.ReadLine());//read choice.
                    var amount = int.Parse(Console.ReadLine());//read amount for update
                    c = bl.Cart.UpdateAmountOfOrder(orderId, amount, c);//get the update cart
                    Console.WriteLine(c);//print the cart
                    break;
                default:
                    Console.WriteLine("ERROR INPUT");
                    break;
            }
        }
    }
}
