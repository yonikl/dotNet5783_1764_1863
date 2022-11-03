namespace Dal;
using DO;

public class DalOrder
{
    public int AddOrder(Order O)
    {
        O.Id = Config.GetIdForOrders;
        DataSource.Orders[Config.OrdersSize++] = O;
        return O.Id;
    }

    public Order GetOrder(int Id)
    {
        for (int i = 0; i < Config.OrdersSize; i++)
        {
            if(Id == DataSource.Orders[i].Id)
            {
                return DataSource.Orders[i];
            }
        }

        throw new Exception("Not Found");
    }

    public Order[] GetAllOrders()
    {
        Order[] orders = new Order[Config.OrdersSize];
        for (int i = 0; i < Config.OrdersSize; i++)
        {
            orders[i] = DataSource.Orders[i];
        }

        return orders;
    }
    public void DeleteOrder(int Id)
    {
        for (int i = 0; i < Config.OrdersSize; i++)
        {
            if (Id == DataSource.Orders[i].Id)
            {
                DataSource.Orders[i].Id = 0;
            }
        }

        throw new Exception("Not Found");
    }

    public void UpdateOrder(Order O)
    {
        for (int i = 0; i < Config.OrdersSize; i++)
        {
            if (O.Id == DataSource.Orders[i].Id)
            {
                DataSource.Orders[i] = O;
            }
        }
        throw new Exception("Not Found");
    }
}
