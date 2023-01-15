using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public static class Simulator
{
    private static event EventHandler<SimulationArguments>? updateSimulation;
    private static event EventHandler? stopSimulation;
    private static volatile bool isSimulationStoped = false;
    private static IBl bl = Factory.Get();

    public static void SubscribeToStopSimulation(EventHandler handler)
    {
        stopSimulation += handler;
    }

    public static void SubscribeToupdateSimulation(EventHandler<SimulationArguments> handler)
    {
        updateSimulation += handler;
    }

    public static void StartSimulation()
    {
        new Thread(() =>
        {
            while (!isSimulationStoped && bl.Order.GetNextOrderToHandle() != null)
            {
                var order = bl.Order.GetOrder(bl.Order.GetNextOrderToHandle() ?? throw new NullReferenceException());
                var timeToHandle = new Random().Next(3, 7);
                var aproximateTime = new Random().Next(timeToHandle - 2, timeToHandle + 2);
                updateSimulation?.Invoke(null, new SimulationArguments(order, aproximateTime));
                Thread.Sleep(timeToHandle);
                if (isSimulationStoped) break;
                if (order.Status == BO.Enums.OrderStatus.InProcess) bl.Order.UpdateShipping(order.ID);
                else bl.Order.UpdateDelivery(order.ID);
            }
            stopSimulation?.Invoke(null, EventArgs.Empty);
        }
        )
        { Name = "simulation" };
    }

    public static void StopSimulation()
    {
        isSimulationStoped = true;
    }
}

public struct SimulationArguments
{
    public BO.Enums.OrderStatus nextStatus, currentStatus;
    int aproximateTime, id;
    public SimulationArguments(BO.Order order, int aproximateTime)
    {
        currentStatus = order.Status ?? throw new NullReferenceException();
        nextStatus = order.Status == BO.Enums.OrderStatus.InProcess ? BO.Enums.OrderStatus.shipped : BO.Enums.OrderStatus.Delivered;
        this.aproximateTime = aproximateTime;
        id = order.ID;
    }
}
