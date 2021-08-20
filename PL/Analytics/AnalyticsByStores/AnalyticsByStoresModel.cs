using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.Analytics.AnalyticsByStores
{
    class AnalyticsByStoresModel
    {
        IBL BL;

        public AnalyticsByStoresModel()
        {
            BL = new BLImp();
        }

        public List<string> GetAllStoresName()
        {
            return BL.GetAllStores().Select(x => x.StoreName).OrderBy(x=>x).Distinct().ToList();
        }

        public List<int> GetAllStoreIdByName(string storeName)
        {
            return BL.GetAllStores(x => x.StoreName == storeName).Select(x => x.StoreId).ToList();
        }

        public string GetStoreCityByStoreId(int storeId)
        {
            return BL.GetAllStores(x => x.StoreId == storeId).Select(x => x.StoreCity).FirstOrDefault();

        }
        public List<BE.ProductOrder> GetAllProductOrders(Func<BE.ProductOrder, bool> predicate = null)
        {
            return BL.GetAllProductOrders(predicate);
        }
    }
}

