namespace Dal;
using DO;
using DalApi;

internal class DalOrder : IOrder
{
    /// <summary>
    /// adding new order to the array
    /// </summary>
    /// <param name="O"></param>
    /// the product that we adding
    /// <returns></returns>
    /// return the id that was given to this order
    public int Add(Order O)
    {
        O.Id = DataSource.Config.GetIdForOrders;
        O.OrderDate = DateTime.Now;
        O.ShipDate = DateTime.Now + TimeSpan.FromDays(10);
        O.DeliveryDate = O.ShipDate + TimeSpan.FromDays(10);
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
    public Order Get(int ID)
    {
        foreach (var t in DataSource.s_orders)
        {
            if(ID == t.GetValueOrDefault().Id)
            {
                return t;
            }
        }

        throw new DalItemNotFoundException();
    }

    /// <summary>
    /// return all the orders
    /// </summary>
    /// <returns></returns>
    /// array of all the orders
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func = null)
    {
        List<Order?> orders = new List<Order?>();
        foreach (var t in DataSource.s_orders)
        {
            orders.Add(t);
        }
        return orders;
    }

    /// <summary>
    /// delete order item by the id parameter
    /// </summary>
    /// <param name="Id"></param>
    /// the id that we looking to delete
    /// <exception cref="Exception"></exception>
    /// if we didn't found this id
    public void Delete(int Id)
    {
        foreach (var t in DataSource.s_orders)
        {
            if (Id == t.Id)
            {
                DataSource.s_orders.Remove(t);
                return;
            }
        }

        throw new DalItemNotFoundException();
    }

    /// <summary>
    /// updating given order in the array
    /// </summary>
    /// <param name="orderItem"></param>
    /// the order that we are updating
    /// <exception cref="Exception"></exception>
    /// if we didn't found what to update
    public void Update(Order orderItem)
    {
        foreach (var t in DataSource.s_orders)
        {
            if (orderItem.Id == t.Id)
            {
                DataSource.s_orders.Remove(t);
                DataSource.s_orders.Add(orderItem);
                return;
            }
        }
        throw new DalItemNotFoundException();
    }
}
