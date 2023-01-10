using BlApi;
using DalApi;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS0105 // The using directive for 'BlApi' appeared previously in this namespace
using BlApi;
#pragma warning restore CS0105 // The using directive for 'BlApi' appeared previously in this namespace
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
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
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
#pragma warning restore CS0168 // The variable 'ex' is declared but never used

    }
}
