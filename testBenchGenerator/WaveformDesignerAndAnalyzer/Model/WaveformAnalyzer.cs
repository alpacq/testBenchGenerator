using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Common;

namespace testBenchGenerator.WaveformDesignerAndAnalyzer.Model
{
    public class WaveformAnalyzer : WaveformProcessor
    {
        #region variables and fields
        private string[] dutLines;
        private string path;
        private Signal refSignal;

        private double inputMag;
        private int linesIgnore;
        private double rmsEfs;
        private double rmsElsbs;
        private double rmsEc;
        private double pRmsE;
        private double gainE;
        private double dcReal;
        private double dcImag;

        public Signal RefSignal
        {
            get { return this.refSignal; }
            set { this.refSignal = value; }
        }

        public double InputMag
        {
            get { return this.inputMag; }
            set { this.inputMag = value; }
        }

        public int LinesIgnore
        {
            get { return this.linesIgnore; }
            set { this.linesIgnore = value; }
        }

        public double RMSEFs
        {
            get { return this.rmsEfs; }
            set { this.rmsEfs = value; }
        }

        public double RMSElsbs
        {
            get { return this.rmsElsbs; }
            set { this.rmsElsbs = value; }
        }

        public double RMSEc
        {
            get { return this.rmsEc; }
            set { this.rmsEc = value; }
        }

        public double PRMSE
        {
            get { return this.pRmsE; }
            set { this.pRmsE = value; }
        }

        public double GainE
        {
            get { return this.gainE; }
            set { this.gainE = value; }
        }

        public double DCReal
        {
            get { return this.dcReal; }
            set { this.dcReal = value; }
        }

        public double DCImag
        {
            get { return this.dcImag; }
            set { this.dcImag = value; }
        }

        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }
        #endregion  

        private double EstimateSineFrequency(double fsSig)
        {
            double freq = this.Freqs.ToList().IndexOf(this.Freqs.Max());

            freq *= (fsSig / this.Freqs.Length);

            if (freq > fsSig / 2)
                freq -= (fsSig / 2);

            return freq;
        }

        private Complex[] CrossCorrelation(Complex[] ffta, Complex[] fftb)
        {
            var conj = ffta.Select(i => new Complex(i.Real, (-1 * i.Imaginary))).ToArray();
            conj = conj.Zip(fftb, (v1, v2) => Complex.Multiply(v1, v2)).ToArray();
            MathNet.Numerics.IntegralTransforms.Fourier.Inverse(conj);
            return conj;
        }

        private Tuple<double, int> CorrelationCoefficient(Complex[] ffta, Complex[] fftb)
        {
            var correlation = CrossCorrelation(ffta, fftb);
            var seq = correlation.Select(i => i.Magnitude);
            var maxCoeff = seq.Max();
            int maxIndex = seq.ToList().IndexOf(maxCoeff);
            return new Tuple<double, int>(maxCoeff, maxIndex);
        }

        private double MeasurePhaseOffset()
        {
            //Complex[] a = this.Signal.X;
            //Complex[] b = this.RefSignal.X;

            ////for(int m = 0; m < this.Signal.X.Length; m++)
            ////{
            ////    a[m] = this.Signal.X[m];
            ////    a[m + this.Signal.X.Length] = new Complex(0, 0);
            ////}
            ////for (int m = 0; m < this.RefSignal.X.Length; m++)
            ////{
            ////    b[m] = this.RefSignal.X[m];
            ////    b[m + this.RefSignal.X.Length] = new Complex(0, 0);
            ////}

            //MathNet.Numerics.IntegralTransforms.Fourier.Forward(b);
            //MathNet.Numerics.IntegralTransforms.Fourier.Forward(a);
            //var cc = this.CorrelationCoefficient(a, b);
            //double ind = cc.Item2;
            //double time = ind / this.Fs;
            //double off = (time * this.Freq) % (2 * Math.PI);
            ////return Math.Acos(ind) * 180 / Math.PI;
            //return off * 180 / Math.PI;
            Complex[] a = this.Signal.X;
            Complex[] b = this.RefSignal.X;
            MathNet.Numerics.IntegralTransforms.Fourier.Forward(b);
            MathNet.Numerics.IntegralTransforms.Fourier.Forward(a);

            int ind = this.Freqs.ToList().IndexOf(this.Freqs.Max());

            Complex max = a[ind] / b[ind];

            return Math.Atan2(max.Imaginary, max.Real) * 180 / Math.PI;
        }

