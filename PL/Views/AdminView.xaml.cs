using PL.Commands;
using PL.Services;
using PL.Stores;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {


        public ICommand UpdateProduct
        {
            get { return (ICommand)GetValue(UpdateProductProperty); }
            set { SetValue(UpdateProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpdateProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpdateProductProperty =
            DependencyProperty.Register("UpdateProduct", typeof(ICommand), typeof(AdminView), new PropertyMetadata(null));


        private BlApi.IBl bl = BlApi.Factory.Get();
        public AdminView()
        {
            InitializeComponent();
           
        }


        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
           if(UpdateProduct != null) UpdateProduct.Execute(null);
        }
    }
}
