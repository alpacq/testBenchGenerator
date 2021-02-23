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
using testBenchGenerator.FIRDesigner.ViewModel;
using testBenchGenerator.FIRDesigner.Model;

namespace testBenchGenerator.FIRDesigner.View
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
            this.wt.SelectedIndex = 0;
            this.ft.SelectedIndex = 0;
            this.pt.SelectedIndex = 0;
            this.viewModel.WinType = (WindowType)Convert.ToInt32(this.wt.SelectedIndex);
            this.viewModel.UpdateWindow();
            this.winResp.ResetAllAxes();
            this.winChart.ResetAllAxes();
        }

        private void design_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.Update();
            this.firChart.ResetAllAxes();
            this.firResp.ResetAllAxes();
            this.winResp.ResetAllAxes();
            this.winChart.ResetAllAxes();
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "Text File|*.txt";
            dlg.Title = "Export Filter Coefficients";
            dlg.ShowDialog();

            if (dlg.FileName != "")
            {
                string[] data = new string[3];
                data[0] = "Filter Order: " + this.viewModel.Length + " Sampling Frequency (Hz): " + this.viewModel.Fs.ToString("F6") + " Cut-Off Frequency Lo (Hz): " + this.viewModel.LowFreq.ToString("F6") + " Cut-Off Frequency Hi (Hz): " + this.viewModel.HighFreq.ToString("F6") + "\n\n";

                data[1] = this.viewModel.WindowedImpulseResponse[0].ToString("F7");
                data[2] = "float coeff[] = {" + this.viewModel.WindowedImpulseResponse[0].ToString("F7") + "f";
                for (int n = 1; n < this.viewModel.Length; n++)
                {
                    data[1] += "," + this.viewModel.WindowedImpulseResponse[n].ToString("F9");
                    data[2] += "," + this.viewModel.WindowedImpulseResponse[n].ToString("F7") + "f";
                }
                data[1] += "\n\n";
                data[2] += "};";

                System.IO.File.WriteAllLines(dlg.FileName, data);
                MessageBox.Show("Coefficients written to file!", "Export Coefficients", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void wt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.viewModel.WinType = (WindowType)Convert.ToInt32(this.wt.SelectedIndex);
            this.viewModel.UpdateWindow();
            this.winResp.ResetAllAxes();
            this.winChart.ResetAllAxes();
        }

        private void ft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.viewModel.FiltType = (FilterType)Convert.ToInt32(this.ft.SelectedIndex);
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

        private void fs_GotFocus(object sender, RoutedEventArgs e)
        {
            this.fs.SelectAll();
        }

        private void len_GotFocus(object sender, RoutedEventArgs e)
        {
            this.len.SelectAll();
        }

        private void ss_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ss.SelectAll();
        }

        private void lf_GotFocus(object sender, RoutedEventArgs e)
        {
            this.lf.SelectAll();
        }

        private void hf_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hf.SelectAll();
        }

        private void fs_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.fs.SelectAll();
        }

        private void len_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.len.SelectAll();
        }

        private void ss_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.ss.SelectAll();
        }

        private void lf_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.lf.SelectAll();
        }

        private void hf_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.hf.SelectAll();
        }

        private void pt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.viewModel.PlotType = this.pt.SelectedItem.ToString();
        }
    }
}
