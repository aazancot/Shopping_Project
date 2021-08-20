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

namespace PL.Orders_History
{
    /// <summary>
    /// Interaction logic for MainListUC.xaml
    /// </summary>
    public partial class MainListUC : UserControl
    {
        public MainListUC()
        {
            InitializeComponent();
        
        }


        private void Button_PlusClick(object sender, RoutedEventArgs e)
        {
            Order o = (sender as Button).DataContext as Order;
            
         
                }
        private void Button_MinusClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
