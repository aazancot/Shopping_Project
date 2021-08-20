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
using PL.Analytics.AnalyticsByCategory.CarouselCategories;

namespace PL.Analytics.AnalyticsByCategory
{
    /// <summary>
    /// Interaction logic for AnalyticsByCategoryView.xaml
    /// </summary>
    public partial class AnalyticsByCategoryView : UserControl
    {
        AnalyticsByCategoryViewModel analyticsByCategoryViewModel;
        MainViewModel mainViewModel;
        public AnalyticsByCategoryView()
        {
            InitializeComponent();
            analyticsByCategoryViewModel = new AnalyticsByCategoryViewModel();
            mainViewModel = new MainViewModel();
            DataContext = new AnalyticsByCategoryViewModel();
            _carouselDABRadioStations.DataContext = mainViewModel;
        }
        private void _calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            if (_calendar.DisplayMode == CalendarMode.Month)
            {
                DateTime date = _calendar.DisplayDate.Date;
                _calendar.DisplayMode = CalendarMode.Year;
                analyticsByCategoryViewModel.Date = date;

                if (analyticsByCategoryViewModel.SelectedNameCategory != null)
                {
                    chartHistogram.ItemsSource = analyticsByCategoryViewModel.getCategoryExpensesPerMonthCollection(analyticsByCategoryViewModel.SelectedNameCategory, analyticsByCategoryViewModel.Date);
                    chartLineSeries.ItemsSource = analyticsByCategoryViewModel.getCategoryExpensesPerDayCollection(analyticsByCategoryViewModel.SelectedNameCategory, analyticsByCategoryViewModel.Date);
                    amountPerWeeks.ItemsSource = analyticsByCategoryViewModel.getCategoryExpensesPerWeeksCollection(analyticsByCategoryViewModel.SelectedNameCategory, analyticsByCategoryViewModel.Date);
                    amountPerCategories.ItemsSource = analyticsByCategoryViewModel.getExpensesPerMonthPerCategoryCollection(analyticsByCategoryViewModel.SelectedNameCategory, analyticsByCategoryViewModel.Date);
                }
            }
        }

 
        private void _buttonLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            _carouselDABRadioStations.RotateRight();
        }

        private void _buttonRightArrow_Click(object sender, RoutedEventArgs e)
        {
            _carouselDABRadioStations.RotateLeft();
        }

        private void _carouselDABRadioStations_SelectionChanged(FrameworkElement selectedElement)
        {
            mainViewModel.SelectedRadioStationDAB = selectedElement.DataContext as RadioStation;
            analyticsByCategoryViewModel.SelectedNameCategory = mainViewModel.SelectedRadioStationDAB.Name;
            chartHistogram.ItemsSource = analyticsByCategoryViewModel.getCategoryExpensesPerMonthCollection(analyticsByCategoryViewModel.SelectedNameCategory, analyticsByCategoryViewModel.Date);
            chartLineSeries.ItemsSource = analyticsByCategoryViewModel.getCategoryExpensesPerDayCollection(analyticsByCategoryViewModel.SelectedNameCategory, analyticsByCategoryViewModel.Date);
            amountPerWeeks.ItemsSource = analyticsByCategoryViewModel.getCategoryExpensesPerWeeksCollection(analyticsByCategoryViewModel.SelectedNameCategory, analyticsByCategoryViewModel.Date);
            amountPerCategories.ItemsSource = analyticsByCategoryViewModel.getExpensesPerMonthPerCategoryCollection(analyticsByCategoryViewModel.SelectedNameCategory, analyticsByCategoryViewModel.Date);


        }
    }
}




