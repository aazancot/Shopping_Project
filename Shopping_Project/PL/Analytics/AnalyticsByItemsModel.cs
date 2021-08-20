using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;

namespace PL.Analytics
{
    public class AnalyticsByItemsModel
    {

        IBL BL;

        public AnalyticsByItemsModel()
        {
            BL = new BLImp();
        }


        // reucperer les produits valider, puis on choicit ceux de la categories, on selectionne les bar codes et on fait distinct car on veut un seul representant par produit
        public List<BE.Product> GetAllBarCodesByCategory(string categoryName)
        {
            List<Product> newList = new List<Product>();
           List<int> barCodeList = BL.GetAllProductOrders(x => x.ProductOrderValidation == true && x.Category.ToString()==categoryName).Select(x => x.BarCode).Distinct().ToList();
            foreach( var bc in barCodeList)
            {
                 newList.Add(BL.GetProduct(x => x.BarCode == bc));
            }
            return newList;

        }

      public List<BE.ProductOrder> GetAllProductOrders(Func<BE.ProductOrder, bool> predicate = null)
        {
            return BL.GetAllProductOrders(predicate);
        }

        public List<ChartLineSeries3DModel> GetAllLabels()
        {
            List<ChartLineSeries3DModel> HistogramList = new List<ChartLineSeries3DModel>();
            HistogramList.Add(new ChartLineSeries3DModel { Month = "year1", Amount = 1.2 });
            HistogramList.Add(new ChartLineSeries3DModel { Month = "year2", Amount = 10.5 });
            HistogramList.Add(new ChartLineSeries3DModel { Month = "year3", Amount = 1.6 });
            HistogramList.Add(new ChartLineSeries3DModel { Month = "year4", Amount = 15.2 });
            HistogramList.Add(new ChartLineSeries3DModel { Month = "year5", Amount = 50.3 });
            HistogramList.Add(new ChartLineSeries3DModel { Month = "year6", Amount = 30.2 });
            return HistogramList;
        }

    }
}
