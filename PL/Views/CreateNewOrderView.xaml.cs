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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.Views
{
    /// <summary>
    /// Interaction logic for CreateNewOrderView.xaml
    /// </summary>
    public partial class CreateNewOrderView : UserControl
    {

        /// <summary>
        /// DependencyProperty for the MouseDoubleClick event
        /// </summary>
        public ICommand ShowProduct
        {
            get { return (ICommand)GetValue(ShowProductProperty); }
            set { SetValue(ShowProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowProductProperty =
            DependencyProperty.Register("ShowProduct", typeof(ICommand), typeof(CreateNewOrderView), new PropertyMetadata(null));


        public CreateNewOrderView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// handle MouseDoubleClick event
        /// </summary>
        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(ShowProduct!=null)
                ShowProduct.Execute(null);
        }
    }
}
