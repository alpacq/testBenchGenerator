using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;

namespace testBenchGenerator.FIRDesignerWinForms
{
    public partial class frmMain : Form
    {

        enum WindowType
        {
            Rectangular,
            Triangular,
            Welch,
            Sine,
            Hann,
            Hamming,
            Blackman,
            Nuttall,
            BlackmanNuttall,
            BlackmanHarris,
            FlatTop
        }

        enum FilterType
        {
            LowPass,
            HighPass,
            BandPass,
            BandStop
        }

        /* Parameters */
        double Ts;
        double LowFreq;
        double HighFreq;
        int Length;
        int ShiftSamples;
        int FreqSamples;
        WindowType WinType;
        FilterType FiltType;

        /* Time Domain */
        double[] TimeVector;
        double[] ImpulseResponse;
        double[] StepResponse;
        double[] Window;
        double[] WindowedImpulseResponse;
        double[] WindowedStepResponse;

        /* Frequency Domain */
        double[] FrequencyVectorHz;
        double[] ImpRespMag;
        double[] WinRespMag;
        double[] WinMag;
        
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            /* Initial settings */
            this.Ts = 0.01;
            this.LowFreq = 20.0;
            this.HighFreq = 20.0;
            this.Length = 64;
            this.ShiftSamples = 32;
            this.WinType = WindowType.Hamming;
            this.FiltType = FilterType.LowPass;
            this.FreqSamples = 256;

            cmbWindow.SelectedIndex = 5;
            radViewImpulse.Checked = true;
            cmbDisplay.SelectedIndex = 0;

            ComputeTimeVector();
            ComputeWindow();
            ComputeResponses();
            ComputeWindowedResponses();

            ComputeFrequencyVector();
            ComputeRespBode();
            ComputeWindowDFT();
            
            UpdateCharts();
        }

        private void UpdateCharts()
        {
            chrtFilterTimeDomain.Series[0].Points.DataBindXY(TimeVector, ImpulseResponse);
            chrtFilterTimeDomain.Series[1].Points.DataBindXY(TimeVector, WindowedImpulseResponse);
            chrtFilterTimeDomain.Series[2].Points.DataBindXY(TimeVector, StepResponse);
            chrtFilterTimeDomain.Series[3].Points.DataBindXY(TimeVector, WindowedStepResponse);
            chrtFilterTimeDomain.Series[2].Enabled = false;
            chrtFilterTimeDomain.Series[3].Enabled = false;

            chrtFilterTimeDomain.ChartAreas[0].RecalculateAxesScale();
            chrtFilterTimeDomain.Update();

            chrtWindowTimeDomain.Series[0].Points.DataBindXY(TimeVector, Window);
            chrtWindowTimeDomain.ChartAreas[0].RecalculateAxesScale();

            chrtFilterFrequencyDomain.Series[0].Points.DataBindXY(FrequencyVectorHz, ImpRespMag);
            chrtFilterFrequencyDomain.Series[1].Points.DataBindXY(FrequencyVectorHz, WinRespMag);
            chrtFilterFrequencyDomain.ChartAreas[0].AxisX.Interval = 10.0 * (0.5 / this.Ts) / ((double)this.FreqSamples - 1.0);
            chrtFilterFrequencyDomain.ChartAreas[0].RecalculateAxesScale();

            chrtWindowFrequencyDomain.Series[0].Points.DataBindXY(FrequencyVectorHz, WinMag);
            chrtWindowFrequencyDomain.ChartAreas[0].AxisX.Interval = 10.0 * (0.5 / this.Ts) / ((double)this.FreqSamples - 1.0);
            chrtWindowFrequencyDomain.ChartAreas[0].RecalculateAxesScale();
        }

        /* Time Domain Functions */
        private void ComputeTimeVector()
        {
            TimeVector = new double[this.Length];

            for (int n = 0; n < this.Length; n++)
            {
                TimeVector[n] = n * this.Ts;
            }
        }
        
