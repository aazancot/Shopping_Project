using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Analytics.AnalyticsByTotal
{
    class AnalyticsByTotalViewModel : INotifyPropertyChanged
    {
        public AnalyticsByTotalModel AnalyticsByTotalModel { get; set; }

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



        public AnalyticsByTotalViewModel()
        {
            AnalyticsByTotalModel = new AnalyticsByTotalModel();
            Date = DateTime.Now;

        }

        //histogram function 
        public ObservableCollection<ChartModel> getTotalExpensesPerMonthCollection(DateTime date)
        {

            ObservableCollection<ChartModel> liste = new ObservableCollection<ChartModel>();
            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            double[] values = new double[12];
            for (int i = 0; i < 12; i++)
            {

                    values[i] += AnalyticsByTotalModel.GetAllProductOrders(x => x.ProductOrderValidation == true && x.Date.Month == i + 1 && x.Date.Year == date.Year).Select(x => x.Count * x.UnitPrice).Sum();

            }
            for (int i = 0; i < 12; i++)
            {
                liste.Add(new ChartModel { LabelX = months[i], LabelY = values[i] });
            }
            //liste.Reverse(); // a verifier le reverse

            return liste;
        }

        //lineSeries function
        public ObservableCollection<ChartModel> getTotalExpensesPerDayCollection(DateTime date)
        {
            ObservableCollection<ChartModel> liste = new ObservableCollection<ChartModel>();
            List<string> days = new List<string>(); // LabelX
            var numOfDaysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            double[] values = new double[numOfDaysInMonth]; // LabelY
            double totalOfDay = 0;

            for (int i = 0; i < numOfDaysInMonth; i++)
            {
               
                    totalOfDay += AnalyticsByTotalModel.GetAllProductOrders(x => x.ProductOrderValidation == true &&
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
        public ObservableCollection<ChartModel> getTotalExpensesPerWeeksCollection(DateTime inputDate)
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
                  

                    double totalOfDay = AnalyticsByTotalModel.GetAllProductOrders(x => x.ProductOrderValidation == true 
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



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
