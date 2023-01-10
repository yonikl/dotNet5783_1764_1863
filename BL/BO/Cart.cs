
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
    public string? CustomerAddress { get; set; }

    /// <summary>
    /// items that the customer choose
    /// </summary>
#pragma warning disable CS8618 // Non-nullable property 'Items' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    public List<OrderItem?> Items { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Items' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

    /// <summary>
    /// The total price
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        Console.WriteLine(
            $" * {nameof(CustomerName)}: {CustomerName}\n * {nameof(CustomerEmail)}: {CustomerEmail}\n * {nameof(CustomerAddress)}: {CustomerAddress}");
        int j = 1;
        Items.ForEach(x => Console.WriteLine($"{j++})" + x));
        return $" * {nameof(TotalPrice)}: { TotalPrice}";
    }
}

