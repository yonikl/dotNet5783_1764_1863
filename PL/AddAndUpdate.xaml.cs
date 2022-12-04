﻿using System;
using System.Windows;
using BlApi;
using BO;

namespace PL;

/// <summary>
/// Interaction logic for AddAndUpdate.xaml
/// </summary>
public partial class AddAndUpdate : Window
{
    private IBl bl;
    public AddAndUpdate(IBl bl)
    {
        this.bl = bl;
        InitializeComponent();

        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        ProceedButton.Content = "Add";
        CategorySelector.Text = "None";
        PriceTextBox.Text = "0";
        InStockTextBox.Text = "0";
        IdTextBox.Text = bl.Product.GetIdForProduct().ToString();
        ProceedButton.Click += addProduct;
    }

    public AddAndUpdate(IBl bl, int id)
    {
        this.bl = bl;
        InitializeComponent();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        var a = bl.Product.GetProductForAdmin(id);
        ProceedButton.Content = "Update";
        IdTextBox.Text = a.ID.ToString();
        CategorySelector.Text = a.Category.ToString();
        NameTextBox.Text = a.Name;
        PriceTextBox.Text = a.Price.ToString();
        InStockTextBox.Text = a.InStock.ToString();
        ProceedButton.Click += updateProduct;

    }

    private void addProduct(object sender, RoutedEventArgs e)
    {
        bl.Product.AddProduct(createProductFromData());
        
        new ListView(bl).Show();
        this.Close();
    }

    private void updateProduct(object sender, RoutedEventArgs e)
    {
        //MessageBoxResult mbr = MessageBox.Show("Are u sure?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
        bl.Product.UpdateProduct(createProductFromData());
        new ListView(bl).Show();
        this.Close();
    }

    private Product createProductFromData()
    {
        return new Product()
        {
            ID = Convert.ToInt32(IdTextBox.Text),
            Category = (BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), CategorySelector.Text),
            InStock = Convert.ToInt32(InStockTextBox.Text),
            Name = NameTextBox.Text,
            Price = Convert.ToDouble(PriceTextBox.Text)
        };
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        new ListView(bl).Show();
        Close();
    }
}

