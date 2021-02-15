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
using System.Windows.Forms;
using testBenchGenerator.ViewModel;
using testBenchGenerator.Model;

namespace testBenchGenerator.View
{
    /// <summary>
    /// Interaction logic for FIRView.xaml
    /// </summary>
    public partial class FIRView : System.Windows.Controls.UserControl
    {
        private FIRViewModel viewModel;
        public FIRView()
        {
            InitializeComponent();
            this.viewModel = new FIRViewModel(new FIR());
            this.DataContext = this.viewModel;
        }

        private void design_Click(object sender, RoutedEventArgs e)
        {

        }

        private void export_Click(object sender, RoutedEventArgs e)
        {

        }

        private void wt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void pt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