        private void UpdateRef()
        {
            this.RefSignal.Fs = this.Fs;
            this.RefSignal.FFTLength = this.FFTLength;
            this.RefSignal.Length = this.Length;
            this.RefSignal.Bitwidth = this.Bitwidth;
            this.RefSignal.RMS = this.Signal.RMS;
        }

        private void CreateRefSignal()
        {
            this.UpdateRef();
            this.RefSignal.Create();
            this.RefSignal.ApplyGain();
        }

        public void ReadFile()
        {
            if (this.Type.Contains("Sine"))
            {
                this.Signal = new SineSignal(this.Fs, this.Length, this.LengthTime, this.OS, this.FFTLength, this.Bitwidth);
                this.RefSignal = new SineSignal(this.Fs, this.Length, this.LengthTime, this.OS, this.FFTLength, this.Bitwidth);
            }
            else if (this.Type.Contains("OFDM"))
            {
                this.RefSignal = new OFDMSignal();
            }
            this.dutLines = System.IO.File.ReadAllLines(this.Path);

            this.Length = this.dutLines.Length - this.LinesIgnore;

            this.I = new double[this.Length];
            this.Q = new double[this.Length];
            this.X = new Complex[this.Length];

            for(int k = 0; k < this.Length; k++)
            {
                if(this.Radix == Radix.Decimal)
                {
                    this.I[k] = Convert.ToInt32(this.dutLines[k + this.LinesIgnore].Split(this.Delimiter == Delimiter.Comma ? ',' : ' ').FirstOrDefault());
                    this.Q[k] = Convert.ToInt32(this.dutLines[k + this.LinesIgnore].Split(this.Delimiter == Delimiter.Comma ? ',' : ' ').LastOrDefault());
                    this.I[k] /= Math.Pow(2, (this.Bitwidth - 1));
                    this.Q[k] /= Math.Pow(2, (this.Bitwidth - 1));
                }
                else if(this.Radix == Radix.Hexadecimal)
                {
                    this.I[k] = int.Parse((this.dutLines[k + this.LinesIgnore].Split(this.Delimiter == Delimiter.Comma ? ',' : ' ').FirstOrDefault()), System.Globalization.NumberStyles.HexNumber);
                    this.Q[k] = int.Parse((this.dutLines[k + this.LinesIgnore].Split(this.Delimiter == Delimiter.Comma ? ',' : ' ').LastOrDefault()), System.Globalization.NumberStyles.HexNumber);
                    this.I[k] /= Math.Pow(2, (this.Bitwidth - 1));
                    this.Q[k] /= Math.Pow(2, (this.Bitwidth - 1));
                }
                else
                {
                    this.I[k] = Convert.ToDouble(this.dutLines[k + this.LinesIgnore].Split(this.Delimiter == Delimiter.Comma ? ',' : ' ').FirstOrDefault());
                    this.Q[k] = Convert.ToDouble(this.dutLines[k + this.LinesIgnore].Split(this.Delimiter == Delimiter.Comma ? ',' : ' ').LastOrDefault());
                }
                
                this.X[k] = new Complex(this.I[k], this.Q[k]);
            }

            this.Signal.ComputeFFT();

            this.RMS = this.Signal.ComputeSignalRMS();

            double fsSig = this.Fs * this.OS;            

            if (this.Type.Contains("Sine"))
            {               
                this.Freq = this.EstimateSineFrequency(fsSig);
                (this.RefSignal as SineSignal).Freq = this.Freq;
                this.CreateRefSignal();
                this.Phoff = this.MeasurePhaseOffset();
                (this.RefSignal as SineSignal).Phoff = this.Phoff;                
            }

            this.CreateRefSignal();
        }

        public WaveformAnalyzer()
        {
            this.Signal = new Signal();
        }
    }
}
