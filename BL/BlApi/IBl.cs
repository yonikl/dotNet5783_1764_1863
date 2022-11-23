
namespace BlApi;

/// <summary>
/// Interface to manage the Bl
/// </summary>
public interface IBl
{
    public IOrder Order { get; }

    public IProduct Product { get; }

    public ICart Cart { get; }
}

