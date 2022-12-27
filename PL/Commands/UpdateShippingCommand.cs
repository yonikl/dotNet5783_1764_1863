using BlApi;
using DalApi;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace PL.Commands;

internal class UpdateShippingCommand : BaseCommand
{
    readonly IBl bl = BlApi.Factory.Get();
    readonly AdminViewModel model;

    public UpdateShippingCommand(AdminViewModel model)
    {
        this.model = model;
    }

    public override void Execute(object? parameter)
    {
        try
        {
            bl.Order.UpdateShipping(model.SelectedOrderTracking.ID);
            model.Message = "Update succesfuly";
        }
        catch (BO.BlItemNotFoundException)
        {
            model.Message = "Item not found";
        }
        catch (BO.BlOrderAlreadyShippedException)//throw exception if the ship date is already update
        {
            model.Message = "Order already shipped";
        }
        catch(Exception ex)
        {
            model.Message = "Unknown error";
        }

    }
}
