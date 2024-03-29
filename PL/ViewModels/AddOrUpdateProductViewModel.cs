﻿using PL.Commands;
using PL.Services;
using PL.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BlApi;
using BO;

namespace PL.ViewModels;
/// <summary>
/// ViewModdel For AddOrUpdateProductView that let the admin to add or update products
/// </summary>
internal class AddOrUpdateProductViewModel : ViewModelBase
{
    private readonly IBl bl = Factory.Get();
    private readonly NavigationStore navigationStore;
    private int productId;
    private string? productName;
    private double productPrice;
    private int productInStock;
    private BO.Enums.Category productCategory;
    private string errorMessages;

    public ICommand GoBack { get; }
    public ICommand AddOrUpdate { get; }

    public IEnumerable<BO.Enums.Category> Categories => (IEnumerable<Enums.Category>)Enum.GetValues(typeof(BO.Enums.Category));
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="navigationStore">
    /// we get the navigation store for changing userControlls
    /// </param>
    /// <param name="id">
    /// the id of the product we updating or 0 if we adding product
    /// </param>
    /// <exception cref="BlItemNotFoundException">
    /// if the item not found
    /// </exception>
    /// <exception cref="BlIDNotValidException">
    /// if the ID not valid
    /// </exception>
#pragma warning disable CS8618 // Non-nullable field 'errorMessages' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
    public AddOrUpdateProductViewModel(NavigationStore navigationStore, int id=0)
#pragma warning restore CS8618 // Non-nullable field 'errorMessages' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
    {
        this.navigationStore = navigationStore;
        GoBack = new NavigationCommand(new NavigationService( this.navigationStore, () => new AdminViewModel(navigationStore)));
        //setting the fields according to add/update state
        submitButtonText = id == 0 ? "Add product" : "Update product";
        AddOrUpdate = id == 0 ? new AddProductCommand(this, navigationStore) : new UpdateProductCommand(this, navigationStore);
        ProductId = id == 0 ? bl.Product.GetIdForProduct() : id;
        if(id != 0) // if its update product
        {
            Product item;
            try {
                item = bl.Product.GetProductForAdmin(id);
            }
            catch (BlItemNotFoundException ex)
            {
                throw new Exception("Item Not Found", ex);
            }catch(BlIDNotValidException ex)
            {
                throw new Exception("ID Not Valid", ex);
            }
            productCategory = item.Category ?? throw new NullReferenceException();
            productInStock = item.InStock;
            productPrice = item.Price;
            productName = item.Name!;
        }
    }
    public string submitButtonText { get; }
    public int ProductId
	{
		get
		{
			return productId;
		}
		set
		{
            productId = value;
			OnPropertyChanged(nameof(ProductId));
		}
	}

    public string ProductName
    {
        get
        {
            return productName ?? "";
        }
        set
        {
            productName = value;
            OnPropertyChanged(nameof(ProductName));
        }
    }
	public double ProductPrice
    {
		get
		{
			return productPrice;
		}
		set
		{
			productPrice = value;
		OnPropertyChanged(nameof(ProductPrice));
		}
	}
    public int ProductInStock
    {
        get
        {
            return productInStock;
        }
        set
        {
            productInStock = value;
            OnPropertyChanged(nameof(ProductInStock));
        }
    }
    public BO.Enums.Category ProductCategory
    {
        get
        {
            return productCategory;
        }
        set
        {
            productCategory = (BO.Enums.Category)value;
            OnPropertyChanged(nameof(ProductCategory));
        }
    }

    public string ErrorMessages
    {
        get => errorMessages;
        set
        {
            errorMessages = value;
            OnPropertyChanged(nameof(ErrorMessages));
        }
    }
}
