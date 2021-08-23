using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            AllOrders = new ObservableCollection<Order>(GetAllOrders(null));

        }

        // logique qui permet de passer d'une liste de liste de PO a une liste de la classe Order
        public List<Order> GetAllOrders(Nullable <DateTime> selectedDate)
        {
            List<Order> AllOrders = new List<Order>();
            List<List<ProductOrder>> po = OrdersHistoryModel.GetAllProductOrdersByDateByStore();
            int i = 0;
            foreach (var item in po)
            {
                Order newOrder = new Order();
                newOrder.productOrders = po[i];
                newOrder.Date = po[i][0].Date;
                newOrder.StoreId = po[i][0].StoreId;
                AllOrders.Add(newOrder);
                i++;
            }

            List<Order> orderedList =  AllOrders.OrderByDescending(x=>x.Date).ToList();
            if(selectedDate == null)
            {
                List<Order> orderedListLastMonth = orderedList.Where(x=>x.Date.Month==DateTime.Now.Month).Select(x=>x).ToList();
                return orderedListLastMonth;
            }
            else
            {
                List<Order> orderedListByDate = orderedList.Where(x => x.Date == selectedDate).Select(x => x).ToList();
               return orderedListByDate;
                
            }
        }
      
    }
}
            
              
        
   



    

