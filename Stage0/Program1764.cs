namespace Stage0;
partial class Program
{
    private static void Main(string[] args)
    {
        Welcome1764();
        Welcome1863();
        Console.ReadKey();
        
    }

    private static void Welcome1764()
    {
        Console.Write("Enter your name: ");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        string name = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        
        Console.WriteLine("{0}, welcome to my first console application", name);
        Console.WriteLine("enter your number");
        
    }

    static partial void Welcome1863();
}