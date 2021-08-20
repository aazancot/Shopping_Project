using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL;
using BE;

namespace PL.Analytics
{
    public class AnalyticsByItemsViewModel
    {
        
        public AnalyticsByItemsModel AnalyticsByItemsModel { get; set; }
        public ObservableCollection<Product> AllBarCodesByFood { get; set; }
        public ObservableCollection<Product> AllBarCodesByBeauty{ get; set; }
        public ObservableCollection<Product> AllBarCodesByClothes { get; set; }
        public ObservableCollection<Product> AllBarCodesByAppliances { get; set; }
        public ObservableCollection<Product> AllBarCodesByMultimedia { get; set; }
        public ObservableCollection<ChartLineSeries3DModel> QuantityPerMonthByItem { get; set; }
        public ObservableCollection<ChartLineSeries3DModel> HistogramList { get; set; }
        public DateTime SelectedDate { get; set; }


        public AnalyticsByItemsViewModel()
        {
            AnalyticsByItemsModel = new AnalyticsByItemsModel();
            AllBarCodesByFood = new ObservableCollection<Product>(AnalyticsByItemsModel.GetAllBarCodesByCategory("Food"));
            AllBarCodesByBeauty = new ObservableCollection<Product>(AnalyticsByItemsModel.GetAllBarCodesByCategory("Beauty"));
            AllBarCodesByClothes = new ObservableCollection<Product>(AnalyticsByItemsModel.GetAllBarCodesByCategory("Clothes"));
            AllBarCodesByAppliances = new ObservableCollection<Product>(AnalyticsByItemsModel.GetAllBarCodesByCategory("Appliances"));
            AllBarCodesByMultimedia = new ObservableCollection<Product>(AnalyticsByItemsModel.GetAllBarCodesByCategory("Multimedia"));

            HistogramList = new ObservableCollection<ChartLineSeries3DModel>(AnalyticsByItemsModel.GetAllLabels());
           
           
        }


        //A organiser
        public ObservableCollection<ChartLineSeries3DModel> getQuantityPerMonthCollection(BE.Product product, DateTime date) // a verifier ce qu'on recupere du calendier
        {
            // recoit une date 
            ObservableCollection<ChartLineSeries3DModel> liste = new ObservableCollection<ChartLineSeries3DModel>();

            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int[] values = new int[12];
            for (int i = 0; i < 12; i++)
            {
                values[i] = AnalyticsByItemsModel.GetAllProductOrders(x => x.ProductOrderValidation == true && x.BarCode == product.BarCode && x.Date.Month == i + 1 && x.Date.Year == date.Year).Select(x => x.Count).Sum();
            }
            for (int i = 0; i < 12; i++)
            {
                liste.Add(new ChartLineSeries3DModel { Month = months[i], Amount = values[i] });
            }
            liste.Reverse(); // a verifier le reverse

            return liste;
        }

    }
}
