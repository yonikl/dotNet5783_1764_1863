using BlApi;
using PL.Stores;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Commands;

internal class TrackOrderCommand : BaseCommand
{
    readonly IBl bl = Factory.Get();
    readonly TrackOrdersViewModel model;
    readonly NavigationStore navigationStore;

    public TrackOrderCommand( TrackOrdersViewModel model, NavigationStore navigationStore)
    {
        this.model = model;
        this.navigationStore = navigationStore;
    }

    public override void Execute(object? parameter)
    {
        try
        {
            model.Message = bl.Order.TrackOrder(model.Id).ToString();
        }
        catch(BO.BlItemNotFoundException)
        {
            model.Message = "Item not found";
        }
        catch(Exception ex)
        {
            model.Message = "Unknown error";
        }
    }
}
