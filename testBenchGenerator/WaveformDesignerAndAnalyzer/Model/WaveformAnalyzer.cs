using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.WaveformDesignerAndAnalyzer.Model
{
    public class WaveformAnalyzer
    {
        #region variables and fields
        private string[] dutLines;
        private double[] i;
        private double[] q;
        private Complex[] x;
        private string path;
        private int length;
        private double[] freqs;
        private double[] ofdm_time_sym;

        private string type;
        private double fs;
        private double gain;
        private double freq;
        private double phoff;
        private string modulation;
        private double fsoff;
        private double os;
        private double ofdmn;
        private double fftLength;
        private double distance;
        private double seed;
        private double cpLength;
        private double nSymbols;
        private int bitwidth;
        private double rms;

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

        public double Gain
        {
            get { return this.gain; }
            set { this.gain = value; }
        }

        public double Freq
        {
            get { return this.freq; }
            set { this.freq = value; }
        }

        public double Phoff
        {
            get { return this.phoff; }
            set { this.phoff = value; }
        }

        public string Modulation
        {
            get { return this.modulation; }
            set { this.modulation = value; }
        }

        public double Fsoff
        {
            get { return this.fsoff; }
            set { this.fsoff = value; }
        }

        public double OS
        {
            get { return this.os; }
            set { this.os = value; }
        }

        public double OFDMN
        {
            get { return this.ofdmn; }
            set { this.ofdmn = value; }
        }

        public double FFTLength
        {
            get { return this.fftLength; }
            set { this.fftLength = value; }
        }

        public double Distance
        {
            get { return this.distance; }
            set { this.distance = value; }
        }

        public double Seed
        {
            get { return this.seed; }
            set { this.seed = value; }
        }

        public double CPLength
        {
            get { return this.cpLength; }
            set { this.cpLength = value; }
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
