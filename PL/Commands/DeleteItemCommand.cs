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

    /// <summary>
    /// constructor for delete item from the cart 
    /// </summary>
    /// <param name="cart">user cart</param>
    /// <param name="model">get the cart view modal</param>
    public DeleteItemCommand(BO.Cart cart,CartViewModel model)
    {
        this.cart = cart;   
        this.model = model;
    }

    /// <summary>
    /// Delete from cart
    /// </summary>
    /// <param name="parameter"></param>
    public override void Execute(object? parameter)
    {
        try
        {
            cart = bl.Cart.UpdateAmountOfOrder(model.Id, 0, cart);//delete the item from the cart using bl
            model.Refresh();//refrash to the list
            model.Message = "Deleted from cart";

        }
        ///catch exception if the there is problem whit the delete item
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
