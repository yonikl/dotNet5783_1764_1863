
namespace BO;

public class Cart
{
    /// <summary>
    /// Customer name
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// Customer email
    /// </summary>
    public string? CustomerEmail { get; set; }

    /// <summary>
    /// Customer address
    /// </summary>
    public string CustomerAddress { get; set; }

    /// <summary>
    /// items that the customer choose
    /// </summary>
    public List<OrderItem?> Items { get; set; }

    /// <summary>
    /// The total price
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        Console.WriteLine(
            $" * {nameof(CustomerName)}: {CustomerName}\n * {nameof(CustomerEmail)}: {CustomerEmail}\n * {nameof(CustomerAddress)}: {CustomerAddress}");
        int j = 1;
        foreach (var i in Items)
        {
            Console.WriteLine($"{j})" + i);
        }
        
        return $" * {nameof(TotalPrice)}: { TotalPrice}";
        }
}

