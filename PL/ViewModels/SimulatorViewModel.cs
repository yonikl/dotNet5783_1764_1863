using BO;
using PL.Commands;
using PL.Services;
using PL.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.ViewModels;

internal class SimulatorViewModel : ViewModelBase
{
    BackgroundWorker bw;
    Stopwatch sw;
    public ICommand StopCommand { get; }
    /// <summary>
    /// cofig the bw
    /// </summary>
    public SimulatorViewModel()
    {
        StopCommand = new StopSimulatorCommand();
        sw = new Stopwatch();
        bw = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        bw.DoWork += DoWork;
        bw.ProgressChanged += progressChanged;
        bw.RunWorkerCompleted += atEndOfSimulation;
        bw.RunWorkerAsync();
    }
    /// <summary>
    /// the DoWork method for the bw
    /// </summary>
    private void DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.SubscribeToStopSimulation(stopSimulation);
        Simulator.SubscribeToUpdateSimulation(strartWorkingOnOrder);
        Simulator.StartSimulation();
        sw.Start();
        while (!bw.CancellationPending)
        {
            Thread.Sleep(1000);
            bw.ReportProgress(0);
        }
    }
    /// <summary>
    /// update the needed properties for the view
    /// </summary>
    private void progressChanged(object? sender, ProgressChangedEventArgs e)
    {
        CurrentTime = sw.Elapsed.ToString().Substring(0, 8);
        OnPropertyChanged(nameof(ProgressValue));
        if (e.UserState != null)
        {
            var args = e.UserState as Tuple<Order, int>;
            var now = DateTime.Now;
            AproximateTime = args?.Item2;
            currentOrderStartTime = now;
            aproximateTimeToFinish = now + new TimeSpan(0, 0, aproximateTime ?? 0);
            OnPropertyChanged(nameof(AproximateTimeToFinish));
            OnPropertyChanged(nameof(CurrentOrderStartTime));
            CurrentOrder = args!.Item1!;
            CurrentStatus = args.Item1.Status ?? throw new NullReferenceException();
            NextStatus = args.Item1.Status == BO.Enums.OrderStatus.InProcess ? BO.Enums.OrderStatus.shipped : BO.Enums.OrderStatus.Delivered;
            CurrentId = args.Item1.ID; 
        }

    }




    /// <summary>
    /// method that subscribed to the simulator for the sake of updating the display
    /// </summary>
    private void strartWorkingOnOrder(object? sender, Tuple<Order, int> args)
    {
        bw.ReportProgress(0, args);
    }
    /// <summary>
    /// method that subscribed to the simulator for the sake of knowing when simulation stopped
    /// </summary>
    private void stopSimulation(object? sender, EventArgs e)
    {
        bw.CancelAsync();
    }
    /// <summary>
    /// clear subscription
    /// </summary>
    private void atEndOfSimulation(object? sender, RunWorkerCompletedEventArgs e)
    {
        Simulator.UnsubscribeFromStopSimulation(stopSimulation);
        Simulator.UnsubscribeFromUpdateSimulation(strartWorkingOnOrder);
    }


    /// properties
    private DateTime? currentOrderStartTime;
    public string? CurrentOrderStartTime => currentOrderStartTime?.ToString("HH:mm:ss");

    private BO.Enums.OrderStatus? currentStatus;
    public BO.Enums.OrderStatus? CurrentStatus
    {
        get => currentStatus;

        set
        {
            currentStatus = value;
            OnPropertyChanged(nameof(CurrentStatus));
        }
    }
    private BO.Enums.OrderStatus? nextStatus;
    public BO.Enums.OrderStatus? NextStatus
    {
        get => nextStatus;
        set
        {
            nextStatus = value;
            OnPropertyChanged(nameof(NextStatus));
        }
    }

    private int? aproximateTime;
    public int? AproximateTime
    {
        get => aproximateTime;
        set
        {
            aproximateTime = value;
            OnPropertyChanged(nameof(AproximateTime));
        }
    }

    private int? currentId;
    public int? CurrentId
    {
        get => currentId;
        set
        {
            currentId = value;
            OnPropertyChanged(nameof(CurrentId));
        }
    }

    private Order? currentOrder;
    public Order? CurrentOrder
    {
        get
        {
            return currentOrder;
        }
        set
        {
            currentOrder = value;
            OnPropertyChanged(nameof(CurrentOrder));
        }
    }

    private string? currentTime;
    public string? CurrentTime
    {
        get
        {
            return currentTime;
        }
        set
        {
            currentTime = value;
            OnPropertyChanged(nameof(CurrentTime));
        }
    }

    private DateTime? aproximateTimeToFinish;
    public string? AproximateTimeToFinish => aproximateTimeToFinish?.ToString("HH:mm:ss");

    public int? ProgressValue => (int)(((DateTime.Now - currentOrderStartTime)?.TotalSeconds / aproximateTime ?? 1) * 100);
}
