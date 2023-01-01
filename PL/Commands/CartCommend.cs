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
            bl.Cart.MakeAnOrder(cart, model.Name, model.Address, model.Email);
            model.Message = "Update succesfuly order in process";
            new NavigationService(navigationStore, () => new CreateNewOrderViewModel(navigationStore));
        }
        catch(BO.BlPersonalDetailsException)
        {
            model.Message = "One or more details is incourect";
        }
        catch(BO.BlAmountNotValidException)
        {
            model.Message = "Amount not valid";
        }
        catch(BO.BlNotEnoughInStockException)
        {
            model.Message = "There is not enough in stock";
        }
        catch(BO.BlItemNotFoundException)
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
