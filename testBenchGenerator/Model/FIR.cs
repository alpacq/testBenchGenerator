using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace testBenchGenerator.Model
{
    public class FIR
    {
        #region variables and properties
        private double fs;
        private int length;
        private int shiftSamples;
        private double lowFreq;
        private double highFreq;
        private int freqSamples;
        private WindowType winType;
        private FilterType filtType;
        private List<double> timeVector;
        private List<double> impulseResponse;
        private List<double> stepResponse;
        private List<double> window;
        private List<double> windowedImpulseResponse;
        private List<double> windowedStepResponse;
        private List<double> frequencyVectorHz;
        private List<double> impRespMag;
        private List<double> winRespMag;
        private List<double> winMag;

        public double Fs
        {
            get { return this.fs; }
            set { this.fs = value; }
        }

        public int Length
        {
            get { return this.length; }
            set { this.length = value; }
        }

        public int ShiftSamples
        {
            get { return this.shiftSamples; }
            set { this.shiftSamples = value; }
        }

        public double LowFreq
        {
            get { return this.lowFreq; }
            set { this.lowFreq = value; }
        }

        public double HighFreq
        {
            get { return this.highFreq; }
            set { this.highFreq = value; }
        }

        public int FreqSamples
        {
            get { return this.freqSamples; }
            set { this.freqSamples = value; }
        }

        public WindowType WinType
        {
            get { return this.winType; }
            set { this.winType = value; }
        }

        public FilterType FiltType
        {
            get { return this.filtType; }
            set { this.filtType = value; }
        }

        public double Ts
        {
            get { return 1.0 / this.Fs; }
        }

        public List<double> TimeVector
        {
            get { return this.timeVector; }
            set { this.timeVector = value; }
        }

        public List<double> ImpulseResponse
        {
            get { return this.impulseResponse; }
            set { this.impulseResponse = value; }
        }

        public List<double> StepResponse
        {
            get { return this.stepResponse; }
            set { this.stepResponse = value; }
        }

        public List<double> Window
        {
            get { return this.window; }
            set { this.window = value; }
        }

        public List<double> WindowedImpulseResponse
        {
            get { return this.windowedImpulseResponse; }
            set { this.windowedImpulseResponse = value; }
        }

        public List<double> WindowedStepResponse
        {
            get { return this.windowedStepResponse; }
            set { this.windowedStepResponse = value; }
        }

        public List<double> FrequencyVectorHz
        {
            get { return this.frequencyVectorHz; }
            set { this.frequencyVectorHz = value; }
        }

        public List<double> ImpRespMag
        {
            get { return this.impRespMag; }
            set { this.impRespMag = value; }
        }

        public List<double> WinRespMag
        {
            get { return this.winRespMag; }
            set { this.winRespMag = value; }
        }

        public List<double> WinMag
        {
            get { return this.winMag; }
            set { this.winMag = value; }
        }
#endregion        

        #region time domain functions
        private void ComputeTimeVector()
        {
            for (int n = 0; n < this.Length; n++)
            {
                this.TimeVector.Add(n * this.Ts);
            }
        }

        private void ComputeResponses()
        {
            this.StepResponse = new double[this.Length].ToList();
            for (int n = 0; n < this.Length; n++)
            {
                if (n != this.ShiftSamples)
                {
                    switch (this.FiltType)
                    {
                        case FilterType.LowPass:
                            this.ImpulseResponse.Add(Sin(2.0 * PI * this.HighFreq * this.Ts * (n - this.ShiftSamples)) / (PI * this.Ts * (n - this.ShiftSamples)));
                            break;
                        case FilterType.HighPass:
                            this.ImpulseResponse.Add((Sin(PI * (n - this.ShiftSamples)) - Sin(2.0 * PI * this.LowFreq * this.Ts * (n - this.ShiftSamples))) / (PI * this.Ts * (n - this.ShiftSamples)));
                            break;
                        case FilterType.BandPass:
                            this.ImpulseResponse.Add((Sin(2.0 * PI * this.HighFreq * this.Ts * (n - this.ShiftSamples)) - Sin(2.0 * PI * this.LowFreq * this.Ts * (n - this.ShiftSamples))) / (PI * this.Ts * (n - this.ShiftSamples)));
                            break;
                        case FilterType.BandStop:
                            this.ImpulseResponse.Add((Sin(2.0 * PI * this.LowFreq * this.Ts * (n - this.ShiftSamples)) - Sin(2.0 * PI * this.HighFreq * this.Ts * (n - this.ShiftSamples)) + Sin(PI * (n - this.ShiftSamples))) / (PI * this.Ts * (n - this.ShiftSamples)));
                            break;
                    }
                }
                else /* Avoid divide-by-zero, limit is 2*fc */
                {
                    switch (this.FiltType)
                    {
                        case FilterType.LowPass:
                            this.ImpulseResponse.Add(2.0 * this.LowFreq);
                            break;
                        case FilterType.HighPass:
                            this.ImpulseResponse.Add(1.0 / this.Ts - 2.0 * this.LowFreq);
                            break;
                        case FilterType.BandPass:
                            this.ImpulseResponse.Add(2.0 * this.HighFreq - 2.0 * this.LowFreq);
                            break;
                        case FilterType.BandStop:
                            this.ImpulseResponse.Add(2.0 * this.LowFreq - 2.0 * this.HighFreq + 1.0 / this.Ts);
                            break;
                    }
                }

            }

            /* Normalise by DC gain to achieve 0dB gain at DC and then compute step response */
            for (int n = 0; n < this.Length; n++)
            {
                this.ImpulseResponse[n] *= this.Ts;

                if (n == 0)
                {
                    this.StepResponse[n] = (0.5 * this.ImpulseResponse[n]);
                }
                else
                {
                    this.StepResponse[n] = (this.StepResponse[n - 1] + 0.5 * (this.ImpulseResponse[n] + this.ImpulseResponse[n - 1]));
                }
            }

        }

        private void ComputeWindow()
        {
            this.Window = new double[Length].ToList();
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
                        Window[n] = Sin(PI * n / ((double)this.Length));
                        break;

                    case WindowType.Hann:
                        Window[n] = 0.5 * (1 - Cos(2.0 * PI * n / ((double)this.Length)));
                        break;

                    case WindowType.Hamming:
                        Window[n] = (25.0 / 46.0) - (21.0 / 46.0) * Cos(2.0 * PI * n / ((double)this.Length));
                        break;

                    case WindowType.Blackman:
                        Window[n] = 0.42 - 0.5 * Cos(2.0 * PI * n / ((double)this.Length)) + 0.08 * Cos(4.0 * PI * n / ((double)this.Length));
                        break;

                    case WindowType.Nuttall:
                        Window[n] = 0.355768 - 0.487396 * Cos(2.0 * PI * n / ((double)this.Length)) + 0.144232 * Cos(4.0 * PI * n / ((double)this.Length)) - 0.012604 * Cos(6.0 * PI * n / ((double)this.Length));
                        break;

                    case WindowType.BlackmanNuttall:
                        Window[n] = 0.3635819 - 0.4891775 * Cos(2.0 * PI * n / ((double)this.Length)) + 0.1365995 * Cos(4.0 * PI * n / ((double)this.Length)) - 0.0106411 * Cos(6.0 * PI * n / ((double)this.Length));
                        break;

                    case WindowType.BlackmanHarris:
                        Window[n] = 0.35875 - 0.48829 * Cos(2.0 * PI * n / ((double)this.Length)) + 0.14128 * Cos(4.0 * PI * n / ((double)this.Length)) - 0.01168 * Cos(6.0 * PI * n / ((double)this.Length));
                        break;

                    case WindowType.FlatTop:
                        Window[n] = 0.21557895 - 0.41663158 * Cos(2.0 * PI * n / ((double)this.Length)) + 0.277263158 * Cos(4.0 * PI * n / ((double)this.Length)) - 0.083578947 * Cos(6.0 * PI * n / ((double)this.Length)) + 0.006947368 * Cos(8.0 * PI * n / ((double)this.Length));
                        break;

                    default:
                        Window[n] = 1.0;
                        break;
                }
            }

        }

        private void ComputeWindowedResponses()
        {
            this.WindowedImpulseResponse = new double[this.Length].ToList();
            this.WindowedStepResponse = new double[this.Length].ToList();


            for (int n = 0; n < this.Length; n++)
            {
                this.WindowedImpulseResponse[n] = this.ImpulseResponse[n] * this.Window[n];


                if (n == 0)
                {
                    this.WindowedStepResponse[n] = 0.5 * this.WindowedStepResponse[n];
                }
                else
                {
                    this.WindowedStepResponse[n] = this.WindowedStepResponse[n - 1] + 0.5 * (this.WindowedImpulseResponse[n] + this.WindowedImpulseResponse[n - 1]);
                }
            }

        }

        #endregion

        #region frequency domain functions

        private void ComputeFrequencyVector()
        {
            this.FrequencyVectorHz = new double[this.FreqSamples].ToList();

            double df = (0.5 / this.Ts) / ((double) this.FreqSamples - 1.0);

            for (int n = 0; n < this.FreqSamples; n++)
            {
                this.FrequencyVectorHz[n] = n * df;
            }
        }

        private void ComputeRespBode()
        {
            this.ImpRespMag = new double[this.FreqSamples].ToList();
            this.WinRespMag = new double[this.FreqSamples].ToList();

            for (int fIndex = 0; fIndex < this.FreqSamples; fIndex++)
            {
                double re = 0.0;
                double im = 0.0;
                double reWin = 0.0;
                double imWin = 0.0;

                for (int n = 0; n < this.Length; n++)
                {
                    re = re + this.ImpulseResponse[n] * Cos(2.0 * PI * this.FrequencyVectorHz[fIndex] * n * this.Ts);
                    im = im - this.ImpulseResponse[n] * Sin(2.0 * PI * this.FrequencyVectorHz[fIndex] * n * this.Ts);
                    reWin = reWin + this.WindowedImpulseResponse[n] * Cos(2.0 * PI * this.FrequencyVectorHz[fIndex] * n * this.Ts);
                    imWin = imWin - this.WindowedImpulseResponse[n] * Sin(2.0 * PI * this.FrequencyVectorHz[fIndex] * n * this.Ts);
                }

                this.ImpRespMag[fIndex] = 10.0 * Log10(re * re + im * im);
                this.WinRespMag[fIndex] = 10.0 * Log10(reWin * reWin + imWin * imWin);
            }
        }

        private double GetGainAtCutOff()
        {
            double re = 0.0;
            double im = 0.0;

            for (int n = 0; n < this.Length; n++)
            {
                re = re + this.ImpulseResponse[n] * Cos(2.0 * PI * this.LowFreq * n * this.Ts);
                im = im - this.ImpulseResponse[n] * Sin(2.0 * PI * this.LowFreq * n * this.Ts);
            }

            return (10.0 * Log10(re * re + im * im));
        }

        private void ComputeWindowDFT()
        {
            this.WinMag = new double[this.FreqSamples].ToList();

            for (int fIndex = 0; fIndex < this.FreqSamples; fIndex++)
            {
                double re = 0.0;
                double im = 0.0;

                for (int n = 0; n < this.Length; n++)
                {
                    re = re + this.Window[n] * Cos(2.0 * PI * this.FrequencyVectorHz[fIndex] * n * this.Ts);
                    im = im - this.Window[n] * Sin(2.0 * PI * this.FrequencyVectorHz[fIndex] * n * this.Ts);
                }

                this.WinMag[fIndex] = 10.0 * Log10(re * re + im * im);
            }
        }

        #endregion
        public FIR()
        {
            this.TimeVector = new List<double>();
            this.ImpulseResponse = new List<double>();
            this.StepResponse = new List<double>();
            this.Window = new List<double>();
            this.WindowedImpulseResponse = new List<double>();
            this.WindowedStepResponse = new List<double>();
            this.FrequencyVectorHz = new List<double>();
            this.ImpRespMag = new List<double>();
            this.WinRespMag = new List<double>();
            this.WinMag = new List<double>();
            this.FreqSamples = 256;
        }

        public void Update()
        {
            this.ComputeTimeVector();
            this.ComputeResponses();
            this.ComputeWindow();
            this.ComputeWindowedResponses();

            this.ComputeFrequencyVector();
            this.ComputeRespBode();
            this.ComputeWindowDFT();
        }
    }
}
