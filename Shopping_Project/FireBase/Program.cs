using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BE;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using Firebase.Storage;
using ZXing;
using DAL;
using BL;

namespace FireBase
{

    /// <A FAIRE>
    ///  1. voir comment associer un @ devant le downloadUrl . le downloadUrl etant un string , on veut pouvoir rajouter le @ devant sans prob.
    ///  2. Pour afficher la date sans les heures, il faut faire .Date.ToShortDateString() ca renvoit un string
    /// </summary>
    /// 
    class Program
    { 
        static void Main(string[] args)
        {
            //IDAL dal = new DALImp();
            //LoadNewQrCodeFirebase(dal);

            IBL bl = new BLImp();
            BE.ProductOrder P = bl.GetAllProductOrders().FirstOrDefault();
            P.ProductOrderValidation = false;
            bl.UpdateProductOrder(P);
           // bl.SaveNewProductOrdersFromQrCode();
            Console.ReadLine();





        }

        // mettre des photos de qr code generator dans le firebase 
        public async static Task LoadNewQrCodeFirebase(IDAL dal)
        {
            var stream = File.Open(@"C:\Shopping_Project\qr_code\0001.png", FileMode.Open);

            // Construct FirebaseStorage with path to where you want to upload the file and put it there
            var task = new FirebaseStorage("anna-avital-project-2021.appspot.com")
             .Child("0001.png")
             .PutAsync(stream);

            // Track progress of the upload
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            // Await the task to wait until upload is completed and get the download url
            var downloadUrl = await task;

            Console.WriteLine(downloadUrl);
            BE.Url newUrl = new BE.Url { DownloadUrl = downloadUrl };


          
            //envoie l'url de la photo au DAL pour le sauvegarder dans le DB
            try
            {
                dal.SaveUrlToDB(newUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


       

        // dechiffrer le qr code et tranformer le string qu on a dechiffre en qr code object
        private static void showDetails(string downloadUrl)
        {
            // DAL.ShoppingDB db = new DAL.ShoppingDB();
            //List<QrCode> QrCodes = new List<QrCode>();

            string imageUrl = downloadUrl;
            // Install-Package ZXing.Net -Version 0.16.5
            var client = new WebClient();
            var stream = client.OpenRead(imageUrl);
            if (stream == null) return;
            var bitmap = new Bitmap(stream);


            IBarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            //Console.WriteLine(result.Text)
            QrCode qR = JsonConvert.DeserializeObject<QrCode>(result.Text); // tranformation en qr code object 

            //var entity = db.QrCodes.Add(qR);
            //db.SaveChanges()

        }
        public static void SupprimerObjets()
        {
            IDAL dal = new DALImp();
            dal.ClearAllData();

        }
    }
}

