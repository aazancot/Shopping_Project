using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALImp : IDAL
    {

        public DALImp () { }

        #region Url
        // On s'occupe des Url des images QRCode qui ont ete sauvegardees dans le FireBase. Ces Urls vont etre enregistrer
        // dans le DB pour etre appeler et transformer en objet QrCode dans BL puis sauvegarder en objet correspondant dans le DB.
        public void SaveUrlToDB(BE.Url url)
        {
            using (var ctx = new ShoppingDB())
            {
                ctx.DownloadUrls.Add(url);
                ctx.SaveChanges();
            }

        }

        public void DeleteAllUrlsFromDB()
        {
            using (var ctx = new ShoppingDB())
            {
                foreach (var url in ctx.DownloadUrls)
                {
                    ctx.DownloadUrls.Remove(url);
                    
                }
                ctx.SaveChanges();
            }

        }

        public List<BE.Url> GetAllUrlsFromDB()
        {
            using (var ctx = new ShoppingDB())
            {
                return ctx.DownloadUrls.ToList();
            }
        }

        #endregion Url

        #region Product

        public void AddProductToDB(BE.Product newProduct)
        {
            using (var ctx = new ShoppingDB())
            {
                ctx.Products.Add(newProduct);
                ctx.SaveChanges();
            }

        }

        public BE.Product GetProduct(Func<BE.Product,bool> predicate=null)
        {
            using (var ctx = new ShoppingDB())
            {
                return ctx.Products.Where(predicate).FirstOrDefault();

            }

        }

        public List<BE.Product> GetAllProducts(Func<BE.Product,bool> predicate = null)
        {
            using (var ctx = new ShoppingDB())
            {
                if (predicate == null)
                {
                    return ctx.Products.ToList();
                }
                else
                {

                    return ctx.Products.Where(predicate).ToList();
                }
            }
        }

        public void UpdateProduct(BE.Product updateProduct)
        {
            using (var ctx = new ShoppingDB())
            {
                ctx.Products.AddOrUpdate(updateProduct);
                ctx.SaveChanges();
            }

        }



        #endregion Product 

        #region Store
        public void AddStoretToDB(BE.Store newStore)
        {
            using (var ctx = new ShoppingDB())
            {
                ctx.Stores.Add(newStore);
                ctx.SaveChanges();
            }

        }
        public BE.Store GetStore(Func<BE.Store, bool> predicate = null)
        {
            using (var ctx = new ShoppingDB())
            {
                return ctx.Stores.Where(predicate).FirstOrDefault();
            }

        }

        public List<BE.Store> GetAllStores()
        {
            using (var ctx = new ShoppingDB())
            {
                return ctx.Stores.ToList();
                
            }

        }
        #endregion Store 

        #region ProductOrder
        public void AddProductOrderToDB(BE.ProductOrder newProductOrder)
        {
            using (var ctx = new ShoppingDB())
            {
                ctx.ProductOrders.Add(newProductOrder);
                ctx.SaveChanges();
            }

        }

        public void DeleteProductOrder(BE.ProductOrder productOrder)
        {
            using (var ctx = new ShoppingDB())
            {
                ctx.ProductOrders.Remove(productOrder);
                ctx.SaveChanges();
            }
          
        }

        // updateProductOrder.ProductOrderValidation = true; A mettre true pour validate 
        public void UpdateProductOrder(BE.ProductOrder updateProductOrder)
        {
            using (var ctx = new ShoppingDB())
            {
                ctx.ProductOrders.AddOrUpdate(updateProductOrder);
                ctx.SaveChanges();
            }

        }


        public List<BE.ProductOrder> GetProductOrders(Func<BE.ProductOrder, bool> predicate = null)
        {
         
            using (var ctx = new ShoppingDB())
            {
                if (predicate == null)
                {
                    return ctx.ProductOrders.ToList();
                }
                else
                {
                   
                   return ctx.ProductOrders.Where(predicate).ToList();
                }  
            }
        }


        #endregion ProductOrder




        public void ClearAllData()
        {

            using (var ctx = new ShoppingDB())
            {
               

                foreach (var entity in ctx.ProductOrders)
                    ctx.ProductOrders.Remove(entity);

                foreach (var entity in ctx.Products)
                    ctx.Products.Remove(entity);

                foreach (var entity in ctx.Stores)
                    ctx.Stores.Remove(entity);

                foreach (var entity in ctx.QrCodes)
                    ctx.QrCodes.Remove(entity);

                foreach (var entity in ctx.DownloadUrls)
                    ctx.DownloadUrls.Remove(entity);

                ctx.SaveChanges();
            }

      

        }
    }
}
