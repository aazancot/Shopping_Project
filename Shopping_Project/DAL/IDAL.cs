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

        // plus tard a rejouter pour faire PDF 

        
        void SaveUrlToDB(BE.Url url);
        void DeleteAllUrlsFromDB();
        List<BE.Url> GetAllUrlsFromDB();

        void AddProductToDB(BE.Product newProduct);
        BE.Product GetProduct(Func<BE.Product, bool> predicate = null);

        void UpdateProduct(BE.Product updateProduct);

        List<BE.Product> GetAllProducts(Func<BE.Product, bool> predicate = null);
        void AddStoretToDB(BE.Store newStore);

        BE.Store GetStore(Func<BE.Store, bool> predicate = null);

        List<BE.Store> GetAllStores();

        void AddProductOrderToDB(BE.ProductOrder newProductOrder);

        void DeleteProductOrder(BE.ProductOrder productOrder);

        void UpdateProductOrder(BE.ProductOrder updateProductOrder);

        List<BE.ProductOrder> GetProductOrders(Func<BE.ProductOrder, bool> predicate = null);


        void ClearAllData();
    }

}
