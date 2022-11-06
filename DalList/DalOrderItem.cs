namespace Dal;
using DO;
using System;

public class DalOrderItem
{
    public int AddOrderItem(OrderItem orderItem)
    {
        orderItem.Id = DataSource.Config.GetIdForOrdersItems;
        DataSource.s_ordersItems[DataSource.Config.ordersItemsSize++] = orderItem;
        return orderItem.Id;
    }

    public OrderItem GetOrderItem(int Id)
    {
        for (int i = 0; i < DataSource.Config.ordersItemsSize; i++)
        {
            if (Id == DataSource.s_ordersItems[i].Id)
                return DataSource.s_ordersItems[i];
        }

        throw new Exception("Not Found");
    }

    public OrderItem[] GetAllOrdersItems()
    {
        OrderItem[] NewOrderItems = new OrderItem[DataSource.Config.ordersItemsSize];
        for (int i = 0; i < DataSource.Config.ordersItemsSize; i++)
        {
            NewOrderItems[i] = DataSource.s_ordersItems[i];
        }

        return NewOrderItems;
    }

    public void DeleteOrderItem(int Id)
    {
        for (int i = 0; i < DataSource.Config.ordersItemsSize; i++)
        {
            if (Id == DataSource.s_ordersItems[i].Id)
            {
                DataSource.s_ordersItems[i].Id = 0;
                return;
            }
        }

        throw new Exception("Not Found");
    }

    public void UpdateOrderItem(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.Config.ordersItemsSize; i++)
        {
            if (orderItem.OrderID == DataSource.s_ordersItems[i].Id)
            {
                DataSource.s_ordersItems[i] = orderItem;
                return;
            }
        }

        throw new Exception("Not Found");
    }

    public OrderItem GetOrderItemByProductIdAndOrderId(int productId, int orderId)
    {
        for (int i = 0; i < DataSource.Config.ordersItemsSize; i++)
        {
            if (orderId == DataSource.s_ordersItems[i].OrderID && productId == DataSource.s_ordersItems[i].ProductID)
                return DataSource.s_ordersItems[i];
        }
        throw new Exception("Not Found");
    }

    public OrderItem[] GetOrderItemsInSpecificOrder(int orderId)
    {
        int Size = 0;
       
        for (int i = 0; i < DataSource.Config.ordersItemsSize; i++)
        {
            if (DataSource.s_ordersItems[i].OrderID == orderId)
                Size++;
        }

        OrderItem[] NewOrderItems = new OrderItem[Size];
        int Count = 0;
        for (int i = 0; i < Size; i++)
        {
            if (DataSource.s_ordersItems[i].OrderID == orderId)
                NewOrderItems[Count++] = DataSource.s_ordersItems[i];
        }

        return NewOrderItems;
    }


}

    