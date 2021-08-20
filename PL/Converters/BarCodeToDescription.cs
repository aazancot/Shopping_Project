using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BL;

namespace PL.Converters
{
    class BarCodeToDescription : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int barCode = int.Parse(value.ToString());
            if (barCode == 0)
                return null;
            string description = new BL.BLImp().GetAllProducts().Where(x => x.BarCode == barCode).FirstOrDefault().Description;
            return description == null ? null : description;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
