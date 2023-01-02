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

internal class AddProductToCartCommand : BaseCommand
{
    readonly IBl bl = Factory.Get();
    Cart cart;
    readonly int id;
    private readonly NavigationStore navigationStore;
    private readonly ProductViewModel model;

    public AddProductToCartCommand(Cart cart, int id, NavigationStore navigationStore,ProductViewModel model)
    {
        this.cart = cart;
        this.id = id;
        this.navigationStore = navigationStore;
        this.model = model;
    }
    public override bool CanExecute(object? parameter)
    {
        return model.Item.InStock;
    }
    public override void Execute(object? parameter)
    {
        try
        {
            cart = bl.Cart.Add(id, cart);
            new NavigationService(navigationStore, () => new CreateNewOrderViewModel(navigationStore, cart)).Navigate();
        }
        catch (BlItemNotFoundInCartException)
        {
            model.ErrorMessages = "Product not found";
        }
        catch (BlAmountNotValidException)
        {
            model.ErrorMessages = "Amount isn't correct";
        }

    }
}
