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

namespace PL.Analytics.AnalyticsByStores
{
    /// <summary>
    /// Interaction logic for AnalyticsByStoresView.xaml
    /// </summary>
    public partial class AnalyticsByStoresView : UserControl
    {
        AnalyticsByStoresViewModel analyticsByStoresViewModel;
        public AnalyticsByStoresView()
        {
            InitializeComponent();
            analyticsByStoresViewModel = new AnalyticsByStoresViewModel();
            DataContext = analyticsByStoresViewModel;
        }
  

        private void _calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {

            if (_calendar.DisplayMode == CalendarMode.Month)
            {

                DateTime date = _calendar.DisplayDate.Date;
                _calendar.DisplayMode = CalendarMode.Year;

                analyticsByStoresViewModel.Date = date;

                if (analyticsByStoresViewModel.SelectedNameStore!= null)
                {

                    chartHistogram.ItemsSource = analyticsByStoresViewModel.getStoreExpensesPerMonthCollection(analyticsByStoresViewModel.SelectedNameStore, analyticsByStoresViewModel.Date);
                    chartLineSeries.ItemsSource = analyticsByStoresViewModel.getStoreExpensesPerDayCollection(analyticsByStoresViewModel.SelectedNameStore, analyticsByStoresViewModel.Date);
                    amountPerWeeks.ItemsSource = analyticsByStoresViewModel.getStoreExpensesPerWeeksCollection(analyticsByStoresViewModel.SelectedNameStore, analyticsByStoresViewModel.Date);
                    amountPerCities.ItemsSource = analyticsByStoresViewModel.getStoreExpensesPerMonthPerCityCollection(analyticsByStoresViewModel.SelectedNameStore, analyticsByStoresViewModel.Date);

                }

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            cb.Visibility = Visibility.Visible;
            cb.ItemsSource = analyticsByStoresViewModel.AllStoresName;

        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String storeName = cb.SelectedItem as String;
            analyticsByStoresViewModel.SelectedNameStore = storeName;
            chartHistogram.ItemsSource = analyticsByStoresViewModel.getStoreExpensesPerMonthCollection(analyticsByStoresViewModel.SelectedNameStore, analyticsByStoresViewModel.Date);
            chartLineSeries.ItemsSource = analyticsByStoresViewModel.getStoreExpensesPerDayCollection(analyticsByStoresViewModel.SelectedNameStore, analyticsByStoresViewModel.Date);
            amountPerWeeks.ItemsSource = analyticsByStoresViewModel.getStoreExpensesPerWeeksCollection(analyticsByStoresViewModel.SelectedNameStore, analyticsByStoresViewModel.Date);
            amountPerCities.ItemsSource = analyticsByStoresViewModel.getStoreExpensesPerMonthPerCityCollection(analyticsByStoresViewModel.SelectedNameStore, analyticsByStoresViewModel.Date);

        }
    }
}



