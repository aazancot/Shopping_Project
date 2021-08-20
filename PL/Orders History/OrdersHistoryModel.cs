using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;

namespace PL.Orders_History
{
    public class OrdersHistoryModel
    {
        IBL BL;

        public OrdersHistoryModel()
        {
            BL = new BLImp();
        }

        public List<List<BE.ProductOrder>> GetAllProductOrdersByDateByStore()
        {
            return BL.GetAllProductOrdersByDateByStore(BL.GetAllProductOrders(x=>x.ProductOrderValidation==true));
        }

    }

    public class Order
    {
        public int StoreId { get; set; }
        public DateTime Date { get; set; }

        // list de tous les product orders qui se sont fait a cette date et dans ce magasin,  ca represente une order 
        public List<ProductOrder> productOrders { get; set; }

    }

}
