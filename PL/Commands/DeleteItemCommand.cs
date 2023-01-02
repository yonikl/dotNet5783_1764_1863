using BlApi;
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
    private NavigationStore navigationStore;
    private readonly int id;

    public DeleteItemCommand(BO.Cart cart, int id, NavigationStore navigationStore)
    {
        this.cart = cart;
        this.id = id;
        this.navigationStore = navigationStore;
    }
    public override void Execute(object? parameter)
    {
        try
        {
            cart = bl.Cart.UpdateAmountOfOrder(id, 0, cart);
            new NavigationService(navigationStore, () => new CartViewModel(navigationStore, cart)).Navigate();

        }
        catch { }

    }
}
