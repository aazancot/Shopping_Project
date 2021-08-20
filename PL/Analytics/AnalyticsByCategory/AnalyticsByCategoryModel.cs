using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;


namespace PL.Analytics.AnalyticsByCategory
{
    class AnalyticsByCategoryModel
    {
        IBL BL;

        public AnalyticsByCategoryModel()
        {
            BL = new BLImp();
        }

        public List<string> GetAllCategoriesName()
        {
            return BL.GetAllCategories().Select(x => x.ToString()).ToList();
        }
        public List<BE.ProductOrder> GetAllProductOrders(Func<BE.ProductOrder, bool> predicate = null)
        {
            return BL.GetAllProductOrders(predicate);
        }
    }
}

