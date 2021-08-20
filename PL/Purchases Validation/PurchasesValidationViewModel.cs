using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BE;
using PL.Commands;

namespace PL.Purchases_Validation
{
    public class PurchasesValidationViewModel
    {
         
        public PurchasesValidationModel PurchasesValidationModel { get; set; }
        public ObservableCollection<ProductOrder> NewProductOrdersNoValidate { get; set; }
  
        public PurchasesValidationViewModel()
        {
            PurchasesValidationModel = new PurchasesValidationModel();
            NewProductOrdersNoValidate = new ObservableCollection<ProductOrder>(PurchasesValidationModel.SaveNewProductOrdersFromQrCode());

        }


        public void DeleteProductOrder(ProductOrder productOrder)
        {
            NewProductOrdersNoValidate.Remove(productOrder);
            PurchasesValidationModel.DeleteProductOrder(productOrder.ProductOrderId);
        }

        public ICommand DeleteProductOrderCommand
        {
            get
            {
                return new DeleteProductOrderCommand(this);
            }
        }
     

        public void ValidateProductOrder(ProductOrder productOrder)
        {
            productOrder.ProductOrderValidation = true;
            NewProductOrdersNoValidate.Remove(productOrder);
            PurchasesValidationModel.UpdateProductOrder(productOrder);
        }

        public ICommand ValidateProductOrderCommand
        {
            get
            {
                return new ValidateProductOrderCommand(this);
            }
        }

        public void UpdateProductOrder(ProductOrder productOrder)
        {
            PurchasesValidationModel.UpdateProductOrder(productOrder);

        }
        public void UpdateProduct( string imagePath, int barCode)
        {
            BE.Product product = PurchasesValidationModel.GetProduct(x => x.BarCode == barCode);
            product.ImageFile = imagePath;
            PurchasesValidationModel.UpdateProduct(product);
        }

        public string OpenImageFile()
        {
            string filename = null;
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension ; *.PNG 
            dlg.DefaultExt = ".png";
            dlg.Filter = "Image Files(*.PNG; *.JPG; *.JPEG)| *.PNG; *.JPG; *.JPEG| All files(*.*) | *.*";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                
                // Open document 
                filename = dlg.FileName;
            }
            return filename;
        }

    }
}
