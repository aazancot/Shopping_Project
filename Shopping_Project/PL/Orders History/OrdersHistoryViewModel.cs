using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace PL.Orders_History
{
    public class OrdersHistoryViewModel
    {
        public OrdersHistoryModel OrdersHistoryModel { get; set; }
        public ObservableCollection<Order> AllOrders { get; set; }

        public OrdersHistoryViewModel()
        {
            OrdersHistoryModel = new OrdersHistoryModel();
            AllOrders = new ObservableCollection<Order>(GetAllOrders());

        }

        // logique qui permet de passer d'une liste de liste de PO a une liste de la classe Order
        public List<Order> GetAllOrders()
        {
            List<Order> AllOrders = new List<Order>();
            List<List<ProductOrder>> po = OrdersHistoryModel.GetAllProductOrdersByDateByStore();
            foreach (var item in po)
            {
                Order newOrder = new Order();
                newOrder.productOrders = po[0];
                newOrder.Date = po[0][0].Date;
                newOrder.StoreId = po[0][0].StoreId;
                AllOrders.Add(newOrder);

            }
            return AllOrders;
        }
    }
}
            
              
        
   



    

