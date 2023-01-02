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
    /// <summary>
    /// constructor to update delivery
    /// </summary>
    /// <param name="model"></param>
    public UpdateDeliveryCommand(AdminViewModel model)
    {
        this.model = model;
    }

    /// <summary>
    /// update delivery using bl
    /// </summary>
    /// <param name="parameter"></param>
    public override void Execute(object? parameter)
    {
        try
        {
            bl.Order.UpdateDelivery(model.SelectedOrderTracking.ID);//update delivery
            model.Message = "Update succesfuly";
            model.Refresh();//refrash the list view
        }
        //catch exception if the item not found or there is problem whit the update
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
            model.Message = "Unknown error";

        }

    }
}
