using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Simulator
{
    private static event EventHandler<Tuple<Order, int>>? updateSimulation;
    private static event EventHandler? stopSimulation;
    private static volatile bool isSimulationStoped = false;
    private static IBl bl = Factory.Get();
    private static Thread? thread;


    /// <summary>
    /// functions to subscribe/unsubscribe to/from the events
    /// </summary>
    public static void SubscribeToStopSimulation(EventHandler handler)
    {
        stopSimulation += handler;
    }

    public static void SubscribeToUpdateSimulation(EventHandler<Tuple<Order, int>> handler)
    {
        updateSimulation += handler;
    }

    public static void UnsubscribeFromStopSimulation(EventHandler handler)
    {
        if (stopSimulation!.GetInvocationList().Contains(handler)) stopSimulation -= handler;
    }

    public static void UnsubscribeFromUpdateSimulation(EventHandler<Tuple<Order, int>> handler)
    {
        if (updateSimulation!.GetInvocationList().Contains(handler)) updateSimulation -= handler;
    }

    /// <summary>
    /// start the simulation
    /// </summary>
    public static void StartSimulation()
    {
        isSimulationStoped = false;
        thread = new Thread(() =>
        {
            while (!isSimulationStoped && bl.Order.GetNextOrderToHandle() != null)
            {
                var order = bl.Order.GetOrder(bl.Order.GetNextOrderToHandle() ?? throw new NullReferenceException());
                var timeToHandle = new Random().Next(3, 7);//calculate time to handle
                var aproximateTime = new Random().Next(timeToHandle - 2, timeToHandle + 2);//calculate approximate time to handle
                updateSimulation?.Invoke(null, new Tuple<Order, int>(order, aproximateTime)); // update
                sleep(timeToHandle);
                if (isSimulationStoped) break; // if we stopped during the sleep
                // handle the order
                if (order.Status == BO.Enums.OrderStatus.InProcess) bl.Order.UpdateShipping(order.ID);
                else if (order.Status == Enums.OrderStatus.shipped) bl.Order.UpdateDelivery(order.ID);
                else stopSimulation?.Invoke(null, EventArgs.Empty); // if there is a problem
            }
            stopSimulation?.Invoke(null, EventArgs.Empty);
        }
        )
        { Name = "Simulation" };
        thread.Start();

    }
    /// <summary>
    /// stop simulation
    /// </summary>
    public static void StopSimulation()
    {
        isSimulationStoped = true;
        thread?.Interrupt();
    }

    /// <summary>
    /// sleep the given amout of seconds
    /// </summary>
    /// <param name="seconds"></param>
    private static void sleep(int seconds)
    {
        try { Thread.Sleep(seconds * 1000); } catch (ThreadInterruptedException) { }
    }
}