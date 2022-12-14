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
        var order_item = DataSource.s_ordersItems.Where(x => x?.Id == ID);
        if (order_item.Any())
            return order_item.First() ?? throw new NullReferenceException();

        throw new DalItemNotFoundException();
    }

    /// <summary>
    /// return all the orders items
    /// </summary>
    /// <returns></returns>
    /// array of all the orders items
    public IEnumerable<OrderItem> GetAll(Func<OrderItem,bool>? func)
    {
        if (func == null)
        {
            //coping the list
            return from ord in DataSource.s_ordersItems select ord ?? throw new NullReferenceException();
        }
        //coping the list by the given func
        return (IEnumerable<OrderItem>)DataSource.s_ordersItems.Where(ord => func(ord ?? throw new NullReferenceException()));
    }

    public OrderItem GetByCondition(Func<OrderItem, bool> func)
    {
        var orderItem = DataSource.s_ordersItems.Where(x => func(x ?? throw new NullReferenceException()));
        if (orderItem.Any())
            return orderItem.First() ?? throw new NullReferenceException();
        throw new DalItemNotFoundException();
     
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
        var order_item = from ord in DataSource.s_ordersItems where ord?.Id == Id select ord;
        if (order_item.Any())
        {
            DataSource.s_ordersItems.Remove(order_item.First());
            return;
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
        Delete(orderItem.Id);
        DataSource.s_ordersItems.Add(orderItem);
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
        return from ordItem in DataSource.s_ordersItems where ordItem?.OrderID == orderId select ordItem ?? throw new NullReferenceException();
    }

    
}

    