using BlApi;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.Commands;

internal class DeleteProductCommand : BaseCommand
{
    private readonly IBl bl = Factory.Get();
    private readonly AdminViewModel model;
    public DeleteProductCommand(AdminViewModel model)
    {
        this.model = model;
    }
    public override void Execute(object? parameter)
    {
        try
        {
            bl.Product.RemoveProduct(model.SelectedProduct.ID);
            model.Refresh();
            model.MessageForProduct = "deleted succesfuly";


        }
        catch(BO.BlItemNotFoundException)
        {
            model.MessageForProduct = "Item not found";
        }
        catch(BO.BlProductExistsInOrdersException)
        {
            model.MessageForProduct = "Product exists in order can't delete";
        }
        catch(Exception e)
        {
            model.MessageForProduct = "Unknown error";
        }
    }
}
