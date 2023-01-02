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

    /// <summary>
    /// constructor for add to cart
    /// </summary>
    /// <param name="cart">cart of the user</param>
    /// <param name="id">product id that the user choose</param>
    /// <param name="navigationStore">navigation for back to cart view</param>
    /// <param name="model">product view modal</param>
    public AddProductToCartCommand(Cart cart, int id, NavigationStore navigationStore,ProductViewModel model)
    {
        this.cart = cart;
        this.id = id;
        this.navigationStore = navigationStore;
        this.model = model;
    }

    /// <summary>
    /// if the product in stock return true
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public override bool CanExecute(object? parameter)
    {
        return model.Item.InStock;
    }
    /// <summary>
    /// add the product to the cart
    /// </summary>
    /// <param name="parameter"></param>
    public override void Execute(object? parameter)
    {
        try
        {
            cart = bl.Cart.Add(id, cart);//add to cart
            new NavigationService(navigationStore, () => new CreateNewOrderViewModel(navigationStore, cart)).Navigate();//navigate to create new order view
        }
        //catch excaptions if the product not found or amount isn't correct
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
