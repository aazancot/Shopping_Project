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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Effects;
using BE;

namespace PL.Test
{
    /// <summary>
    /// Interaction logic for TestUC.xaml
    /// </summary>
    public partial class TestUC : UserControl
    {
        TestViewModel testViewModel;
        public TestUC()
        {
            InitializeComponent();
            testViewModel = new TestViewModel();
            DataContext = testViewModel;

            // ----------------------------------------CODE CAMEMBERT2 -------------------------------------------------------
            SfChart3D chart = new SfChart3D();
            chart.Name = "chart1";
            // add border to the header 
            Border border = new Border()
            {
                BorderThickness = new Thickness(0.5),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(10),
                CornerRadius = new CornerRadius(5)
            };
            TextBlock textBlock = new TextBlock()
            {
                Text = "Expenses by Category",
                Margin = new Thickness(5),
                FontSize = 15,
                FontWeight = FontWeight.FromOpenTypeWeight(700)
            };
            textBlock.Effect = new DropShadowEffect()
            {
                Color = Colors.Black,
                Opacity = 0.5
            };
            border.Child = textBlock; // dans border c'est le textblock 
            chart.Header = border; // le header du chart c'est le border


            //chart.Height = 500;
            //chart.Width = 500;
            chart.RenderSize = new Size(100, 100);

            ChartLegend legend = new ChartLegend();
            legend.FontSize = 10;// taille ecriture de la legend
            legend.FontWeight = FontWeight.FromOpenTypeWeight(700); //bold
            legend.IconHeight = 10; // taille des diamants de la legende
            legend.IconWidth = 10;
            chart.Legend = legend;  // on insert l'objet legend qu'on a creer 

            chart.EnableRotation = false; // permission de le bouger ou non 
            chart.Tilt = -30; // angle 
            chart.Rotation = 30;// angle incline 
            chart.Depth = 30; // epaisseur du camembert
            chart.PerspectiveAngle = 90;
            chart.FontWeight = FontWeight.FromOpenTypeWeight(700);

           
            


            PieSeries3D series = new PieSeries3D();

            series.Name = "PieChart";
            series.XBindingPath = "LabelX";
            series.YBindingPath = "LabelY";
            series.ShowTooltip = true; // pour qu'on voit le chiffre quand on place la souris dessus 
            Product p = new Product { BarCode = 1 };
            DateTime d = DateTime.Parse("01/02/2021");
           
            series.ItemsSource = testViewModel.getQuantityPerWeeksCollection(p,d );
            
            series.Palette = ChartColorPalette.FloraHues;// couleur 

            series.AdornmentsInfo = new ChartAdornmentInfo3D()
            {
                ShowLabel = true, // pour qu'on voit les chiffres 
                ShowConnectorLine = true,
                // calcul : x value / total = percentage for each category 
                SegmentLabelContent = LabelContent.Percentage,
                ConnectorHeight = 80,
                LabelTemplate = this.Resources["DataLabelTemplate"] as DataTemplate,
            };
        
            chart.Series.Add(series);
            //BorderG3.Child = chart;

            // PieGrid.Children.Add(chart);
            //Content = chart;
            // BorderG3.Child = chart;
            //this.Content = BorderG3;


            //Button b = new Button();
            //b.Visibility = Visibility.Visible;
            //b.Background = new SolidColorBrush(Colors.Black);
            //b.Name = "AA";
            //PieGrid.Children.Add(b);


        }




    private void _calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
         
            if (_calendar.DisplayMode == CalendarMode.Month)
            {

                DateTime date = _calendar.DisplayDate.Date;
                _calendar.DisplayMode = CalendarMode.Year;
       
                testViewModel.Date = date;
         
              
                if (testViewModel.SelectedProduct != null)
                {

                    chartHistogram.ItemsSource = testViewModel.getQuantityPerMonthCollection(testViewModel.SelectedProduct, testViewModel.Date);
                    chartLineSeries.ItemsSource = testViewModel.getQuantityPerDayCollection(testViewModel.SelectedProduct, testViewModel.Date);
                    //chartPieSeries.ItemsSource = testViewModel.getQuantityPerWeeksCollection(testViewModel.SelectedProduct, testViewModel.Date);
                    amountPerWeeks.ItemsSource = testViewModel.getQuantityPerWeeksCollection(testViewModel.SelectedProduct, testViewModel.Date);
                    ObservableCollection<ChartModel> liste = testViewModel.getQuantityPerWeeksCollection(testViewModel.SelectedProduct, testViewModel.Date);
                    foreach( var item in liste)
                    {
                        MessageBox.Show(item.LabelX, item.LabelY.ToString());
                    }

                    //ObservableCollection<ChartModel> list = CategoryWeeks(testViewModel.SelectedProduct, testViewModel.Date);
                    //foreach (var item in list)
                    //{
                    //    MessageBox.Show(item.LabelX, item.LabelY.ToString());
                    //}

                }
                
            }
        }



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

         
            Product p = (sender as MenuItem).DataContext as Product;
            testViewModel.SelectedProduct = p;
            //chartHistogram.ItemsSource = testViewModel.getQuantityPerMonthCollection(testViewModel.SelectedProduct, testViewModel.Date);
            chartHistogram.ItemsSource = testViewModel.getQuantityPerWeeksCollection(testViewModel.SelectedProduct, testViewModel.Date);
            chartLineSeries.ItemsSource = testViewModel.getQuantityPerDayCollection(testViewModel.SelectedProduct, testViewModel.Date);
            amountPerWeeks.ItemsSource = testViewModel.getQuantityPerWeeksCollection(testViewModel.SelectedProduct, testViewModel.Date);
            //chartPieSeries.ItemsSource = testViewModel.getQuantityPerWeeksCollection(testViewModel.SelectedProduct, testViewModel.Date);

            //testViewModel.series1 = testViewModel.getQuantityPerMonthCollection(testViewModel.SelectedProduct, testViewModel.Date);
            //testViewModel.series2 = testViewModel.getQuantityPerDayCollection(testViewModel.SelectedProduct, testViewModel.Date);

        }
    }
}
