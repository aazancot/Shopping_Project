using Accord.MachineLearning.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBL
    {
        #region UrlToObject

        List<BE.ProductOrder> SaveNewProductOrdersFromQrCode();
        void LoadNewObjects(BE.QrCode qR);
        string decryptUrlToDetailsFromQrCode(string downloadUrl);

        #endregion UrlToObject

        #region ProductOrder
        List<BE.ProductOrder> GetAllProductOrders(Func<BE.ProductOrder, bool> predicate = null);
        List<int> GetAllOrderYears();
        double GetPriceWithCount(BE.ProductOrder productOrder);
        List<List<BE.ProductOrder>> GetAllProductOrdersByDateByStore(List<BE.ProductOrder> allProductOrders);
        void DeleteProductOrder(int productOrderId);
        void AddProductOrder(BE.ProductOrder newProductOrder);
        void UpdateProductOrder(BE.ProductOrder updateProductOrder);

        #endregion ProductOrder

        #region Product
        List<BE.Product> GetAllProducts(Func<BE.Product, bool> predicate = null);
        void UpdateProduct(BE.Product updateProduct) ;
        BE.Product GetProduct(Func<BE.Product, bool> predicate = null);

        #endregion Product

        #region Store 
        List<BE.Store> GetAllStores(Func<BE.Store, bool> predicate = null);

        #endregion Store 

        #region Category 
        List<BE.Category> GetAllCategories();

        #endregion Category 

        #region Machine Learning 
        AssociationRule<BE.Product>[] GetAssociationRules();

        #endregion Machine Learning 

        #region PDF
        void CreatePDF(List<object[]> items);

        #endregion PDF

    }



}
