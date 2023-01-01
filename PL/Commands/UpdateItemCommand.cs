using BlApi;
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
    private BO.OrderItem item;

    public UpdateItemCommand(BO.Cart cart, BO.OrderItem item)
    {
        this.cart = cart;
        this.item = item;   
    }
    public override void Execute(object? parameter)
    {
        try
        {
           cart =  bl.Cart.UpdateAmountOfOrder(item.ID, item.Amount, cart);
        }
        catch(Exception ex)
        {

        }
      
    }
}
