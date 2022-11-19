
namespace BlApi;

public interface IBl
{
    /// <summary>
    /// 
    /// </summary>
    public IOrder Order { get; internal set; }

    public IProduct Product { get; internal set; }

    public ICart Cart { get; internal set; }
}

