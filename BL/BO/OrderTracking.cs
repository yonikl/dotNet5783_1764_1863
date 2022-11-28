
using System.Xml.Xsl;

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
        return $"{nameof(ID)}: {ID}, {nameof(Status)}: {Status}, {nameof(OrderTimeLine)}: {OrderTimeLine}";
    }
    
}