        private void ComputeResponses()
        {
            ImpulseResponse = new double[this.Length];
            StepResponse = new double[this.Length];

            for (int n = 0; n < this.Length; n++)
            {
                if (n != this.ShiftSamples)
                {
                    switch (this.FiltType)
                    {
                        case FilterType.LowPass:
                            ImpulseResponse[n] = Sin(2.0 * PI * this.LowFreq * this.Ts * (n - this.ShiftSamples)) / (PI * this.Ts * (n - this.ShiftSamples));
                            break;
                        case FilterType.HighPass:                            
                            ImpulseResponse[n] = (Sin(PI * (n - this.ShiftSamples)) - Sin(2.0 * PI * this.LowFreq * this.Ts * (n - this.ShiftSamples))) / (PI * this.Ts * (n - this.ShiftSamples));
                            break;
                        case FilterType.BandPass:
                            ImpulseResponse[n] = (Sin(2.0 * PI * this.HighFreq * this.Ts * (n - this.ShiftSamples)) - Sin(2.0 * PI * this.LowFreq * this.Ts * (n - this.ShiftSamples))) / (PI * this.Ts * (n - this.ShiftSamples));
                            break;
                        case FilterType.BandStop:
                            ImpulseResponse[n] = (Sin(2.0 * PI * this.LowFreq * this.Ts * (n - this.ShiftSamples)) - Sin(2.0 * PI * this.HighFreq * this.Ts * (n - this.ShiftSamples)) + Sin(PI * (n - this.ShiftSamples))) / (PI * this.Ts * (n - this.ShiftSamples));
                            break;
                    }
                } else /* Avoid divide-by-zero, limit is 2*fc */
                {
                    switch (this.FiltType)
                    {
                        case FilterType.LowPass:
                            ImpulseResponse[n] = 2.0 * this.LowFreq;
                            break;
                        case FilterType.HighPass:
                            ImpulseResponse[n] = 1.0 / this.Ts - 2.0 * this.LowFreq;
                            break;
                        case FilterType.BandPass:
                            ImpulseResponse[n] = 2.0 * this.HighFreq - 2.0 * this.LowFreq;
                            break;
                        case FilterType.BandStop:
                            ImpulseResponse[n] = 2.0 * this.LowFreq - 2.0 * this.HighFreq + 1.0 / this.Ts;
                            break;
                    }
                }
                           
            }

            /* Normalise by DC gain to achieve 0dB gain at DC and then compute step response */
            for (int n =  0; n < this.Length; n++)
            {
                ImpulseResponse[n] *= this.Ts;

                if (n == 0)
                {
                    StepResponse[n] = 0.5 * ImpulseResponse[n];
                }
                else
                {
                    StepResponse[n] = StepResponse[n - 1] + 0.5 * (ImpulseResponse[n] + ImpulseResponse[n - 1]);
                }
            }

        }

        private void ComputeWindow()
        {
            Window = new double[this.Length];
            
            for (int n = 0; n < this.Length; n++)
            {
                switch (this.WinType)
                {
                    case WindowType.Rectangular:
                        Window[n] = 1.0;
                        break;

                    case WindowType.Triangular:
                        Window[n] = 1.0 - Abs((n - 0.5 * this.Length) / (0.5 * this.Length));
                        break;

                    case WindowType.Welch:
                        Window[n] = 1.0 - Pow((n - 0.5 * this.Length) / (0.5 * this.Length), 2.0);
                        break;

                    case WindowType.Sine:
                        Window[n] = Sin(PI * n / ((double) this.Length));
                        break;

                    case WindowType.Hann:
                        Window[n] = 0.5 * (1 - Cos(2.0 * PI * n / ((double) this.Length)));
                        break;

                    case WindowType.Hamming:
                        Window[n] = (25.0 / 46.0) - (21.0 / 46.0) * Cos(2.0 * PI * n / ((double) this.Length));
                        break;

                    case WindowType.Blackman:
                        Window[n] = 0.42 - 0.5 * Cos(2.0 * PI * n / ((double) this.Length)) + 0.08 * Cos(4.0 * PI * n / ((double) this.Length));
                        break;

                    case WindowType.Nuttall:
                        Window[n] = 0.355768 - 0.487396 * Cos(2.0 * PI * n / ((double) this.Length)) + 0.144232 * Cos(4.0 * PI * n / ((double) this.Length)) - 0.012604 * Cos(6.0 * PI * n / ((double) this.Length));
                        break;

                    case WindowType.BlackmanNuttall:
                        Window[n] = 0.3635819 - 0.4891775 * Cos(2.0 * PI * n / ((double) this.Length)) + 0.1365995 * Cos(4.0 * PI * n / ((double) this.Length)) - 0.0106411 * Cos(6.0 * PI * n / ((double) this.Length));
                        break;

                    case WindowType.BlackmanHarris:
                        Window[n] = 0.35875 - 0.48829 * Cos(2.0 * PI * n / ((double) this.Length)) + 0.14128 * Cos(4.0 * PI * n / ((double) this.Length)) - 0.01168 * Cos(6.0 * PI * n / ((double) this.Length));
                        break;

                    case WindowType.FlatTop:
                        Window[n] = 0.21557895 - 0.41663158 * Cos(2.0 * PI * n / ((double) this.Length)) + 0.277263158 * Cos(4.0 * PI * n / ((double) this.Length)) - 0.083578947 * Cos(6.0 * PI * n / ((double) this.Length)) + 0.006947368 * Cos(8.0 * PI * n / ((double) this.Length));
                        break;

                    default:
                        Window[n] = 1.0;
                        break;
                }
            }

        }

