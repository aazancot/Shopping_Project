using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;


namespace PL.Purchases_Validation
{
    public class PurchasesValidationModel
    {
        IBL BL;

        public PurchasesValidationModel()
        {
            BL = new BLImp();
        }

        public List<ProductOrder> SaveNewProductOrdersFromQrCode()
        {
            return BL.SaveNewProductOrdersFromQrCode();
        }

        public void UpdateProductOrder(ProductOrder newProductOrder)
        {
   
            BL.UpdateProductOrder(newProductOrder);
        }

        public void DeleteProductOrder(BE.ProductOrder productOrder)
        {
            BL.DeleteProductOrder(productOrder);
        }

        public BE.Product GetProduct(Func<BE.Product, bool> predicate = null)
        {
            return BL.GetProduct(predicate);
        }

        public void UpdateProduct(Product newProduct)
        {

            BL.UpdateProduct(newProduct);
        }




    }
}
