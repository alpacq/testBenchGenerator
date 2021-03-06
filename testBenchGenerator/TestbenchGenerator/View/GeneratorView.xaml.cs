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
using testBenchGenerator.TestbenchGenerator.Model;
using testBenchGenerator.TestbenchGenerator.ViewModel;

namespace testBenchGenerator.TestbenchGenerator.View
{
    /// <summary>
    /// Interaction logic for GeneratorView.xaml
    /// </summary>
    public partial class GeneratorView : UserControl
    {
        private GeneratorViewModel viewModel;
        public bool IsDragging { get; set; }
        #region DraggedItem

        /// <summary>
        /// DraggedItem Dependency Property
        /// </summary>
        public static readonly DependencyProperty DraggedItemProperty =
            DependencyProperty.Register("DraggedItem", typeof(TestCaseViewModel), typeof(GeneratorView));

        /// <summary>
        /// Gets or sets the DraggedItem property.  This dependency property 
        /// indicates ....
        /// </summary>
        public TestCaseViewModel DraggedItem
        {
            get { return (TestCaseViewModel)GetValue(DraggedItemProperty); }
            set { SetValue(DraggedItemProperty, value); }
        }

        #endregion
        public GeneratorView()
        {
            InitializeComponent();
            this.viewModel = new GeneratorViewModel(null);
            this.DataContext = this.viewModel;            
        }

        private void dut_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".sv";
            dlg.Filter = "SystemVerilog Files (*.sv)|*.sv|Verilog Files (*.v)|*.v|VHDL Files (*.vhd)|*.vhd";


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
                bool result = this.viewModel.Generate();

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

        private void datains_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NewTCView newTCWindow = new NewTCView(new NewTCViewModel(this.viewModel.ModuleFile, this.viewModel.SelectedTestCase));
            newTCWindow.ShowDialog();
            this.viewModel.TestCases = ((NewTCViewModel)newTCWindow.DataContext).ModuleFileVM.TestCases;
            this.datains.Items.Refresh();
        }

        private void genView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gView = this.datains.View as GridView;
            var workingWidth = this.ActualWidth - SystemParameters.VerticalScrollBarWidth - 45; // take into account vertical scrollbar
            var wFrac = workingWidth / 26;
            gView.Columns[0].Width = wFrac;     //order
            gView.Columns[1].Width = wFrac * 2; //clk sync
            gView.Columns[2].Width = wFrac * 5; //ins
            gView.Columns[3].Width = wFrac * 2; //vld in
            gView.Columns[4].Width = wFrac * 4; //dv
            gView.Columns[5].Width = wFrac * 2; //seq
            gView.Columns[6].Width = wFrac * 5; //outs
            gView.Columns[7].Width = wFrac * 2; //vld out
            gView.Columns[8].Width = wFrac * 2; //radix
            gView.Columns[9].Width = wFrac;     //loop

            this.infoBlock.Margin = new Thickness(wFrac, 0, wFrac, 0);
            this.run.Margin = new Thickness(wFrac, 0, wFrac, 0);

            workingWidth = this.ActualWidth - (5 * SystemParameters.VerticalScrollBarWidth) - 60; // take into account vertical scrollbar
            wFrac = workingWidth / 24;
            this.clocks.Width = wFrac * 4 + SystemParameters.VerticalScrollBarWidth;
            gView = this.clocks.View as GridView;
            gView.Columns[0].Width = wFrac * 3;
            gView.Columns[1].Width = wFrac * 1;
            this.resets.Width = wFrac * 4 + SystemParameters.VerticalScrollBarWidth;
            gView = this.resets.View as GridView;
            gView.Columns[0].Width = wFrac * 3;
            gView.Columns[1].Width = wFrac * 1;
            this.paramss.Width = wFrac * 4 + SystemParameters.VerticalScrollBarWidth;
            gView = this.paramss.View as GridView;
            gView.Columns[0].Width = wFrac * 3;
            gView.Columns[1].Width = wFrac * 1;
            this.inputs.Width = wFrac * 6 + SystemParameters.VerticalScrollBarWidth;
            gView = this.inputs.View as GridView;
            gView.Columns[0].Width = wFrac * 3;
            gView.Columns[1].Width = wFrac * 3;
            this.outputs.Width = wFrac * 6 + SystemParameters.VerticalScrollBarWidth;
            gView = this.outputs.View as GridView;
            gView.Columns[0].Width = wFrac * 3;
            gView.Columns[1].Width = wFrac * 3;

            var workingHeight = this.ActualHeight - this.dutpanel.ActualHeight - this.btnspanel.ActualHeight - this.genpanel.ActualHeight - this.clkpanel.ActualHeight - this.tcpanel.ActualHeight - 70;
            this.clocks.Height = workingHeight * 1 / 3;
            this.resets.Height = workingHeight * 1 / 3;
            this.paramss.Height = workingHeight * 1 / 3;
            this.inputs.Height = workingHeight * 1 / 3;
            this.outputs.Height = workingHeight * 1 / 3;
            this.datains.Height = workingHeight * 2 / 3;
        }

        private void datains_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var row = UIHelpers.TryFindFromPoint<ListViewItem>((UIElement)sender,
                                                    e.GetPosition(this.datains));
            if (row == null) return;
            if (((TestCaseViewModel)(row.Content)).Loop)
                return;
            //set flag that indicates we're capturing mouse movements
            this.IsDragging = true;
            this.DraggedItem = (TestCaseViewModel)row.Content;
        }

        private void datains_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsDragging)
            {
                this.ResetDragDrop();
                return;
            }

            var row = UIHelpers.TryFindFromPoint<ListViewItem>((UIElement)sender,
                                                    e.GetPosition(this));
            if (row == null)
            {
                this.ResetDragDrop();
                return;
            }
            //get the target item
            TestCaseViewModel targetItem = (TestCaseViewModel)(row.Content);

            if (!ReferenceEquals(this.DraggedItem, targetItem))
            {
                var prevOrder = this.DraggedItem.Order;
                var nextOrder = targetItem.Order;
                
                foreach (TestCaseViewModel tc in this.viewModel.TestCases)
                {
                    if (tc != this.DraggedItem)
                    {
                        if (nextOrder < prevOrder)
                        {
                            if (tc.Order >= nextOrder && tc.Order < prevOrder)
                            {
                                tc.Order += 1;
                            }
                        }
                        else if (nextOrder > prevOrder)
                        {
                            if (tc.Order > prevOrder && tc.Order <= nextOrder)
                            {
                                tc.Order -= 1;
                            }
                        }
                    }
                }

                this.DraggedItem.Order = nextOrder;
                this.viewModel.TestCases = this.viewModel.TestCases.OrderBy(t => t.Order).ToList<TestCaseViewModel>();
                this.datains.Items.Refresh();
            }

            //reset
            this.ResetDragDrop();
        }

        private void datains_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsDragging || e.LeftButton != MouseButtonState.Pressed) return;

            //display the popup if it hasn't been opened yet
            if (!this.popup1.IsOpen)
            {
                //make sure the popup is visible
                this.popup1.IsOpen = true;
            }

            Size popupSize = new Size(this.popup1.ActualWidth, this.popup1.ActualHeight);
            this.popup1.PlacementRectangle = new Rect(e.GetPosition(this), popupSize);
        }

        private void ResetDragDrop()
        {
            this.IsDragging = false;
            this.popup1.IsOpen = false;
            this.datains.SelectedItem = null;
        }
    }
}
