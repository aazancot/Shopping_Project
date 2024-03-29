﻿using BE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using DAL;

namespace BL
{
    public class BLImp : IBL
    {


        public IDAL dal { get; set; }
        public BLImp()
        {
            dal = new DALImp();
        }


        #region UrlToObject
        public List<BE.ProductOrder> SaveNewProductOrdersFromQrCode()
        {
            // On recupere tous les Url qui sont dans la DB. 
            List<string> allUrls = (dal.GetAllUrlsFromDB()).Select(x => x.DownloadUrl).ToList();
            string resultDetails;

            // on dechiffre un par un les url pour transformer en QrCode et construire les autres objets.
            foreach (string url in allUrls)
            {
                resultDetails = decryptUrlToDetailsFromQrCode(url);
                if (resultDetails != null)
                {
                    QrCode qR = JsonConvert.DeserializeObject<QrCode>(resultDetails); // tranformation en qr code object
                    LoadNewObjects(qR); //nouveaux objets
                }   
            }
            //on supprime tous les Url de la DB.
            dal.DeleteAllUrlsFromDB();

            // on recupere tous les nouveaux product order avec Validate= false 
            return GetAllProductOrders(x => x.ProductOrderValidation == false);
         
        }

        // du qrcode on construit des nouveaux oblets a partir des donnees qu'il y avait ds le qrcode
        public void LoadNewObjects(QrCode qR)
        {
            if ((dal.GetProduct(x=> x.BarCode ==qR.BarCode)) == null)
            {
                BE.Product newProduct = new BE.Product { BarCode = qR.BarCode, ProductName = qR.ProductName, Description = qR.Description };
                dal.AddProductToDB(newProduct);

            }
            if ((dal.GetStore(x => x.StoreName == qR.StoreName && x.StoreCity == qR.StoreCity)) == null)
            {
                BE.Store newStore = new BE.Store { StoreCity = qR.StoreCity, StoreName = qR.StoreName };
                dal.AddStoretToDB(newStore);
            }

            int storeId = (dal.GetStore(x => x.StoreName == qR.StoreName && x.StoreCity == qR.StoreCity)).StoreId;

            BE.ProductOrder newProductOrder = new BE.ProductOrder
            {
                BarCode = qR.BarCode,
                Count = qR.Count,
                Date = qR.Date.Date,
                UnitPrice = qR.UnitPrice,
                StoreId = storeId
            };
            dal.AddProductOrderToDB(newProductOrder);
        }

        // on sort le string avec les donnees du qrcode 
        // CA MARCHE
        public string decryptUrlToDetailsFromQrCode(string downloadUrl)
        { 

            string imageUrl = downloadUrl;
            // Install-Package ZXing.Net -Version 0.16.5
            var client = new WebClient();
            var stream = client.OpenRead(imageUrl);
            if (stream == null) return null;
            var bitmap = new Bitmap(stream);


            IBarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap); // result = string avec les details 
            return result.Text;

        }

        #endregion UrlToObject

        // 
        #region ProductOrder

        public double GetPriceWithCount(BE.ProductOrder productOrder)
        {
            return productOrder.Count * productOrder.UnitPrice;
        }

        public List<ProductOrder> GetAllProductOrders(Func<ProductOrder, bool> predicate = null)
        {
            return dal.GetProductOrders(predicate);
        }

        public List<List<BE.ProductOrder>> GetAllProductOrdersByDateByStore(List<BE.ProductOrder> allProductOrders)
        {
            var results = from p in allProductOrders
                          group p by new { p.Date, p.StoreId } into g
                          orderby g.Key.Date
                          select g.ToList();



                          //new { order = g.ToList() }; //idDate = g.Key.Date, idStore = g.Key.StoreId,

           return results.ToList();

        }

        public List<int> GetAllOrderYears()
        {
            return GetAllProductOrders().Select(x => x.Date.Year).Distinct().OrderBy(x => x).ToList();
  
        }

        #endregion ProductOrder

        public void AddProductOrder(BE.ProductOrder newProductOrder) => dal.AddProductOrderToDB(newProductOrder);
        public void DeleteProductOrder(BE.ProductOrder productOrder) => dal.DeleteProductOrder(productOrder);

        public void UpdateProductOrder(BE.ProductOrder updateProductOrder) => dal.UpdateProductOrder(updateProductOrder);



        #region Product 

        public BE.Product GetProduct(Func<BE.Product, bool> predicate = null)
        {
            return dal.GetProduct(predicate);
        }
        public void UpdateProduct(BE.Product updateProduct) => dal.UpdateProduct(updateProduct);

        // predicate pour filtrer par exemple tous les produits qui appartiennent a une certaine categories 
        public List<BE.Product> GetAllProducts(Func<BE.Product, bool> predicate = null) => dal.GetAllProducts(predicate);

        #endregion Product 

        #region Store 

        public List<BE.Store> GetAllStores() => dal.GetAllStores();

        #endregion Store 

      



    }
}
