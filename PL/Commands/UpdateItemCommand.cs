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

    public UpdateItemCommand(BO.Cart cart, int id, UpdateItemInCartViewModel? model, NavigationStore navigationStore, int amount = -1)
    {
        this.cart = cart;
        this.id = id;
        if(amount == -1)
        {
            this.model = model;
        }
        else
        {
            this.amount = amount;
        }
        this.navigationStore = navigationStore;

    }
    public override void Execute(object? parameter)
    {
        try
        {
           cart =  bl.Cart.UpdateAmountOfOrder(id, model == null ? amount : model.SelectedAmount, cart);
           new NavigationService(navigationStore, () => new CartViewModel(navigationStore,cart)).Navigate();
        }
        catch(Exception ex)
        {

        }
      
    }
}
