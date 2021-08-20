using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE; 

namespace PL.Test
{
    
 
    public class TestModel
    {  
        IBL BL;

        public TestModel()
        {
            BL = new BLImp();
        }


        // reucperer les produits valider, puis on choicit ceux de la categories, on selectionne les bar codes et on fait distinct car on veut un seul representant par produit
        public List<BE.Product> GetAllBarCodesByCategory(string categoryName)
        {
            List<Product> newList = new List<Product>();
            List<int> barCodeList = BL.GetAllProductOrders(x => x.ProductOrderValidation == true && x.Category.ToString() == categoryName).Select(x => x.BarCode).Distinct().ToList();
            foreach (var bc in barCodeList)
            {
                newList.Add(BL.GetProduct(x => x.BarCode == bc));
            }
            return newList;

        }

        public List<BE.ProductOrder> GetAllProductOrders(Func<BE.ProductOrder, bool> predicate = null)
        {
            return BL.GetAllProductOrders(predicate);
        }
    }
}
