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

namespace PL.Analytics.AnalyticsByTotal
{
    /// <summary>
    /// Interaction logic for AnalyticsByTotalView.xaml
    /// </summary>
    public partial class AnalyticsByTotalView : UserControl
    {
        AnalyticsByTotalViewModel analyticsByTotalViewModel;
        public AnalyticsByTotalView()
        {
            InitializeComponent();
            analyticsByTotalViewModel = new AnalyticsByTotalViewModel();
            chartHistogram.ItemsSource = analyticsByTotalViewModel.getTotalExpensesPerMonthCollection(analyticsByTotalViewModel.Date);
            chartLineSeries.ItemsSource = analyticsByTotalViewModel.getTotalExpensesPerDayCollection(analyticsByTotalViewModel.Date);
            amountPerWeeks.ItemsSource = analyticsByTotalViewModel.getTotalExpensesPerWeeksCollection(analyticsByTotalViewModel.Date);
            
        }

        private void _calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            if (_calendar.DisplayMode == CalendarMode.Month)
            {
                DateTime date = _calendar.DisplayDate.Date;
       
                _calendar.DisplayMode = CalendarMode.Year;
                analyticsByTotalViewModel.Date = date;
               chartHistogram.ItemsSource = analyticsByTotalViewModel.getTotalExpensesPerMonthCollection(analyticsByTotalViewModel.Date);
               chartLineSeries.ItemsSource = analyticsByTotalViewModel.getTotalExpensesPerDayCollection(analyticsByTotalViewModel.Date);
               amountPerWeeks.ItemsSource = analyticsByTotalViewModel.getTotalExpensesPerWeeksCollection(analyticsByTotalViewModel.Date); 
              //chartPieSeries.ItemsSource = testViewModel.getQuantityPerWeeksCollection(testViewModel.SelectedProduct, testViewModel.Date;
            }
        }

    }
}



