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
        foreach (var i in OrderTimeLine)
        {
            Console.WriteLine($" * item {i.Item2}: {i.Item1}");
        }

        return "";
    }
    
}

