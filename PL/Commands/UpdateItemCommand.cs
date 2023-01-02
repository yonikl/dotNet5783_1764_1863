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

internal class UpdateItemCommand : BaseCommand
{
    readonly IBl bl = BlApi.Factory.Get();
    private Cart cart;
    private int id;
    private readonly UpdateItemInCartViewModel? model;
    private readonly NavigationStore navigationStore;

    /// <summary>
    /// contructor for update amount of item in the cart 
    /// </summary>
    /// <param name="cart">cart of the user</param>
    /// <param name="id">product id the user want to update</param>
    /// <param name="model">update item in cart modal</param>
    /// <param name="navigationStore">to navigate back to cart view</param>
    public UpdateItemCommand(Cart cart, int id, UpdateItemInCartViewModel? model, NavigationStore navigationStore)
    {
        this.cart = cart;
        this.id = id;
        this.model = model;
        this.navigationStore = navigationStore;

    }
    /// <summary>
    /// Update ammunt of order using bl 
    /// </summary>
    /// <param name="parameter"></param>
    public override void Execute(object? parameter)
    {
        try
        {
           cart =  bl.Cart.UpdateAmountOfOrder(id,model!.SelectedAmount, cart);//update amount
           new NavigationService(navigationStore, () => new CartViewModel(navigationStore,cart)).Navigate();//Navigate back to cart view
        }
        ///catch exceptions
        catch (BlAmountNotValidException)
        {
            model!.Message = "Amount not valid";
        }
        catch(BlItemNotFoundInCartException)
        {
            model!.Message = "Item not found in the cart";
        }
        catch (Exception ex)
        {
            model!.Message = "Unknown error";
        }

    }
}