        private void ComputeWindowedResponses()
        {
            WindowedImpulseResponse = new double[this.Length];
            WindowedStepResponse = new double[this.Length];

            
            for (int n = 0; n < this.Length; n++)
            {
                WindowedImpulseResponse[n] = ImpulseResponse[n] * Window[n];


                if (n == 0)
                {
                    WindowedStepResponse[n] = 0.5 * WindowedStepResponse[n];
                }
                else
                {
                    WindowedStepResponse[n] = WindowedStepResponse[n - 1] + 0.5 * (WindowedImpulseResponse[n] + WindowedImpulseResponse[n - 1]);
                }
            }

        }

        /* Frequency Domain Functions */
        private void ComputeFrequencyVector()
        {
            FrequencyVectorHz = new double[this.FreqSamples];

            double df = (0.5 / this.Ts) / ((double) this.FreqSamples - 1.0);

            for (int n = 0; n < this.FreqSamples; n++)
            {
                FrequencyVectorHz[n] = n * df;
            }
        }


        private void ComputeRespBode()
        {
            ImpRespMag   = new double[this.FreqSamples];
            WinRespMag   = new double[this.FreqSamples];

            for (int fIndex = 0; fIndex < this.FreqSamples; fIndex++)
            {
                double re = 0.0;
                double im = 0.0;
                double reWin = 0.0;
                double imWin = 0.0;

                for (int n = 0; n < this.Length; n++)
                {
                    re = re + ImpulseResponse[n] * Cos(2.0 * PI * FrequencyVectorHz[fIndex] * n * this.Ts);
                    im = im - ImpulseResponse[n] * Sin(2.0 * PI * FrequencyVectorHz[fIndex] * n * this.Ts);
                    reWin = reWin + WindowedImpulseResponse[n] * Cos(2.0 * PI * FrequencyVectorHz[fIndex] * n * this.Ts);
                    imWin = imWin - WindowedImpulseResponse[n] * Sin(2.0 * PI * FrequencyVectorHz[fIndex] * n * this.Ts);
                }

                ImpRespMag[fIndex] = 10.0 * Log10(re * re + im * im);
                WinRespMag[fIndex] = 10.0 * Log10(reWin * reWin + imWin * imWin);
            }
        }

        private double GetGainAtCutOff()
        {
            double re = 0.0;
            double im = 0.0;

            for (int n = 0; n < this.Length; n++)
            {
                re = re + ImpulseResponse[n] * Cos(2.0 * PI * this.LowFreq * n * this.Ts);
                im = im - ImpulseResponse[n] * Sin(2.0 * PI * this.LowFreq * n * this.Ts);
            }

            return (10.0 * Log10(re * re + im * im));
        }

        private void ComputeWindowDFT()
        {
            WinMag = new double[this.FreqSamples];

            for (int fIndex = 0; fIndex < this.FreqSamples; fIndex++)
            {
                double re = 0.0;
                double im = 0.0;

                for (int n = 0; n < this.Length; n++)
                {
                    re = re + Window[n] * Cos(2.0 * PI * FrequencyVectorHz[fIndex] * n * this.Ts);
                    im = im - Window[n] * Sin(2.0 * PI * FrequencyVectorHz[fIndex] * n * this.Ts);
                }

                WinMag[fIndex] = 10.0 * Log10(re * re + im * im);
            }
        }

