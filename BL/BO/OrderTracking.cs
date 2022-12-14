namespace BO;

public class OrderTracking 
{
    /// <summary>
    /// ID of the order tracking
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Status of the order tracking
    /// </summary>
    public Enums.OrderStatus? Status { get; set; }
    /// <summary>
    /// List of tuples of pairs of date and string that represent the order events in timeline
    /// </summary>
    public List<Tuple<DateTime?, string?>?>? OrderTimeLine { get; set; }

    public override string ToString()
    {
        Console.WriteLine($" * {nameof(ID)}: {ID}\n * {nameof(Status)}: {Status}");
        OrderTimeLine?.ForEach(item => Console.WriteLine($"* item {item.Item2} : {item.Item1}"));
        return "";
    }
    
}

