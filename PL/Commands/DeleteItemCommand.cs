using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Commands;

internal class DeleteItemCommand : BaseCommand
{
    private BO.Cart cart;
    private BO.OrderItem item;

    public DeleteItemCommand(BO.Cart cart, BO.OrderItem item)
    {
        this.cart = cart;
        this.item = item;   
    }
    public override void Execute(object? parameter)
    {
        throw new NotImplementedException();
    }
}
