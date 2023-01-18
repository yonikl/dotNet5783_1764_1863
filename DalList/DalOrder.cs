namespace Dal;
using DO;
using DalApi;
using System.Runtime.CompilerServices;

internal class DalOrder : IOrder
{
    /// <summary>
    /// adding new order to the array
    /// </summary>
    /// <param name="O"></param>
    /// the product that we adding
    /// <returns></returns>
    /// return the id that was given to this order
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order O)
    {
        O.Id = DataSource.Config.GetIdForOrders;
        O.OrderDate = DateTime.Now;
        DataSource.s_orders.Add(O);
        return O.Id;
    }

    /// <summary>
    ///  getting order from the array
    /// </summary>
    /// <param name="ID"></param>
    /// the id that we looking for in the array
    /// <returns></returns>
    /// return the order item
    /// <exception cref="Exception"></exception>
    /// if we didn't found
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(int ID)
    {
        if((DataSource.s_orders.Where(x => x?.Id == ID)).Any())
            return (DataSource.s_orders.Where(x => x?.Id == ID)).First() ?? throw new NullReferenceException();
        throw new DalItemNotFoundException();
    }

    /// <summary>
    /// return all the orders
    /// </summary>
    /// <returns></returns>
    /// array of all the orders
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order> GetAll(Func<Order,bool>? func)
    {
        List<Order> orders = new List<Order>();
        if (func == null)
        {
            return DataSource.s_orders.Select(x => x ?? throw new NullReferenceException());
        }

        return from x in DataSource.s_orders where func(x ?? throw new NullReferenceException()) select x ?? throw new NullReferenceException();

    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order GetByCondition(Func<Order, bool> func)
    {
        var order = from x in DataSource.s_orders where func(x ?? throw new NullReferenceException()) select x;
        if (order.Any())
            return order.First() ?? throw new NullReferenceException();
        throw new DalItemNotFoundException();
    }

    /// <summary>
    /// delete order item by the id parameter
    /// </summary>
    /// <param name="Id"></param>
    /// the id that we looking to delete
    /// <exception cref="Exception"></exception>
    /// if we didn't found this id
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int Id)
    {
        var order = Get(Id);
        DataSource.s_orders.Remove(order);
    }

    /// <summary>
    /// updating given order in the array
    /// </summary>
    /// <param name="orderItem"></param>
    /// the order that we are updating
    /// <exception cref="Exception"></exception>
    /// if we didn't found what to update
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order orderItem)
    {
        Delete(orderItem.Id);
        DataSource.s_orders.Add(orderItem);
    }

}
