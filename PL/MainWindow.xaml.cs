﻿
using PL.Stores;
using System.Windows;

namespace PL;

/// <summary>
/// The main window for the application MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private BlApi.IBl bl = BlApi.Factory.Get();
    public MainWindow()
    {
        InitializeComponent();
    }
}

