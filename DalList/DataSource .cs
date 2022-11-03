using DO;
namespace Dal;

internal static class DataSource
{
    static readonly int Random;
    internal static DalOrder[] Orders = new DalOrder[200];
    internal static DalOrderItem[] OrdersItems = new DalOrderItem[200];
    internal static DalProduct[] Products = new DalProduct[50];

    private static void AddNewOrder(DalOrder Order)
    {
        Orders.Append(Order);
    }
    private static void AddNewOrderItem(DalOrderItem  orderItem)
    {
        OrdersItems.Append(orderItem);
    }
    private static void AddNewProduct(DalProduct Product)
    {
        Products.Append(Product);
    }

}