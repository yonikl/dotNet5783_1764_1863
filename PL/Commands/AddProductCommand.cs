using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BlApi;
using BO;
using PL.Services;
using PL.Stores;
using PL.ViewModels;

namespace PL.Commands;

internal class AddProductCommand : BaseCommand
{
    readonly IBl bl = Factory.Get();
    readonly AddOrUpdateProductViewModel model;
    readonly NavigationStore navigationStore;
    public AddProductCommand(AddOrUpdateProductViewModel model, NavigationStore navigationStore)
    {
        this.model = model;
        this.navigationStore = navigationStore;
    }
    public override void Execute(object? parameter)
    {
        try
        {
            var product = new BO.Product()
            {
                ID = model.ProductId,
                Name = model.ProductName,
                InStock = model.ProductInStock,
                Category = model.ProductCategory,
                Price = model.ProductPrice
            };
            bl.Product.AddProduct(product);
            bl.Product.UpdateProduct(product);
            new NavigationService(navigationStore, () => new AdminViewModel(navigationStore)).Navigate();
        }
        catch (BlIDNotValidException)
        {
            model.ErrorMessages = "Id not valid";
        }
        catch (BlNameEmptyException)
        {
            model.ErrorMessages = "Name is empty";
        }
        catch (BlAmountNotValidException)
        {
            model.ErrorMessages = "Amount not valid";
        }
        catch (BlPriceNotValidException)
        {
            model.ErrorMessages = "Price not valid";
        }
        catch(BlCategoryDoesntSet)
        {
            model.ErrorMessages = "Category Doesnt Set";
        }
        catch (Exception exception)
        {
            model.ErrorMessages = "Unknown error";
        }

    }
}