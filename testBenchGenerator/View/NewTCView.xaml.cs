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
            if (this.viewModel.Clock != null)
                this.clkCB.SelectedIndex = this.viewModel.Clocks.FindIndex(c => c.Name == this.viewModel.Clock.Name);
            if (this.viewModel.ValidIn != null)
                this.vipCB.SelectedIndex = this.viewModel.Inputs.FindIndex(i => i.Name == this.viewModel.ValidIn.Name);
            if (this.viewModel.ValidOut != null)
                this.vopCB.SelectedIndex = this.viewModel.Outputs.FindIndex(i => i.Name == this.viewModel.ValidOut.Name);
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
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Files (*.txt)|*.txt";


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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.viewModel.CheckIfCanSave();
        }
    }
}
