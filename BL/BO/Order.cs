
using System.Threading.Channels;

namespace BO;

public class Order
{
    /// <summary>
    /// ID for order
    /// </summary>
    public int ID { get; set; }

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
    /// Thw time the order made
    /// </summary>
    public DateTime? OrderDate { get; set; }

    /// <summary>
    /// The status of the ship
    /// </summary>
    public Enums.OrderStatus? Status { get; set; }

    /// <summary>
    /// Ship date
    /// </summary>
    public DateTime? ShipDate { get; set; }

    /// <summary>
    /// Delivery date
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

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
            $" * {nameof(ID)}: {ID}\n * {nameof(CustomerName)}: {CustomerName}\n * {nameof(CustomerEmail)}: {CustomerEmail}\n * {nameof(CustomerAddress)}: {CustomerAddress}\n * {nameof(OrderDate)}: {OrderDate}\n * {nameof(Status)}: {Status}\n * {nameof(ShipDate)}: {ShipDate}\n * {nameof(DeliveryDate)}: {DeliveryDate}\n * Product's:");
        int j = 1;
        Items.ForEach(x => Console.WriteLine($"   {j++}) {nameof(x.Name)} : {x.Name}"));
        return ($" * {nameof(TotalPrice)} : {TotalPrice}");
    }
}

