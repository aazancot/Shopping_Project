using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Converters
{
     class StoreIdToStoreCityStoreName : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int storeId = int.Parse(value.ToString());
            if (storeId == 0)
                return null;
            string storeCity = new BL.BLImp().GetAllStores().Where(x => x.StoreId == storeId).FirstOrDefault().StoreCity;
            string storeName = new BL.BLImp().GetAllStores().Where(x => x.StoreId == storeId).FirstOrDefault().StoreName;
            if (storeCity == null && storeName == null)
                return null;
            else
                return storeName + " | " +storeCity;
                //return ("{0}  {1}", storeName, storeCity);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}


