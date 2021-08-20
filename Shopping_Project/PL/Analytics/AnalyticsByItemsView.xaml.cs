using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BE;

namespace PL.Analytics
{
    /// <summary>
    /// Interaction logic for AnalyticsByItems.xaml
    /// </summary>
    public partial class AnalyticsByItemsView : UserControl
    {
        AnalyticsByItemsViewModel analyticsByItemsViewModel;
     
        public AnalyticsByItemsView()
        {
            InitializeComponent();
            analyticsByItemsViewModel = new AnalyticsByItemsViewModel();
            DataContext = analyticsByItemsViewModel;

            //Product p = new Product { BarCode = 1 };
            //analyticsByItemsViewModel.QuantityPerMonthByItem = analyticsByItemsViewModel.getQuantityPerMonthCollection(p,DateTime.Now);

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //BE.Product product = (sender as MenuItem).DataContext as Product;
            //analyticsByItemsViewModel.QuantityPerMonthByItem = analyticsByItemsViewModel.getQuantityPerMonthCollection(product, DateTime.Now);
            //foreach (var item in analyticsByItemsViewModel.QuantityPerMonthByItem)
            //{
            //    MessageBox.Show(item.LabelX, item.LabelY.ToString());
            //}
            //chartLineSeries3D.Collection = analyticsByItemsViewModel.QuantityPerMonthByItem;
            //chartLineSeries3D.DataContext = analyticsByItemsViewModel.QuantityPerMonthByItem;
        }
    }
}
