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
using testBenchGenerator.Common;
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
        private BackgroundWorker bwAA;
        public AnalyzerView()
        {
            InitializeComponent();
            wfdF.Style = (Style)FindResource(typeof(OxyPlot.Wpf.Plot));
            wfdI.Style = (Style)FindResource(typeof(OxyPlot.Wpf.Plot));
            wfdQ.Style = (Style)FindResource(typeof(OxyPlot.Wpf.Plot));
            wfaF.Style = (Style)FindResource(typeof(OxyPlot.Wpf.Plot));
            wfaI.Style = (Style)FindResource(typeof(OxyPlot.Wpf.Plot));
            wfaQ.Style = (Style)FindResource(typeof(OxyPlot.Wpf.Plot));
            this.bwD = new BackgroundWorker();
            this.bwA = new BackgroundWorker();
            this.bwAA = new BackgroundWorker();
            this.bwD.DoWork += BwD_DoWork;
            this.bwA.DoWork += BwA_DoWork;
            this.bwAA.DoWork += BwAA_DoWork;
            this.bwD.RunWorkerCompleted += BwD_RunWorkerCompleted;
            this.bwA.RunWorkerCompleted += BwA_RunWorkerCompleted;
            this.bwAA.RunWorkerCompleted += BwAA_RunWorkerCompleted;
            this.wfdVM = new WaveformDesignerViewModel(new WaveformDesigner());
            this.wfaVM = new WaveformAnalyzerViewModel(new WaveformAnalyzer());
            this.designer.DataContext = this.wfdVM;
            this.analyzer.DataContext = this.wfaVM;
            this.desType.SelectedIndex = 0;
            this.modType.SelectedIndex = 0;
            this.anType.SelectedIndex = 0;
            this.desRadix.SelectedIndex = 0;
            this.desDel.SelectedIndex = 0;
            this.anRadix.SelectedIndex = 0;
            this.anDel.SelectedIndex = 0;
            this.wfdI.ResetAllAxes();
            this.wfdQ.ResetAllAxes();
            this.wfdF.ResetAllAxes();
            this.splashD.Visibility = Visibility.Hidden;
            this.splashA.Visibility = Visibility.Hidden;
            this.splashAA.Visibility = Visibility.Hidden;
        }

        private void BwAA_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.splashAA.Visibility = Visibility.Hidden;
            this.anInfoBlock.Text = DateTime.Now.ToLongTimeString() + " Waveform analyzed.";
        }

        private void BwAA_DoWork(object sender, DoWorkEventArgs e)
        {
            this.wfaVM.Analyze();
        }

        private void BwA_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.wfaI.ResetAllAxes();
            this.wfaQ.ResetAllAxes();
            this.wfaF.ResetAllAxes();
            this.splashA.Visibility = Visibility.Hidden;
            this.anInfoBlock.Text = DateTime.Now.ToLongTimeString() + " Waveform imported successfully.";
            this.splashAA.Visibility = Visibility.Visible;
            this.bwAA.RunWorkerAsync();

        }

        private void BwD_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.wfdI.ResetAllAxes();
            this.wfdQ.ResetAllAxes();
            this.wfdF.ResetAllAxes();
            this.splashD.Visibility = Visibility.Hidden;
            this.desInfoBlock.Text = DateTime.Now.ToLongTimeString() + " Waveform designed.";
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
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "Text File|*.txt";
            dlg.Title = "Export Waveform";
            dlg.FileName = (this.wfdVM.Type.Contains("Sine") ? 
                            ("sine_" + (this.wfdVM.Freq/1000000).ToString("f2").Replace(".", "p").Replace(",","p") + "_" + (this.wfdVM.Fs/1000000).ToString("f2").Replace(".","p").Replace(",", "p") + "_" + this.wfdVM.RMS.ToString().Replace(".", "p").Replace(",", "p") + "_" + (this.wfdVM.LengthTime*1000).ToString().Replace(",", "p").Replace(".", "p") + "ms.txt") : 
                            "waveform.txt");
            dlg.ShowDialog();

            if (dlg.FileName != "")
            {
                this.wfdVM.Export(dlg.FileName);
                this.wfdI.ResetAllAxes();
                this.wfdQ.ResetAllAxes();
                this.wfdF.ResetAllAxes();

                this.desInfoBlock.Text = DateTime.Now.ToLongTimeString() + " Waveform exported successfully.";
            }
            
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
            this.wfaVM.Type = this.anType.SelectedItem.ToString();
            if (this.anType.SelectedItem.ToString().Contains("Sine"))
            {
                this.anInputMag.Visibility = Visibility.Visible;
                this.anNS.Visibility = Visibility.Collapsed;
                this.anFFT.Visibility = Visibility.Collapsed;
                this.anFreq.Visibility = Visibility.Visible;
                this.anToff.Visibility = Visibility.Collapsed;
                this.anRMS.Visibility = Visibility.Visible;
                this.anAvgEVM.Visibility = Visibility.Collapsed;
                this.anRMSEFs.Visibility = Visibility.Visible;
                this.anAvgSNR.Visibility = Visibility.Collapsed;
                this.anRMSElsbs.Visibility = Visibility.Visible;
                this.anWEVM.Visibility = Visibility.Collapsed;
                this.anPRMSE.Visibility = Visibility.Visible;
                this.anWSNR.Visibility = Visibility.Collapsed;
                this.anGainE.Visibility = Visibility.Visible;
                this.anDCReal.Visibility = Visibility.Visible;
                this.anDCImag.Visibility = Visibility.Visible;
                this.aim.Visibility = Visibility.Visible;
                this.ans.Visibility = Visibility.Collapsed;
                this.afft.Visibility = Visibility.Collapsed;
                this.af.Visibility = Visibility.Visible;
                this.ato.Visibility = Visibility.Collapsed;
                this.arms.Visibility = Visibility.Visible;
                this.aae.Visibility = Visibility.Collapsed;
                this.armsfs.Visibility = Visibility.Visible;
                this.aas.Visibility = Visibility.Collapsed;
                this.armslsbs.Visibility = Visibility.Visible;
                this.awe.Visibility = Visibility.Collapsed;
                this.aprmse.Visibility = Visibility.Visible;
                this.aws.Visibility = Visibility.Collapsed;
                this.age.Visibility = Visibility.Visible;
                this.adcr.Visibility = Visibility.Visible;
                this.adci.Visibility = Visibility.Visible;
            }
            else if (this.anType.SelectedItem.ToString().Contains("OFDM"))
            {
                this.anInputMag.Visibility = Visibility.Collapsed;
                this.anNS.Visibility = Visibility.Visible;
                this.anFFT.Visibility = Visibility.Visible;
                this.anFreq.Visibility = Visibility.Collapsed;
                this.anToff.Visibility = Visibility.Visible;
                this.anRMS.Visibility = Visibility.Collapsed;
                this.anAvgEVM.Visibility = Visibility.Visible;
                this.anRMSEFs.Visibility = Visibility.Collapsed;
                this.anAvgSNR.Visibility = Visibility.Visible;
                this.anRMSElsbs.Visibility = Visibility.Collapsed;
                this.anWEVM.Visibility = Visibility.Visible;
                this.anPRMSE.Visibility = Visibility.Collapsed;
                this.anWSNR.Visibility = Visibility.Visible;
                this.anGainE.Visibility = Visibility.Collapsed;
                this.anDCReal.Visibility = Visibility.Collapsed;
                this.anDCImag.Visibility = Visibility.Collapsed;
                this.aim.Visibility = Visibility.Collapsed;
                this.ans.Visibility = Visibility.Visible;
                this.afft.Visibility = Visibility.Visible;
                this.af.Visibility = Visibility.Collapsed;
                this.ato.Visibility = Visibility.Visible;
                this.arms.Visibility = Visibility.Collapsed;
                this.aae.Visibility = Visibility.Visible;
                this.armsfs.Visibility = Visibility.Collapsed;
                this.aas.Visibility = Visibility.Visible;
                this.armslsbs.Visibility = Visibility.Collapsed;
                this.awe.Visibility = Visibility.Visible;
                this.aprmse.Visibility = Visibility.Collapsed;
                this.aws.Visibility = Visibility.Visible;
                this.age.Visibility = Visibility.Collapsed;
                this.adcr.Visibility = Visibility.Collapsed;
                this.adci.Visibility = Visibility.Collapsed;
            }
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
                this.desFreq.Visibility = Visibility.Visible;
                this.desPhoff.Visibility = Visibility.Visible;
                this.modType.Visibility = Visibility.Collapsed;
                this.desFsoff.Visibility = Visibility.Collapsed;
                this.desInputMag.Visibility = Visibility.Visible;
                this.desOFDMN.Visibility = Visibility.Collapsed;
                this.desFFTLength.Visibility = Visibility.Collapsed;
                this.desCPLength.Visibility = Visibility.Collapsed;
                this.desNSymbols.Visibility = Visibility.Collapsed;
                this.desOS.Visibility = Visibility.Collapsed;
                this.desDistance.Visibility = Visibility.Collapsed;
                this.df.Visibility = Visibility.Visible;
                this.dpo.Visibility = Visibility.Visible;
                this.dmod.Visibility = Visibility.Collapsed;
                this.dfo.Visibility = Visibility.Collapsed;
                this.dim.Visibility = Visibility.Visible;
                this.dofdmn.Visibility = Visibility.Collapsed;
                this.dfft.Visibility = Visibility.Collapsed;
                this.dcp.Visibility = Visibility.Collapsed;
                this.dns.Visibility = Visibility.Collapsed;
                this.dos.Visibility = Visibility.Collapsed;
                this.ddis.Visibility = Visibility.Collapsed;
                this.desRand.Visibility = Visibility.Collapsed;
                this.desSeed.Visibility = Visibility.Collapsed;
                this.drand.Visibility = Visibility.Collapsed;
                this.dseed.Visibility = Visibility.Collapsed;
            }
            else if(this.desType.SelectedItem.ToString().Contains("OFDM"))
            {
                this.desFreq.Visibility = Visibility.Collapsed;
                this.desPhoff.Visibility = Visibility.Visible;
                this.modType.Visibility = Visibility.Visible;
                this.desFsoff.Visibility = Visibility.Visible;
                this.desInputMag.Visibility = Visibility.Collapsed;
                this.desOFDMN.Visibility = Visibility.Visible;
                this.desFFTLength.Visibility = Visibility.Visible;
                this.desCPLength.Visibility = Visibility.Visible;
                this.desNSymbols.Visibility = Visibility.Visible;
                this.desOS.Visibility = Visibility.Visible;
                this.desDistance.Visibility = Visibility.Visible;
                this.df.Visibility = Visibility.Collapsed;
                this.dpo.Visibility = Visibility.Visible;
                this.dmod.Visibility = Visibility.Visible;
                this.dfo.Visibility = Visibility.Visible;
                this.dim.Visibility = Visibility.Collapsed;
                this.dofdmn.Visibility = Visibility.Visible;
                this.dfft.Visibility = Visibility.Visible;
                this.dcp.Visibility = Visibility.Visible;
                this.dns.Visibility = Visibility.Visible;
                this.dos.Visibility = Visibility.Visible;
                this.ddis.Visibility = Visibility.Visible;
                this.desRand.Visibility = Visibility.Visible;
                this.desSeed.Visibility = Visibility.Visible;
                this.drand.Visibility = Visibility.Visible;
                this.dseed.Visibility = Visibility.Visible;
            }
            else
            {
                this.desFreq.Visibility = Visibility.Collapsed;
                this.desPhoff.Visibility = Visibility.Collapsed;
                this.modType.Visibility = Visibility.Collapsed;
                this.desFsoff.Visibility = Visibility.Collapsed;
                this.desInputMag.Visibility = Visibility.Collapsed;
                this.desOFDMN.Visibility = Visibility.Collapsed;
                this.desFFTLength.Visibility = Visibility.Collapsed;
                this.desCPLength.Visibility = Visibility.Collapsed;
                this.desNSymbols.Visibility = Visibility.Collapsed;
                this.desOS.Visibility = Visibility.Collapsed;
                this.desDistance.Visibility = Visibility.Collapsed;
                this.df.Visibility = Visibility.Collapsed;
                this.dpo.Visibility = Visibility.Collapsed;
                this.dmod.Visibility = Visibility.Collapsed;
                this.dfo.Visibility = Visibility.Collapsed;
                this.dim.Visibility = Visibility.Collapsed;
                this.dofdmn.Visibility = Visibility.Collapsed;
                this.dfft.Visibility = Visibility.Collapsed;
                this.dcp.Visibility = Visibility.Collapsed;
                this.dns.Visibility = Visibility.Collapsed;
                this.dos.Visibility = Visibility.Collapsed;
                this.ddis.Visibility = Visibility.Collapsed;
                this.desRand.Visibility = Visibility.Collapsed;
                this.desSeed.Visibility = Visibility.Collapsed;
                this.drand.Visibility = Visibility.Collapsed;
                this.dseed.Visibility = Visibility.Collapsed;
            }
        }

        private void desRadix_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.wfdVM.Radix = (Radix)(this.desRadix.SelectedIndex);
        }

        private void desDel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.wfdVM.Delimiter = (Delimiter)(this.desDel.SelectedIndex);
        }

        private void anRadix_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.wfaVM.Radix = (Radix)(this.anRadix.SelectedIndex);
        }

        private void anDel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.wfaVM.Delimiter = (Delimiter)(this.anDel.SelectedIndex);
        }
    }
}
