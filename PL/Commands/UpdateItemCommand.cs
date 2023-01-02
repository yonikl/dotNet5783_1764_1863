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
    private BO.Cart cart;
    private int id;
    private readonly UpdateItemInCartViewModel? model;
    private readonly int amount;
    private readonly NavigationStore navigationStore;

    public UpdateItemCommand(BO.Cart cart, int id, UpdateItemInCartViewModel? model, NavigationStore navigationStore)
    {
        this.cart = cart;
        this.id = id;
        this.model = model;
        this.navigationStore = navigationStore;

    }
    public override void Execute(object? parameter)
    {
        try
        {
           cart =  bl.Cart.UpdateAmountOfOrder(id,model!.SelectedAmount, cart);
           new NavigationService(navigationStore, () => new CartViewModel(navigationStore,cart)).Navigate();
        }
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
