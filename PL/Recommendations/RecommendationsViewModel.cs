using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Accord.MachineLearning.Rules;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Input;
using PL.Commands;
using PL.Converters;

namespace PL.Recommendations
{
    public class RecommendationsViewModel : INotifyPropertyChanged
    {

        public RecommendationsModel RecommendationsModel { get; set; }
        public ObservableCollection<RecommendatedItems> AllRecommendatedItems { get; set; }
        public ObservableCollection<AssociationRules> AllAssociationRules { get; set; }

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


        public RecommendationsViewModel()
        {
            RecommendationsModel = new RecommendationsModel();
            AllRecommendatedItems = new ObservableCollection<RecommendatedItems>(GetAllRecommendatedItems(DateTime.Now));
            AllAssociationRules = new ObservableCollection<AssociationRules>(GetAllAssociationRules());
            date = DateTime.Now.Date;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public List<AssociationRules> GetAllAssociationRules()
        {
            List<AssociationRules> result = new List<AssociationRules>();
            AssociationRule<Product>[] associationRules = RecommendationsModel.GetAssociationRules().Where(x=>x.Confidence>0.5).ToArray();
            foreach (var item in associationRules)
            {
                AssociationRules newRule = new AssociationRules();
                newRule.X = SortedListToString(item.X);
                newRule.Y = SortedListToString(item.Y);
                newRule.Confidence = double.Parse(item.Confidence.ToString("0.00"));

                result.Add(newRule);
            }

            List<AssociationRules> noduplicates = result.Distinct().ToList();

            return noduplicates;
        }

        public string SortedListToString(SortedSet<Product> set)
        {
            string output = "";
            foreach (Product p in set)
            {
                output += p.ProductName + " ";
            }
            return output;
        }
        public List<RecommendatedItems> GetAllRecommendatedItems(DateTime date)
        {
            List<RecommendatedItems> AllRecommendatedItems = new List<RecommendatedItems>();
            List<RecommendatedItems> TenRecommendatedItems = new List<RecommendatedItems>();
            int selectedDay = (int)date.DayOfWeek;
            List<ProductOrder> productOrdersByDay = RecommendationsModel.GetAllProductOrders(x => x.ProductOrderValidation == true &&
           ((int)x.Date.DayOfWeek == selectedDay));
            List<int> productOrdersBarCodeByDay = productOrdersByDay.Select(x => x.BarCode).Distinct().ToList();



            foreach (int barCode in productOrdersBarCodeByDay)
            {
                int totalCount = 0;
                foreach (var po in productOrdersByDay)
                {
                    if (po.BarCode == barCode)
                    {
                        totalCount += po.Count;
                    }

                }
                float[] tmpArr = GetStoreIdCheaper(barCode);

                RecommendatedItems newRecommendatedItems = new RecommendatedItems
                {
                    BarCode = barCode,
                    TotalCount = totalCount,
                    StoreId = (int)tmpArr[0],
                    CheapUnitPrice = tmpArr[1].ToString() + " " + "shekels",
                    IsChecked = true,


                };
                AllRecommendatedItems.Add(newRecommendatedItems);


            }

            AllRecommendatedItems = AllRecommendatedItems.OrderByDescending(x => x.TotalCount).ToList();
            if (AllRecommendatedItems.Count < 10)
            {
                return AllRecommendatedItems;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    TenRecommendatedItems.Add(AllRecommendatedItems[i]);
                }
                return TenRecommendatedItems;
            }

        }


        public float[] GetStoreIdCheaper(int barCode)
        {

            float cheapStoreId = 0;
            float cheapUnitPrice = float.MaxValue;
            List<List<ProductOrder>> result = new List<List<ProductOrder>>();
            List<ProductOrder> listHelp = RecommendationsModel.GetAllProductOrders(x => x.ProductOrderValidation == true && x.BarCode == barCode).OrderByDescending(x => x.Date).ToList();
            var list = from p in listHelp
                       group p by new { p.StoreId } into g
                       select g.ToList();

            result = list.ToList();

            foreach (var item in result)
            {
                var newItem = item.OrderByDescending(x => x.Date).ToList();
                if (newItem[0].UnitPrice < cheapUnitPrice)
                {
                    cheapUnitPrice = newItem[0].UnitPrice;
                    cheapStoreId = newItem[0].StoreId;
                }
            }

            float[] tmpArr = { cheapStoreId, cheapUnitPrice };
            return tmpArr;


        }

        public void CreatePDF2(List<object[]> items)
        {
            RecommendationsModel.CreatePDF(items);
        }

        public ICommand CreatePdf
        {
            get
            {
                return new Command(delegate ()
                {
                    List<object[]> items = new List<object[]>();
                    foreach (RecommendatedItems item in AllRecommendatedItems)
                    {
                        if (item.IsChecked == true)
                        {
                            items.Add(new object[] {
                        new BarCodeToProductName().Convert(item.BarCode, null, null, CultureInfo.CurrentCulture),
                        new BarCodeToDescription().Convert(item.BarCode, null, null, CultureInfo.CurrentCulture),
                        new StoreIdToStoreCityStoreName().Convert(item.StoreId, null, null, CultureInfo.CurrentCulture),
                        item.CheapUnitPrice.ToString()});
                        }
                    }
                    RecommendationsModel.CreatePDF(items);


                });
                }
        }

    }
    
}







  