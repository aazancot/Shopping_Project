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
using System.Threading;
using System.Windows.Threading;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using System.ComponentModel;
using PL.Converters;
using System.Globalization;

namespace PL.Recommendations
{
    /// <summary>
    /// Interaction logic for RecommendationsView.xaml
    /// </summary>
    public partial class RecommendationsView : UserControl
    {

        public RecommendationsViewModel recommendationsViewModel;
        public RecommendationsView()
        {
            InitializeComponent();
            recommendationsViewModel = new RecommendationsViewModel();

            DataContext = recommendationsViewModel;

            _calendar.DisplayDateStart = DateTime.Now;
            _calendar.DisplayDateEnd = DateTime.Now.AddDays(7);

            Task.Factory.StartNew(() => LoadDataInThread());


        }
  

        private void _calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_calendar.SelectedDate.HasValue)
            {
                listRecommendations.ItemsSource = recommendationsViewModel.GetAllRecommendatedItems(_calendar.SelectedDate.Value);
            }
            recommendationsViewModel.Date = _calendar.SelectedDate.Value.Date;
        }
        private void LoadDataInThread()
        {

            recommendationsViewModel = new RecommendationsViewModel();
            Thread.Sleep(5000);
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.DataContext = recommendationsViewModel;

            }), DispatcherPriority.Background);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //List<object[]> items = new List<object[]>();
            //foreach (RecommendatedItems item in recommendationsViewModel.AllRecommendatedItems)
            //{
            //    if (item.IsChecked == true)
            //    {
            //        items.Add(new object[] {
            //            new BarCodeToProductName().Convert(item.BarCode, null, null, CultureInfo.CurrentCulture),
            //            new BarCodeToDescription().Convert(item.BarCode, null, null, CultureInfo.CurrentCulture),
            //            new StoreIdToStoreCityStoreName().Convert(item.StoreId, null, null, CultureInfo.CurrentCulture),
            //            item.CheapUnitPrice.ToString()});
            //    }
            //}
            //recommendationsViewModel.CreatePDF2(items);

        }

    }
}

        

