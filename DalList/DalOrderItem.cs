namespace Dal;
using DO;
using System;

public class DalOrderItem
{
    public int AddOrderItem(OrderItem O)
    {
        O.OrderID = DataSource.Config.GetIdForOrdersItems;
        DataSource.s_ordersItems[DataSource.Config.OrdersItemsSize++] = O;
        return O.OrderID;
    }

    public OrderItem GetOrderItem(int Id)
    {
        for (int i = 0; i < DataSource.Config.OrdersItemsSize; i++)
        {
            if (Id == DataSource.s_ordersItems[i].OrderID)
                return DataSource.s_ordersItems[i];
        }

        throw new Exception("Not Found");
    }

    public OrderItem[] GetAllOrdersItems()
    {
        OrderItem[] NewOrderItems = new OrderItem[DataSource.Config.OrdersItemsSize];
        for (int i = 0; i < DataSource.Config.OrdersItemsSize; i++)
        {
            NewOrderItems[i] = DataSource.s_ordersItems[i];
        }

        return NewOrderItems;
    }

    public void DeleteOrderItem(int Id)
    {
        for (int i = 0; i < DataSource.Config.OrdersItemsSize; i++)
        {
            if (Id == DataSource.s_ordersItems[i].OrderID)
                DataSource.s_ordersItems[i].OrderID = 0;
        }

        throw new Exception("Not Found");
    }

    public void UpdateOrderItem(OrderItem O)
    {
        for (int i = 0; i < DataSource.Config.OrdersItemsSize; i++)
        {
            if (O.OrderID == DataSource.s_ordersItems[i].OrderID)
                DataSource.s_ordersItems[i] = O;
        }

        throw new Exception("Not Found");
    }

    public OrderItem GetOrderItemByProductIdAndOrderId(int ProductId, int OrderId)
    {
        for (int i = 0; i < DataSource.Config.OrdersItemsSize; i++)
        {
            if (OrderId == DataSource.s_ordersItems[i].OrderID && ProductId == DataSource.s_ordersItems[i].ProductID)
                return DataSource.s_ordersItems[i];
        }
        throw new Exception("Not Found");
    }

    public OrderItem[] GetOrderItemsInSpecificOrder(int OrderId)
    {
        int Size = 0;
       
        for (int i = 0; i < DataSource.Config.OrdersItemsSize; i++)
        {
            if (DataSource.s_ordersItems[i].OrderID == OrderId)
                Size++;
        }

        OrderItem[] NewOrderItems = new OrderItem[Size];
        int Count = 0;
        for (int i = 0; i < Size; i++)
        {
            if (DataSource.s_ordersItems[i].OrderID == OrderId)
                NewOrderItems[Count++] = DataSource.s_ordersItems[i];
        }

        return NewOrderItems;
    }


}

    