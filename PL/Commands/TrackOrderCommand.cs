using BlApi;
using PL.Stores;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Commands;

internal class TrackOrderCommand : BaseCommand
{
    readonly IBl bl = Factory.Get();
    readonly TrackOrdersViewModel model;

    /// <summary>
    /// constructor for track order
    /// </summary>
    /// <param name="model">get the track order modal</param>
 
    public TrackOrderCommand( TrackOrdersViewModel model)
    {
        this.model = model;
  
    }

    /// <summary>
    /// print the order status 
    /// </summary>
    /// <param name="parameter"></param>
    public override void Execute(object? parameter)
    {
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
        try
        {
            model.Message = bl.Order.TrackOrder(model.Id).ToString();//get from bl the order stutus
        }
        ///if the iten not found 
        catch(BO.BlItemNotFoundException)
        {
            model.Message = "Item not found";
        }
        catch(Exception ex)
        {
            model.Message = "Unknown error";
        }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
    }
}
