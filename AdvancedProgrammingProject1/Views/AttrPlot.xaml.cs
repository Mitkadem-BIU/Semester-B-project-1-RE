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

namespace AdvancedProgrammingProject1
{
    /// <summary>
    /// Interaction logic for AttrPlot.xaml
    /// </summary>
    public partial class AttrPlot : UserControl
    {
        AttrPlotViewModel vm;

        public AttrPlot()
        {
            InitializeComponent();
            vm = new AttrPlotViewModel(new AttrPlotModel());
            DataContext = vm;
            
            /* List<SingleAttr> items = new List<SingleAttr>();
            //foreach (var item in )
            //{

            //}
            items.Add(new SingleAttr() { Title = "Complete this WPF tutorial", Completion = 0 });
            items.Add(new SingleAttr() { Title = "Learn C#", Completion = 0 });
            items.Add(new SingleAttr() { Title = "Wash the car", Completion = 0 });

            AttributesList.ItemsSource = items; */
        }
    }
    public class SingleAttr
    {
        public string Title { get; set; }
        public int Completion { get; set; }
    }
}
