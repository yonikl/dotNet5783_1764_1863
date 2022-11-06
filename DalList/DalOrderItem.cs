namespace Dal;
using DO;
using System;

public class DalOrderItem
{
    /// <summary>
    /// add order item to the array
    /// </summary>
    /// <param name="orderItem"></param>
    /// the order item to add
    /// <returns></returns>
    /// return the id that was given to this order item
    public int AddOrderItem(OrderItem orderItem)
    {
        orderItem.Id = DataSource.Config.GetIdForOrdersItems;
        DataSource.s_ordersItems[DataSource.Config.ordersItemsSize++] = orderItem;
        return orderItem.Id;
    }
    /// <summary>
    /// getting order item from the array
    /// </summary>
    /// <param name="Id"></param>
    /// the id that we looking for in the array
    /// <returns></returns>
    /// return the order item
    /// <exception cref="Exception"></exception>
    /// if we didn't found
    public OrderItem GetOrderItem(int Id)
    {
        for (int i = 0; i < DataSource.Config.ordersItemsSize; i++)
        {
            if (Id == DataSource.s_ordersItems[i].Id)
                return DataSource.s_ordersItems[i];
        }

        throw new Exception("Not Found");
    }

    /// <summary>
    /// return all the orders items
    /// </summary>
    /// <returns></returns>
    /// array of all the orders items
    public OrderItem[] GetAllOrdersItems()
    {
        OrderItem[] NewOrderItems = new OrderItem[DataSource.Config.ordersItemsSize];
        for (int i = 0; i < DataSource.Config.ordersItemsSize; i++)
        {
            NewOrderItems[i] = DataSource.s_ordersItems[i];
        }

        return NewOrderItems;
    }

    /// <summary>
    /// delete order item by the id parameter
    /// </summary>
    /// <param name="Id"></param>
    /// the id that we looking to delete
    /// <exception cref="Exception"></exception>
    /// if we didn't found this id
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

    /// <summary>
    /// updating given order item in the array
    /// </summary>
    /// <param name="orderItem"></param>
    /// the order item that we are updating
    /// <exception cref="Exception"></exception>
    /// if we didn't found what to update
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

    /// <summary>
    /// looking for specific product in specific order
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="orderId"></param>
    /// the parameters we looking by
    /// <returns></returns>
    /// return what we found
    /// <exception cref="Exception"></exception>
    /// if we didn't found
    public OrderItem GetOrderItemByProductIdAndOrderId(int productId, int orderId)
    {
        for (int i = 0; i < DataSource.Config.ordersItemsSize; i++)
        {
            if (orderId == DataSource.s_ordersItems[i].OrderID && productId == DataSource.s_ordersItems[i].ProductID)
                return DataSource.s_ordersItems[i];
        }
        throw new Exception("Not Found");
    }

    /// <summary>
    /// we looking for all the orders items in specific order
    /// </summary>
    /// <param name="orderId"></param>
    /// the order id
    /// <returns></returns>
    /// return array of the orders items in given order
    /// <exception cref="Exception"></exception>
    /// if we didn't found at all
    public OrderItem[] GetOrderItemsInSpecificOrder(int orderId)
    {
        int size = 0;
       
        for (int i = 0; i < DataSource.Config.ordersItemsSize; i++)
        {
            if (DataSource.s_ordersItems[i].OrderID == orderId)
                size++;
        }

        OrderItem[] NewOrderItems = new OrderItem[size];
        int Count = 0;
        for (int i = 0; i < size; i++)
        {
            if (DataSource.s_ordersItems[i].OrderID == orderId)
                NewOrderItems[Count++] = DataSource.s_ordersItems[i];
        }

        if (size == 0) throw new Exception("Not Found");
        return NewOrderItems;
    }


}

    