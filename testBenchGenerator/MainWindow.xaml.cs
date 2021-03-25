using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FPGADeveloperTools.FIRDesigner.View;
using FPGADeveloperTools.TestbenchGenerator.View;
using FPGADeveloperTools.WaveformDesignerAndAnalyzer.View;

namespace FPGADeveloperTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FIRView firView;
        private GeneratorView genView;
        private WaveformProcessorView anView;

        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            tbgBtn.Style = (Style)FindResource(typeof(Button));
            tbgBtn.Template = (ControlTemplate)FindResource("btnTmpltMenu");
            firBtn.Style = (Style)FindResource(typeof(Button));
            firBtn.Template = (ControlTemplate)FindResource("btnTmpltMenu");
            wfpBtn.Style = (Style)FindResource(typeof(Button));
            wfpBtn.Template = (ControlTemplate)FindResource("btnTmpltMenu");
            exit.Style = (Style)FindResource(typeof(Button));
            exit.Template = (ControlTemplate)FindResource("btnTmpltFile");
            this.firView = new FIRView();
            this.genView = new GeneratorView();
            this.anView = new WaveformProcessorView();
            this.firView.Style = (Style)FindResource(typeof(Page));
            this.genView.Style = (Style)FindResource(typeof(Page));
            this.anView.Style = (Style)FindResource(typeof(Page));
            this.presenter.Content = this.genView;
        }        

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Height = SystemParameters.PrimaryScreenHeight * 0.95;
            this.Width = SystemParameters.PrimaryScreenWidth * 0.95;
            this.CenterWindowOnScreen();
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void tbgBtn_Click(object sender, RoutedEventArgs e)
        {
            this.presenter.Content = this.genView;
        }

        private void wfpBtn_Click(object sender, RoutedEventArgs e)
        {
            this.presenter.Content = this.anView;
        }

        private void firBtn_Click(object sender, RoutedEventArgs e)
        {
            this.presenter.Content = this.firView;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void tbgBtn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            popupWrapper.PlacementTarget = tbgBtn;
            popupWrapper.Placement = PlacementMode.Bottom;
            popupWrapper.IsOpen = true;
            header.PopupText.Text = "Testbench generator";
        }

        private void wfpBtn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            popupWrapper.PlacementTarget = wfpBtn;
            popupWrapper.Placement = PlacementMode.Bottom;
            popupWrapper.IsOpen = true;
            header.PopupText.Text = "Waveform processor";
        }

        private void firBtn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            popupWrapper.PlacementTarget = firBtn;
            popupWrapper.Placement = PlacementMode.Bottom;
            popupWrapper.IsOpen = true;
            header.PopupText.Text = "FIR designer";
        }

        private void onMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            popupWrapper.Visibility = Visibility.Collapsed;
            popupWrapper.IsOpen = false;
        }
    }
}
