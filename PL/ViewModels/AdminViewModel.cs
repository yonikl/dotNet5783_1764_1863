﻿using PL.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using PL.Models;
using System.Windows.Input;
using PL.Commands;
using PL.Services;
using System.Collections.Specialized;

namespace PL.ViewModels;

internal class AdminViewModel : ViewModelBase, INotifyCollectionChanged
{
    private IBl? bl = Factory.Get();

    public IEnumerable<OrderForList?> Orders => bl!.Order.GetAllOrders();
    public IEnumerable<ProductForList?> Products
    {
        get
        {
            if (category.ToString() == "None") 
                return bl!.Product.GetAllProducts();
            return bl!.Product.GetAllProducts(x => x?.Category.ToString() == category.ToString());
        }
    }

    public IEnumerable<BO.Enums.Category> Categories => (IEnumerable<Enums.Category>)Enum.GetValues(typeof(BO.Enums.Category));

    public ICommand AddProduct { get; }
    public ICommand UpdateProduct { get; }
    public ICommand CategorySelctor { get; }

    public ICommand Back { get; }
    public AdminViewModel(NavigationStore navigationStore)
    {
        AddProduct = new NavigationCommand(new NavigationService(navigationStore, () => new AddOrUpdateProductViewModel(navigationStore)));
        navigationStore.CurrentViewModel = this;

        Back = new NavigationCommand(new NavigationService(navigationStore, () => new MainWindowViewModel(navigationStore)));
        UpdateProduct = new NavigationCommand(new NavigationService(navigationStore, () => new AddOrUpdateProductViewModel(navigationStore, selectedProduct!.ID)));

    }
    private BO.Enums.Category category;
    private BO.ProductForList selectedProduct;

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public BO.Enums.Category Category
    {
        get => category;
        set
        {
            category = value;
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Products));
        }
    }

    public BO.ProductForList SelectedProduct
    {
        get => selectedProduct;
        set
        {
            selectedProduct = value;
            OnPropertyChanged(nameof(SelectedProduct));
        }
    }
}
