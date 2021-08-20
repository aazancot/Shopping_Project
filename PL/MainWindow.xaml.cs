
using System.Windows;
using System.Windows.Controls;
using PL.Purchases_Validation;
using PL.Orders_History; 
using PL.Analytics.AnalyticsByItems;
using PL.Analytics.AnalyticsByStores;
using PL.Analytics.AnalyticsByTotal;
using PL.Analytics.AnalyticsByCategory;
using PL.Recommendations;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PurchasesValidationView purchasesValidationView;
        private OrdersHistoryView ordersHistoryView;
        private AnalyticsByItemsView analyticsByItemsView;
        private AnalyticsByStoresView analyticsByStoresView;
        private AnalyticsByTotalView analyticsByTotalView;
        private AnalyticsByCategoryView analyticsByCategoryView;
        private RecommendationsView recommendationsView;
 

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


        private void ButtonRecommendations(object sender, RoutedEventArgs e)
        {
            if (!(MainUC.Content is RecommendationsView))
            {
                recommendationsView = new RecommendationsView();

            }
            MainUC.Content = recommendationsView; 
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem m = sender as MenuItem;
            if(m.Header.ToString() == "Products Statistics")
            {
                if (!(MainUC.Content is AnalyticsByItemsView))
                {
                    analyticsByItemsView = new AnalyticsByItemsView();

                }
                MainUC.Content = analyticsByItemsView;
            }
            else if(m.Header.ToString() == "Stores Statistics")
            {
                if (!(MainUC.Content is AnalyticsByStoresView))
                {
                    analyticsByStoresView = new AnalyticsByStoresView();

                }
                MainUC.Content = analyticsByStoresView;
            }
            else if (m.Header.ToString() == "Purchase Costs Statistics")
            {
                if (!(MainUC.Content is AnalyticsByTotalView))
                {
                    analyticsByTotalView = new AnalyticsByTotalView();

                }
                MainUC.Content = analyticsByTotalView;

            }
            else if (m.Header.ToString() == "Categories Statistics")
            {
                if (!(MainUC.Content is AnalyticsByCategoryView))
                {
                    analyticsByCategoryView = new AnalyticsByCategoryView();

                }
                MainUC.Content = analyticsByCategoryView;
            }

        }
       
    }
}
