
using DalFacade;

using System;
using Dal;
using DO;

public class Program
{
    public static int Main(string[] args)
    {
        int choice;
        PrintMenu();
        choice = Convert.ToInt32(Console.ReadLine());

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
                          "To delete order from your list-e\n");
        char choose = Convert.ToChar(Console.Read());
        Order o;
        switch (choose)
        {
            case 'a':

                break;
        }
    }
    public static void PrintProductMenu()
    {
        Console.WriteLine("For exist - 0\n" +
                          "For check order - 1\n" +
                          "For check product - 2\n" +
                          "For check item - 3\n");
    }
    public static void PrintItemMenu()
    {
        Console.WriteLine("For exist - 0\n" +
                          "For check order - 1\n" +
                          "For check product - 2\n" +
                          "For check item - 3\n");
    }
}
