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

    public AddProductToCartCommand(Cart cart, int id, NavigationStore navigationStore)
    {
        this.cart = cart;
        this.id = id;
        this.navigationStore = navigationStore;
    }
    public override void Execute(object? parameter)
    {
        try
        {
            cart = bl.Cart.Add(id, cart);
        }
        catch { }
        new NavigationService(navigationStore, () => new CreateNewOrderViewModel(navigationStore, cart)).Navigate();
    }
}
