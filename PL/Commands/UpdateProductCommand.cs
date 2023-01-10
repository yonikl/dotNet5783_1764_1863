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

internal class UpdateProductCommand : BaseCommand
{
    readonly IBl bl = Factory.Get();
    readonly AddOrUpdateProductViewModel model;
    readonly NavigationStore navigationStore;

    /// <summary>
    /// constructor for update product in bl 
    /// </summary>
    /// <param name="model">get product modal</param>
    /// <param name="navigationStore">get navigation store to navigate back to admin view</param>
    public UpdateProductCommand(AddOrUpdateProductViewModel model, NavigationStore navigationStore)
    {
        this.model = model;
        this.navigationStore = navigationStore;
        
    }
    /// <summary>
    /// Crate new peosuct read all details from modal and update using bl
    /// </summary>
    /// <param name="parameter"></param>
    public override void Execute(object? parameter)
    {
#pragma warning disable CS0168 // The variable 'exception' is declared but never used
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
            bl.Product.UpdateProduct(product);
            new NavigationService(navigationStore, () => new AdminViewModel(navigationStore)).Navigate();//navigate back to admin view
        }
        //catch exceptions if there is problem whit the detailes
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
        catch (BlCategoryDoesntSet)
        {
            model.ErrorMessages = "Category Doesnt Set";
        }
        catch(BO.BlEmptyOrderExistsException)
        {
            model.ErrorMessages = "Empty order exists";
        }
        catch (Exception exception)
        {
            model.ErrorMessages = "Unknown error";
        }
#pragma warning restore CS0168 // The variable 'exception' is declared but never used

    }

}

