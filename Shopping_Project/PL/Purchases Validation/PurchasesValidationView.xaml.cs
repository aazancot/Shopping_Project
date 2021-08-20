using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;

namespace PL.Purchases_Validation
{
    /// <summary>
    /// Interaction logic for PurchasesValidationView.xaml
    /// </summary>
    public partial class PurchasesValidationView : UserControl
    {
        public PurchasesValidationViewModel purchasesValidationViewModel;
        public PurchasesValidationView()
        {
            InitializeComponent();
            purchasesValidationViewModel = new PurchasesValidationViewModel();
            DataContext = purchasesValidationViewModel;
           
        }

        
        private void Button_OpenImageFile(object sender, RoutedEventArgs e)
        {
            string imagePath= purchasesValidationViewModel.OpenImageFile();
            if(imagePath != null)
            {

                ProductOrder selectedPO = (sender as Button).DataContext as BE.ProductOrder;
                purchasesValidationViewModel.UpdateProduct(imagePath, selectedPO.BarCode);
               
            }
            liste.Items.Refresh();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductOrder selectedPO = (BE.ProductOrder) (sender as ComboBox).DataContext;

            if (selectedPO != null)
            {
                //MessageBox.Show(selectedPO.Category.ToString());
                //BE.Category curCategory = (BE.Category)(sender as ComboBox).SelectedItem;
                //MessageBox.Show(curCategory.ToString());
                //selectedPO.Category = curCategory;
                //MessageBox.Show(selectedPO.Category.ToString());
               // purchasesValidationViewModel.UpdateProductOrder(selectedPO);
                //liste.Items.Refresh();
            }
            
        }

        private void Button_DeleteProductOrder(object sender, RoutedEventArgs e)
        {
            ProductOrder selectedPO = (sender as Button).DataContext as BE.ProductOrder;
            purchasesValidationViewModel.DeleteProductOrder(selectedPO);
            //liste.Items.Refresh();
        }

     

        private void Button_ValidateProductOrder(object sender, RoutedEventArgs e)
        {
            ProductOrder selectedPO = (sender as Button).DataContext as BE.ProductOrder;
            selectedPO.ProductOrderValidation = true;
            purchasesValidationViewModel.ValidateProductOrder(selectedPO);
            //liste.Items.Refresh();
        }

        private void Button_AllValidate(object sender, RoutedEventArgs e)
        {
     
            foreach(BE.ProductOrder PO in liste.Items)
            {
                PO.ProductOrderValidation = true;
                purchasesValidationViewModel.UpdateProductOrder(PO);
            }

            purchasesValidationViewModel.NewProductOrdersNoValidate.Clear();


        }
    }
}
