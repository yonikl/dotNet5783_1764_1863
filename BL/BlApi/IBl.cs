
namespace BlApi;

public interface IBl
{
    /// <summary>
    /// 
    /// </summary>
    public IOrder Order { get; }

    public IProduct Product { get; }

    public ICart Cart { get; }
}

