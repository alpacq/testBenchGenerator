using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using testBenchGenerator.WaveformDesignerAndAnalyzer.Model;
using testBenchGenerator.WaveformDesignerAndAnalyzer.ViewModel;

namespace testBenchGenerator.WaveformDesignerAndAnalyzer.View
{
    /// <summary>
    /// Interaction logic for AnalyzerView.xaml
    /// </summary>
    public partial class AnalyzerView : UserControl
    {
        private WaveformDesignerViewModel wfdVM;
        private WaveformAnalyzerViewModel wfaVM;
        private BackgroundWorker bwD;
        private BackgroundWorker bwA;
        public AnalyzerView()
        {
            InitializeComponent();
            this.bwD = new BackgroundWorker();
            this.bwA = new BackgroundWorker();
            this.bwD.DoWork += BwD_DoWork;
            this.bwA.DoWork += BwA_DoWork;
            this.bwD.RunWorkerCompleted += BwD_RunWorkerCompleted;
            this.bwA.RunWorkerCompleted += BwA_RunWorkerCompleted;
            this.wfdVM = new WaveformDesignerViewModel(new WaveformDesigner());
            this.wfaVM = new WaveformAnalyzerViewModel(new WaveformAnalyzer());
            this.designer.DataContext = this.wfdVM;
            this.analyzer.DataContext = this.wfaVM;
            this.desType.SelectedIndex = 0;
            this.modType.SelectedIndex = 0;
            this.anType.SelectedIndex = 0;
            this.wfdI.ResetAllAxes();
            this.wfdQ.ResetAllAxes();
            this.wfdF.ResetAllAxes();
            this.splashD.Visibility = Visibility.Hidden;
            this.splashA.Visibility = Visibility.Hidden;
        }

        private void BwA_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.wfaI.ResetAllAxes();
            this.wfaQ.ResetAllAxes();
            this.wfaF.ResetAllAxes();
            this.splashA.Visibility = Visibility.Hidden;
        }

