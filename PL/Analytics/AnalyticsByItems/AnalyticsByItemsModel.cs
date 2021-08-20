using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;

namespace PL.Analytics.AnalyticsByItems
{
    public class AnalyticsByItemsModel
    {
        IBL BL;

        public AnalyticsByItemsModel()
        {
            BL = new BLImp();
        }


        // reucperer les produits valider, puis on choicit ceux de la categories, on selectionne les bar codes et on fait distinct car on veut un seul representant par produit
        public List<string> GetAllProductsNameByCategory(string categoryName)
        {
            List<string> newList = new List<string>();
            List<int> barCodeList = BL.GetAllProductOrders(x => x.ProductOrderValidation == true && x.Category.ToString() == categoryName).Select(x => x.BarCode).Distinct().ToList();
            foreach (var bc in barCodeList)
            {
                newList.Add((BL.GetProduct(x => x.BarCode == bc)).ProductName);
            }
            //return newList;
            List<string> noDuplicates = newList.OrderBy(x => x).Distinct().ToList();
            return noDuplicates;
        }

        public List<BE.ProductOrder> GetAllProductOrders(Func<BE.ProductOrder, bool> predicate = null)
        {
            return BL.GetAllProductOrders(predicate);
        }

        public List<int> GetAllBarCodeByName(string productName)
        {
            return BL.GetAllProducts(x => x.ProductName == productName).Select(x => x.BarCode).ToList();
        }
    }
}
