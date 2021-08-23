using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;
using System.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.Data;

using System.IO;
using System.ComponentModel;

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

        public List<BE.Store> GetAllStores(Func<BE.Store, bool> predicate = null)
        {
            using (var ctx = new ShoppingDB())
            {
                if (predicate == null)
                {
                    return ctx.Stores.ToList();
                }
                else
                {

                    return ctx.Stores.Where(predicate).ToList();
                }
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


        public void DeleteProductOrder(int productOrderId)
        {
            using (var ctx = new ShoppingDB())
            {
                var y = (from x in ctx.ProductOrders where x.ProductOrderId == productOrderId select x).First();
                ctx.ProductOrders.Remove(y);
                ctx.SaveChanges();
              
            }

        }

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

        #region Clear
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

        #endregion Clear

        #region PDF
        public void CreatePDF(List<object[]> items)
        {
            //Create a new PDF document.
            PdfDocument doc = new PdfDocument();
            //Add a page.
            PdfPage page = doc.Pages.Add();
            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Loads the image from disk
            // string CSPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
            //string ImagePath = Path.Combine(CSPath, @"DAL\Images\LongLogo.jpeg");
            //string ImagePath = Path.Combine(CSPath, @"DAL\multimedia.jpg");
            // PdfImage image = PdfImage.FromFile(ImagePath);
            PdfImage image = PdfImage.FromFile("C:\\Shopping_Project\\Help pictures\\logoShop.jpg");
            RectangleF bounds = new RectangleF(50, 0, 400, 150);
            //Draws the image to the PDF page
            page.Graphics.DrawImage(image, bounds);
            Font font = new Font("Nirmala UI", 13);
            PdfFont pdfFont = new PdfTrueTypeFont(font, true);
            PdfStringFormat format = new PdfStringFormat();

            //Set left to right text direction for RTL text
            format.TextDirection = PdfTextDirection.LeftToRight ;
            format.Alignment = PdfTextAlignment.Left;

            //Draw grid to the page of PDF document.
            PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            bounds = new RectangleF(0, bounds.Bottom + 90, page.Graphics.ClientSize.Width, 30);
            //Draws a rectangle to place the heading in that region.
            //page.Graphics.DrawRectangle(solidBrush, bounds);
            //Creates a text element to add the invoice number

            page.Graphics.DrawString( "LIST OF PRODUCT RECOMMENDATIONS", pdfFont, PdfBrushes.DarkRed, new PointF(165, 260));




            PdfTextElement element = new PdfTextElement("", pdfFont);
            element.Brush = PdfBrushes.White;
            //Draws the heading on the page
            PdfLayoutResult result = element.Draw(page, new PointF(10, bounds.Top + 8));
            string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
            //Measures the width of the text to place it in the correct location
            SizeF textSize = pdfFont.MeasureString(currentDate);
            PointF textPosition = new PointF(0, 0);
            //Draws the date by using DrawString method
            page.Graphics.DrawString(currentDate, pdfFont, element.Brush, textPosition, format);
            //Creates text elements to add the address and draw it to the page.
            element = new PdfTextElement("List of product recommendations:", pdfFont);
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            element.StringFormat = format;
            result = element.Draw(page, new PointF(page.Graphics.ClientSize.Width - textSize.Width + 70, result.Bounds.Bottom + 25));
            PdfPen linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
            PointF startPoint = new PointF(0, result.Bounds.Bottom + 3);
            PointF endPoint = new PointF(page.Graphics.ClientSize.Width, result.Bounds.Bottom + 3);
            //Draws a line at the bottom of the address
            page.Graphics.DrawLine(linePen, startPoint, endPoint);



            //Create a DataTable.
            DataTable dataTable = new DataTable();
            //Add columns to the DataTable
            dataTable.Columns.Add("Product Name");
            dataTable.Columns.Add("Product Description");
            dataTable.Columns.Add("Recently Less Expensive in :");
            dataTable.Columns.Add("Product Price");
            //Add rows to the DataTable.
            foreach (var item in items)
            {
                dataTable.Rows.Add(item);
            }

            //Creates the datasource for the table
            DataTable Details = dataTable;
            //Creates a PDF grid
            PdfGrid grid = new PdfGrid();
            grid.Style.Font = pdfFont;

            //Adds the data source
            grid.DataSource = Details;
            //Creates the grid cell styles
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            //PdfGridRow header = grid.Headers[0];
            //Creates the header style
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = pdfFont;
            headerStyle.StringFormat = format;
            //Applies the header style
            grid.Headers[0].ApplyStyle(headerStyle);
            foreach (PdfGridColumn Column in grid.Columns)
            {
                Column.Format = format;
            }
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = pdfFont;
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));
            cellStyle.StringFormat = format;


            //Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;
            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(page.Graphics.ClientSize.Width, page.Graphics.ClientSize.Height - 100)), layoutFormat);

            pdfGrid.Draw(page, new PointF(10, 10));

            doc.Save("Output.pdf");
            //Save the document.
            try
            {
               
                System.Diagnostics.Process.Start("Output.pdf");
                doc.Close(true);
            }
            catch (Win32Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //close the document
            //doc.Close(true);
        }

        #endregion PDF


    }
}
