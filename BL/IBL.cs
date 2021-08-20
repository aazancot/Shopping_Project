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
        // plus tard a rajouter PDF, l'alogorithme pour amlatsa kniyote, assocations rules
        // et recuperer les products de chaque magasin 

       // une liste des annees ou il a fait des achats reeleemt 

        List<BE.ProductOrder> SaveNewProductOrdersFromQrCode();
        void LoadNewObjects(BE.QrCode qR);
        string decryptUrlToDetailsFromQrCode(string downloadUrl);

        List<BE.ProductOrder> GetAllProductOrders(Func<BE.ProductOrder, bool> predicate = null);

        List<int> GetAllOrderYears();
        double GetPriceWithCount(BE.ProductOrder productOrder);
        List<List<BE.ProductOrder>> GetAllProductOrdersByDateByStore(List<BE.ProductOrder> allProductOrders);
        void DeleteProductOrder(int productOrderId);
        List<BE.Product> GetAllProducts(Func<BE.Product, bool> predicate = null);

        List<BE.Store> GetAllStores(Func<BE.Store, bool> predicate = null);

        void AddProductOrder(BE.ProductOrder newProductOrder);
        void UpdateProductOrder(BE.ProductOrder updateProductOrder);
        void UpdateProduct(BE.Product updateProduct) ;

        BE.Product GetProduct(Func<BE.Product, bool> predicate = null);

        List<BE.Category> GetAllCategories();

        AssociationRule<BE.Product>[] GetAssociationRules();

        void CreatePDF(List<object[]> items);



    }
   


}
