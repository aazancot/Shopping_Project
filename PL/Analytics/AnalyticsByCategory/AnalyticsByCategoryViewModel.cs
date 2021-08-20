using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Analytics.AnalyticsByCategory
{
    class AnalyticsByCategoryViewModel : INotifyPropertyChanged
    {
        public AnalyticsByCategoryModel AnalyticsByCategoryModel { get; set; }
        public ObservableCollection<ChartModel> series1 { get; set; }
        public ObservableCollection<ChartModel> series2 { get; set; }

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


        private String selectedNameCategory { get; set; }
        public String SelectedNameCategory
        {
            get { return selectedNameCategory; }
            set
            {
                selectedNameCategory = value;
                OnPropertyChanged("selectedCategory");
            }
        }


        public AnalyticsByCategoryViewModel()
        {
            AnalyticsByCategoryModel = new AnalyticsByCategoryModel();
            Date = DateTime.Now;
            selectedNameCategory = "Food";
            series1 = getCategoryExpensesPerDayCollection(SelectedNameCategory, Date);
            series2 = getCategoryExpensesPerMonthCollection(SelectedNameCategory, Date);

        }

        //histogram function 
        public ObservableCollection<ChartModel> getCategoryExpensesPerMonthCollection(string categoryName, DateTime date)
        {

            ObservableCollection<ChartModel> liste = new ObservableCollection<ChartModel>();
            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            double[] values = new double[12];

            for (int i = 0; i < 12; i++)
            {
               values[i] += AnalyticsByCategoryModel.GetAllProductOrders(x => x.ProductOrderValidation == true && x.Category.ToString() == categoryName 
               && x.Date.Month == i + 1 && x.Date.Year == date.Year).Select(x => x.Count * x.UnitPrice).Sum();

            }
            for (int i = 0; i < 12; i++)
            {
                liste.Add(new ChartModel { LabelX = months[i], LabelY = values[i] });
            }
            //liste.Reverse(); // a verifier le reverse

            return liste;
        }

        //lineSeries function
        public ObservableCollection<ChartModel> getCategoryExpensesPerDayCollection(String categoryName, DateTime date)
        {
            ObservableCollection<ChartModel> liste = new ObservableCollection<ChartModel>();

            List<string> days = new List<string>(); // LabelX
            var numOfDaysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            double[] values = new double[numOfDaysInMonth]; // LabelY
            double totalOfDay = 0;

            for (int i = 0; i < numOfDaysInMonth; i++)
            {
                
                totalOfDay += AnalyticsByCategoryModel.GetAllProductOrders(x => x.ProductOrderValidation == true && x.Category.ToString() == categoryName &&
                x.Date.Day == i + 1 && x.Date.Month == date.Month && x.Date.Year == date.Year).Select(x => x.Count * x.UnitPrice).Sum();

                values[i] = totalOfDay;
                days.Add((i + 1).ToString() + "/" + date.Month.ToString());
                totalOfDay = 0;

            }
            for (int i = 0; i < numOfDaysInMonth; i++)
            {
                liste.Add(new ChartModel { LabelX = days[i], LabelY = values[i] });
            }

            return liste;
        }

        //tableau par semaine
        public ObservableCollection<ChartModel> getCategoryExpensesPerWeeksCollection(String categoryName, DateTime inputDate)
        {
            ObservableCollection<ChartModel> amountPerWeeks = new ObservableCollection<ChartModel>();

            int totalDaysInMonth = DateTime.DaysInMonth(inputDate.Year, inputDate.Month); // nombre de jour total dans le mois 
            DateTime firstOfMonth = new DateTime(inputDate.Year, inputDate.Month, 1); // date du premier du mois 
            int firstDayOfWeek = (int)firstOfMonth.DayOfWeek;    //days of week starts by default as Sunday = 0
            int weeksInMonth = (int)Math.Ceiling((firstDayOfWeek + totalDaysInMonth) / 7.0); // nombre de semaine dans ce mois 


            int date = 0;
            double totalOfWeek = 0;
            int firstDateWeek = 1;
            string rangeDateWeek = "";


            for (int j = 0; j < weeksInMonth; j++)
            {

                for (int i = firstDayOfWeek; i < 7; i++)
                {
                    date++;
                    double totalOfDay = AnalyticsByCategoryModel.GetAllProductOrders(x => x.ProductOrderValidation == true && x.Category.ToString() == categoryName
                    && x.Date.Year == inputDate.Year && x.Date.Month == inputDate.Month && x.Date.Day == date).Select(x => x.Count * x.UnitPrice).Sum();

                    totalOfWeek += totalOfDay;
                    
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

        //2e tableau
        public ObservableCollection<ChartModel> getExpensesPerMonthPerCategoryCollection(string categoryName, DateTime date)
        {

            ObservableCollection<ChartModel> liste = new ObservableCollection<ChartModel>();
            List<string> categoriesName = AnalyticsByCategoryModel.GetAllCategoriesName();

            double totalSum = 0;

            foreach (var name in categoriesName)
            {
                // recupere la somme totale d achats
                totalSum = AnalyticsByCategoryModel.GetAllProductOrders(x => x.ProductOrderValidation == true && x.Category.ToString()== name && x.Date.Month == date.Month && x.Date.Year == date.Year).Select(x => x.Count * x.UnitPrice).Sum();


                //les rentrer ds une liste
                liste.Add(new ChartModel
                {
                    LabelX = name,
                    LabelY = double.Parse(totalSum.ToString("0.00"))

                });

            }

            return liste;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
