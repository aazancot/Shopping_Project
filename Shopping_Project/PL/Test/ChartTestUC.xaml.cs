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

namespace PL.Test
{
    /// <summary>
    /// Interaction logic for ChartTestUC.xaml
    /// </summary>
    public partial class ChartTestUC : UserControl
    {

        public string ChartHeader { get; set; }
        public string HeaderAxisX { get; set; }
        public string HeaderAxisY { get; set; }
        public ObservableCollection<ChartModel> Collection { get; set; }
        public string XSource { get; set; }
        public string YSource { get; set; }
        public ChartTestUC()
        {
            InitializeComponent();
            DataContext= this;
        }
    }
}
