namespace Dal;
using DO;

public class DalOrder
{
    public int AddOrder(Order O)
    {
        O.Id = DataSource.Config.GetIdForOrders;
        DataSource.s_orders[DataSource.Config.OrdersSize++] = O;
        return O.Id;
    }

    public Order GetOrder(int Id)
    {
        for (int i = 0; i < DataSource.Config.OrdersSize; i++)
        {
            if(Id == DataSource.s_orders[i].Id)
            {
                return DataSource.s_orders[i];
            }
        }

        throw new Exception("Not Found");
    }

    public Order[] GetAllOrders()
    {
        Order[] orders = new Order[DataSource.Config.OrdersSize];
        for (int i = 0; i < DataSource.Config.OrdersSize; i++)
        {
            orders[i] = DataSource.s_orders[i];
        }

        return orders;
    }
    public void DeleteOrder(int Id)
    {
        for (int i = 0; i < DataSource.Config.OrdersSize; i++)
        {
            if (Id == DataSource.s_orders[i].Id)
            {
                DataSource.s_orders[i].Id = 0;
            }
        }

        throw new Exception("Not Found");
    }

    public void UpdateOrder(Order O)
    {
        for (int i = 0; i < DataSource.Config.OrdersSize; i++)
        {
            if (O.Id == DataSource.s_orders[i].Id)
            {
                DataSource.s_orders[i] = O;
            }
        }
        throw new Exception("Not Found");
    }
}
