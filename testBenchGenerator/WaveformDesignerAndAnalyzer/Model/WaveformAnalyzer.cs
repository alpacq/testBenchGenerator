using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.TestbenchGenerator.Model;

namespace testBenchGenerator.WaveformDesignerAndAnalyzer.Model
{
    public class WaveformAnalyzer
    {
        #region variables and fields
        private string[] dutLines;
        private double[] i;
        private double[] q;
        private Complex[] x;
        private Radix radix;
        private Delimiter delimiter;
        private string path;
        private int length;
        private double[] freqs;
        private double[] ofdm_time_sym;

        private string type;
        private double fs;
        private double freq;
        private double os;
        private double fftLength;
        private double nSymbols;
        private int bitwidth;
        private double rms;
        private double inputMag;
        private double linesIgnore;
        private double rmsEfs;
        private double rmsElsbs;
        private double rmsEc;
        private double pRmsE;
        private double gainE;
        private double dcReal;
        private double dcImag;

        public double[] Freqs
        {
            get { return this.freqs; }
            set { this.freqs = value; }
        }

        public double[] OFDMTimeSym
        {
            get { return this.ofdm_time_sym; }
            set { this.ofdm_time_sym = value; }
        }

        public Radix Radix
        {
            get { return this.radix; }
            set { this.radix = value; }
        }

        public Delimiter Delimiter
        {
            get { return this.delimiter; }
            set { this.delimiter = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public double Fs
        {
            get { return this.fs; }
            set { this.fs = value; }
        }

        public double Freq
        {
            get { return this.freq; }
            set { this.freq = value; }
        }

        public double OS
        {
            get { return this.os; }
            set { this.os = value; }
        }

        public double FFTLength
        {
            get { return this.fftLength; }
            set { this.fftLength = value; }
        }

        public double NSymbols
        {
            get { return this.nSymbols; }
            set { this.nSymbols = value; }
        }

        public int Bitwidth
        {
            get { return this.bitwidth; }
            set { this.bitwidth = value; }
        }

        public double RMS
        {
            get { return this.rms; }
            set { this.rms = value; }
        }

        public int Length
        {
            get { return this.length; }
            set { this.length = value; }
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

        public double[] I
        {
            get { return this.i; }
            set { this.i = value; }
        }

        public double[] Q
        {
            get { return this.q; }
            set { this.q = value; }
        }

        public Complex[] X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }
        #endregion
        private void ComputeFFT()
        {
            Complex[] data = this.X;
            MathNet.Numerics.IntegralTransforms.Fourier.Forward(data);
            this.Freqs = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
                this.Freqs[i] = data[i].Magnitude;
        }

        public void ReadFile()
        {
            this.dutLines = System.IO.File.ReadAllLines(this.Path);

            this.Length = this.dutLines.Length;

            this.I = new double[this.Length];
            this.Q = new double[this.Length];
            this.X = new Complex[this.Length];

            for(int k = 0; k < this.Length; k++)
            {
                this.I[k] = Convert.ToInt32(this.dutLines[k].Split(' ').FirstOrDefault());
                this.I[k] /= Math.Pow(2, (this.Bitwidth - 1));
                this.Q[k] = Convert.ToInt32(this.dutLines[k].Split(' ').FirstOrDefault());
                this.Q[k] /= Math.Pow(2, (this.Bitwidth - 1));
                this.X[k] = new Complex(this.I[k], this.Q[k]);
            }

            this.ComputeFFT();
        }

        public WaveformAnalyzer()
        {
            this.Bitwidth = 16;
            this.FFTLength = 512;
            this.NSymbols = 1;
        }
    }
}
