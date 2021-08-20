using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace PL.Test
{
    public class TestViewModel : INotifyPropertyChanged
    {

        public TestModel TestModel { get; set; }
        public ObservableCollection<Product> AllBarCodesByFood { get; set; }
        public ObservableCollection<Product> AllBarCodesByBeauty { get; set; }
        public ObservableCollection<Product> AllBarCodesByClothes { get; set; }
        public ObservableCollection<Product> AllBarCodesByAppliances { get; set; }
        public ObservableCollection<Product> AllBarCodesByMultimedia { get; set; }
        public ObservableCollection<ChartModel> series { get; set; }



        private DateTime date { get; set; } 
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("date");
            }
        }


        private Product selectedProduct { get; set; }
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("selectedProduct");
            }
        }


        public TestViewModel()
        {
            TestModel = new TestModel();
            AllBarCodesByFood = new ObservableCollection<Product>(TestModel.GetAllBarCodesByCategory("Food"));
            AllBarCodesByBeauty = new ObservableCollection<Product>(TestModel.GetAllBarCodesByCategory("Beauty"));
            AllBarCodesByClothes = new ObservableCollection<Product>(TestModel.GetAllBarCodesByCategory("Clothes"));
            AllBarCodesByAppliances = new ObservableCollection<Product>(TestModel.GetAllBarCodesByCategory("Appliances"));
            AllBarCodesByMultimedia = new ObservableCollection<Product>(TestModel.GetAllBarCodesByCategory("Multimedia"));
            Date = DateTime.Now;
            //series = new ObservableCollection<ChartModel>
            //    {
            //    new ChartModel {LabelX= "1" , LabelY=6},
            //    new ChartModel { LabelX = "2" , LabelY =6},
            //    new ChartModel { LabelX = "3", LabelY =5},
            //    new ChartModel { LabelX = "4" , LabelY =10},
            //    new ChartModel { LabelX = "5" , LabelY =1},

            //};

        }


        // mettre les noms des mois et pas les chiffres
        //histogram function 
        public ObservableCollection<ChartModel> getQuantityPerMonthCollection(Product product, DateTime date) // a verifier ce qu'on recupere du calendier
        {
            //recoit une date
           //ProductOrder productOrder = TestModel.GetAllProductOrders(X => X.BarCode == 1).FirstOrDefault();

            //DateTime date = DateTime.Now;

            ObservableCollection<ChartModel> liste = new ObservableCollection<ChartModel>();

            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int[] values = new int[12];
            for (int i = 0; i < 12; i++)
            {
                values[i] = TestModel.GetAllProductOrders(x => x.ProductOrderValidation == true && x.BarCode == product.BarCode && x.Date.Month == i + 1 && x.Date.Year == date.Year).Select(x => x.Count).Sum();
            }
            for (int i = 0; i < 12; i++)
            {
                liste.Add(new ChartModel { LabelX = months[i], LabelY = values[i] });
            }
            liste.Reverse(); // a verifier le reverse

            return liste;
        }

        //lineSeries function
        public ObservableCollection<ChartModel> getQuantityPerDayCollection(Product product, DateTime date) // a verifier ce qu'on recupere du calendier
        {
           //recoit une date
           //ProductOrder productOrder = TestModel.GetAllProductOrders(X => X.BarCode == 1).FirstOrDefault();
           // DateTime date = DateTime.Now;

            ObservableCollection<ChartModel> liste = new ObservableCollection<ChartModel>();
            var numOfDaysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            List<string> days = new List<string>();
            int[] values = new int[numOfDaysInMonth];
            for (int i = 0; i < numOfDaysInMonth; i++)
            {
                days.Add((i + 1).ToString() + "/" + date.Month.ToString());
                values[i] = TestModel.GetAllProductOrders(x => x.ProductOrderValidation == true && x.BarCode == product.BarCode && x.Date.Day == i + 1 && x.Date.Month == date.Month && x.Date.Year == date.Year).Select(x => x.Count).Sum();
            }
            for (int i = 0; i < numOfDaysInMonth; i++)
            {
                liste.Add(new ChartModel { LabelX = days[i], LabelY = values[i] });
            }
            liste.Reverse(); // a verifier le reverse

            return liste;
        }



        public ObservableCollection<ChartModel> getQuantityPerWeeksCollection(Product product, DateTime inputDate)
        {
            ObservableCollection<ChartModel> amountPerWeeks = new ObservableCollection<ChartModel>();
         
  
            int totalDaysInMonth = DateTime.DaysInMonth(inputDate.Year, inputDate.Month); // nombre de jour total dans le mois 
            DateTime firstOfMonth = new DateTime(inputDate.Year, inputDate.Month, 1); // date du premier du mois 
            int firstDayOfWeek = (int)firstOfMonth.DayOfWeek;    //days of week starts by default as Sunday = 0
            int weeksInMonth = (int)Math.Ceiling((firstDayOfWeek + totalDaysInMonth) / 7.0); // nombre de semaine dans ce mois 


            int date = 0;
            int totalOfWeek = 0;
            int firstDateWeek = 1;
            string rangeDateWeek = "";


            for (int j = 0; j < weeksInMonth; j++)
            {

                for (int i = firstDayOfWeek; i < 7; i++)
                {
                    date++;
                    int totalOfDay = TestModel.GetAllProductOrders(x => x.ProductOrderValidation == true && x.BarCode == product.BarCode
                    && x.Date.Year == inputDate.Year && x.Date.Month == inputDate.Month && x.Date.Day == date).Select(x => x.Count).Sum();

                    totalOfWeek += totalOfDay;


                    if (date == DateTime.DaysInMonth(inputDate.Year, inputDate.Month))
                        break;

                }

                rangeDateWeek = firstDateWeek.ToString() + "/" + (inputDate.Month).ToString() + " - " + date.ToString() + "/" + (inputDate.Month).ToString();
                firstDateWeek = date + 1; //1er jour de la semaine d apres
                amountPerWeeks.Add(new ChartModel { LabelX = rangeDateWeek, LabelY = totalOfWeek });
                
              
                firstDayOfWeek = 0;
                totalOfWeek = 0;
            }

            return amountPerWeeks;

        }





        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
