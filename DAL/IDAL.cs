using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
    {

        #region Url
        void SaveUrlToDB(BE.Url url);
        void DeleteAllUrlsFromDB();
        List<BE.Url> GetAllUrlsFromDB();

        #endregion Url

        #region Product
        void AddProductToDB(BE.Product newProduct);
        BE.Product GetProduct(Func<BE.Product, bool> predicate = null);

        void UpdateProduct(BE.Product updateProduct);

        List<BE.Product> GetAllProducts(Func<BE.Product, bool> predicate = null);

        #endregion Product

        #region Store
        void AddStoretToDB(BE.Store newStore);

        BE.Store GetStore(Func<BE.Store, bool> predicate = null);

        List<BE.Store> GetAllStores(Func<BE.Store, bool> predicate = null);

        #endregion Store

        #region ProductOrder
        void AddProductOrderToDB(BE.ProductOrder newProductOrder);

        void DeleteProductOrder(int productOrderId);
        void UpdateProductOrder(BE.ProductOrder updateProductOrder);

        List<BE.ProductOrder> GetProductOrders(Func<BE.ProductOrder, bool> predicate = null);

        #endregion ProductOrder

        #region Clear
        void ClearAllData();

        #endregion Clear

        #region PDF
        void CreatePDF(List<object[]> items);
        
        #endregion PDF
    }

}
