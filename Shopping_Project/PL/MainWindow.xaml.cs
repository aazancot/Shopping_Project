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
using PL.Purchases_Validation;
using PL.Orders_History; 
using PL.Analytics;
using PL.Recommendations;
using PL.Settings;
using PL.Test;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PurchasesValidationView purchasesValidationView;
        private OrdersHistoryView ordersHistoryView;
        // private AnalyticsByItemsView analyticsView;

        private TestUC analyticsView;
        private RecommendationsView recommendationsView;
        private SettingsView settingsView;

        public MainWindow()
        {
            InitializeComponent();
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonPurchasesValidation(object sender, RoutedEventArgs e)
        {
            if(!(MainUC.Content is PurchasesValidationView))
            {
                purchasesValidationView = new PurchasesValidationView();

            }
            MainUC.Content = purchasesValidationView;
        }

        private void ButtonOrdersHistory(object sender, RoutedEventArgs e)
        {
            if (!(MainUC.Content is OrdersHistoryView))
            {
                ordersHistoryView = new OrdersHistoryView();

            }
            MainUC.Content = ordersHistoryView;
        }

        private void ButtonAnalytics(object sender, RoutedEventArgs e)
        {
            //if (!(MainUC.Content is AnalyticsByItemsView))
            //{
            //    analyticsView = new AnalyticsByItemsView();

            //}
            //MainUC.Content = analyticsView;

            if (!(MainUC.Content is TestUC))
            {
                analyticsView = new TestUC();

            }
            MainUC.Content = analyticsView;
        }

        private void ButtonRecommendations(object sender, RoutedEventArgs e)
        {
            if (!(MainUC.Content is RecommendationsView))
            {
                recommendationsView = new RecommendationsView();

            }
            MainUC.Content = purchasesValidationView; // a changer
        }

        private void ButtonSettings(object sender, RoutedEventArgs e)
        {
            if (!(MainUC.Content is SettingsView))
            {
                settingsView = new SettingsView();

            }
            MainUC.Content = purchasesValidationView; // a changer
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem m = sender as MenuItem;
            if(m.Header.ToString() == "Products Statistics")
            {
                if (!(MainUC.Content is TestUC))
                {
                    analyticsView = new TestUC();

                }
                MainUC.Content = analyticsView;
            }
         
        }
    }
}
