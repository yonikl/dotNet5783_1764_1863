using BlApi;
using BO;
using PL.Services;
using PL.Stores;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Commands;

internal class DeleteItemCommand : BaseCommand
{
    private readonly IBl bl = Factory.Get();
    private BO.Cart cart;
    private readonly CartViewModel model;

    public DeleteItemCommand(BO.Cart cart,CartViewModel model)
    {
        this.cart = cart;   
        this.model = model;
    }
    public override void Execute(object? parameter)
    {
        try
        {
            cart = bl.Cart.UpdateAmountOfOrder(model.Id, 0, cart);
            model.Refresh();
            model.Message = "Deleted from cart";

        }
        catch (BlAmountNotValidException)
        {
            model!.Message = "Amount not valid";
        }
        catch (BlItemNotFoundInCartException)
        {
            model!.Message = "Item not found in the cart";
        }
        catch (Exception ex)
        {
            model!.Message = "Unknown error";
        }
                                                           
    }
}
