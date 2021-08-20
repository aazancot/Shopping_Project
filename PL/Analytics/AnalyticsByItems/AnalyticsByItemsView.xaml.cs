using System;
using Syncfusion.UI.Xaml.Charts;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;

namespace PL.Analytics.AnalyticsByItems
{
    /// <summary>
    /// Interaction logic for AnalyticsByItemsView.xaml
    /// </summary>
    public partial class AnalyticsByItemsView : UserControl
    {

        AnalyticsByItemsViewModel analyticsByItemsViewModel;
        public AnalyticsByItemsView()
        {
            InitializeComponent();
            analyticsByItemsViewModel = new AnalyticsByItemsViewModel();
            DataContext = analyticsByItemsViewModel;

        }

        private void _calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {

            if (_calendar.DisplayMode == CalendarMode.Month)
            {
                DateTime date = _calendar.DisplayDate.Date;

                _calendar.DisplayMode = CalendarMode.Year;

                analyticsByItemsViewModel.Date = date;

                if (analyticsByItemsViewModel.SelectedProductName != null)
                {
                   chartHistogram.ItemsSource = analyticsByItemsViewModel.getQuantityPerMonthCollection(analyticsByItemsViewModel.SelectedProductName, analyticsByItemsViewModel.Date);
                   chartLineSeries.ItemsSource = analyticsByItemsViewModel.getQuantityPerDayCollection(analyticsByItemsViewModel.SelectedProductName, analyticsByItemsViewModel.Date);
                   amountPerWeeks.ItemsSource = analyticsByItemsViewModel.getQuantityPerWeeksCollection(analyticsByItemsViewModel.SelectedProductName, analyticsByItemsViewModel.Date);
             

                }

            }
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            cb.Visibility = Visibility.Visible;
            cb.ItemsSource = analyticsByItemsViewModel.GetAllProductsNameByCategory(menuItem.Header.ToString());
           
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string productName = cb.SelectedItem as string;
            analyticsByItemsViewModel.SelectedProductName = productName;
            chartHistogram.ItemsSource = analyticsByItemsViewModel.getQuantityPerMonthCollection(analyticsByItemsViewModel.SelectedProductName, analyticsByItemsViewModel.Date);
            chartLineSeries.ItemsSource = analyticsByItemsViewModel.getQuantityPerDayCollection(analyticsByItemsViewModel.SelectedProductName, analyticsByItemsViewModel.Date);
            amountPerWeeks.ItemsSource = analyticsByItemsViewModel.getQuantityPerWeeksCollection(analyticsByItemsViewModel.SelectedProductName, analyticsByItemsViewModel.Date);

        }
    }
}



