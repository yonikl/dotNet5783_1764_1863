namespace Dal;
using DO;
using System;

public class DalOrderItem
{
    public int AddOrderItem(OrderItem O)
    {
        O.OrderID = Config.GetIdForOrdersItems;
        DataSource.OrdersItems[Config.OrdersItemsSize++] = O;
        return O.OrderID;
    }

    public OrderItem GetOrderItem(int Id)
    {
        for (int i = 0; i < Config.OrdersItemsSize; i++)
        {
            if (Id == DataSource.OrdersItems[i].OrderID)
                return DataSource.OrdersItems[i];
        }

        throw new Exception("Not Found");
    }

    public OrderItem[] GetAllOrdersItems()
    {
        OrderItem[] NewOrderItems = new OrderItem[Config.OrdersItemsSize];
        for (int i = 0; i < Config.OrdersItemsSize; i++)
        {
            NewOrderItems[i] = DataSource.OrdersItems[i];
        }

        return NewOrderItems;
    }

    public void DeleteOrderItem(int Id)
    {
        for (int i = 0; i < Config.OrdersItemsSize; i++)
        {
            if (Id == DataSource.OrdersItems[i].OrderID)
                DataSource.OrdersItems[i].OrderID = 0;
        }

        throw new Exception("Not Found");
    }

    public void UpdateOrderItem(OrderItem O)
    {
        for (int i = 0; i < Config.OrdersItemsSize; i++)
        {
            if (O.OrderID == DataSource.OrdersItems[i].OrderID)
                DataSource.OrdersItems[i] = O;
        }

        throw new Exception("Not Found");
    }

    public OrderItem GetOrderItemByProductIdAndOrderId(int ProductId, int OrderId)
    {
        for (int i = 0; i < Config.OrdersItemsSize; i++)
        {
            if (OrderId == DataSource.OrdersItems[i].OrderID && ProductId == DataSource.OrdersItems[i].ProductID)
                return DataSource.OrdersItems[i];
        }
        throw new Exception("Not Found");
    }

    public OrderItem[] GetOrderItemsInSpecificOrder(int OrderId)
    {
        int Size = 0;
       
        for (int i = 0; i < Config.OrdersItemsSize; i++)
        {
            if (DataSource.OrdersItems[i].OrderID == OrderId)
                Size++;
        }

        OrderItem[] NewOrderItems = new OrderItem[Size];
        int Count = 0;
        for (int i = 0; i < Size; i++)
        {
            if (DataSource.OrdersItems[i].OrderID == OrderId)
                NewOrderItems[Count++] = DataSource.OrdersItems[i];
        }

        return NewOrderItems;
    }


}

    