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
    /// Interaction logic for WaveformDesignerView.xaml
    /// </summary>
    public partial class WaveformDesignerView : UserControl
    {
        private WaveformDesignerViewModel wfdVM;        

        public WaveformDesignerView()
        {
            InitializeComponent();            
            this.wfdVM = new WaveformDesignerViewModel(new WaveformDesigner(), this);
            this.DataContext = this.wfdVM;
            design.Style = (Style)FindResource(typeof(Button));
            design.Template = (ControlTemplate)FindResource("btnTmplt");
            export.Style = (Style)FindResource(typeof(Button));
            export.Template = (ControlTemplate)FindResource("btnTmplt");
            desType.Style = (Style)FindResource("ComboBoxFlatStyle");
            modType.Style = (Style)FindResource("ComboBoxFlatStyle");
            desRadix.Style = (Style)FindResource("ComboBoxFlatStyle");
            desDel.Style = (Style)FindResource("ComboBoxFlatStyle");
            this.desType.SelectedIndex = 0;
            this.modType.SelectedIndex = 0;
            this.desRadix.SelectedIndex = 0;
            this.desDel.SelectedIndex = 0;
            this.wfdI.ResetAllAxes();
            this.wfdQ.ResetAllAxes();
            this.wfdF.ResetAllAxes();
            this.splashD.Visibility = Visibility.Hidden;
        }    

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void TextBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void modType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.wfdVM.Modulation = this.modType.SelectedItem.ToString();
        }

        private void desType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.wfdVM.Type = this.desType.SelectedItem.ToString();
            if (this.desType.SelectedItem.ToString().Contains("Sine") || this.desType.SelectedItem.ToString().Contains("Square") || this.desType.SelectedItem.ToString().Contains("Saw") || this.desType.SelectedItem.ToString().Contains("Triangle"))
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
                //this.desRand.Visibility = Visibility.Collapsed;
                //this.desSeed.Visibility = Visibility.Collapsed;
                //this.drand.Visibility = Visibility.Collapsed;
                //this.dseed.Visibility = Visibility.Collapsed;
            }
            else if (this.desType.SelectedItem.ToString().Contains("OFDM"))
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
                //this.desRand.Visibility = Visibility.Visible;
                //this.desSeed.Visibility = Visibility.Visible;
                //this.drand.Visibility = Visibility.Visible;
                //this.dseed.Visibility = Visibility.Visible;
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
                //this.desRand.Visibility = Visibility.Collapsed;
                //this.desSeed.Visibility = Visibility.Collapsed;
                //this.drand.Visibility = Visibility.Collapsed;
                //this.dseed.Visibility = Visibility.Collapsed;
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
