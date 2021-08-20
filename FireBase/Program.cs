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
            IDAL dal = new DALImp();
            //LoadNewQrCodeFirebase(dal);



            //IBL bl = new BLImp();

            //BE.ProductOrder P = bl.GetAllProductOrders().FirstOrDefault();
            //P.ProductOrderValidation = false;
            //bl.UpdateProductOrder(P);
            // bl.SaveNewProductOrdersFromQrCode();



            List<BE.Product> listProducts = new List<BE.Product>()
            {
                // LEGUMES
                new Product { BarCode=1,ProductName= "Baby Cucumber", Description="By Kilo", ImageFile="Pictures/1.jpg" },
                new Product { BarCode=2,ProductName= "Cherry Tomatoes", Description="Per Pack", ImageFile="Pictures/2.jpg" },
                new Product { BarCode=3,ProductName= "Champignon Mushrooms White", Description="Per Pack", ImageFile="Pictures/3.jpg" },
                new Product { BarCode=4,ProductName= "Garlic Cloves", Description="Per Pack", ImageFile= "Pictures/4.jpg" },
                new Product { BarCode=5,ProductName= "Romaine Lettuce Heart", Description="Per Pack", ImageFile= "Pictures/5.jpg" },
                new Product { BarCode=6,ProductName= "Yellow Pepper", Description="By Kilo ", ImageFile= "Pictures/6.jpg"},
                new Product { BarCode=7,ProductName= "Red Pepper", Description="By Kilo", ImageFile= "Pictures/7.jpg" },



                // PRODUITS LAITIERS
                new Product { BarCode=8,ProductName= "Milk 3%", Description="Tnuva Carton 1.5L", ImageFile= "Pictures/8.jpg" }, //11.90
                new Product { BarCode=9,ProductName= "Yogurt Danone 0%", Description="Strauss Bio 200g ", ImageFile= "Pictures/9.jpg" }, // 5.9
                new Product { BarCode=10,ProductName= "French Butter", Description="President 200g", ImageFile= "Pictures/10.jpg" }, //21.90
                new Product { BarCode=11,ProductName= "Yogurt Danone 1.7%", Description="Strauss Bio 8x150g", ImageFile= "Pictures/11.jpg" }, //24.90
                new Product { BarCode=12,ProductName= "Oat Milk Chocolate Drink", Description="Tnuva 1L", ImageFile= "Pictures/12.jpg" }, //16.90
                new Product { BarCode=13,ProductName= "Choco Chocolate Milk Drink", Description="Yotvata 350 ml", ImageFile= "Pictures/13.jpg" }, //6.90
                new Product { BarCode=14,ProductName= "Vanilla Soy Milk", Description="Tnuva 1L", ImageFile= "Pictures/14.jpg" }, //14.90
                new Product { BarCode=15,ProductName= "Alternative Peach Soy Drink", Description="Tnuva Bio 250 ml", ImageFile="Pictures/15.jpg" }, // 8.90
                new Product { BarCode=16,ProductName= "Rice Drink", Description="Alpro 1L", ImageFile= "Pictures/16.jpg" }, // 16.90
                new Product { BarCode=17,ProductName= "Babybel Original Cheese", Description="Baby Babybel 5pcs", ImageFile= "Pictures/17.jpg" }, // 18.90


               //  HATIFIMS
                new Product { BarCode=18,ProductName= "Chocolate Covered Cornflakes", Description="Click 65g",ImageFile= "Pictures/18.jpg"}, //8.90
                new Product { BarCode=19,ProductName= "Thin Rosemary Crackers", Description="Fitness 140g", ImageFile= "Pictures/19.jpg" }, //16.90
                new Product { BarCode=20,ProductName= "Doritos Mexican Nacho Chips", Description="Corn Tortilla Chips Elit 70g", ImageFile= "Pictures/20.jpg" }, //5.90
                new Product { BarCode=21,ProductName= "Bamba Snacks Big Bag", Description="Ossem 200g", ImageFile= "Pictures/21.jpg"}, //12.90
                new Product { BarCode=22,ProductName= "Rice Crackers", Description="Ossem", ImageFile= "Pictures/22.jpg" },  //7.90
                new Product { BarCode=23,ProductName= "Cheetos Peanut", Description="Elit 80g", ImageFile= "Pictures/23.jpg"},
                new Product { BarCode=24,ProductName= "TapuChips", Description="Potato Chips Sour Cream Elit 200g", ImageFile= "Pictures/24.jpg" }, //14.90
                new Product { BarCode=25,ProductName= "Gold Crackers", Description="Ossem 150g", ImageFile= "Pictures/25.jpg" },//10.90
                new Product { BarCode=26,ProductName= "Chocolate Chip Cookies", Description="Merba 225g", ImageFile= "Pictures/26.jpg" }, //12.90
                new Product { BarCode=27,ProductName= "Cream Cereal Bars", Description="Fitness 6x22.5g", ImageFile= "Pictures/27.jpg"}, //19.90
                new Product { BarCode=28,ProductName= "Minis Cocoa Cream Cookies", Description="Hit 130g", ImageFile= "Pictures/28.jpg" }, //8.90
                new Product { BarCode=29,ProductName= "White Chocolate Coated Cookies", Description="Oreo 246g", ImageFile= "Pictures/29.jpg" }, //12.90
                new Product { BarCode=30,ProductName= "Long Stick Pretzels", Description="Ossem 400g", ImageFile= "Pictures/30.jpg" }, //14.90
                new Product { BarCode=31,ProductName= "Moroccan Cookies", Description="Dolecha Vita 600g", ImageFile= "Pictures/31.jpg"}, //22.90
                new Product { BarCode=32,ProductName= "Tapuhonim Apple Chips", Description="Green 6pcs", ImageFile= "Pictures/32.jpg" }, //28.90


                //CONFITURE
                new Product { BarCode=33,ProductName= "Classic Peanut Butter", Description="Skippy 462g", ImageFile= "Pictures/33.jpg" }, //24.90
                new Product { BarCode=34,ProductName= "Pure Honey", Description="Yad Mordechai 400g", ImageFile= "Pictures/34.jpg" }, //29.90
                new Product { BarCode=35,ProductName= "Nutella Chocolate Spread", Description="Ferrero Large 750g", ImageFile= "Pictures/35.jpg" }, //32.90
                new Product { BarCode=36,ProductName= "Rasperry Preserve Confiture", Description="Bonne Maman 370g", ImageFile= "Pictures/36.jpg" }, //21.90

                //CEREALES
                new Product { BarCode=37,ProductName= "Frosties Cereal Frosted Flakes", Description="Kellog's 750g", ImageFile= "Pictures/37.jpg" }, //26.90
                new Product { BarCode=38,ProductName= "Honey Nut Cheerios Cereal", Description="Nestle 625g", ImageFile= "Pictures/38.jpg" }, //31.90
                new Product { BarCode=39,ProductName= "Lion Choco Caramel Cereal", Description="Nestle 480g", ImageFile= "Pictures/39.jpg" }, //30.90

               // BOISSON + Eau
                new Product { BarCode=40,ProductName= "Organic Coconut Water", Description="Green Coco 1L", ImageFile= "Pictures/40.jpg" }, //20.90
                new Product { BarCode=41,ProductName= "Coca-Cola", Description="1.5L", ImageFile= "Pictures/41.jpg" }, //7.50
                new Product { BarCode=42,ProductName= "Coca-Cola Zero", Description="6x330 ml", ImageFile= "Pictures/42.jpg" }, //29.90
                new Product { BarCode=43,ProductName= "Pepsi Cola", Description="1.5l", ImageFile= "Pictures/43.jpg" }, //8.90
                new Product { BarCode=44,ProductName= "Fuze Tea Peach", Description="1.5L", ImageFile= "Pictures/44.jpg" }, //9.90
                new Product { BarCode=45,ProductName= "Carbonated Mineral Water", Description="Perrier 1L", ImageFile= "Pictures/45.jpg" }, //30.90
                new Product { BarCode=46,ProductName= "Neviot Water", Description="6x1.5L", ImageFile= "Pictures/46.jpg" }, // 15.90
                new Product { BarCode=47,ProductName= "San Benedetto Mineral Water", Description="6x500 ml", ImageFile= "Pictures/47.jpg" }, //18.90


               // POULET OEUFS
                new Product { BarCode=48,ProductName= "Dozen Eggs L-XL", Description="12pcs", ImageFile= "Pictures/48.jpg" }, //17.90
                new Product { BarCode=49,ProductName= "Organic Eggs M-L", Description="12pcs", ImageFile= "Pictures/49.jpg" }, //28.90
                new Product { BarCode=50,ProductName= "Mini Real Chicken Nuggets", Description="Mama 500g", ImageFile= "Pictures/50.jpg"}, //34.90
                new Product { BarCode=51,ProductName= "Fresh Grilled Chicken Breasts", Description="Of Tov 420g", ImageFile= "Pictures/51.jpg" }, //48.90
                new Product { BarCode=52,ProductName= "Sesame Chicken Schnitzel Fingers", Description="Of Tov 600g", ImageFile= "Pictures/52.jpg" }, //44.90
                new Product { BarCode=53,ProductName= "Real Chicken Shawarma", Description="Mama 500g", ImageFile= "Pictures/53.jpg" }, //39.90


                // CHARCUTERIE+ SAUSCISSE
                new Product { BarCode=54,ProductName="Smoked Turkey Breast", Description="Tirat Zvi 300g", ImageFile= "Pictures/54.jpg" }, //39.90
                new Product { BarCode=55,ProductName= "Mini Beef Kabanos", Description="Titat Zvi 120g", ImageFile= "Pictures/55.jpg"}, //14.90
                new Product { BarCode=56,ProductName= "Chicken Hot Dog Sausages", Description="Of Tov 400g", ImageFile= "Pictures/56.jpg" }, //19.90
                new Product { BarCode=57,ProductName= "Veal Sausages", Description="Tirat Zvi 500g", ImageFile= "Pictures/57.jpg" }, //33.90

                // VIANDEE
                new Product { BarCode=58,ProductName= "Kebab Greek", Description="Tibon Vil 400g", ImageFile= "Pictures/58.jpg" }, //29.90
                new Product { BarCode=59,ProductName= "Froozen Hamburger 100% Beef", Description="Tibon Vil 400g", ImageFile= "Pictures/59.jpg" }, //26.90

               // POISSON
                new Product { BarCode=60,ProductName= "Tuna In Water", Description="Star-Kist 4 cans", ImageFile= "Pictures/60.jpg" }, //15.90
                new Product { BarCode=61,ProductName= "Smoked Salmon", Description="Sterling 100g", ImageFile= "Pictures/61.jpg"}, //26.90
                new Product { BarCode=62,ProductName= "Froozen Red Tuna Steak", Description="Per 100g", ImageFile= "Pictures/62.jpg" }, //25.90
                new Product { BarCode=63,ProductName= "Sardine In Olive Oil", Description="Robert 120g", ImageFile= "Pictures/63.jpg" }, //10.90

                 //PATE+ COUSCOUS
                new Product { BarCode=64,ProductName= "Barilla Pasta Spaghetti", Description="600g", ImageFile= "Pictures/64.jpg" }, //9.90
                new Product { BarCode=65,ProductName= "Gluten Free Pasta Barilla Penne", Description="400g", ImageFile= "Pictures/65.jpg"}, //12.90
                new Product { BarCode=66,ProductName= "Couscous Medium", Description="Dari 500g", ImageFile= "Pictures/66.jpg" }, //20.90

               // RIZ
                new Product { BarCode=67,ProductName= "Classic Jasmin Rice", Description="Sugat 1Kg", ImageFile= "Pictures/67.jpg" }, //15.90
                new Product { BarCode=68,ProductName= "Organic Quinoa", Description="Tvuot 500g", ImageFile= "Pictures/68.jpg" }, //23.90
                new Product { BarCode=69,ProductName= "Organic Chickpeas", Description="Tvuot 500g", ImageFile= "Pictures/69.jpg" }, //18.90
                new Product { BarCode=70,ProductName= "Organic Green Split Peas", Description="Sugat 500g", ImageFile= "Pictures/70.jpg" }, //9.90
                new Product { BarCode=71,ProductName= "White Basmati Rice", Description="Daawat 1Kg", ImageFile= "Pictures/71.jpg" }, //16.90


               // PAIN
                new Product { BarCode=72,ProductName= "Gluten Free Pita Bread", Description="Green 450g", ImageFile= "Pictures/72.jpg" }, //24.90
                new Product { BarCode=73,ProductName= "Wheat Bread", Description="GreenLite No Sugar 550g", ImageFile= "Pictures/73.jpg" }, //24.90
                new Product { BarCode=74,ProductName= "Hot Dog Sausage Breads", Description="Berman 6pcs", ImageFile= "Pictures/74.jpg" }, //15.90
                new Product { BarCode=75,ProductName= "Tortillas Wrap ", Description="Small 365g", ImageFile= "Pictures/75.jpg" }, //13.90
                new Product { BarCode=76,ProductName= "Hallots", Description="Angel Medium", ImageFile= "Pictures/76.jpg" }, //14.90
                
                
               // SAUCE + HUILE
                new Product { BarCode=77,ProductName= "Tomato Ketchup Sauce", Description="Heinz 460g", ImageFile= "Pictures/77.jpg" }, //11.90
                new Product { BarCode=78,ProductName= "Garlic Sauce", Description="Ossem 290g", ImageFile= "Pictures/78.jpg" }, //14.90   ----- a faire jusque la , non imclus 
                new Product { BarCode=79,ProductName= "Chicken Soup Broth", Description="Ossem 400g", ImageFile= "Pictures/79.jpg" }, //17.90
                new Product { BarCode=80,ProductName= "Olive Oil", Description="Yad Mordechai 750 ml", ImageFile= "Pictures/80.jpg" }, //42.90
                new Product { BarCode=81,ProductName= "Lemon Juice", Description="Priniv 500 ml", ImageFile= "Pictures/81.jpg" }, //12.90
                new Product { BarCode=82,ProductName= "Tomato Puree Sauce", Description="Passata 700g", ImageFile= "Pictures/82.jpg" }, //16.90
                new Product { BarCode=83,ProductName= "Extra-Fort Mustard", Description="Reine Dijon 350g", ImageFile= "Pictures/83.jpg" }, //14.90
                new Product { BarCode=84,ProductName= "Real Mayonnaise", Description="Heinz 420g", ImageFile= "Pictures/84.jpg" }, //18.90
                new Product { BarCode=85,ProductName= "Organic Apple Cider Vinegar 5%", Description="750 ml", ImageFile= "Pictures/85.jpg" }, //27.90
                new Product { BarCode=86,ProductName= "Pizza Sauce", Description="Yahin 240g", ImageFile= "Pictures/86.jpg" }, //7.90
                new Product { BarCode=87,ProductName= "White Sugar", Description="Sugat 1Kg", ImageFile= "Pictures/87.jpg" }, //11.90
                new Product { BarCode=88,ProductName= "Sea Salt Dry", Description="250g", ImageFile= "Pictures/88.jpg" }, //8.90


                // PRODUIT BEBE
                new Product { BarCode=89,ProductName= "Baby Wipes", Description="Huggies 4 pack", ImageFile= "Pictures/89.jpg" }, //26.90
               

                // VIN
                new Product { BarCode=90,ProductName= "Cabernet Wine", Description="Gamla", ImageFile= "Pictures/90.jpg" }, //77.90
                new Product { BarCode=91,ProductName= "Merlot Wine", Description="Recanati", ImageFile= "Pictures/91.jpg" }, //72.90
                new Product { BarCode=92,ProductName= "Heineken Beer", Description="500 ml", ImageFile= "Pictures/92.jpg" }, //13.90


                //FROMAGE
                new Product { BarCode=93,ProductName= "Cottage Cheese 5%", Description="Strauss 250g", ImageFile= "Pictures/93.jpg" }, //8.90
                new Product { BarCode=94,ProductName= "Cottage Cheese 3%", Description="Tara 250g", ImageFile= "Pictures/94.jpg" }, //8.90
                new Product { BarCode=95,ProductName= "Cheese Sticks Mozzarella", Description="Emek 8x21.5g", ImageFile= "Pictures/95.jpg"}, //22.90
                new Product { BarCode=96,ProductName= "Goat Feta 20%", Description="Gad 500g", ImageFile= "Pictures/96.jpg" }, //34.90
                new Product { BarCode=97,ProductName= "Unsalted Butter ", Description="Tnuva 200g", ImageFile= "Pictures/97.jpg" }, //10.90
                new Product { BarCode=98,ProductName= "Cheeses 28%", Description="Gush Halav 400g", ImageFile= "Pictures/98.jpg"}, //33.90
                new Product { BarCode=99,ProductName= "Shredded Mozzarella Cheese 22%", Description="Gad 300g", ImageFile= "Pictures/99.jpg" }, //29.9
                new Product { BarCode=100,ProductName= "Gratted Parmesan", Description="Tnuva 100g", ImageFile= "Pictures/100.jpg"}, //23.90
                new Product { BarCode=101,ProductName= "Goat White Cheese 5%", Description="Gad 250g", ImageFile= "Pictures/101.jpg" }, //23.90


               // CAFE + THE

                new Product { BarCode=102,ProductName= "Coffee Gold", Description="Jacobs 200g", ImageFile= "Pictures/102.jpg" }, //32.90
                new Product { BarCode=103,ProductName= "Turkish Coffee", Description="Elite 200g", ImageFile= "Pictures/103.jpg" }, //19.90
                new Product { BarCode=104,ProductName= "Tea Classic", Description="Vysotsky 50 bags", ImageFile= "Pictures/104.jpg" }, //16.90
                new Product { BarCode=105,ProductName= "Green Tea", Description="Vysotsky 50 bags", ImageFile= "Pictures/105.jpg" }, //30.90

                // PLAstiQUES
                new Product { BarCode=106,ProductName= "Paper Plates", Description="100 count", ImageFile= "Pictures/106.jpg" }, //29.90
                new Product { BarCode=107,ProductName= "Plastic Cups", Description="50 count", ImageFile= "Pictures/107.jpg" }, //14.90


               // ----------------------------------------------------ClOTHES----------------------------------------
               // ******WOMAN******
                new Product { BarCode=108,ProductName= "Woman T-Shirt", Description="Letter Graphic Drop Shoulder Tee", ImageFile= "Pictures/108.jpg" }, //
                new Product { BarCode=109,ProductName= "Woman T-Shirt", Description="Tie Dye And Graphic Print Tee", ImageFile= "Pictures/109.jpg" }, //
                new Product { BarCode=110,ProductName= "Woman T-Shirt", Description="Mushroom Print Crew Neck Tee", ImageFile= "Pictures/110.jpg" }, //
                new Product { BarCode=111,ProductName= "Woman T-Shirt", Description="Butterfly Print Short Sleeve Tee", ImageFile= "Pictures/111.jpg" }, //



                new Product { BarCode=112,ProductName= "Woman Sweatshirt", Description="Zip Up Drop Shoulder Drawstring", ImageFile= "Pictures/112.jpg" }, // 49 
                new Product { BarCode=113,ProductName= "Woman Sweatshirt", Description="Cartoon Graphic Lined Oversized", ImageFile= "Pictures/113.jpg" }, //39
                new Product { BarCode=114,ProductName= "Woman Sweatshirt", Description="Moon And Sun Kangaroo Pocket", ImageFile= "Pictures/114.jpg" }, //69
                new Product { BarCode=115,ProductName= "Woman Sweatshirt", Description="Drop Shoulder Letter Graphic", ImageFile= "Pictures/115.jpg" }, // 49



                new Product { BarCode=116,ProductName= "Woman Jacket", Description="Striped Button Up Bomber", ImageFile= "Pictures/116.jpg" }, // 69
                new Product { BarCode=117,ProductName= "Woman Jacket", Description="Solid Zip Up Hooded", ImageFile= "Pictures/117.jpg" }, // 39
                new Product { BarCode=118,ProductName= "Woman Jacket", Description="Dual Pocket Corduroy", ImageFile= "Pictures/118.jpg" }, // 49


                new Product { BarCode=119,ProductName= "Woman Outerwear", Description="Buffalo Plaid Coat", ImageFile= "Pictures/119.jpg" }, // 79

                new Product { BarCode=120,ProductName= "Woman Blazer", Description="Double Breasted Lapel Neck", ImageFile= "Pictures/120.jpg" }, // 109
                new Product { BarCode=121,ProductName= "Woman Blazer", Description="Notch Collar Single Button", ImageFile= "Pictures/121.jpg" }, // 49
                new Product { BarCode=122,ProductName= "Woman Blazer", Description="Asymmetrical Hem Button Front", ImageFile= "Pictures/122.jpg" }, // 39

              

              //  *****MAN*******

                new Product { BarCode=123,ProductName= "Man Shirt", Description="Floral Print Button Front", ImageFile= "Pictures/123.jpg" }, //39
                new Product { BarCode=124,ProductName= "Man Shirt", Description="Flamingo & Tree Hawaiian", ImageFile= "Pictures/124.jpg" }, // 39
                new Product { BarCode=125,ProductName= "Man Shirt", Description="Allover Plants Print", ImageFile= "Pictures/125.jpg" }, // 49


                new Product { BarCode=126,ProductName= "Man Polo Shirt", Description="Contrast Trim Polo", ImageFile= "Pictures/126.jpg" }, // 49
                new Product { BarCode=127,ProductName= "Man Polo Shirt", Description="Deer Embroidered Polo", ImageFile= "Pictures/127.jpg" }, //39
                new Product { BarCode=128,ProductName= "Man Polo Shirt", Description="Striped Cuff Polo", ImageFile= "Pictures/128.jpg" }, // 49

                new Product { BarCode=129,ProductName= "Man Jean", Description="Splash Ink Distressed Tapered", ImageFile= "Pictures/129.jpg" }, // 89


                new Product { BarCode=130,ProductName= "Man Pant", Description="2pcs Letter Waist Carrot", ImageFile= "Pictures/130.jpg" }, // 99
                new Product { BarCode=131,ProductName= "Man Pant", Description="Drawstring Flap Pocket Cargo", ImageFile= "Pictures/131.jpg" }, // 69
                new Product { BarCode=132,ProductName= "Man Pant", Description="Neon Orange Push Buckle Cargo", ImageFile= "Pictures/132.jpg" }, //69


                new Product { BarCode=133,ProductName= "Man Short", Description="Cartoon Bear & Letter Graphic", ImageFile= "Pictures/133.jpg" }, // 39
                new Product { BarCode=134,ProductName= "Man Short", Description="Patched Front Track", ImageFile= "Pictures/134.jpg" }, // 39
                new Product { BarCode=135,ProductName= "Man Short", Description="Slant Pocket Bermuda", ImageFile= "Pictures/135.jpg" }, // 31



                new Product { BarCode=136,ProductName= "Man Sweatshirt", Description="Letter & Cartoon Graphic", ImageFile= "Pictures/136.jpg" }, // 49
                new Product { BarCode=137,ProductName= "Man Sweatshirt", Description="Slogan Graphic Patch Detail", ImageFile= "Pictures/137.jpg" }, //39
                new Product { BarCode=138,ProductName= "Man Sweatshirt", Description="Slogan Graphic Pullover", ImageFile= "Pictures/138.jpg" }, //59


               new Product { BarCode=139,ProductName= "Man Cardigan", Description="Embroidery Button Front", ImageFile= "Pictures/139.jpg" }, //59

              // Woman
               new Product { BarCode=140,ProductName= "Woman Hat", Description="Minimalist Plain Beanie", ImageFile= "Pictures/140.jpg" }, //9
               new Product { BarCode=141,ProductName= "Woman Hat", Description="Plain Baseball Cap", ImageFile= "Pictures/141.jpg" }, //9
              new Product { BarCode=142,ProductName= "Woman Hat", Description="2pcs Letter Embroidery Bucket", ImageFile= "Pictures/142.jpg" }, //29
              //Woman


              new Product { BarCode=143,ProductName= "Man Belt", Description="Clover Belt", ImageFile= "Pictures/143.jpg" }, //29
              new Product { BarCode=144,ProductName= "Man Belt", Description="PU Leather", ImageFile= "Pictures/144.jpg" }, //19

               new Product { BarCode=145,ProductName= "Man Socks", Description="Sea Wave Pattern", ImageFile= "Pictures/145.jpg" }, //9
               new Product { BarCode=146,ProductName= "Man Socks", Description="1pair Men Cartoon Graphic", ImageFile= "Pictures/146.jpg" }, //9
               new Product { BarCode=147,ProductName= "Man Socks", Description="Avocado Pattern Calf Length", ImageFile= "Pictures/147.jpg" }, //9



               //----------------------------------------------------BEAUTY----------------------------------------

               new Product { BarCode=148,ProductName= "Makeup", Description="Natasha Denona Eyeliner Crayon", ImageFile= "Pictures/148.jpg" }, // 24
               new Product { BarCode=149,ProductName= "Makeup", Description="Urban Decay Lip Balm", ImageFile= "Pictures/149.jpg" }, // 20
               new Product { BarCode=150,ProductName= "Makeup", Description="KVD Beauty Lipstick", ImageFile= "Pictures/150.jpg" }, // 18
               new Product { BarCode=151,ProductName= "Makeup", Description="Paula's Choice Lip Booster", ImageFile= "Pictures/151.jpg" }, // 39
               new Product { BarCode=152,ProductName= "Makeup", Description="Estée Lauder Long-Wear Makeup", ImageFile= "Pictures/152.jpg" }, // 43
               new Product { BarCode=153,ProductName= "Makeup", Description="Lancôme Volumizing Mascara", ImageFile= "Pictures/153.jpg" }, //  26
               new Product { BarCode=154,ProductName= "Makeup", Description= "Dior Waterproof Mascara", ImageFile= "Pictures/154.jpg" },//29.50
               new Product { BarCode=155,ProductName= "Makeup", Description= "Huda Beauty Eyeshadow Palette", ImageFile= "Pictures/155.jpg" },//27
               new Product { BarCode=156,ProductName= "Makeup", Description= "Fenty Beauty Cream Blush", ImageFile= "Pictures/156.jpg" }, //20
               new Product { BarCode=157,ProductName= "Makeup", Description= "Tarte 12-Hour Blush", ImageFile= "Pictures/157.jpg" }, //29
               new Product { BarCode=158,ProductName= "Makeup", Description= "Charlotte Tilbury Matte Bronzer", ImageFile= "Pictures/158.jpg" }, //55


               new Product { BarCode=159,ProductName= "Skincare", Description="Fresh Night Moisturizer", ImageFile= "Pictures/159.jpg" },//52
               new Product { BarCode=160,ProductName= "Skincare", Description="Laneige Sleeping Mask", ImageFile= "Pictures/160.jpg" },//39
               new Product { BarCode=161,ProductName= "Skincare", Description="Algenist Restorative Cream", ImageFile= "Pictures/161.jpg" }, //94
               new Product { BarCode=162,ProductName= "Skincare", Description="Dr. Barbara Sturm Night Serum", ImageFile= "Pictures/162.jpg" }, // 310




               new Product { BarCode=163,ProductName= "Haircare", Description="Olaplex Moisture Mask", ImageFile= "Pictures/163.jpg" }, //28
               new Product { BarCode=164,ProductName= "Haircare", Description="Moroccanoil Hydrating Mask", ImageFile= "Pictures/164.jpg" }, //38
               new Product { BarCode=165,ProductName= "Haircare", Description="Drybar Dry Shampoo", ImageFile= "Pictures/165.jpg" }, //23
               new Product { BarCode=166,ProductName= "Haircare", Description="Kérastase Shampoo for Dry Hair", ImageFile= "Pictures/166.jpg" },//31
               new Product { BarCode=167,ProductName= "Haircare", Description="Verb Curl Shampoo", ImageFile= "Pictures/167.jpg" }, //18


               new Product { BarCode=168,ProductName= "Woman Fragrance", Description="Carolina Herrera Floral 50mL", ImageFile= "Pictures/168.jpg" }, //98
               new Product { BarCode=169,ProductName= "Woman Fragrance", Description="Yves Saint Laurent Spicy 90 mL", ImageFile= "Pictures/169.jpg" }, //130
               new Product { BarCode=170,ProductName= "Woman Fragrance", Description="Dior Floral 100mL", ImageFile= "Pictures/170.jpg" }, //138
               new Product { BarCode=171,ProductName= "Woman Fragrance", Description="Gucci Floral 50mL", ImageFile= "Pictures/171.jpg" }, //85
               new Product { BarCode=172,ProductName= "Woman Fragrance", Description="Armani Beauty Fresh 50mL", ImageFile= "Pictures/172.jpg" }, //81

               new Product { BarCode=173,ProductName= "Man Fragrance", Description="Armani Beauty Woody 75mL", ImageFile= "Pictures/173.jpg" }, //102
               new Product { BarCode=174,ProductName= "Man Fragrance", Description="Paco Rabanne Spicy 100mL", ImageFile= "Pictures/174.jpg" }, //94
               new Product { BarCode=175,ProductName= "Man Fragrance", Description="Chanel Woody 50mL", ImageFile= "Pictures/175.jpg" }, //125
               
               
              // HAIRCARE


               new Product { BarCode=176,ProductName= "Haircare", Description="Head & Shoulders Classic Shampoo", ImageFile= "Pictures/176.jpg" }, //19.90
               new Product { BarCode=177,ProductName= "Haircare", Description="Pinouk Classic Shampoo", ImageFile= "Pictures/177.jpg" }, //9.90
               new Product { BarCode=178,ProductName= "Haircare", Description="Hawaï Shampoo for Normal Hair", ImageFile= "Pictures/178.jpg" }, //11.90
               new Product { BarCode=179,ProductName= "Haircare", Description="Dove Conditioner Dry Hair", ImageFile= "Pictures/179.jpg" }, //14.90
               
              // HAIRCARE


               //----------------------------------------------------APPLIANCES----------------------------------------

               new Product { BarCode=180,ProductName= "Toaster", Description="Hemilton Pressing Toaster 750W", ImageFile= "Pictures/180.jpg" }, //79
               new Product { BarCode=181,ProductName= "Toaster", Description="Morphy Richards Toaster 900W", ImageFile= "Pictures/181.jpg" }, //255
               new Product { BarCode=182,ProductName= "Toaster", Description="Sol Toaster 850W", ImageFile= "Pictures/182.jpg" }, //189

               new Product { BarCode=183,ProductName= "Electric Kettle", Description="Kitchen Chef LED 1.8L", ImageFile= "Pictures/183.jpg" }, //166
               new Product { BarCode=184,ProductName= "Electric Kettle", Description="Hemilton Thermos 30 Cups Shabbat", ImageFile= "Pictures/184.jpg" }, //234

               new Product { BarCode=185,ProductName= "Dishwasher", Description="Bosch 12 Sets Stainless Steel", ImageFile= "Pictures/185.jpg" }, //2333

               new Product { BarCode=186,ProductName= "Refrigerator", Description="LG 3 Doors 790L No Frost Shabbat", ImageFile= "Pictures/186.jpg" }, //7648

               new Product { BarCode=187,ProductName= "Freezer", Description="Midea 261L 7 Drawers White", ImageFile= "Pictures/187.jpg" }, //2149

               new Product { BarCode=188,ProductName= "Oven", Description="Bosch 66L Turbo 3D", ImageFile= "Pictures/188.jpg" }, //1444

               new Product { BarCode=189,ProductName= "Microwave", Description="Samsung 23L 800W Grey", ImageFile= "Pictures/189.jpg" }, //429

               new Product { BarCode=190,ProductName= "Washing Machine", Description="Bosch 8 kg 1200rpm", ImageFile= "Pictures/190.jpg" }, //3298

               new Product { BarCode=191,ProductName= "Vacuum", Description="Dyson Cleaner V10", ImageFile= "Pictures/191.jpg" }, //2188

               new Product { BarCode=192,ProductName= "Fryers & Burners", Description="Hamilton Slow Cooker 6.5L 300W", ImageFile= "Pictures/192.jpg" }, //333

              // ----------------------------------------------------MULTIMEDIA----------------------------------------

               new Product { BarCode=193,ProductName= "TV", Description="Toshiba TV 32 Iches LED", ImageFile= "Pictures/193.jpg" }, //1099

               new Product { BarCode=194,ProductName= "SoundBar", Description="LG Bluetooth 300W", ImageFile= "Pictures/194.jpg" }, //749
               new Product { BarCode=195,ProductName= "Tablet", Description="Samsung Galaxy Tab A7 32GB", ImageFile= "Pictures/195.jpg" }, //899

               new Product { BarCode=196,ProductName= "Mobile Phone", Description="Samsung Galaxy Note 20 256GB", ImageFile= "Pictures/196.jpg" }, //2,849

               new Product { BarCode=197,ProductName= "Headphones", Description="Asus Rog Cetra II Core Gaming", ImageFile= "Pictures/197.jpg" }, //279
               new Product { BarCode=198,ProductName= "Headphones", Description="Asus Rog Strix Headset", ImageFile= "Pictures/198.jpg" }, //271

               new Product { BarCode=199,ProductName= "Web Camera", Description="Asus Webcam C3 1080p", ImageFile= "Pictures/199.jpg" }, //295

               new Product { BarCode=200,ProductName= "Gamepad", Description="Microsoft Xbox Series-X Wireless", ImageFile= "Pictures/200.jpg" }, // 235
               
               new Product { BarCode=201,ProductName= "Printer", Description="HP Deskjet Plus Wireless 4120", ImageFile= "Pictures/201.jpg" }, // 379

               new Product { BarCode=202,ProductName= "Laptop", Description="HP Pavilion 14-CE3006NJ", ImageFile= "Pictures/202.jpg" }, //2990

               new Product { BarCode=203,ProductName= "Accessories", Description="HP Pavilion 14-CE3006NJ", ImageFile= "Pictures/203.jpg" }, //139

               new Product { BarCode=204,ProductName= "Watches", Description="SAFA Wall Clock MC-259", ImageFile= "Pictures/204.jpg" }, //199

                 //----------------------------------------------------APPLIANCES----------------------------------------

                new Product { BarCode=205,ProductName= "Coffee Machine", Description="Nespresso Krups Inissia Red", ImageFile= "Pictures/205.jpg" }, //389

                new Product { BarCode=206,ProductName= "Stand Mixer", Description="Hyundai Mixer 1000W Red", ImageFile= "Pictures/206.jpg" }, //₪298

            

                //new Product { BarCode=,ProductName= "", Description="", ImageFile= "" }, //
            };

            //foreach (var product in listProducts)
            //{
            //    dal.AddProductToDB(product);
            //}
            //Console.ReadLine();
            //Console.WriteLine("la fin");




            // ----------------------------------------------------STORES----------------------------------------
            //List<BE.Store> listStores = new List<BE.Store>()
            //{
            //    // dans le DB, cest le StoreID +1
            //    //-------------------FOOD
            //    new Store{StoreId=1,  StoreName="Supersal", StoreCity="Jerusalem"},*
            //    new Store{StoreId=2,  StoreName="Supersal", StoreCity="Tel Aviv"},*
            //    new Store{StoreId=3,  StoreName="Supersal", StoreCity="Ashdod"},
            //    new Store{StoreId=4,  StoreName="Supersal", StoreCity="Modiin"},*
            //    new Store{StoreId=5,  StoreName="Ocher Ad", StoreCity="Jerusalem"},*
            //    new Store{StoreId=6,  StoreName="Ocher Ad", StoreCity="Ashdod"},*
            //    new Store{StoreId=7,  StoreName="Ocher Ad", StoreCity="Ramat Gan"},*
            //    new Store{StoreId=8,  StoreName="Ocher Ad", StoreCity="Bet Shemesh"},*
            //    new Store{StoreId=9,  StoreName="Rami Levy", StoreCity="Jerusalem"},*
            //    new Store{StoreId=10,  StoreName="Rami Levy", StoreCity="Ramat Gan"},
            //    new Store{StoreId=11,  StoreName="Rami Levy", StoreCity="Bet Shemesh"},*
            //    new Store{StoreId=12,  StoreName="Mayan 2000", StoreCity="Jerusalem"},*



            //    //-------------------CLOTHES
            //    new Store{StoreId=13,  StoreName="Renuar", StoreCity="Jerusalem"},
            //    new Store{StoreId=14,  StoreName="Renuar", StoreCity="Tel Aviv"},
            //    new Store{StoreId=15,  StoreName="Renuar", StoreCity="Bet Shemesh"},
            //    new Store{StoreId=16,  StoreName="Zara", StoreCity="Eilat"},
            //    new Store{StoreId=17,  StoreName="Zara", StoreCity="Jerusalem"},
            //    new Store{StoreId=18,  StoreName="Zara", StoreCity="Tel Aviv"},
            //    new Store{StoreId=19,  StoreName="Mango", StoreCity="Jerusalem"},
            //    new Store{StoreId=20,  StoreName="Mango", StoreCity="Ramat Gan"},
            //    new Store{StoreId=21,  StoreName="Zoya", StoreCity="Bne Brak"},
            //    new Store{StoreId=22,  StoreName="Zoya", StoreCity="Jerusalem"},

            //    //-------------------BEAUTY
            //    new Store{StoreId=23,  StoreName="Superpharm", StoreCity="Tel Aviv"},
            //    new Store{StoreId=24,  StoreName="Superpharm", StoreCity="Jerusalem"},
            //    new Store{StoreId=25,  StoreName="Superpharm", StoreCity="Ramat Gan"},
            //    new Store{StoreId=26,  StoreName="Superpharm", StoreCity="Eilat"},
            //    new Store{StoreId=27,  StoreName="Superpharm", StoreCity="Ashdod"},

            //      new Store{StoreId=28,  StoreName="Beautycare", StoreCity="Jerusalem"},
            //      new Store{StoreId=29,  StoreName="Beautycare", StoreCity="Netanya"},
            //      new Store{StoreId=30,  StoreName="Beautycare", StoreCity="Bat Yam"},
            //      new Store{StoreId=31,  StoreName="Beautycare", StoreCity="Bet Shemesh"},

            //       //-------------------APPLIANCES 
            //       new Store{StoreId=32,  StoreName="KSP", StoreCity="Jerusalem"},
            //       new Store{StoreId=33,  StoreName="KSP", StoreCity="Eilat"},
            //       new Store{StoreId=34,  StoreName="KSP", StoreCity="Tel Aviv"},

            //       new Store{StoreId=35,  StoreName="Ikea", StoreCity="Rishon Letsion"},
            //       new Store{StoreId=36,  StoreName="Ikea", StoreCity="Beer Sheva"},
            //       new Store{StoreId=37,  StoreName="Ikea", StoreCity="Netanya"},



            //       //-------------------MULTIMEDIA

            //       new Store { StoreId = 38, StoreName = "Kravitz", StoreCity = "Jerusalem" },
            //       new Store { StoreId = 39, StoreName = "Kravitz", StoreCity = "Tel Aviv" },
            //       new Store { StoreId = 40, StoreName = "Kravitz", StoreCity = "Eilat" },

            //};

            //    foreach(var store in listStores)
            //    {
            //        dal.AddStoretToDB(store);
            //    }
            //    Console.ReadLine();






            // ----------------------------------------------------PRODUCT ORDERS----------------------------------------


            List<ProductOrder> productOrdersList = new List<ProductOrder>()
            {
                // OCHER AD JERUSALEM , JEUDI 05/08 STORE ID 6
                new ProductOrder { BarCode = 2, Count=2, UnitPrice= (float) 6.5 , StoreId=6, Date= DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 3, Count=1, UnitPrice= (float) 9.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count=3, UnitPrice= (float) 10.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 10, Count=8, UnitPrice= (float) 5, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 14, Count= 3, UnitPrice= (float) 6.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 18, Count= 2, UnitPrice= (float) 18.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 19, Count=4, UnitPrice= (float) 7.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 23, Count=2, UnitPrice= (float) 5.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 25, Count=4, UnitPrice= (float) 12.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 27, Count= 2, UnitPrice= (float) 10.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 34, Count=5, UnitPrice= (float) 19.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 37, Count=1, UnitPrice= (float) 19.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 40, Count= 2, UnitPrice= (float) 25.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count= 8, UnitPrice= (float) 6.50, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 43, Count= 2, UnitPrice= (float) 29.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 45, Count= 3, UnitPrice= (float) 9.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 46, Count=4, UnitPrice= (float) 29.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 49, Count= 3, UnitPrice= (float) 13.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 51, Count= 1, UnitPrice= (float) 34.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 54, Count= 2, UnitPrice= (float) 37.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count= 5, UnitPrice= (float) 13.90, StoreId=6, Date=DateTime.Parse("05/08/2021"), Category = Category.Food , ProductOrderValidation = true },




                // SUPERSAL  JERUSALEM , MERCREDI 04/08
                new ProductOrder { BarCode = 56, Count=2, UnitPrice= (float) 14.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 57, Count=3, UnitPrice= (float) 19.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 59, Count=3, UnitPrice= (float) 29.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 60, Count=2, UnitPrice= (float) 26.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 61, Count=4, UnitPrice= (float) 14.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 63, Count=1, UnitPrice= (float) 25.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 64, Count= 2, UnitPrice= (float) 10.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 65, Count=4, UnitPrice= (float) 8.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 67, Count= 1, UnitPrice= (float) 20.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 68, Count= 2, UnitPrice= (float) 15.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 69, Count= 2, UnitPrice= (float) 20.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 73, Count= 5, UnitPrice= (float) 24.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 75, Count= 3, UnitPrice= (float) 15.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 78, Count= 1, UnitPrice= (float) 10.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 84, Count= 1, UnitPrice= (float) 14.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 82, Count= 2, UnitPrice= (float) 10.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 88, Count= 1, UnitPrice= (float) 10.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 91, Count= 2, UnitPrice= (float) 42.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 94, Count= 2, UnitPrice= (float) 10.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 98, Count= 3, UnitPrice= (float) 12.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 104, Count= 1, UnitPrice= (float) 22.90, StoreId=2, Date=DateTime.Parse("04/08/2021"), Category = Category.Food , ProductOrderValidation = true },



                // RAMI LEVY  JERUSALEM , JEUDI 12/08
                new ProductOrder { BarCode = 9, Count= 3, UnitPrice= (float) 11.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 10, Count= 6, UnitPrice= (float) 5.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 15, Count= 1, UnitPrice= (float) 13.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 22, Count= 3, UnitPrice= (float) 11.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 25, Count= 2, UnitPrice= (float) 12.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 39, Count= 3, UnitPrice= (float) 25.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count= 4, UnitPrice= (float) 6.50, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 47, Count= 2, UnitPrice= (float) 15.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 53, Count= 1, UnitPrice= (float) 40.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 49, Count= 3, UnitPrice= (float) 15.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 57, Count= 2, UnitPrice= (float) 18.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 61, Count= 4, UnitPrice= (float) 13.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 63, Count= 2, UnitPrice= (float) 24.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 65, Count= 3, UnitPrice= (float) 9.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 71, Count= 1, UnitPrice= (float) 9.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 74, Count= 2, UnitPrice= (float) 24.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 73, Count= 2, UnitPrice= (float) 24.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count= 5, UnitPrice= (float) 14.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 81, Count= 1, UnitPrice= (float) 30.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 89, Count= 2, UnitPrice= (float) 8.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count= 2, UnitPrice= (float) 30.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 99, Count= 3, UnitPrice= (float) 25.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 106, Count= 1, UnitPrice= (float) 27.90, StoreId=10, Date=DateTime.Parse("12/08/2021"), Category = Category.Food , ProductOrderValidation = true },




                // OCHER AD BET SHEMESH   MARDI 27/07
                new ProductOrder { BarCode = 6, Count= 3, UnitPrice= (float) 9.90, StoreId=9, Date=DateTime.Parse("27/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 8, Count= 2, UnitPrice= (float) 12.90, StoreId=9, Date=DateTime.Parse("27/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count= 3, UnitPrice= (float) 10.90, StoreId=9, Date=DateTime.Parse("27/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 16, Count= 1, UnitPrice= (float) 6.90, StoreId=9, Date=DateTime.Parse("27/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 14, Count= 2, UnitPrice= (float) 6.90, StoreId=9, Date=DateTime.Parse("27/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 12, Count= 2, UnitPrice= (float) 23.90, StoreId=9, Date=DateTime.Parse("27/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 29, Count= 2, UnitPrice= (float) 8.90, StoreId=9, Date=DateTime.Parse("27/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 32, Count= 2, UnitPrice= (float) 20.90, StoreId=9, Date=DateTime.Parse("27/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                

                // RAMI LEVY BET SHEMESH JEUDI 22/07
                new ProductOrder { BarCode = 24, Count= 2, UnitPrice= (float) 7.90, StoreId=12, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 33, Count= 2, UnitPrice= (float) 25.90, StoreId=12, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 37, Count= 2, UnitPrice= (float) 20.90, StoreId=12, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 34, Count= 2, UnitPrice= (float) 22.90, StoreId=12, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 35, Count= 1, UnitPrice= (float) 20.90, StoreId=12, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 39, Count= 2, UnitPrice= (float) 25.90, StoreId=12, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 44, Count= 5, UnitPrice= (float) 8.90, StoreId=12, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 48, Count= 2, UnitPrice= (float) 17.90, StoreId=12, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count= 4, UnitPrice= (float) 7.50, StoreId=12, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 49, Count= 3, UnitPrice= (float) 12.90, StoreId=12, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 53, Count= 2, UnitPrice= (float) 39.90, StoreId=12, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 58, Count= 2, UnitPrice= (float) 29.90, StoreId=12, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 59, Count= 2, UnitPrice= (float) 27.90, StoreId=12, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count= 5, UnitPrice= (float) 14.90, StoreId=5, Date=DateTime.Parse("22/07/2021"), Category = Category.Food , ProductOrderValidation = true },


                // SUPERSAL TEL AVIV  LUNDI 19/07
                new ProductOrder { BarCode = 62, Count= 1, UnitPrice= (float) 30.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 63, Count= 2, UnitPrice= (float) 25.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 65, Count= 3, UnitPrice= (float) 9.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 66, Count= 3, UnitPrice= (float) 12.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 70, Count= 2, UnitPrice= (float) 18.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 72, Count= 1, UnitPrice= (float) 15.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 73, Count= 2, UnitPrice= (float) 24.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 76, Count= 1, UnitPrice= (float) 13.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 83, Count= 3, UnitPrice= (float) 15.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 80, Count= 2, UnitPrice= (float) 16.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 88, Count= 1, UnitPrice= (float) 11.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 90, Count= 4, UnitPrice= (float) 25.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 95, Count= 2, UnitPrice= (float) 8.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 96, Count= 1, UnitPrice= (float) 20.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 97, Count= 1, UnitPrice= (float) 29.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 100, Count= 2, UnitPrice= (float) 25.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 101, Count= 1, UnitPrice= (float) 23.90, StoreId=3, Date=DateTime.Parse("19/07/2021"), Category = Category.Food , ProductOrderValidation = true },



                // MAYAN 2000 JERUSALEM STORE 13  MERCREDI 14/07
                new ProductOrder { BarCode = 103, Count= 2, UnitPrice= (float) 32.90, StoreId=13, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 104, Count= 2, UnitPrice= (float) 19.90, StoreId=13, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 105, Count= 1, UnitPrice= (float) 16.90, StoreId=13, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 107, Count= 3, UnitPrice= (float) 19.90, StoreId=13, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 4, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count= 4, UnitPrice= (float) 11.90, StoreId=13, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 16, Count= 1, UnitPrice= (float) 8.90, StoreId=13, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 13, Count= 2, UnitPrice= (float) 16.90, StoreId=13, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 7, Count= 1, UnitPrice= (float) 12.90, StoreId=13, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 8, Count= 1, UnitPrice= (float) 12.90, StoreId=13, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 4, Count= 1, UnitPrice= (float) 12.90, StoreId=13, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 5, Count= 2, UnitPrice= (float) 6.90, StoreId=13, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },




                // SUPERSAL MODIIN  DIMANCHE 04/07 
                new ProductOrder { BarCode = 20, Count= 3, UnitPrice= (float) 15.90, StoreId=5, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 19, Count= 2, UnitPrice= (float) 8.90, StoreId=5, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 25, Count= 2, UnitPrice= (float) 14.90, StoreId=5, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 23, Count= 3, UnitPrice= (float) 7.90, StoreId=5, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 26, Count= 3, UnitPrice= (float) 10.90, StoreId=5, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 29, Count= 3, UnitPrice= (float) 8.90, StoreId=5, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 31, Count= 3, UnitPrice= (float) 14.90, StoreId=5, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 36, Count= 2, UnitPrice= (float) 30.90, StoreId=5, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 35, Count= 2, UnitPrice= (float) 28.90, StoreId=5, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 39, Count= 3, UnitPrice= (float) 27.90, StoreId=5, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 38, Count= 2, UnitPrice= (float) 26.90, StoreId=5, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count= 7, UnitPrice= (float) 7.50, StoreId=5, Date=DateTime.Parse("14/07/2021"), Category = Category.Food , ProductOrderValidation = true },


                // MAYANE 2000 JERUSALEM HALLOTS STORE13
                new ProductOrder { BarCode = 77, Count= 5, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("02/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count= 1, UnitPrice= (float) 31.90, StoreId=13, Date=DateTime.Parse("02/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count= 6, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("09/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count= 2, UnitPrice= (float) 31.90, StoreId=13, Date=DateTime.Parse("09/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count= 4, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("16/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count= 2, UnitPrice= (float) 31.90, StoreId=5, Date=DateTime.Parse("16/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count= 5, UnitPrice= (float) 14.90, StoreId=5, Date=DateTime.Parse("30/07/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count= 2, UnitPrice= (float) 31.90, StoreId=5, Date=DateTime.Parse("30/07/2021"), Category = Category.Food , ProductOrderValidation = true },



                // OCHER AD ASHDOD STORE ID 7  21/06 LUNDI 

                new ProductOrder { BarCode = 6, Count= 3, UnitPrice= (float) 9.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 8, Count= 2, UnitPrice= (float) 12.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count= 3, UnitPrice= (float) 10.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 16, Count= 1, UnitPrice= (float) 6.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 14, Count= 2, UnitPrice= (float) 6.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 12, Count= 2, UnitPrice= (float) 23.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 29, Count= 2, UnitPrice= (float) 8.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 32, Count= 2, UnitPrice= (float) 20.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 93, Count= 6, UnitPrice= (float) 13.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 90, Count= 3, UnitPrice= (float) 26.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 107, Count= 2, UnitPrice= (float) 17.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 3, UnitPrice= (float) 14.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 68, Count= 4, UnitPrice= (float) 14.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 76, Count= 3, UnitPrice= (float) 13.90, StoreId=7, Date=DateTime.Parse("21/06/2021"), Category = Category.Food , ProductOrderValidation = true },

                //OCHER AD RAMAT GAN  STORE ID 8  30/06 MERCREDI 
                new ProductOrder { BarCode = 6, Count= 3, UnitPrice= (float) 9.90, StoreId=8, Date=DateTime.Parse("30/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 8, Count= 2, UnitPrice= (float) 12.90, StoreId=8, Date=DateTime.Parse("30/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count= 3, UnitPrice= (float) 10.90, StoreId=8, Date=DateTime.Parse("30/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 16, Count= 1, UnitPrice= (float) 6.90, StoreId=8, Date=DateTime.Parse("30/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 14, Count= 2, UnitPrice= (float) 6.90, StoreId=8, Date=DateTime.Parse("30/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 12, Count= 2, UnitPrice= (float) 23.90, StoreId=8, Date=DateTime.Parse("30/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 29, Count= 2, UnitPrice= (float) 8.90, StoreId=8, Date=DateTime.Parse("30/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 32, Count= 2, UnitPrice= (float) 20.90, StoreId=8, Date=DateTime.Parse("30/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 85, Count= 1, UnitPrice= (float) 15.90, StoreId=8, Date=DateTime.Parse("30/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 86, Count= 3, UnitPrice= (float) 24.90, StoreId=8, Date=DateTime.Parse("30/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 102, Count= 2, UnitPrice= (float) 22.90, StoreId=8, Date=DateTime.Parse("30/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 101, Count= 3, UnitPrice= (float) 22.90, StoreId=8, Date=DateTime.Parse("30/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 46, Count= 3, UnitPrice= (float) 27.90, StoreId=8, Date=DateTime.Parse("30/06/2021"), Category = Category.Food , ProductOrderValidation = true },



                // SUPERSAL ASHDOD  STORE ID 4   MERCREDI 16/06

                new ProductOrder { BarCode = 20, Count= 3, UnitPrice= (float) 15.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 19, Count= 2, UnitPrice= (float) 8.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 25, Count= 2, UnitPrice= (float) 14.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 23, Count= 3, UnitPrice= (float) 7.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 26, Count= 3, UnitPrice= (float) 10.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 29, Count= 3, UnitPrice= (float) 8.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 31, Count= 3, UnitPrice= (float) 14.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 36, Count= 2, UnitPrice= (float) 30.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 35, Count= 2, UnitPrice= (float) 28.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 39, Count= 3, UnitPrice= (float) 27.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 38, Count= 2, UnitPrice= (float) 26.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count= 7, UnitPrice= (float) 7.50, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 56, Count= 3, UnitPrice= (float) 14.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 52, Count= 3, UnitPrice= (float) 45.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 7, Count= 2, UnitPrice= (float) 12.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 6, Count= 4, UnitPrice= (float) 11.90, StoreId=4, Date=DateTime.Parse("16/06/2021"), Category = Category.Food , ProductOrderValidation = true },

                // RAMI LEVY RAMAT GAN  STORE ID 11  LUNDI 07/06
                 new ProductOrder { BarCode = 9, Count= 3, UnitPrice= (float) 11.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 10, Count= 6, UnitPrice= (float) 5.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 15, Count= 1, UnitPrice= (float) 13.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 22, Count= 3, UnitPrice= (float) 11.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 25, Count= 2, UnitPrice= (float) 12.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 39, Count= 3, UnitPrice= (float) 25.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count= 4, UnitPrice= (float) 6.50, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 47, Count= 2, UnitPrice= (float) 15.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 53, Count= 1, UnitPrice= (float) 40.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 49, Count= 3, UnitPrice= (float) 15.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 57, Count= 2, UnitPrice= (float) 18.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 61, Count= 4, UnitPrice= (float) 13.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 63, Count= 2, UnitPrice= (float) 24.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 65, Count= 3, UnitPrice= (float) 9.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 71, Count= 1, UnitPrice= (float) 9.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 74, Count= 2, UnitPrice= (float) 24.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 73, Count= 2, UnitPrice= (float) 24.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 81, Count= 1, UnitPrice= (float) 30.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 89, Count= 2, UnitPrice= (float) 8.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count= 2, UnitPrice= (float) 30.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 99, Count= 3, UnitPrice= (float) 25.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 106, Count= 1, UnitPrice= (float) 27.90, StoreId=11, Date=DateTime.Parse("07/06/2021"), Category = Category.Food , ProductOrderValidation = true },


                // SUPERSAL JERUSALEM 01/06 MARDI 
                new ProductOrder { BarCode = 56, Count=2, UnitPrice= (float) 14.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 57, Count=3, UnitPrice= (float) 19.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 59, Count=3, UnitPrice= (float) 29.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 60, Count=2, UnitPrice= (float) 26.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 61, Count=4, UnitPrice= (float) 14.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 63, Count=1, UnitPrice= (float) 25.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 64, Count= 2, UnitPrice= (float) 10.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 65, Count=4, UnitPrice= (float) 8.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 67, Count= 1, UnitPrice= (float) 20.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 68, Count= 2, UnitPrice= (float) 15.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 69, Count= 2, UnitPrice= (float) 20.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 73, Count= 5, UnitPrice= (float) 24.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 95, Count= 3, UnitPrice= (float) 4.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count= 2, UnitPrice= (float) 24.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 72, Count= 3, UnitPrice= (float) 14.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 80, Count= 3, UnitPrice= (float) 15.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 79, Count= 1, UnitPrice= (float) 14.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 78, Count= 1, UnitPrice= (float) 11.90, StoreId=2, Date=DateTime.Parse("01/06/2021"), Category = Category.Food , ProductOrderValidation = true },


                // MOIS DE JUIN VENDREDI 
                new ProductOrder { BarCode = 77, Count= 5, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("04/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count= 1, UnitPrice= (float) 31.90, StoreId=13, Date=DateTime.Parse("04/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count= 6, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("11/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count= 2, UnitPrice= (float) 31.90, StoreId=13, Date=DateTime.Parse("11/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count= 4, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("18/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 68, Count= 1, UnitPrice= (float) 14.90, StoreId=5, Date=DateTime.Parse("18/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count= 5, UnitPrice= (float) 14.90, StoreId=5, Date=DateTime.Parse("25/06/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 71, Count= 2, UnitPrice= (float) 9.90, StoreId=5, Date=DateTime.Parse("25/06/2021"), Category = Category.Food , ProductOrderValidation = true },



            };

            List<ProductOrder> productOrdersList2 = new List<ProductOrder>
            {
                // MOIS DE MAI LES VENDREDI 28/05, TRUE AVEC LE PROGRAMME 
                new ProductOrder { BarCode = 77, Count= 5, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("07/05/2021"), Category = Category.Food , ProductOrderValidation = false },
                new ProductOrder { BarCode = 92, Count= 1, UnitPrice= (float) 31.90, StoreId=13, Date=DateTime.Parse("07/05/2021"), Category = Category.Food , ProductOrderValidation = false },
                new ProductOrder { BarCode = 77, Count= 2, UnitPrice= (float) 31.90, StoreId=13, Date=DateTime.Parse("14/05/2021"), Category = Category.Food , ProductOrderValidation = false },
                new ProductOrder { BarCode = 92, Count= 2, UnitPrice= (float) 31.90, StoreId=13, Date=DateTime.Parse("14/05/2021"), Category = Category.Food , ProductOrderValidation = false },
                new ProductOrder { BarCode = 77, Count= 4, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("21/05/2021"), Category = Category.Food , ProductOrderValidation = false },
                new ProductOrder { BarCode = 9, Count= 3, UnitPrice= (float) 10.90, StoreId=13, Date=DateTime.Parse("21/05/2021"), Category = Category.Food , ProductOrderValidation = false },
                new ProductOrder { BarCode = 25, Count= 5, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("21/05/2021"), Category = Category.Food , ProductOrderValidation = false },
                new ProductOrder { BarCode = 77, Count= 5, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("28/05/2021"), Category = Category.Food , ProductOrderValidation = false },
                new ProductOrder { BarCode = 71, Count= 2, UnitPrice= (float) 9.90, StoreId=13, Date=DateTime.Parse("28/05/2021"), Category = Category.Food , ProductOrderValidation = false },
            };


            List<ProductOrder> productOrdersList3 = new List<ProductOrder>
            {
                // 23 MAI DIMANCHE OCHER AD JERUSALEM
                new ProductOrder { BarCode = 18, Count = 2, UnitPrice = (float)18.90, StoreId = 6, Date = DateTime.Parse("23/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 19, Count = 4, UnitPrice = (float)7.90, StoreId = 6, Date = DateTime.Parse("23/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 23, Count = 2, UnitPrice = (float)5.90, StoreId = 6, Date = DateTime.Parse("23/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 25, Count = 4, UnitPrice = (float)12.90, StoreId = 6, Date = DateTime.Parse("23/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 27, Count = 2, UnitPrice = (float)10.90, StoreId = 6, Date = DateTime.Parse("23/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 34, Count = 5, UnitPrice = (float)19.90, StoreId = 6, Date = DateTime.Parse("23/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 37, Count = 1, UnitPrice = (float)19.90, StoreId = 6, Date = DateTime.Parse("23/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 40, Count = 2, UnitPrice = (float)25.90, StoreId = 6, Date = DateTime.Parse("23/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count = 8, UnitPrice = (float)6.50, StoreId = 6, Date = DateTime.Parse("23/05/2021"), Category = Category.Food, ProductOrderValidation = true },


                // MARDI 18 MAI  OCHER AD ASHDOD
                new ProductOrder { BarCode = 14, Count = 2, UnitPrice = (float)6.90, StoreId = 7, Date = DateTime.Parse("18/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 12, Count = 2, UnitPrice = (float)23.90, StoreId = 7, Date = DateTime.Parse("18/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 29, Count = 2, UnitPrice = (float)8.90, StoreId = 7, Date = DateTime.Parse("18/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 32, Count = 2, UnitPrice = (float)20.90, StoreId = 7, Date = DateTime.Parse("18/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 93, Count = 6, UnitPrice = (float)13.90, StoreId = 7, Date = DateTime.Parse("18/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 90, Count = 3, UnitPrice = (float)26.90, StoreId = 7, Date = DateTime.Parse("18/05/2021"), Category = Category.Food, ProductOrderValidation = true },


                // JEUDI 13 MAI RAMI LEVY JERUSALEM 
                new ProductOrder { BarCode = 24, Count = 1, UnitPrice = (float)7.90, StoreId = 12, Date = DateTime.Parse("13/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 33, Count = 2, UnitPrice = (float)25.90, StoreId = 12, Date = DateTime.Parse("13/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 37, Count = 3, UnitPrice = (float)20.90, StoreId = 12, Date = DateTime.Parse("13/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 34, Count = 2, UnitPrice = (float)22.90, StoreId = 12, Date = DateTime.Parse("13/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 35, Count = 1, UnitPrice = (float)20.90, StoreId = 12, Date = DateTime.Parse("13/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 39, Count = 2, UnitPrice = (float)25.90, StoreId = 12, Date = DateTime.Parse("13/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 44, Count = 1, UnitPrice = (float)8.90, StoreId = 12, Date = DateTime.Parse("13/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 48, Count = 2, UnitPrice = (float)17.90, StoreId = 12, Date = DateTime.Parse("13/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count = 3, UnitPrice = (float)7.50, StoreId = 12, Date = DateTime.Parse("13/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 49, Count = 3, UnitPrice = (float)12.90, StoreId = 12, Date = DateTime.Parse("13/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 53, Count = 2, UnitPrice = (float)39.90, StoreId = 12, Date = DateTime.Parse("13/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 58, Count = 2, UnitPrice = (float)29.90, StoreId = 12, Date = DateTime.Parse("13/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 59, Count = 1, UnitPrice = (float)27.90, StoreId = 12, Date = DateTime.Parse("13/05/2021"), Category = Category.Food, ProductOrderValidation = true },


                // LUNDI 03 MAI SUPERSAL MODIIN
                new ProductOrder { BarCode = 31, Count = 2, UnitPrice = (float)14.90, StoreId = 5, Date = DateTime.Parse("03/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 36, Count = 2, UnitPrice = (float)30.90, StoreId = 5, Date = DateTime.Parse("03/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 35, Count = 3, UnitPrice = (float)28.90, StoreId = 5, Date = DateTime.Parse("03/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 39, Count = 3, UnitPrice = (float)27.90, StoreId = 5, Date = DateTime.Parse("03/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 38, Count = 2, UnitPrice = (float)26.90, StoreId = 5, Date = DateTime.Parse("03/05/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count = 4, UnitPrice = (float)7.50, StoreId = 5, Date = DateTime.Parse("03/05/2021"), Category = Category.Food, ProductOrderValidation = true },


                // MERCREDI 28/04 SUPERSAL TEL AVIV

                new ProductOrder { BarCode = 72, Count = 1, UnitPrice = (float)15.90, StoreId = 3, Date = DateTime.Parse("28/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 73, Count = 1, UnitPrice = (float)24.90, StoreId = 3, Date = DateTime.Parse("28/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 76, Count = 1, UnitPrice = (float)13.90, StoreId = 3, Date = DateTime.Parse("28/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 83, Count = 3, UnitPrice = (float)15.90, StoreId = 3, Date = DateTime.Parse("28/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 80, Count = 4, UnitPrice = (float)16.90, StoreId = 3, Date = DateTime.Parse("28/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 88, Count = 1, UnitPrice = (float)11.90, StoreId = 3, Date = DateTime.Parse("28/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 90, Count = 4, UnitPrice = (float)25.90, StoreId = 3, Date = DateTime.Parse("28/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 95, Count = 2, UnitPrice = (float)8.90, StoreId = 3, Date = DateTime.Parse("28/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 96, Count = 1, UnitPrice = (float)20.90, StoreId = 3, Date = DateTime.Parse("28/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 97, Count = 1, UnitPrice = (float)29.90, StoreId = 3, Date = DateTime.Parse("28/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 100, Count = 3, UnitPrice = (float)25.90, StoreId = 3, Date = DateTime.Parse("28/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 101, Count = 1, UnitPrice = (float)23.90, StoreId = 3, Date = DateTime.Parse("28/04/2021"), Category = Category.Food, ProductOrderValidation = true },

                // MARDI 20/04  OCHER AD BET SHEMESH 
                new ProductOrder { BarCode = 8, Count = 1, UnitPrice = (float)12.90, StoreId = 9, Date = DateTime.Parse("20/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count = 5, UnitPrice = (float)10.90, StoreId = 9, Date = DateTime.Parse("20/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 16, Count = 1, UnitPrice = (float)6.90, StoreId = 9, Date = DateTime.Parse("20/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 14, Count = 2, UnitPrice = (float)6.90, StoreId = 9, Date = DateTime.Parse("20/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 12, Count = 4, UnitPrice = (float)23.90, StoreId = 9, Date = DateTime.Parse("20/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 29, Count = 2, UnitPrice = (float)8.90, StoreId = 9, Date = DateTime.Parse("20/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 32, Count = 3, UnitPrice = (float)20.90, StoreId = 9, Date = DateTime.Parse("20/04/2021"), Category = Category.Food, ProductOrderValidation = true },

                // JEUDI 15/04  RAMI LEVY JERUSALEM 
                new ProductOrder { BarCode = 42, Count = 3, UnitPrice = (float)6.50, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 47, Count = 2, UnitPrice = (float)15.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 53, Count = 1, UnitPrice = (float)40.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 49, Count = 5, UnitPrice = (float)15.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 57, Count = 2, UnitPrice = (float)18.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 61, Count = 4, UnitPrice = (float)13.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 63, Count = 2, UnitPrice = (float)24.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 65, Count = 3, UnitPrice = (float)9.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 71, Count = 1, UnitPrice = (float)9.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 74, Count = 2, UnitPrice = (float)24.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 73, Count = 3, UnitPrice = (float)24.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 81, Count = 1, UnitPrice = (float)30.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 89, Count = 1, UnitPrice = (float)8.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count = 2, UnitPrice = (float)30.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 99, Count = 3, UnitPrice = (float)25.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 106, Count = 2, UnitPrice = (float)27.90, StoreId = 10, Date = DateTime.Parse("15/04/2021"), Category = Category.Food, ProductOrderValidation = true },


                // DIMANCHE 04/04 RAMI LEVY RAMAT GAN 
                new ProductOrder { BarCode = 9, Count = 3, UnitPrice = (float)11.90, StoreId = 11, Date = DateTime.Parse("04/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 10, Count = 6, UnitPrice = (float)5.90, StoreId = 11, Date = DateTime.Parse("04/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 15, Count = 1, UnitPrice = (float)13.90, StoreId = 11, Date = DateTime.Parse("04/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 22, Count = 3, UnitPrice = (float)11.90, StoreId = 11, Date = DateTime.Parse("04/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 25, Count = 2, UnitPrice = (float)12.90, StoreId = 11, Date = DateTime.Parse("04/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 39, Count = 3, UnitPrice = (float)25.90, StoreId = 11, Date = DateTime.Parse("04/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count = 4, UnitPrice = (float)6.50, StoreId = 11, Date = DateTime.Parse("04/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 47, Count = 2, UnitPrice = (float)15.90, StoreId = 11, Date = DateTime.Parse("04/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 53, Count = 1, UnitPrice = (float)40.90, StoreId = 11, Date = DateTime.Parse("04/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 49, Count = 3, UnitPrice = (float)15.90, StoreId = 11, Date = DateTime.Parse("04/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 57, Count = 2, UnitPrice = (float)18.90, StoreId = 11, Date = DateTime.Parse("04/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 61, Count = 4, UnitPrice = (float)13.90, StoreId = 11, Date = DateTime.Parse("04/04/2021"), Category = Category.Food, ProductOrderValidation = true },

                // MOIS DE AVRIL VENDREDI 
                new ProductOrder { BarCode = 77, Count = 5, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("02/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count = 1, UnitPrice = (float)31.90, StoreId = 13, Date = DateTime.Parse("02/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 93, Count = 5, UnitPrice = (float)13.90, StoreId = 13, Date = DateTime.Parse("02/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count = 6, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("09/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count = 2, UnitPrice = (float)31.90, StoreId = 13, Date = DateTime.Parse("09/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 107, Count = 3, UnitPrice = (float)15.90, StoreId = 13, Date = DateTime.Parse("09/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count = 2, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("09/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count = 4, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("16/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 68, Count = 1, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("16/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 82, Count = 2, UnitPrice = (float)11.90, StoreId = 13, Date = DateTime.Parse("16/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count = 5, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("23/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 71, Count = 2, UnitPrice = (float)9.90, StoreId = 13, Date = DateTime.Parse("23/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count = 4, UnitPrice = (float)20.90, StoreId = 13, Date = DateTime.Parse("23/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 68, Count = 1, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("30/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count = 1, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("30/04/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 67, Count = 2, UnitPrice = (float)20.90, StoreId = 13, Date = DateTime.Parse("30/04/2021"), Category = Category.Food, ProductOrderValidation = true },


                // MOIS DE MARS 30/03 MARDI BEIT SHEMESH
                new ProductOrder { BarCode = 24, Count = 2, UnitPrice = (float)7.90, StoreId = 12, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 33, Count = 2, UnitPrice = (float)25.90, StoreId = 12, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 37, Count = 2, UnitPrice = (float)20.90, StoreId = 12, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 34, Count = 2, UnitPrice = (float)22.90, StoreId = 12, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 35, Count = 1, UnitPrice = (float)20.90, StoreId = 12, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 39, Count = 2, UnitPrice = (float)25.90, StoreId = 12, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 44, Count = 5, UnitPrice = (float)8.90, StoreId = 12, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 48, Count = 2, UnitPrice = (float)17.90, StoreId = 12, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count = 4, UnitPrice = (float)7.50, StoreId = 12, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 49, Count = 3, UnitPrice = (float)12.90, StoreId = 12, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 53, Count = 2, UnitPrice = (float)39.90, StoreId = 12, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 58, Count = 2, UnitPrice = (float)29.90, StoreId = 12, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 59, Count = 2, UnitPrice = (float)27.90, StoreId = 12, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count = 5, UnitPrice = (float)14.90, StoreId = 5, Date = DateTime.Parse("30/03/2021"), Category = Category.Food, ProductOrderValidation = true },

                // JEUDI 25/03
                new ProductOrder { BarCode = 65, Count = 3, UnitPrice = (float)8.90, StoreId = 2, Date = DateTime.Parse("25/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 67, Count = 1, UnitPrice = (float)20.90, StoreId = 2, Date = DateTime.Parse("25/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 68, Count = 2, UnitPrice = (float)15.90, StoreId = 2, Date = DateTime.Parse("25/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 69, Count = 2, UnitPrice = (float)20.90, StoreId = 2, Date = DateTime.Parse("25/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 73, Count = 3, UnitPrice = (float)24.90, StoreId = 2, Date = DateTime.Parse("25/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 95, Count = 3, UnitPrice = (float)4.90, StoreId = 2, Date = DateTime.Parse("25/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count = 2, UnitPrice = (float)24.90, StoreId = 2, Date = DateTime.Parse("25/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 72, Count = 3, UnitPrice = (float)14.90, StoreId = 2, Date = DateTime.Parse("25/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 80, Count = 3, UnitPrice = (float)15.90, StoreId = 2, Date = DateTime.Parse("25/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 79, Count = 2, UnitPrice = (float)14.90, StoreId = 2, Date = DateTime.Parse("25/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 78, Count = 4, UnitPrice = (float)11.90, StoreId = 2, Date = DateTime.Parse("25/03/2021"), Category = Category.Food, ProductOrderValidation = true },

                //DIMANCHE 14 MARS
                new ProductOrder { BarCode = 20, Count = 3, UnitPrice = (float)15.90, StoreId = 4, Date = DateTime.Parse("14/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 19, Count = 2, UnitPrice = (float)8.90, StoreId = 4, Date = DateTime.Parse("14/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 25, Count = 2, UnitPrice = (float)14.90, StoreId = 4, Date = DateTime.Parse("14/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 23, Count = 3, UnitPrice = (float)7.90, StoreId = 4, Date = DateTime.Parse("14/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 26, Count = 3, UnitPrice = (float)10.90, StoreId = 4, Date = DateTime.Parse("14/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 29, Count = 3, UnitPrice = (float)8.90, StoreId = 4, Date = DateTime.Parse("14/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 31, Count = 3, UnitPrice = (float)14.90, StoreId = 4, Date = DateTime.Parse("14/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 36, Count = 2, UnitPrice = (float)30.90, StoreId = 4, Date = DateTime.Parse("14/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 35, Count = 2, UnitPrice = (float)28.90, StoreId = 4, Date = DateTime.Parse("14/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 39, Count = 3, UnitPrice = (float)27.90, StoreId = 4, Date = DateTime.Parse("14/03/2021"), Category = Category.Food, ProductOrderValidation = true },

                //LUNDI 08 MARS
                new ProductOrder { BarCode = 32, Count = 2, UnitPrice = (float)20.90, StoreId = 8, Date = DateTime.Parse("08/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 85, Count = 1, UnitPrice = (float)15.90, StoreId = 8, Date = DateTime.Parse("08/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 86, Count = 3, UnitPrice = (float)24.90, StoreId = 8, Date = DateTime.Parse("08/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 102, Count = 2, UnitPrice = (float)22.90, StoreId = 8, Date = DateTime.Parse("08/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 101, Count = 3, UnitPrice = (float)22.90, StoreId = 8, Date = DateTime.Parse("08/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 46, Count = 3, UnitPrice = (float)27.90, StoreId = 8, Date = DateTime.Parse("08/03/2021"), Category = Category.Food, ProductOrderValidation = true },


                //MERCREDI 03 MARS 
                new ProductOrder { BarCode = 67, Count = 1, UnitPrice = (float)20.90, StoreId = 2, Date = DateTime.Parse("03/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 68, Count = 2, UnitPrice = (float)15.90, StoreId = 2, Date = DateTime.Parse("03/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 69, Count = 2, UnitPrice = (float)20.90, StoreId = 2, Date = DateTime.Parse("03/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 73, Count = 5, UnitPrice = (float)24.90, StoreId = 2, Date = DateTime.Parse("03/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 75, Count = 3, UnitPrice = (float)15.90, StoreId = 2, Date = DateTime.Parse("03/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 78, Count = 1, UnitPrice = (float)10.90, StoreId = 2, Date = DateTime.Parse("03/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 84, Count = 1, UnitPrice = (float)14.90, StoreId = 2, Date = DateTime.Parse("03/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 82, Count = 2, UnitPrice = (float)10.90, StoreId = 2, Date = DateTime.Parse("03/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 88, Count = 1, UnitPrice = (float)10.90, StoreId = 2, Date = DateTime.Parse("03/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 91, Count = 2, UnitPrice = (float)42.90, StoreId = 2, Date = DateTime.Parse("03/03/2021"), Category = Category.Food, ProductOrderValidation = true },

                //VENDREDI MARS 
                new ProductOrder { BarCode = 77, Count = 5, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("06/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count = 1, UnitPrice = (float)31.90, StoreId = 13, Date = DateTime.Parse("06/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count = 2, UnitPrice = (float)31.90, StoreId = 13, Date = DateTime.Parse("13/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count = 2, UnitPrice = (float)31.90, StoreId = 13, Date = DateTime.Parse("13/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count = 4, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("20/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count = 3, UnitPrice = (float)10.90, StoreId = 13, Date = DateTime.Parse("20/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 25, Count = 5, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("27/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count = 5, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("27/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 71, Count = 2, UnitPrice = (float)9.90, StoreId = 13, Date = DateTime.Parse("27/03/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 82, Count = 2, UnitPrice = (float)10.90, StoreId = 2, Date = DateTime.Parse("06/03/2021"), Category = Category.Food, ProductOrderValidation = true },

                // MOIS DE FEVRIER 2021 
                //jeudi 25 fevrier 
                new ProductOrder { BarCode = 47, Count = 2, UnitPrice = (float)15.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 53, Count = 1, UnitPrice = (float)40.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 49, Count = 3, UnitPrice = (float)15.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 57, Count = 2, UnitPrice = (float)18.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 61, Count = 4, UnitPrice = (float)13.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 63, Count = 2, UnitPrice = (float)24.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 65, Count = 3, UnitPrice = (float)9.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 71, Count = 1, UnitPrice = (float)9.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 74, Count = 2, UnitPrice = (float)24.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 73, Count = 2, UnitPrice = (float)24.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count = 5, UnitPrice = (float)14.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 81, Count = 1, UnitPrice = (float)30.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 89, Count = 2, UnitPrice = (float)8.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count = 2, UnitPrice = (float)30.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 99, Count = 3, UnitPrice = (float)25.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 106, Count = 1, UnitPrice = (float)27.90, StoreId = 10, Date = DateTime.Parse("25/02/2021"), Category = Category.Food, ProductOrderValidation = true },


                // MARDI 16 FEVRIER
                new ProductOrder { BarCode = 6, Count = 3, UnitPrice = (float)9.90, StoreId = 9, Date = DateTime.Parse("16/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 8, Count = 2, UnitPrice = (float)12.90, StoreId = 9, Date = DateTime.Parse("16/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count = 3, UnitPrice = (float)10.90, StoreId = 9, Date = DateTime.Parse("16/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 16, Count = 1, UnitPrice = (float)6.90, StoreId = 9, Date = DateTime.Parse("16/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 14, Count = 2, UnitPrice = (float)6.90, StoreId = 9, Date = DateTime.Parse("16/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 12, Count = 2, UnitPrice = (float)23.90, StoreId = 9, Date = DateTime.Parse("16/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 29, Count = 2, UnitPrice = (float)8.90, StoreId = 9, Date = DateTime.Parse("16/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 32, Count = 2, UnitPrice = (float)20.90, StoreId = 9, Date = DateTime.Parse("16/02/2021"), Category = Category.Food, ProductOrderValidation = true },


                //MERCREDI 10 FEVRIER
                new ProductOrder { BarCode = 103, Count = 2, UnitPrice = (float)32.90, StoreId = 13, Date = DateTime.Parse("10/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 104, Count = 2, UnitPrice = (float)19.90, StoreId = 13, Date = DateTime.Parse("10/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 105, Count = 1, UnitPrice = (float)16.90, StoreId = 13, Date = DateTime.Parse("10/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 107, Count = 3, UnitPrice = (float)19.90, StoreId = 13, Date = DateTime.Parse("10/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count = 4, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("10/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count = 4, UnitPrice = (float)11.90, StoreId = 13, Date = DateTime.Parse("10/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 16, Count = 1, UnitPrice = (float)8.90, StoreId = 13, Date = DateTime.Parse("10/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 13, Count = 2, UnitPrice = (float)16.90, StoreId = 13, Date = DateTime.Parse("10/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 7, Count = 1, UnitPrice = (float)12.90, StoreId = 13, Date = DateTime.Parse("10/02/2021"), Category = Category.Food, ProductOrderValidation = true },

                //LUNI 01 FEVRIER
                new ProductOrder { BarCode = 23, Count = 3, UnitPrice = (float)7.90, StoreId = 5, Date = DateTime.Parse("01/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 26, Count = 3, UnitPrice = (float)10.90, StoreId = 5, Date = DateTime.Parse("01/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 29, Count = 3, UnitPrice = (float)8.90, StoreId = 5, Date = DateTime.Parse("01/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 31, Count = 3, UnitPrice = (float)14.90, StoreId = 5, Date = DateTime.Parse("01/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 36, Count = 2, UnitPrice = (float)30.90, StoreId = 5, Date = DateTime.Parse("01/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 35, Count = 2, UnitPrice = (float)28.90, StoreId = 5, Date = DateTime.Parse("01/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 39, Count = 3, UnitPrice = (float)27.90, StoreId = 5, Date = DateTime.Parse("01/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 38, Count = 2, UnitPrice = (float)26.90, StoreId = 5, Date = DateTime.Parse("01/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count = 7, UnitPrice = (float)7.50, StoreId = 5, Date = DateTime.Parse("01/02/2021"), Category = Category.Food, ProductOrderValidation = true },


                // VENDREDI MOIS DE FEVRIER 
                new ProductOrder { BarCode = 77, Count = 5, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("05/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count = 1, UnitPrice = (float)31.90, StoreId = 13, Date = DateTime.Parse("05/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count = 2, UnitPrice = (float)31.90, StoreId = 13, Date = DateTime.Parse("12/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count = 2, UnitPrice = (float)31.90, StoreId = 13, Date = DateTime.Parse("12/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count = 4, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("19/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count = 3, UnitPrice = (float)10.90, StoreId = 13, Date = DateTime.Parse("19/02/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 25, Count = 5, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("26/02/2021"), Category = Category.Food, ProductOrderValidation = true },

                // MOIS DE JANVIER 
                // mardi 26 janvier
                new ProductOrder { BarCode = 6, Count = 3, UnitPrice = (float)9.90, StoreId = 9, Date = DateTime.Parse("26/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 8, Count = 2, UnitPrice = (float)12.90, StoreId = 9, Date = DateTime.Parse("26/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count = 3, UnitPrice = (float)10.90, StoreId = 9, Date = DateTime.Parse("26/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 16, Count = 1, UnitPrice = (float)6.90, StoreId = 9, Date = DateTime.Parse("26/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 14, Count = 2, UnitPrice = (float)6.90, StoreId = 9, Date = DateTime.Parse("26/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 12, Count = 2, UnitPrice = (float)23.90, StoreId = 9, Date = DateTime.Parse("26/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 29, Count = 2, UnitPrice = (float)8.90, StoreId = 9, Date = DateTime.Parse("26/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 32, Count = 2, UnitPrice = (float)20.90, StoreId = 9, Date = DateTime.Parse("26/01/2021"), Category = Category.Food, ProductOrderValidation = true },


                // DIMANCHE 17 01
                new ProductOrder { BarCode = 105, Count = 1, UnitPrice = (float)16.90, StoreId = 13, Date = DateTime.Parse("17/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 107, Count = 3, UnitPrice = (float)19.90, StoreId = 13, Date = DateTime.Parse("17/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count = 4, UnitPrice = (float)14.90, StoreId = 13, Date = DateTime.Parse("17/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count = 4, UnitPrice = (float)11.90, StoreId = 13, Date = DateTime.Parse("17/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 16, Count = 1, UnitPrice = (float)8.90, StoreId = 13, Date = DateTime.Parse("17/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 13, Count = 2, UnitPrice = (float)16.90, StoreId = 13, Date = DateTime.Parse("17/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 7, Count = 1, UnitPrice = (float)12.90, StoreId = 13, Date = DateTime.Parse("17/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 8, Count = 1, UnitPrice = (float)12.90, StoreId = 13, Date = DateTime.Parse("17/01/2021"), Category = Category.Food, ProductOrderValidation = true },


                // JEUDI 14 01
                new ProductOrder { BarCode = 2, Count = 1, UnitPrice = (float)6.5, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 3, Count = 1, UnitPrice = (float)9.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count = 3, UnitPrice = (float)10.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 10, Count = 8, UnitPrice = (float)5, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 14, Count = 4, UnitPrice = (float)6.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 18, Count = 2, UnitPrice = (float)18.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 19, Count = 4, UnitPrice = (float)7.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 23, Count = 2, UnitPrice = (float)5.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 25, Count = 4, UnitPrice = (float)12.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 27, Count = 2, UnitPrice = (float)10.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 34, Count = 6, UnitPrice = (float)19.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 37, Count = 1, UnitPrice = (float)19.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 40, Count = 2, UnitPrice = (float)25.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count = 5, UnitPrice = (float)6.50, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 43, Count = 2, UnitPrice = (float)29.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 45, Count = 3, UnitPrice = (float)9.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 46, Count = 3, UnitPrice = (float)29.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 49, Count = 3, UnitPrice = (float)13.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 51, Count = 2, UnitPrice = (float)34.90, StoreId = 6, Date = DateTime.Parse("14/01/2021"), Category = Category.Food, ProductOrderValidation = true },

                //MARDI 05 
                new ProductOrder { BarCode = 35, Count = 1, UnitPrice = (float)20.90, StoreId = 12, Date = DateTime.Parse("05/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 39, Count = 2, UnitPrice = (float)25.90, StoreId = 12, Date = DateTime.Parse("05/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 44, Count = 5, UnitPrice = (float)8.90, StoreId = 12, Date = DateTime.Parse("05/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 48, Count = 2, UnitPrice = (float)17.90, StoreId = 12, Date = DateTime.Parse("05/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 42, Count = 4, UnitPrice = (float)7.50, StoreId = 12, Date = DateTime.Parse("05/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 49, Count = 3, UnitPrice = (float)12.90, StoreId = 12, Date = DateTime.Parse("05/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 53, Count = 2, UnitPrice = (float)39.90, StoreId = 12, Date = DateTime.Parse("05/01/2021"), Category = Category.Food, ProductOrderValidation = true },
                new ProductOrder { BarCode = 58, Count = 2, UnitPrice = (float)29.90, StoreId = 12, Date = DateTime.Parse("05/01/2021"), Category = Category.Food, ProductOrderValidation = true },



                // VENDREDI DE JANVIER
                new ProductOrder { BarCode = 77, Count= 5, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("01/01/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count= 1, UnitPrice= (float) 31.90, StoreId=13, Date=DateTime.Parse("01/01/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count= 2, UnitPrice= (float) 31.90, StoreId=13, Date=DateTime.Parse("08/01/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 92, Count= 2, UnitPrice= (float) 31.90, StoreId=13, Date=DateTime.Parse("08/01/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count= 4, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("15/01/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 9, Count= 3, UnitPrice= (float) 10.90, StoreId=13, Date=DateTime.Parse("15/01/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 25, Count= 5, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("22/01/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count= 5, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("22/01/2021"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 77, Count= 5, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("29/01/2021"), Category = Category.Food , ProductOrderValidation = true },
            };

            //foreach (var po in productOrdersList3)
            //{
            //    dal.AddProductOrderToDB(po);
            //}
            //Console.ReadLine();
            //Console.WriteLine("la fin");
            List<ProductOrder> productOrdersList4 = new List<ProductOrder>
            {
                //FORCAGE ASSOCIATION VERRE PLASTIQUE + ASSIETTE (106 + 107)
                new ProductOrder { BarCode = 107, Count= 1, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("10/12/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 1, UnitPrice= (float) 13.90, StoreId=13, Date=DateTime.Parse("10/12/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 107, Count= 2, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("06/11/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 1, UnitPrice= (float) 13.90, StoreId=13, Date=DateTime.Parse("06/11/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 107, Count= 1, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("25/10/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 2, UnitPrice= (float) 13.90, StoreId=13, Date=DateTime.Parse("25/10/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 107, Count= 2, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("17/09/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 3, UnitPrice= (float) 13.90, StoreId=13, Date=DateTime.Parse("17/09/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 107, Count= 3, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("10/08/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 2, UnitPrice= (float) 13.90, StoreId=13, Date=DateTime.Parse("10/08/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 107, Count= 2, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("22/07/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 4, UnitPrice= (float) 13.90, StoreId=13, Date=DateTime.Parse("22/07/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 107, Count= 1, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("4/06/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 1, UnitPrice= (float) 13.90, StoreId=13, Date=DateTime.Parse("04/06/2020"), Category = Category.Food , ProductOrderValidation = true },
                 new ProductOrder { BarCode = 107, Count= 3, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("18/05/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 1, UnitPrice= (float) 13.90, StoreId=13, Date=DateTime.Parse("18/05/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 107, Count= 1, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("09/04/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 2, UnitPrice= (float) 13.90, StoreId=13, Date=DateTime.Parse("09/04/2020"), Category = Category.Food , ProductOrderValidation = true },
                 new ProductOrder { BarCode = 107, Count= 2, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("20/03/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 1, UnitPrice= (float) 13.90, StoreId=13, Date=DateTime.Parse("20/03/2020"), Category = Category.Food , ProductOrderValidation = true },
                 new ProductOrder { BarCode = 107, Count= 2, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("11/02/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 1, UnitPrice= (float) 13.90, StoreId=13, Date=DateTime.Parse("11/02/2020"), Category = Category.Food , ProductOrderValidation = true },
                 new ProductOrder { BarCode = 107, Count= 1, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("01/01/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 108, Count= 2, UnitPrice= (float) 13.90, StoreId=13, Date=DateTime.Parse("01/01/2020"), Category = Category.Food , ProductOrderValidation = true },

            };

            List<ProductOrder> productOrdersList6 = new List<ProductOrder>
            {
                new ProductOrder { BarCode = 107, Count= 1, UnitPrice= (float) 14.90, StoreId=13, Date=DateTime.Parse("30/06/2020"), Category = Category.Food , ProductOrderValidation = true },
                 new ProductOrder { BarCode = 50, Count= 1, UnitPrice= (float) 21.90, StoreId=13, Date=DateTime.Parse("10/12/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count= 1, UnitPrice= (float) 24.90, StoreId=13, Date=DateTime.Parse("10/12/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 50, Count= 2, UnitPrice= (float) 21.90, StoreId=13, Date=DateTime.Parse("06/11/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count= 1, UnitPrice= (float) 24.90, StoreId=13, Date=DateTime.Parse("06/11/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 50, Count= 1, UnitPrice= (float) 21.90, StoreId=13, Date=DateTime.Parse("25/10/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count= 2, UnitPrice= (float) 24.90, StoreId=13, Date=DateTime.Parse("25/10/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 50, Count= 2, UnitPrice= (float) 21.90, StoreId=13, Date=DateTime.Parse("17/09/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count= 1, UnitPrice= (float) 24.90, StoreId=13, Date=DateTime.Parse("17/09/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 50, Count= 1, UnitPrice= (float) 21.90, StoreId=13, Date=DateTime.Parse("10/08/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count= 2, UnitPrice= (float) 24.90, StoreId=13, Date=DateTime.Parse("10/08/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 50, Count= 2, UnitPrice= (float) 21.90, StoreId=13, Date=DateTime.Parse("22/07/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count= 4, UnitPrice= (float) 24.90, StoreId=13, Date=DateTime.Parse("22/07/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 50, Count= 1, UnitPrice= (float) 21.90, StoreId=13, Date=DateTime.Parse("4/06/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count= 1, UnitPrice= (float) 24.90, StoreId=13, Date=DateTime.Parse("04/06/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 50, Count= 1, UnitPrice= (float) 21.90, StoreId=13, Date=DateTime.Parse("18/05/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 50, Count= 1, UnitPrice= (float) 24.90, StoreId=13, Date=DateTime.Parse("15/04/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 50, Count= 2, UnitPrice= (float) 21.90, StoreId=13, Date=DateTime.Parse("09/04/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count= 2, UnitPrice= (float) 24.90, StoreId=13, Date=DateTime.Parse("09/04/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count= 2, UnitPrice= (float) 21.90, StoreId=13, Date=DateTime.Parse("02/12/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count= 1, UnitPrice= (float) 24.90, StoreId=13, Date=DateTime.Parse("20/03/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 50, Count= 2, UnitPrice= (float) 21.90, StoreId=13, Date=DateTime.Parse("11/02/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count= 1, UnitPrice= (float) 24.90, StoreId=13, Date=DateTime.Parse("11/02/2020"), Category = Category.Food , ProductOrderValidation = true },
                 new ProductOrder { BarCode = 62, Count= 1, UnitPrice= (float) 21.90, StoreId=13, Date=DateTime.Parse("14/12/2020"), Category = Category.Food , ProductOrderValidation = true },
                new ProductOrder { BarCode = 62, Count= 2, UnitPrice= (float) 24.90, StoreId=13, Date=DateTime.Parse("01/01/2020"), Category = Category.Food , ProductOrderValidation = true },


            };


            // LIST CLOTHES !!!!!!!

            List<ProductOrder> productOrdersList7 = new List<ProductOrder>
            {
                new ProductOrder { BarCode = 109, Count= 1, UnitPrice= (float) 103, StoreId=14, Date=DateTime.Parse("02/08/2021"), ProductOrderValidation = false },
                new ProductOrder { BarCode = 117, Count= 1,  UnitPrice= (float) 79, StoreId=14, Date=DateTime.Parse("02/08/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 126, Count= 3,  UnitPrice= (float) 119.90, StoreId=18, Date=DateTime.Parse("12/08/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 136, Count= 2,  UnitPrice= (float) 119.90, StoreId= 26, Date=DateTime.Parse("15/08/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 146, Count= 4,  UnitPrice= (float) 14.90, StoreId= 26, Date=DateTime.Parse("15/08/2021"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 129, Count= 4,  UnitPrice= (float) 185.50, StoreId=21, Date=DateTime.Parse("12/07/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 130, Count= 3,  UnitPrice= (float) 119.90, StoreId=21, Date=DateTime.Parse("12/07/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 134, Count= 2,  UnitPrice= (float) 137.50, StoreId= 17, Date=DateTime.Parse("21/07/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 135, Count= 2,  UnitPrice= (float) 219, StoreId= 21, Date=DateTime.Parse("01/07/2021"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 131, Count= 2,  UnitPrice= (float) 99.90, StoreId=17, Date=DateTime.Parse("23/06/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 121, Count= 2, UnitPrice= (float) 109, StoreId=18, Date=DateTime.Parse("10/06/2021"), Category = Category.Clothes, ProductOrderValidation = false },
                new ProductOrder { BarCode = 148, Count= 3,  UnitPrice= (float) 13.90, StoreId= 15, Date=DateTime.Parse("10/06/2021"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 113, Count= 1,  UnitPrice= (float) 60, StoreId= 14, Date=DateTime.Parse("26/05/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 139, Count= 2,  UnitPrice= (float) 119.90, StoreId= 16, Date=DateTime.Parse("16/05/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 145, Count= 2,  UnitPrice= (float) 32.5, StoreId= 19, Date=DateTime.Parse("04/05/2021"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 132, Count= 1,  UnitPrice= (float) 152.50, StoreId= 19, Date=DateTime.Parse("13/04/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 137, Count= 1,  UnitPrice= (float) 219.90, StoreId= 20, Date=DateTime.Parse("18/04/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 138, Count= 2,  UnitPrice= (float) 169.50, StoreId= 15, Date=DateTime.Parse("28/04/2021"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 114, Count= 2, UnitPrice= (float) 150, StoreId=21, Date=DateTime.Parse("15/03/2021"), Category = Category.Clothes, ProductOrderValidation = false },
                new ProductOrder { BarCode = 123, Count= 1,  UnitPrice= (float) 250, StoreId=21, Date=DateTime.Parse("15/03/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 141, Count= 4,  UnitPrice= (float) 12.90, StoreId= 22, Date=DateTime.Parse("03/03/2021"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 133, Count= 4,  UnitPrice= (float) 134.50, StoreId= 20, Date=DateTime.Parse("14/02/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 144, Count= 3,  UnitPrice= (float) 29.90, StoreId= 15, Date=DateTime.Parse("03/02/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 142, Count= 4,  UnitPrice= (float) 15.90, StoreId= 23, Date=DateTime.Parse("22/02/2021"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 148, Count= 3,  UnitPrice= (float) 13.90, StoreId= 15, Date=DateTime.Parse("07/01/2021"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 120, Count= 2, UnitPrice= (float) 215.90, StoreId=19, Date=DateTime.Parse("07/01/2021"), Category = Category.Clothes, ProductOrderValidation = false },
                new ProductOrder { BarCode = 140, Count= 2,  UnitPrice= (float) 311.50, StoreId= 19, Date=DateTime.Parse("20/01/2021"), Category = Category.Clothes , ProductOrderValidation = false },




                new ProductOrder { BarCode = 115, Count= 3,  UnitPrice= (float) 259.90, StoreId=17, Date=DateTime.Parse("14/12/2020"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 116, Count= 1, UnitPrice= (float) 120, StoreId=17, Date=DateTime.Parse("14/12/2020"), Category = Category.Clothes, ProductOrderValidation = false },
                new ProductOrder { BarCode = 146, Count= 5,  UnitPrice= (float) 14.90, StoreId= 16, Date=DateTime.Parse("01/12/2020"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 127, Count= 2,  UnitPrice= (float) 119.90, StoreId= 16, Date=DateTime.Parse("15/10/2020"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 130, Count= 3, UnitPrice= (float) 139.90, StoreId=16, Date=DateTime.Parse("15/10/2020"), Category = Category.Clothes, ProductOrderValidation = false },
                new ProductOrder { BarCode = 143, Count= 2,  UnitPrice= (float) 29.90, StoreId= 22, Date=DateTime.Parse("29/10/2020"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 110, Count= 3,  UnitPrice= (float) 89.90, StoreId=19, Date=DateTime.Parse("24/08/2020"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 111, Count= 2,  UnitPrice= (float) 89.90, StoreId=19, Date=DateTime.Parse("24/08/2020"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 147, Count= 5,  UnitPrice= (float) 12.90, StoreId= 14, Date=DateTime.Parse("12/08/2020"), Category = Category.Clothes , ProductOrderValidation = false },


                new ProductOrder { BarCode = 147, Count= 3,  UnitPrice= (float) 12.90, StoreId= 14, Date=DateTime.Parse("05/07/2020"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 124, Count= 3,  UnitPrice= (float) 72.90, StoreId=15, Date=DateTime.Parse("17/06/2020"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 128, Count= 1,  UnitPrice= (float) 119.90, StoreId=15, Date=DateTime.Parse("17/06/2020"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 148, Count= 3,  UnitPrice= (float) 13.90, StoreId= 15, Date=DateTime.Parse("03/05/2020"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 112, Count= 3,  UnitPrice= (float) 39.90, StoreId=23, Date=DateTime.Parse("14/05/2020"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 118, Count= 2,  UnitPrice= (float) 49.90, StoreId=22, Date=DateTime.Parse("06/04/2020"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 119, Count= 1,  UnitPrice= (float) 59.50, StoreId=22, Date=DateTime.Parse("06/04/2020"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 147, Count= 1,  UnitPrice= (float) 12.90, StoreId= 14, Date=DateTime.Parse("09/03/2020"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 122, Count= 3,  UnitPrice= (float) 352.50, StoreId=20, Date=DateTime.Parse("11/02/2020"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 147, Count= 2,  UnitPrice= (float) 12.90, StoreId= 14, Date=DateTime.Parse("27/02/2020"), Category = Category.Clothes , ProductOrderValidation = false },

                new ProductOrder { BarCode = 125, Count= 3,  UnitPrice= (float) 80, StoreId=14, Date=DateTime.Parse("13/01/2020"), Category = Category.Clothes , ProductOrderValidation = false },
                new ProductOrder { BarCode = 146, Count= 5,  UnitPrice= (float) 14.90, StoreId= 16, Date=DateTime.Parse("09/01/2020"), Category = Category.Clothes , ProductOrderValidation = false },

            };

            List<ProductOrder> productOrdersList8 = new List<ProductOrder>
            {

                    //BEAUTY
                    new ProductOrder { BarCode = 149, Count= 3,  UnitPrice= (float) 45.90, StoreId=24, Date=DateTime.Parse("17/08/2021"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 150, Count= 2,  UnitPrice= (float) 110, StoreId= 24, Date=DateTime.Parse("17/08/2021"), Category = Category.Beauty , ProductOrderValidation = true },
                    new ProductOrder { BarCode = 167, Count= 1,  UnitPrice= (float) 415.5, StoreId=27, Date=DateTime.Parse("08/08/2021"), Category = Category.Beauty, ProductOrderValidation = true },


                    new ProductOrder { BarCode = 151, Count= 2,  UnitPrice= (float) 119.90, StoreId=25, Date=DateTime.Parse("18/07/2021"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 149, Count= 1,  UnitPrice= (float) 45.90, StoreId= 25, Date=DateTime.Parse("18/07/2021"), Category = Category.Beauty , ProductOrderValidation = true },
                    new ProductOrder { BarCode = 170, Count= 1,  UnitPrice= (float) 450.5, StoreId= 28, Date=DateTime.Parse("08/07/2021"), Category = Category.Beauty , ProductOrderValidation = true },


                    new ProductOrder { BarCode = 152, Count= 2,  UnitPrice= (float) 160, StoreId=26, Date=DateTime.Parse("10/06/2021"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 153, Count= 1,  UnitPrice= (float) 215, StoreId= 26, Date=DateTime.Parse("10/06/2021"), Category = Category.Beauty , ProductOrderValidation = true },
                    new ProductOrder { BarCode = 171, Count= 1,  UnitPrice= (float) 390.9, StoreId= 25, Date=DateTime.Parse("20/06/2021"), Category = Category.Beauty , ProductOrderValidation = true },

                    new ProductOrder { BarCode = 172, Count= 1,  UnitPrice= (float) 470, StoreId= 31, Date=DateTime.Parse("14/05/2021"), Category = Category.Beauty , ProductOrderValidation = true },
                    new ProductOrder { BarCode = 173, Count= 1,  UnitPrice= (float) 390.5, StoreId= 31, Date=DateTime.Parse("14/05/2021"), Category = Category.Beauty , ProductOrderValidation = true },
                    new ProductOrder { BarCode = 178, Count= 4,  UnitPrice= (float) 9.90, StoreId= 30, Date=DateTime.Parse("27/05/2021"), Category = Category.Beauty , ProductOrderValidation = true },


                    new ProductOrder { BarCode = 154, Count= 2,  UnitPrice= (float) 219.5, StoreId=27, Date=DateTime.Parse("11/04/2021"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 155, Count= 2,  UnitPrice= (float) 177.5, StoreId= 27, Date=DateTime.Parse("11/04/2021"), Category = Category.Beauty , ProductOrderValidation = true },
                    new ProductOrder { BarCode = 174, Count= 1,  UnitPrice= (float) 410.5, StoreId= 28, Date=DateTime.Parse("21/04/2021"), Category = Category.Beauty , ProductOrderValidation = true },


                    new ProductOrder { BarCode = 156, Count= 1,  UnitPrice= (float) 210, StoreId= 28, Date=DateTime.Parse("17/03/2021"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 157, Count= 3,  UnitPrice= (float) 187.9, StoreId= 28, Date=DateTime.Parse("17/03/2021"), Category = Category.Beauty , ProductOrderValidation = true },
                    new ProductOrder { BarCode = 175, Count= 1,  UnitPrice= (float) 435.9, StoreId= 27, Date=DateTime.Parse("02/03/2021"), Category = Category.Beauty , ProductOrderValidation = true },


                    new ProductOrder { BarCode = 158, Count= 2,  UnitPrice= (float) 130.90, StoreId= 29, Date=DateTime.Parse("16/02/2021"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 159, Count= 2,  UnitPrice= (float) 230, StoreId= 29, Date=DateTime.Parse("16/02/2021"), Category = Category.Beauty , ProductOrderValidation = true },
                     new ProductOrder { BarCode = 176, Count= 1,  UnitPrice= (float) 490, StoreId= 30, Date=DateTime.Parse("04/02/2021"), Category = Category.Beauty , ProductOrderValidation = true },


                    new ProductOrder { BarCode = 160, Count= 2,  UnitPrice= (float) 140.90 , StoreId=30, Date=DateTime.Parse("04/01/2021"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 161, Count= 1,  UnitPrice= (float) 390.5, StoreId= 30, Date=DateTime.Parse("04/01/2021"), Category = Category.Beauty , ProductOrderValidation = true },
                    new ProductOrder { BarCode = 176, Count= 1,  UnitPrice= (float) 490, StoreId= 30, Date=DateTime.Parse("04/01/2021"), Category = Category.Beauty , ProductOrderValidation = true },

                    new ProductOrder { BarCode = 162, Count= 1,  UnitPrice= (float) 360.9, StoreId= 31, Date=DateTime.Parse("17/12/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 163, Count= 2,  UnitPrice= (float) 310, StoreId= 31, Date=DateTime.Parse("17/12/2020"), Category = Category.Beauty , ProductOrderValidation = true },

                    new ProductOrder { BarCode = 164, Count= 3,  UnitPrice= (float) 110, StoreId=32, Date=DateTime.Parse("30/11/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 165, Count= 2,  UnitPrice= (float) 169, StoreId= 32, Date=DateTime.Parse("30/11/2020"), Category = Category.Beauty , ProductOrderValidation = true },
                    new ProductOrder { BarCode = 180, Count= 2,  UnitPrice= (float) 14.90, StoreId= 24, Date=DateTime.Parse("01/11/2020"), Category = Category.Beauty , ProductOrderValidation = true },

                     new ProductOrder { BarCode = 166, Count= 5,  UnitPrice= (float) 24.50, StoreId= 24, Date=DateTime.Parse("19/10/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 167, Count= 3,  UnitPrice= (float) 35.90, StoreId= 24, Date=DateTime.Parse("19/10/2020"), Category = Category.Beauty , ProductOrderValidation = true },

                    new ProductOrder { BarCode = 168, Count= 3,  UnitPrice= (float) 29.90, StoreId=25, Date=DateTime.Parse("21/09/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 149, Count= 3,  UnitPrice= (float) 45.90, StoreId= 25, Date=DateTime.Parse("21/09/2020"), Category = Category.Beauty , ProductOrderValidation = true },

                    new ProductOrder { BarCode = 150, Count= 2,  UnitPrice= (float) 110, StoreId=26, Date=DateTime.Parse("19/08/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 151, Count= 1,  UnitPrice= (float) 119.90, StoreId= 26, Date=DateTime.Parse("19/08/2020"), Category = Category.Beauty , ProductOrderValidation = true },

                    new ProductOrder { BarCode = 152, Count= 1,  UnitPrice= (float) 160, StoreId=27, Date=DateTime.Parse("13/07/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 153, Count= 2,  UnitPrice= (float) 215, StoreId= 27, Date=DateTime.Parse("13/07/2020"), Category = Category.Beauty , ProductOrderValidation = true },

                    new ProductOrder { BarCode = 154, Count= 1,  UnitPrice= (float) 219.5, StoreId=28, Date=DateTime.Parse("11/06/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 155, Count= 2,  UnitPrice= (float) 177.5, StoreId= 28, Date=DateTime.Parse("11/06/2020"), Category = Category.Beauty , ProductOrderValidation = true },

                    new ProductOrder { BarCode = 179, Count= 2,  UnitPrice= (float) 11.90, StoreId= 25, Date=DateTime.Parse("07/05/2020"), Category = Category.Beauty , ProductOrderValidation = true },
                    new ProductOrder { BarCode = 156, Count= 2,  UnitPrice= (float) 210, StoreId=29, Date=DateTime.Parse("10/05/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 157, Count= 2,  UnitPrice= (float) 187.9, StoreId= 29, Date=DateTime.Parse("10/05/2020"), Category = Category.Beauty , ProductOrderValidation = true },


                    new ProductOrder { BarCode = 158, Count= 2,  UnitPrice= (float) 130.90, StoreId=30, Date=DateTime.Parse("08/04/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 159, Count= 1,  UnitPrice= (float) 230, StoreId= 30, Date=DateTime.Parse("08/04/2020"), Category = Category.Beauty , ProductOrderValidation = true },

                    new ProductOrder { BarCode = 160, Count= 2,  UnitPrice= (float) 140.90, StoreId=30, Date=DateTime.Parse("12/03/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 161, Count= 2,  UnitPrice= (float) 390.5, StoreId= 30, Date=DateTime.Parse("12/03/2020"), Category = Category.Beauty , ProductOrderValidation = true },

                    new ProductOrder { BarCode = 162, Count= 1,  UnitPrice= (float) 360.9, StoreId=31, Date=DateTime.Parse("13/02/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 163, Count= 1,  UnitPrice= (float) 310, StoreId= 31, Date=DateTime.Parse("13/02/2020"), Category = Category.Beauty , ProductOrderValidation = true },

                    new ProductOrder { BarCode = 164, Count= 3,  UnitPrice= (float) 110, StoreId=32, Date=DateTime.Parse("03/01/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 165, Count= 1,  UnitPrice= (float) 169, StoreId= 32, Date=DateTime.Parse("03/01/2020"), Category = Category.Beauty , ProductOrderValidation = true },
                    new ProductOrder { BarCode = 177, Count= 2,  UnitPrice= (float) 19.90, StoreId= 31, Date=DateTime.Parse("20/01/2020"), Category = Category.Beauty , ProductOrderValidation = true },

                    new ProductOrder { BarCode = 166, Count= 4,  UnitPrice= (float) 24.50, StoreId=24, Date=DateTime.Parse("04/08/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 167, Count= 4,  UnitPrice= (float) 35.90, StoreId= 24, Date=DateTime.Parse("04/08/2020"), Category = Category.Beauty , ProductOrderValidation = true },
                    new ProductOrder { BarCode = 168, Count= 2,  UnitPrice= (float) 29.90, StoreId=24, Date=DateTime.Parse("04/08/2020"), Category = Category.Beauty, ProductOrderValidation = true },
                    

                    //APPLIANCES
                    new ProductOrder { BarCode = 181, Count= 2,  UnitPrice= (float) 79.90, StoreId=36, Date=DateTime.Parse("16/08/2021"), Category = Category.Appliances, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 185, Count= 1,  UnitPrice= (float) 234.5, StoreId=37, Date=DateTime.Parse("01/08/2021"), Category = Category.Appliances, ProductOrderValidation = true },

                    new ProductOrder { BarCode = 192, Count= 1,  UnitPrice= (float) 2188, StoreId=38, Date=DateTime.Parse("19/07/2021"), Category = Category.Appliances, ProductOrderValidation = true },


                    new ProductOrder { BarCode = 182, Count= 1,  UnitPrice= (float) 255.9, StoreId=33, Date=DateTime.Parse("08/06/2021"), Category = Category.Appliances, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 184, Count= 2,  UnitPrice= (float) 166, StoreId=35, Date=DateTime.Parse("22/06/2021"), Category = Category.Appliances, ProductOrderValidation = true },

                    new ProductOrder { BarCode = 190, Count= 1,  UnitPrice= (float) 429, StoreId=37, Date=DateTime.Parse("13/05/2021"), Category = Category.Appliances, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 183, Count= 2,  UnitPrice= (float) 189, StoreId=33, Date=DateTime.Parse("23/05/2021"), Category = Category.Appliances, ProductOrderValidation = true },

                    new ProductOrder { BarCode = 193, Count= 1,  UnitPrice= (float) 333, StoreId=35, Date=DateTime.Parse("13/04/2021"), Category = Category.Appliances, ProductOrderValidation = true },

                    new ProductOrder { BarCode = 186, Count= 1,  UnitPrice= (float) 2333.5, StoreId=34, Date=DateTime.Parse("07/01/2021"), Category = Category.Appliances, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 189, Count= 1,  UnitPrice= (float) 1444, StoreId=35, Date=DateTime.Parse("12/01/2021"), Category = Category.Appliances, ProductOrderValidation = true },

                    new ProductOrder { BarCode = 187, Count= 1,  UnitPrice= (float) 7648, StoreId=34, Date=DateTime.Parse("16/02/2021"), Category = Category.Appliances, ProductOrderValidation = true },

                    new ProductOrder { BarCode = 188, Count= 1,  UnitPrice= (float) 2149, StoreId=36, Date=DateTime.Parse("18/03/2021"), Category = Category.Appliances, ProductOrderValidation = true },

                    new ProductOrder { BarCode = 191, Count= 1,  UnitPrice= (float) 3298, StoreId=38, Date=DateTime.Parse("31/12/2020"), Category = Category.Appliances, ProductOrderValidation = true },

                    //MULTIMEDIA
                    new ProductOrder { BarCode = 194, Count= 1,  UnitPrice= (float) 1099, StoreId=39, Date=DateTime.Parse("3/08/2021"), Category = Category.Multimedia, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 195, Count= 1,  UnitPrice= (float) 749, StoreId=40, Date=DateTime.Parse("04/07/2021"), Category = Category.Multimedia, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 196, Count= 1,  UnitPrice= (float) 899, StoreId=41, Date=DateTime.Parse("17/06/2021"), Category = Category.Multimedia, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 197, Count= 1,  UnitPrice= (float) 2849, StoreId=33, Date=DateTime.Parse("19/05/2021"), Category = Category.Multimedia, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 198, Count= 1,  UnitPrice= (float) 279, StoreId=35, Date=DateTime.Parse("12/04/2021"), Category = Category.Multimedia, ProductOrderValidation = true },
                    new ProductOrder { BarCode = 199, Count= 1,  UnitPrice= (float) 271, StoreId=39, Date=DateTime.Parse("03/02/2021"), Category = Category.Multimedia, ProductOrderValidation = true },
                };

            //foreach (var item in productOrdersList8)
            //{
            //    dal.AddProductOrderToDB(item);
            //}


            // a partir de bar code 199 on a pas rentrer 


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


