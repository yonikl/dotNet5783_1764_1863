using BlApi;
using BO;
using DalApi;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using System.Windows;

namespace PL.Commands;

internal class UpdateDeliveryCommand: BaseCommand
{

    readonly IBl bl = BlApi.Factory.Get();
    readonly AdminViewModel model;

    public UpdateDeliveryCommand(AdminViewModel model)
    {
        this.model = model;
    }

    public override void Execute(object? parameter)
    {
        try
        {
            bl.Order.UpdateDelivery(model.SelectedOrderTracking.ID);
            model.Message = "Update succesfuly";
            model.Refresh();
        }
        catch (BlItemNotFoundException)
        {
            model.Message = "Item not found";
        }
        catch (BlOrderDoesNotShippedException)//throw exception if the ship date is already update
        {
            model.Message = "Order does not shipped";
        }
        catch (BlOrderAlreadyDeliveredException)
        {
            model.Message = "Order already delivered";
        }
      
        catch (Exception ex)
        {
            throw;

        }

    }
}
