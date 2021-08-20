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

namespace PL.Orders_History
{
    /// <summary>
    /// Interaction logic for OrderHistoryView.xaml
    /// </summary>
    public partial class OrdersHistoryView : UserControl
    {
        public OrdersHistoryViewModel ordersHistoryViewModel;
        public OrdersHistoryView()
        {
            InitializeComponent();
            ordersHistoryViewModel = new OrdersHistoryViewModel();
            DataContext = ordersHistoryViewModel;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = _datePicker.SelectedDate.Value;
            ListBox3.ItemsSource = ordersHistoryViewModel.GetAllOrders(selectedDate);
        }
    }
}
