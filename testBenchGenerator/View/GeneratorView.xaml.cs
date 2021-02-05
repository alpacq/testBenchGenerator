﻿using System;
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
using testBenchGenerator.Model;
using testBenchGenerator.ViewModel;

namespace testBenchGenerator.View
{
    /// <summary>
    /// Interaction logic for GeneratorView.xaml
    /// </summary>
    public partial class GeneratorView : UserControl
    {
        private GeneratorViewModel viewModel;
        public GeneratorView()
        {
            InitializeComponent();
        }

        private void dut_Click(object sender, RoutedEventArgs e)
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
                this.viewModel = new GeneratorViewModel(filename);
                this.DataContext = this.viewModel;
                dutFile.Text = filename;
            }
        }

        private void run_Click(object sender, RoutedEventArgs e)
        {
            if (this.viewModel != null)
            {             
                bool result = this.viewModel.GenerateFile();

                this.infoBlock.Text = result ? DateTime.Now.ToLongTimeString() + " Testbench file generated successfully." : DateTime.Now.ToLongTimeString() + " Error - testbench file not generated.";
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            NewTCView newTCWindow = new NewTCView(new NewTCViewModel(this.viewModel.ModuleFile));
            newTCWindow.ShowDialog();
            this.viewModel.TestCases = ((NewTCViewModel)newTCWindow.DataContext).ModuleFileVM.TestCases;
            this.datains.Items.Refresh();
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.TestCases.Remove(this.viewModel.SelectedTestCase);
            this.viewModel.SelectedTestCase = null;
            this.datains.Items.Refresh();
        }
    }
}
