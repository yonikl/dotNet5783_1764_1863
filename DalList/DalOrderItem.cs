namespace Dal;
using DO;
using System;
using DalApi;

internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// add order item to the array
    /// </summary>
    /// <param name="orderItem"></param>
    /// the order item to add
    /// <returns></returns>
    /// return the id that was given to this order item
    public int Add(OrderItem orderItem)
    {
        orderItem.Id = DataSource.Config.GetIdForOrdersItems;
        DataSource.s_ordersItems.Add(orderItem);
        return orderItem.Id;
    }
    /// <summary>
    /// getting order item from the array
    /// </summary>
    /// <param name="ID"></param>
    /// the id that we looking for in the array
    /// <returns></returns>
    /// return the order item
    /// <exception cref="Exception"></exception>
    /// if we didn't found
    public OrderItem Get(int ID)
    {
        foreach (var t in DataSource.s_ordersItems)
        {
            if (ID == t.GetValueOrDefault().Id)
                return t.Value;
        }

        throw new DalItemNotFoundException();
    }

    /// <summary>
    /// return all the orders items
    /// </summary>
    /// <returns></returns>
    /// array of all the orders items
    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool>? func)
    {
        List<OrderItem> newOrderItems = new List<OrderItem>();
        foreach (var t in DataSource.s_ordersItems)
        {
            newOrderItems.Add(t.Value);
        }

        return newOrderItems;
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
        foreach (var t in DataSource.s_ordersItems)
        {
            if (Id == t.GetValueOrDefault().Id)
            {
                DataSource.s_ordersItems.Remove(t);
                return;
            }
        }

        throw new DalItemNotFoundException();
    }

    /// <summary>
    /// updating given order item in the array
    /// </summary>
    /// <param name="orderItem"></param>
    /// the order item that we are updating
    /// <exception cref="Exception"></exception>
    /// if we didn't found what to update
    public void Update(OrderItem orderItem)
    {
        foreach (var t in DataSource.s_ordersItems)
        {
            if (orderItem.OrderID == t.GetValueOrDefault().Id)
            {
                DataSource.s_ordersItems.Remove(t);
                DataSource.s_ordersItems.Add(orderItem);
                return;
            }
        }

        throw new DalItemNotFoundException();
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
        foreach (var t in DataSource.s_ordersItems)
        {
            if (orderId == t.GetValueOrDefault().OrderID && productId == t.GetValueOrDefault().ProductID)
                return t.Value;
        }

        throw new DalItemNotFoundException();
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
    public IEnumerable<OrderItem> GetOrderItemsInSpecificOrder(int orderId)
    {
        List<OrderItem> newOrderItems = new List<OrderItem>();
        foreach (var t in DataSource.s_ordersItems)
        {
            if (t.GetValueOrDefault().OrderID == orderId)
                newOrderItems.Add(t.Value);
        }
        return newOrderItems;
    }

    
}

    