        private void UpdatePlotSettings()
        {
            if (radViewImpulse.Checked)
            {
                if (cmbDisplay.SelectedIndex == 0)
                {
                    chrtFilterTimeDomain.Series[0].Enabled = true;
                    chrtFilterTimeDomain.Series[1].Enabled = true;
                }
                else if (cmbDisplay.SelectedIndex == 1)
                {
                    chrtFilterTimeDomain.Series[0].Enabled = true;
                    chrtFilterTimeDomain.Series[1].Enabled = false;
                }
                else if (cmbDisplay.SelectedIndex == 2)
                {
                    chrtFilterTimeDomain.Series[0].Enabled = false;
                    chrtFilterTimeDomain.Series[1].Enabled = true;
                }

                chrtFilterTimeDomain.Series[2].Enabled = false;
                chrtFilterTimeDomain.Series[3].Enabled = false;

            }
            else
            {
                if (cmbDisplay.SelectedIndex == 0)
                {
                    chrtFilterTimeDomain.Series[2].Enabled = true;
                    chrtFilterTimeDomain.Series[3].Enabled = true;
                }
                else if (cmbDisplay.SelectedIndex == 1)
                {
                    chrtFilterTimeDomain.Series[2].Enabled = true;
                    chrtFilterTimeDomain.Series[3].Enabled = false;
                }
                else if (cmbDisplay.SelectedIndex == 2)
                {
                    chrtFilterTimeDomain.Series[2].Enabled = false;
                    chrtFilterTimeDomain.Series[3].Enabled = true;
                }

                chrtFilterTimeDomain.Series[0].Enabled = false;
                chrtFilterTimeDomain.Series[1].Enabled = false;
            }

            if (cmbDisplay.SelectedIndex == 0)
            {
                chrtFilterFrequencyDomain.Series[0].Enabled = true;
                chrtFilterFrequencyDomain.Series[1].Enabled = true;
            }
            else if (cmbDisplay.SelectedIndex == 1)
            {
                chrtFilterFrequencyDomain.Series[0].Enabled = true;
                chrtFilterFrequencyDomain.Series[1].Enabled = false;
            }
            else if (cmbDisplay.SelectedIndex == 2)
            {
                chrtFilterFrequencyDomain.Series[0].Enabled = false;
                chrtFilterFrequencyDomain.Series[1].Enabled = true;
            }

            chrtFilterTimeDomain.ChartAreas[0].RecalculateAxesScale();
        }

