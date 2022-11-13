﻿namespace DalApi;

public interface IDal
{
    public IProduct Product { get; }
    public IOrderItem OrderItem { get; }
    public IOrder Order { get; }
}

