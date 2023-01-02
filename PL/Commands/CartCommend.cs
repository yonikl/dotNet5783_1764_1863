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

internal class CartCommend : BaseCommand
{
    readonly IBl bl = BlApi.Factory.Get();
    readonly OrderConfirmationViewModel model;
    readonly NavigationStore navigationStore;
    readonly BO.Cart cart;

    public CartCommend(OrderConfirmationViewModel model,BO.Cart cart,NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;
        this.cart = cart;
        this.model = model;
    }
    public override void Execute(object? parameter)
    {
        try
        {
            int idForOrder = bl.Cart.MakeAnOrder(cart, model.Name, model.Address, model.Email);
            model.Message = "Update succesfuly order in process\n" + "Order number is: " + idForOrder.ToString();
            model.Back = new NavigationCommand(new NavigationService(navigationStore, () => new MainWindowViewModel(navigationStore)));
        }
        catch(BlPersonalDetailsException)
        {
            model.Message = "One or more details is not valid";
        }
        catch(BlEmailIncourect)
        {
            model.Message = "Email format is incourect";
        }
        catch(BlAmountNotValidException)
        {
            model.Message = "Amount not valid";
        }
        catch(BlNotEnoughInStockException ex)
        {
            model.Message = "There is not enough " + ex.Message + " in stock";
        }
        catch(BlItemNotFoundException)
        {
            model.Message = "Item not found";
        }
        catch (NotItemsInCart)
        {
            model.Message = "Not found items in your cart can't make an order";
        }
        catch (Exception ex)
        {
            model.Message = "Unnown error"; 
        }
    }

   
}
