using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Analytics
{
    /// <summary>
    /// Interaction logic for ChartLineSeries3DUC.xaml
    /// </summary>
    public partial class ChartLineSeries3DUC : UserControl
    {

     
        public string ChartHeader { get; set; }
        public string HeaderAxisX { get; set; }
        public string HeaderAxisY { get; set; }
        public ObservableCollection<ChartLineSeries3DModel> Collection { get; set; }
        public string XSource { get; set; }
        public string YSource { get; set; }

        public ChartLineSeries3DUC()
        {
            InitializeComponent();
            DataContext = this;
            
        }
    }
}
