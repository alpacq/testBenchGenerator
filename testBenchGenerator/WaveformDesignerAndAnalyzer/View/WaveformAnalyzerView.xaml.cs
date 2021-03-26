using FPGADeveloperTools.Common;
using FPGADeveloperTools.WaveformDesignerAndAnalyzer.Model;
using FPGADeveloperTools.WaveformDesignerAndAnalyzer.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FPGADeveloperTools.WaveformDesignerAndAnalyzer.View
{
    /// <summary>
    /// Interaction logic for WaveformAnalyzerView.xaml
    /// </summary>
    public partial class WaveformAnalyzerView : UserControl
    {
        private WaveformAnalyzerViewModel wfaVM;        

        public WaveformAnalyzerView()
        {
            InitializeComponent();
            this.wfaVM = new WaveformAnalyzerViewModel(new WaveformAnalyzer(), this);
            import.Style = (Style)FindResource(typeof(Button));
            import.Template = (ControlTemplate)FindResource("btnTmplt");
            anType.Style = (Style)FindResource("ComboBoxFlatStyle");
            anRadix.Style = (Style)FindResource("ComboBoxFlatStyle");
            anDel.Style = (Style)FindResource("ComboBoxFlatStyle");
            this.DataContext = this.wfaVM;            
            this.anType.SelectedIndex = 0;
            this.anRadix.SelectedIndex = 0;
            this.anDel.SelectedIndex = 0;
            this.wfaI.ResetAllAxes();
            this.wfaQ.ResetAllAxes();
            this.wfaF.ResetAllAxes();
            this.splashA.Visibility = Visibility.Hidden;
            this.splashAA.Visibility = Visibility.Hidden;
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


        private void anRadix_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.wfaVM.Radix = (Radix)(this.anRadix.SelectedIndex);
        }

        private void anDel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.wfaVM.Delimiter = (Delimiter)(this.anDel.SelectedIndex);
        }

        #region label formatters
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
        #endregion
    }
}