        private void radViewImpulse_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlotSettings();
        }

        private void cmbDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePlotSettings();
        }

        private void btnDesignFilter_Click(object sender, EventArgs e)
        {
            this.Ts = 1.0 / Convert.ToDouble(txtSamplingFrequency.Text);
            this.Length = Convert.ToInt32(txtFilterLength.Text);
            this.ShiftSamples = Convert.ToInt32(txtShiftSamples.Text);
            this.WinType = (WindowType) Convert.ToInt32(cmbWindow.SelectedIndex);

            if (radLP.Checked)
            {
                this.FiltType = FilterType.LowPass;
                this.LowFreq = Convert.ToDouble(txtCutOffFrequencyHigh.Text);
            }
            else if (radHP.Checked)
            {
                this.FiltType = FilterType.HighPass;
                this.LowFreq = Convert.ToDouble(txtCutOffFrequency.Text);
            }
            else if (radBP.Checked)
            {
                this.FiltType = FilterType.BandPass;
                this.LowFreq = Convert.ToDouble(txtCutOffFrequency.Text);
                this.HighFreq = Convert.ToDouble(txtCutOffFrequencyHigh.Text);
            }
            else if (radBS.Checked)
            {
                this.FiltType = FilterType.BandStop;
                this.LowFreq = Convert.ToDouble(txtCutOffFrequency.Text);
                this.HighFreq = Convert.ToDouble(txtCutOffFrequencyHigh.Text);
            }

            if (this.Ts < 0.0)
            {
                MessageBox.Show("Sampling frequency cannot be negative.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.LowFreq >= 0.5 / this.Ts || this.HighFreq >= 0.5 / this.Ts)
            {
                MessageBox.Show("Cut-off frequency has to be less than the Nyquist frequency (i.e. sampling frequency / 2).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            if (this.Length < 0 || this.ShiftSamples < 0)
            {
                MessageBox.Show("Total number of samples and sample shift number both need to be integers, greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ComputeTimeVector();
            ComputeResponses();
            ComputeWindow();
            ComputeWindowedResponses();

            ComputeFrequencyVector();
            ComputeRespBode();
            ComputeWindowDFT();
            
            UpdateCharts();
            UpdatePlotSettings();
        }

        private void cmbWindow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWindow.SelectedIndex >= 0)
            {
                this.WinType = (WindowType)Convert.ToInt32(cmbWindow.SelectedIndex);

                ComputeFrequencyVector();
                ComputeWindow();
                ComputeWindowDFT();

                chrtWindowTimeDomain.Series[0].Points.DataBindXY(TimeVector, Window);

                chrtWindowFrequencyDomain.Series[0].Points.DataBindXY(FrequencyVectorHz, WinMag);

                chrtWindowTimeDomain.ChartAreas[0].RecalculateAxesScale();
                chrtWindowFrequencyDomain.ChartAreas[0].RecalculateAxesScale();
            }
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FIR Filter Designer\nWritten by Philip M. Salmony\n29 November 2019\nphilsal.co.uk", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FilterTypeChange()
        {
            if (radLP.Checked)
            {
                txtCutOffFrequency.Enabled = false;
                txtCutOffFrequencyHigh.Enabled = true;
            } else if (radHP.Checked)
            {
                txtCutOffFrequency.Enabled = true;
                txtCutOffFrequencyHigh.Enabled = false;
            } else if (radBP.Checked)
            {
                txtCutOffFrequency.Enabled = true;
                txtCutOffFrequencyHigh.Enabled = true;
            } else if (radBS.Checked)
            {
                txtCutOffFrequency.Enabled = true;
                txtCutOffFrequencyHigh.Enabled = true;
            }
        }

        private void radLP_CheckedChanged(object sender, EventArgs e)
        {
            FilterTypeChange();
        }

        private void radHP_CheckedChanged(object sender, EventArgs e)
        {
            FilterTypeChange();
        }

        private void radBP_CheckedChanged(object sender, EventArgs e)
        {
            FilterTypeChange();
        }

        private void radBS_CheckedChanged(object sender, EventArgs e)
        {
            FilterTypeChange();
        }

        private void exportCoefficientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Text File|*.txt";
            saveFileDialog.Title  = "Export Filter Coefficients";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                string[] data = new string[3];
                data[0] = "Filter Order: " + this.Length + " Sampling Frequency (Hz): " + (1.0 / this.Ts).ToString("F6") + " Cut-Off Frequency Lo (Hz): " + this.LowFreq.ToString("F6") + " Cut-Off Frequency Hi (Hz): " + this.HighFreq.ToString("F6") + "\n\n";

                data[1] = WindowedImpulseResponse[0].ToString("F7");
                data[2] = "float coeff[] = {" + WindowedImpulseResponse[0].ToString("F7") + "f";
                for (int n = 1; n < this.Length; n++)
                {
                    data[1] += "," + WindowedImpulseResponse[n].ToString("F9");
                    data[2] += "," + WindowedImpulseResponse[n].ToString("F7") + "f";
                }
                data[1] += "\n\n";
                data[2] += "};";
                
                System.IO.File.WriteAllLines(saveFileDialog.FileName, data);
                MessageBox.Show("Coefficients written to file!", "Export Coefficients", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void exportTimeDomainDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Text File|*.txt";
            saveFileDialog.Title  = "Export Time Domain Data";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                string[] data = new string[4];
                data[0] = "[TIME DOMAIN DATA (TIME/IMPULSE/STEP)] Filter Order: " + this.Length + " Sampling Frequency (Hz): " + (1.0 / this.Ts).ToString("F6") + " Cut-Off Frequency Lo (Hz): " + this.LowFreq.ToString("F6") + " Cut-Off Frequency Hi (Hz): " + this.HighFreq.ToString("F6") + "\n\n";

                data[1] = TimeVector[0].ToString("F6");
                data[2] = WindowedImpulseResponse[0].ToString("F9");
                data[3] = WindowedStepResponse[0].ToString("F9");
                for (int n = 1; n < this.Length; n++)
                {
                    data[1] += "," + TimeVector[n].ToString("F6");
                    data[2] += "," + WindowedImpulseResponse[n].ToString("F9");
                    data[3] += "," + WindowedStepResponse[n].ToString("F9") ;
                }

                data[1] += "\n\n";
                data[2] += "\n\n";

                System.IO.File.WriteAllLines(saveFileDialog.FileName, data);
                MessageBox.Show("Data written to file!", "Export Time Domain Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void exportFrequencyDomainDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Text File|*.txt";
            saveFileDialog.Title  = "Export Frequency Domain Data";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                string[] data = new string[4];
                data[0] = "[FREQUENCY DOMAIN DATA (FREQ/RAW/WINDOWED)] Filter Order: " + this.Length + " Sampling Frequency (Hz): " + (1.0 / this.Ts).ToString("F6") + " Cut-Off Frequency Lo (Hz): " + this.LowFreq.ToString("F6") + " Cut-Off Frequency Hi (Hz): " + this.HighFreq.ToString("F6") + "\n\n";

                data[1] = FrequencyVectorHz[0].ToString("F6");
                data[2] = ImpRespMag[0].ToString("F9");
                data[3] = WinRespMag[0].ToString("F9");
                for (int n = 1; n < this.FreqSamples; n++)
                {
                    data[1] += "," + FrequencyVectorHz[n].ToString("F6");
                    data[2] += "," + ImpRespMag[n].ToString("F9");
                    data[3] += "," + WinRespMag[n].ToString("F9");
                }
                
                data[1] += "\n\n";
                data[2] += "\n\n";

                System.IO.File.WriteAllLines(saveFileDialog.FileName, data);
                MessageBox.Show("Data written to file!", "Export Frequency Domain Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
} 