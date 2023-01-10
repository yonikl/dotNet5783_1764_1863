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
        string s;
        s = ($"* {nameof(ID)}: {ID}\n* {nameof(Status)}: {Status}\n");
      
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        OrderTimeLine?.ForEach(item => s += ($"* {item.Item2} : {item.Item1}\n").ToString());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        return s;
    }
    
}

