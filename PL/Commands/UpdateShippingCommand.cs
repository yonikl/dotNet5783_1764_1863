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

    /// <summary>
    /// contructor for update shipping in order
    /// </summary>
    /// <param name="model">modal of admin view</param>
    public UpdateShippingCommand(AdminViewModel model)
    {
        this.model = model;
    }
    /// <summary>
    /// update shipping using bl
    /// </summary>
    /// <param name="parameter"></param>
    public override void Execute(object? parameter)
    {
        try
        {
            bl.Order.UpdateShipping(model.SelctedOrder.ID);//update shipping using bl and modal 
            model.Message = "Update succesfuly";
            model.Refresh();//refrash the list
        }
        //catch exceptions if the order already shipped or the item not found
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
