using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;
using System.ComponentModel;

namespace PL.Analytics.AnalyticsByItems
{
    public class AnalyticsByItemsViewModel : INotifyPropertyChanged
    {

        public AnalyticsByItemsModel AnalyticsByItemsModel { get; set; }
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


        private string selectedProductName { get; set; }
        public string SelectedProductName
        {
            get { return selectedProductName; }
            set
            {
                selectedProductName = value;
                OnPropertyChanged("selectedProductName");
            }
        }


        public AnalyticsByItemsViewModel()
        {
            AnalyticsByItemsModel = new AnalyticsByItemsModel();
            Date = DateTime.Now;


        }

        public List<string> GetAllProductsNameByCategory(string categoryName)
        {
            return AnalyticsByItemsModel.GetAllProductsNameByCategory(categoryName);

        }

        //histogram function 
        public ObservableCollection<ChartModel> getQuantityPerMonthCollection(string productName, DateTime date) // a verifier ce qu'on recupere du calendier
        {
            //recoit une date
            //ProductOrder productOrder = TestModel.GetAllProductOrders(X => X.BarCode == 1).FirstOrDefault();

            //DateTime date = DateTime.Now;

            ObservableCollection<ChartModel> liste = new ObservableCollection<ChartModel>();
            List<int> productsId = AnalyticsByItemsModel.GetAllBarCodeByName(productName);

            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int[] values = new int[12];
            for (int i = 0; i < 12; i++)
            {
                foreach (int id in productsId)
                {
                    values[i] += AnalyticsByItemsModel.GetAllProductOrders(x => x.ProductOrderValidation == true && x.BarCode == id && x.Date.Month == i + 1 && x.Date.Year == date.Year).Select(x => x.Count).Sum();
                }
            }
            for (int i = 0; i < 12; i++)
            {
                liste.Add(new ChartModel { LabelX = months[i], LabelY = values[i] });
            }
   

            return liste;
        }

        //lineSeries function
        public ObservableCollection<ChartModel> getQuantityPerDayCollection(string productName, DateTime date) // a verifier ce qu'on recupere du calendier
        {
            //recoit une date
            //ProductOrder productOrder = TestModel.GetAllProductOrders(X => X.BarCode == 1).FirstOrDefault();
            // DateTime date = DateTime.Now;

            ObservableCollection<ChartModel> liste = new ObservableCollection<ChartModel>();
            List<int> productsId = AnalyticsByItemsModel.GetAllBarCodeByName(productName);
            var numOfDaysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            List<string> days = new List<string>();
            int[] values = new int[numOfDaysInMonth];
            for (int i = 0; i < numOfDaysInMonth; i++)
            {
                foreach (int id in productsId)
                {
                    
                    values[i] += AnalyticsByItemsModel.GetAllProductOrders(x => x.ProductOrderValidation == true && 
                    x.BarCode == id && x.Date.Day == i + 1 && x.Date.Month == date.Month && x.Date.Year == date.Year).Select(x => x.Count).Sum();
                }
                days.Add((i + 1).ToString() + "/" + date.Month.ToString());
            }
            for (int i = 0; i < numOfDaysInMonth; i++)
            {
                liste.Add(new ChartModel { LabelX = days[i], LabelY = values[i] });
            }

            return liste;
        }


        //table function
        public ObservableCollection<ChartModel> getQuantityPerWeeksCollection(string productName, DateTime inputDate)
        {
            ObservableCollection<ChartModel> amountPerWeeks = new ObservableCollection<ChartModel>();
            List<int> productsId = AnalyticsByItemsModel.GetAllBarCodeByName(productName);

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
                    foreach (int id in productsId)
                    {
                        int totalOfDay = AnalyticsByItemsModel.GetAllProductOrders(x => x.ProductOrderValidation == true && x.BarCode == id
                        && x.Date.Year == inputDate.Year && x.Date.Month == inputDate.Month && x.Date.Day == date).Select(x => x.Count).Sum();

                        totalOfWeek += totalOfDay;
                    }


                    if (date == DateTime.DaysInMonth(inputDate.Year, inputDate.Month))
                        break;

                }

                rangeDateWeek = firstDateWeek.ToString() + "/" + (inputDate.Month).ToString() + " - " + date.ToString() + "/" + (inputDate.Month).ToString();
                firstDateWeek = date + 1; //1er jour de la semaine d apres
                amountPerWeeks.Add(new ChartModel { LabelX = rangeDateWeek, LabelY = double.Parse(totalOfWeek.ToString("0.00")) });


                firstDayOfWeek = 0;
                totalOfWeek = 0;
            }

            return amountPerWeeks;

        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
