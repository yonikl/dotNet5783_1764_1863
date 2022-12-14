using System;
using System.Diagnostics;
using System.Windows;

using BO;

namespace PL;

/// <summary>
/// Interaction logic for AddAndUpdate.xaml
/// </summary>
public partial class AddAndUpdate : Window
{
    private BlApi.IBl bl = BlApi.Factory.Get();

    /// <summary>
    /// constructor for add and update window puts in the blank default values
    /// </summary>
    /// <param name="bl"></param>
    public AddAndUpdate()
    {
      
        InitializeComponent();

        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        ProceedButton.Content = "Add";
        CategorySelector.Text = "None";
        PriceTextBox.Text = "0";
        InStockTextBox.Text = "0";
        IdTextBox.Text = bl.Product.GetIdForProduct().ToString();
        ProceedButton.Click += addProduct;//call add product function
    }
    /// <summary>
    /// constructor for update fill the blank in the product values
    /// </summary>
    /// <param name="bl"></param>
    /// <param name="id"></param>
    public AddAndUpdate(int id)
    {
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

    /// <summary>
    /// add product and close the window
    /// catch an exception if the input is incorrect 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void addProduct(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.Product.AddProduct(createProductFromData());
            new ListView().Show();
            this.Close();
        }
        catch (BlIDNotValidException)
        {
            MessageBox.Show("Id not valid", "ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
        }
        catch (BlNameEmptyException)
        {
            MessageBox.Show("Name is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (BlAmountNotValidException)
        {
            MessageBox.Show("Amount not valid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
           
        }
        catch (BlPriceNotValidException)
        {
            MessageBox.Show("Price not valid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception exception)
        {
            MessageBox.Show("Unknown error", "ERROR", MessageBoxButton.OK,MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// update the product and close the window
    /// catch an exception if the input is incorrect 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void updateProduct(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.Product.UpdateProduct(createProductFromData());
            new ListView().Show();
            this.Close();
        }
        catch (BlIDNotValidException)
        {
            MessageBox.Show("Id not valid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (BlNameEmptyException)
        {
            MessageBox.Show("Name is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (BlAmountNotValidException)
        {
            MessageBox.Show("Amount not valid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        catch (BlPriceNotValidException)
        {
            MessageBox.Show("Price not valid", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception exception)
        {
            MessageBox.Show("Unknown error", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// create new product 
    /// </summary>
    /// <returns></returns>
    private Product createProductFromData()
    {
        return new Product()
        {
            ID = Convert.ToInt32(IdTextBox.Text),
            Category = (Enums.Category)Enum.Parse(typeof(Enums.Category), CategorySelector.Text),
            InStock = Convert.ToInt32(InStockTextBox.Text),
            Name = NameTextBox.Text,
            Price = Convert.ToDouble(PriceTextBox.Text)
        };
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        new ListView().Show();
        Close();
    }
}