        private void BwD_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.wfdI.ResetAllAxes();
            this.wfdQ.ResetAllAxes();
            this.wfdF.ResetAllAxes();
            this.splashD.Visibility = Visibility.Hidden;
        }

        private void BwA_DoWork(object sender, DoWorkEventArgs e)
        {
            this.wfaVM.Import();
        }

        private void BwD_DoWork(object sender, DoWorkEventArgs e)
        {
            this.wfdVM.Design();
        }

        private string ValueAxisLabelFormatter(double input)
        {
            double res = double.NaN;
            string suffix = string.Empty;

            // Prevod malych hodnot
            if (Math.Abs(input) <= 0.001)
            {
                Dictionary<int, string> siLow = new Dictionary<int, string>
                {
                    [-12] = "p",
                    [-9] = "n",
                    [-6] = "μ",
                    [-3] = "m",
                    //[-2] = "c",
                    //[-1] = "d",
                };

                foreach (var v in siLow.Keys)
                {
                    if (input != 0 && Math.Abs(input) <= Math.Pow(10, v))
                    {
                        res = input * Math.Pow(10, Math.Abs(v));
                        suffix = siLow[v];
                        break;
                    }
                }
            }

            // Prevod velkych hodnot
            if (Math.Abs(input) >= 1000)
            {
                Dictionary<int, string> siHigh = new Dictionary<int, string>
                {
                    [12] = "T",
                    [9] = "G",
                    [6] = "M",
                    [3] = "k",
                    //[2] = "h",
                    //[1] = "da",
                };

                foreach (var v in siHigh.Keys)
                {
                    if (input != 0 && Math.Abs(input) >= Math.Pow(10, v))
                    {
                        res = input / Math.Pow(10, Math.Abs(v));
                        suffix = siHigh[v];
                        break;
                    }
                }
            }

            return double.IsNaN(res) ? input.ToString() : $"{res}{suffix}";
        }

        private string ValueAxisLabelFormatters(double input)
        {
            double res = double.NaN;
            string suffix = string.Empty;

            // Prevod malych hodnot
            if (Math.Abs(input) <= 0.001)
            {
                Dictionary<int, string> siLow = new Dictionary<int, string>
                {
                    [-12] = "p",
                    [-9] = "n",
                    [-6] = "μ",
                    [-3] = "m",
                    //[-2] = "c",
                    //[-1] = "d",
                };

                foreach (var v in siLow.Keys)
                {
                    if (input != 0 && Math.Abs(input) <= Math.Pow(10, v))
                    {
                        res = input * Math.Pow(10, Math.Abs(v));
                        suffix = siLow[v];
                        break;
                    }
                }
            }

            // Prevod velkych hodnot
            if (Math.Abs(input) >= 1000)
            {
                Dictionary<int, string> siHigh = new Dictionary<int, string>
                {
                    [12] = "T",
                    [9] = "G",
                    [6] = "M",
                    [3] = "k",
                    //[2] = "h",
                    //[1] = "da",
                };

                foreach (var v in siHigh.Keys)
                {
                    if (input != 0 && Math.Abs(input) >= Math.Pow(10, v))
                    {
                        res = input / Math.Pow(10, Math.Abs(v));
                        suffix = siHigh[v];
                        break;
                    }
                }
            }

            return double.IsNaN(res) ? input.ToString() : $"{res}{suffix}s";
        }

        private string ValueAxisLabelFormatterHz(double input)
        {
            double res = double.NaN;
            string suffix = string.Empty;

            // Prevod malych hodnot
            if (Math.Abs(input) <= 0.001)
            {
                Dictionary<int, string> siLow = new Dictionary<int, string>
                {
                    [-12] = "p",
                    [-9] = "n",
                    [-6] = "μ",
                    [-3] = "m",
                    //[-2] = "c",
                    //[-1] = "d",
                };

                foreach (var v in siLow.Keys)
                {
                    if (input != 0 && Math.Abs(input) <= Math.Pow(10, v))
                    {
                        res = input * Math.Pow(10, Math.Abs(v));
                        suffix = siLow[v];
                        break;
                    }
                }
            }

            // Prevod velkych hodnot
            if (Math.Abs(input) >= 1000)
            {
                Dictionary<int, string> siHigh = new Dictionary<int, string>
                {
                    [12] = "T",
                    [9] = "G",
                    [6] = "M",
                    [3] = "k",
                    //[2] = "h",
                    //[1] = "da",
                };

                foreach (var v in siHigh.Keys)
                {
                    if (input != 0 && Math.Abs(input) >= Math.Pow(10, v))
                    {
                        res = input / Math.Pow(10, Math.Abs(v));
                        suffix = siHigh[v];
                        break;
                    }
                }
            }

            return double.IsNaN(res) ? input.ToString() : $"{res}{suffix}Hz";
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            this.wfdVM.Export();
            this.wfdI.ResetAllAxes();
            this.wfdQ.ResetAllAxes();
            this.wfdF.ResetAllAxes();
        }

        private void import_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "txt Files (*.txt)|*.txt";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                this.wfaVM.Path = filename;
                this.splashA.Visibility = Visibility.Visible;
                this.bwA.RunWorkerAsync();
            }            
        }

        private void design_Click(object sender, RoutedEventArgs e)
        {
            this.splashD.Visibility = Visibility.Visible;
            this.bwD.RunWorkerAsync();           
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void TextBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void anType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO
        }

        private void modType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.wfdVM.Modulation = this.modType.SelectedItem.ToString();
        }

        private void desType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.wfdVM.Type = this.desType.SelectedItem.ToString();
            if(this.desType.SelectedItem.ToString().Contains("Sine"))
            {
                this.desFs.IsEnabled = true;
                this.desGain.IsEnabled = true;
                this.desLength.IsEnabled = true;
                this.desRMS.IsEnabled = true;
                this.desBitwidth.IsEnabled = true;
                this.desFreq.IsEnabled = true;
                this.desPhoff.IsEnabled = true;
                this.modType.IsEnabled = false;
                this.desFsoff.IsEnabled = false;
                this.desOFDMN.IsEnabled = false;
                this.desFFTLength.IsEnabled = false;
                this.desCPLength.IsEnabled = false;
                this.desNSymbols.IsEnabled = false;
                this.desOS.IsEnabled = false;
                this.desDistance.IsEnabled = false;
                this.desSeed.IsEnabled = false;
            }
            else if(this.desType.SelectedItem.ToString().Contains("OFDM"))
            {
                this.desFs.IsEnabled = true;
                this.desGain.IsEnabled = true;
                this.desLength.IsEnabled = true;
                this.desRMS.IsEnabled = true;
                this.desBitwidth.IsEnabled = true;
                this.desFreq.IsEnabled = false;
                this.desPhoff.IsEnabled = true;
                this.modType.IsEnabled = true;
                this.desFsoff.IsEnabled = true;
                this.desOFDMN.IsEnabled = true;
                this.desFFTLength.IsEnabled = true;
                this.desCPLength.IsEnabled = true;
                this.desNSymbols.IsEnabled = true;
                this.desOS.IsEnabled = true;
                this.desDistance.IsEnabled = true;
                this.desSeed.IsEnabled = true;
            }
            else
            {
                this.desFs.IsEnabled = true;
                this.desGain.IsEnabled = true;
                this.desLength.IsEnabled = true;
                this.desRMS.IsEnabled = true;
                this.desBitwidth.IsEnabled = true;
                this.desFreq.IsEnabled = false;
                this.desPhoff.IsEnabled = false;
                this.modType.IsEnabled = false;
                this.desFsoff.IsEnabled = false;
                this.desOFDMN.IsEnabled = false;
                this.desFFTLength.IsEnabled = false;
                this.desCPLength.IsEnabled = false;
                this.desNSymbols.IsEnabled = false;
                this.desOS.IsEnabled = false;
                this.desDistance.IsEnabled = false;
                this.desSeed.IsEnabled = false;
            }
        }
    }
}
