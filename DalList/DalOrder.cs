namespace Dal;
using DO;

public class DalOrder
{
    /// <summary>
    /// adding new order to the array
    /// </summary>
    /// <param name="O"></param>
    /// the product that we adding
    /// <returns></returns>
    /// return the id that was given to this order
    public int AddOrder(Order O)
    {
        O.Id = DataSource.Config.GetIdForOrders;
        DataSource.s_orders[DataSource.Config.ordersSize++] = O;
        return O.Id;
    }

    /// <summary>
    ///  getting order from the array
    /// </summary>
    /// <param name="Id"></param>
    /// the id that we looking for in the array
    /// <returns></returns>
    /// return the order item
    /// <exception cref="Exception"></exception>
    /// if we didn't found
    public Order GetOrder(int Id)
    {
        for (int i = 0; i < DataSource.Config.ordersSize; i++)
        {
            if(Id == DataSource.s_orders[i].Id)
            {
                return DataSource.s_orders[i];
            }
        }

        throw new Exception("Not Found");
    }

    /// <summary>
    /// return all the orders
    /// </summary>
    /// <returns></returns>
    /// array of all the orders
    public Order[] GetAllOrders()
    {
        Order[] orders = new Order[DataSource.Config.ordersSize];
        for (int i = 0; i < DataSource.Config.ordersSize; i++)
        {
            orders[i] = DataSource.s_orders[i];
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
    public void DeleteOrder(int Id)
    {
        for (int i = 0; i < DataSource.Config.ordersSize; i++)
        {
            if (Id == DataSource.s_orders[i].Id)
            {
                DataSource.s_orders[i].Id = 0;
                return;
            }
        }

        throw new Exception("Not Found");
    }

    /// <summary>
    /// updating given order in the array
    /// </summary>
    /// <param name="O"></param>
    /// the order that we are updating
    /// <exception cref="Exception"></exception>
    /// if we didn't found what to update
    public void UpdateOrder(Order O)
    {
        for (int i = 0; i < DataSource.Config.ordersSize; i++)
        {
            if (O.Id == DataSource.s_orders[i].Id)
            {
                DataSource.s_orders[i] = O;
                return;
            }
        }
        throw new Exception("Not Found");
    }
}
