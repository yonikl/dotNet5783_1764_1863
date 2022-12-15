﻿using PL.Commands;
using PL.Services;
using PL.Stores;
using PL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PL.Models;

namespace PL.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    private readonly Shop shop;
    public ICommand GoToAdminView { get; }
    public ICommand GoToCreateNewOrder { get; }
    public ICommand GoToTrackOrders { get; }

    public MainWindowViewModel(NavigationStore navigationStore, Shop shop)
    {
        this.navigationStore = navigationStore;
        this.shop = shop;
        GoToAdminView = new NavigationCommand(new NavigationService(navigationStore, CreateNewAdminViewModel));
        GoToCreateNewOrder = new NavigationCommand(new NavigationService(navigationStore, CreateNewCreateNewOrderViewModel));
        GoToTrackOrders = new NavigationCommand(new NavigationService(navigationStore, CreateNewTrackOrdersViewModel));
    }

    public ViewModelBase CreateNewAdminViewModel()
    {
        return new AdminViewModel(navigationStore, shop);
    }
    public ViewModelBase CreateNewCreateNewOrderViewModel()
    {
        return new CreateNewOrderViewModel();
    }
    public ViewModelBase CreateNewTrackOrdersViewModel()
    {
        return new TrackOrdersViewModel();
    }
}

