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
        private double linesIgnore;
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

        public double LinesIgnore
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
            Complex[] data = this.X;
            MathNet.Numerics.IntegralTransforms.Fourier.Forward(data);

            double freq = data.ToList().IndexOf(data.Max(c => c.Magnitude));

            freq /= fsSig;

            if (freq > fsSig / 2)
                freq -= (fsSig / 2);

            return freq;
        }

        private Complex[] ApplyGain(double[] iArray, double[] qArray, double rms)
        {
            Complex[] data = new Complex[iArray.Length];
            double[] frequencies = new double[iArray.Length];
            for (int k = 0; k < iArray.Length; k++)
                frequencies[k] = Math.Sqrt(iArray[k] * iArray[k] + qArray[k] * qArray[k]);
            double gain = 20 * Math.Log10(MathNet.Numerics.Statistics.Statistics.RootMeanSquare(frequencies)) - rms;
            for (int k = 0; k < this.I.Length; k++)
            {
                iArray[k] *= Math.Pow(10, ((gain * (-1)) / 20));
                iArray[k] *= Math.Pow(2, (this.Bitwidth - 1));
                qArray[k] *= Math.Pow(10, ((gain * (-1)) / 20));
                qArray[k] *= Math.Pow(2, (this.Bitwidth - 1));
                data[k] = new Complex(iArray[k], qArray[k]);
            }

            return data;
        }

        private void UpdateRef()
        {
            this.RefSignal.Fs = this.Fs;
            this.RefSignal.FFTLength = this.FFTLength;
            this.RefSignal.Length = this.Length;
            this.RefSignal.Bitwidth = this.Bitwidth;
            this.RefSignal.RMS = this.Signal.RMS;
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

            this.Length = this.dutLines.Length;

            this.I = new double[this.Length];
            this.Q = new double[this.Length];
            this.X = new Complex[this.Length];

            for(int k = 0; k < this.Length; k++)
            {
                if(this.Radix == Radix.Decimal)
                {
                    this.I[k] = Convert.ToInt32(this.dutLines[k].Split(this.Delimiter == Delimiter.Comma ? ',' : ' ').FirstOrDefault());
                    this.Q[k] = Convert.ToInt32(this.dutLines[k].Split(this.Delimiter == Delimiter.Comma ? ',' : ' ').LastOrDefault());
                    this.I[k] /= Math.Pow(2, (this.Bitwidth - 1));
                    this.Q[k] /= Math.Pow(2, (this.Bitwidth - 1));
                }
                else if(this.Radix == Radix.Hexadecimal)
                {
                    this.I[k] = int.Parse((this.dutLines[k].Split(this.Delimiter == Delimiter.Comma ? ',' : ' ').FirstOrDefault()), System.Globalization.NumberStyles.HexNumber);
                    this.Q[k] = int.Parse((this.dutLines[k].Split(this.Delimiter == Delimiter.Comma ? ',' : ' ').LastOrDefault()), System.Globalization.NumberStyles.HexNumber);
                    this.I[k] /= Math.Pow(2, (this.Bitwidth - 1));
                    this.Q[k] /= Math.Pow(2, (this.Bitwidth - 1));
                }
                else
                {
                    this.I[k] = Convert.ToDouble(this.dutLines[k].Split(this.Delimiter == Delimiter.Comma ? ',' : ' ').FirstOrDefault());
                    this.Q[k] = Convert.ToDouble(this.dutLines[k].Split(this.Delimiter == Delimiter.Comma ? ',' : ' ').LastOrDefault());
                }
                
                this.X[k] = new Complex(this.I[k], this.Q[k]);
            }

            this.Signal.ComputeFFT();

            this.RMS = this.Signal.ComputeSignalRMS();

            double fsSig = this.Fs * this.OS;

            this.UpdateRef();
            this.RefSignal.Create();

            if (this.Type.Contains("Sine"))
            {               
                (this.RefSignal as SineSignal).Freq = this.EstimateSineFrequency(fsSig);
                //todo estimate phoff;                
            }

            
        }

        public WaveformAnalyzer()
        {
            this.Signal = new Signal();
        }
    }
}
