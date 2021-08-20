using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;

namespace PL.Analytics.AnalyticsByTotal
{
    class AnalyticsByTotalModel
    {
        IBL BL;

        public AnalyticsByTotalModel()
        {
            BL = new BLImp();
        }

        public List<BE.ProductOrder> GetAllProductOrders(Func<BE.ProductOrder, bool> predicate = null)
        {
            return BL.GetAllProductOrders(predicate);
        }


    }
}
