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
using System.Windows.Shapes;
using testBenchGenerator.ViewModel;

namespace testBenchGenerator.View
{
    /// <summary>
    /// Interaction logic for NewTCView.xaml
    /// </summary>
    public partial class NewTCView : Window
    {
        private NewTCViewModel viewModel;
        public NewTCView(NewTCViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.DataContext = this.viewModel;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.Add();
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void in_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".sv";
            dlg.Filter = "SystemVerilog Files (*.sv)|*.sv|Verilog Files (*.v)|*.v";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                this.viewModel.InputPath = filename;
            }
        }
    }
}
