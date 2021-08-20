using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Analytics.AnalyticsByCategory.CarouselCategories
{
    public class MainViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        public MainViewModel()
        {
            RadioStationsDAB = new System.Collections.ObjectModel.ObservableCollection<RadioStation>();
            RadioStationsDAB.Add(new RadioStation() { Name = "Food", ImageSource = "C:\\Shopping_Project\\categories_picture\\food.jpg" });
            RadioStationsDAB.Add(new RadioStation() { Name = "Beauty", ImageSource = "C:\\Shopping_Project\\categories_picture\\beauty.jpg" });
            RadioStationsDAB.Add(new RadioStation() { Name = "Clothes", ImageSource = "C:\\Shopping_Project\\categories_picture\\clothes.jpg" });
            RadioStationsDAB.Add(new RadioStation() { Name = "Appliances", ImageSource = "C:\\Shopping_Project\\categories_picture\\appliances.jpg" });
            RadioStationsDAB.Add(new RadioStation() { Name = "Multimedia", ImageSource = "C:\\Shopping_Project\\categories_picture\\multimedia.jpg" });

            SelectedRadioStationDAB = RadioStationsDAB[0];
        }

        private System.Collections.ObjectModel.ObservableCollection<RadioStation> _radioStationsDAB;
        public System.Collections.ObjectModel.ObservableCollection<RadioStation> RadioStationsDAB
        {
            get
            {
                return _radioStationsDAB;
            }
            set
            {
                _radioStationsDAB = value;
                NotifyPropertyChanged("RadioStationsDAB");
            }
        }

        private RadioStation _selectedRadioStationDAB;
        public RadioStation SelectedRadioStationDAB
        {
            get
            {
                return _selectedRadioStationDAB;
            }
            set
            {
                _selectedRadioStationDAB = value;
                NotifyPropertyChanged("SelectedRadioStationDAB");
            }
        }



        #region INotifyPropertyChanged

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged
    }
}

