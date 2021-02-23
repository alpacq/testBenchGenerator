using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace testBenchGenerator.WaveformDesignerAndAnalyzer.Model
{
    public class WaveformDesigner
    {
        #region variables and fields
        private double[] timeVector;
        private double[] i;
        private double[] q;
        private Complex[] x;
        private double fgain;
        private double[] freqs;
        private double[] ofdm_time_sym;

        private string type;
        private double fs;
        private double gain;
        private double length;
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

        public double[] TimeVector
        {
            get { return this.timeVector; }
            set { this.timeVector = value; }
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

        public double FGain
        {
            get { return this.fgain; }
            set { this.fgain = value; }
        }

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

        public double Length
        {
            get { return this.length; }
            set { this.length = value; }
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

        #endregion
        private void CreateSineTimeVector()
        {
            int len = Convert.ToInt32(this.Length * this.Fs);
            this.TimeVector = new double[len];
            this.TimeVector[0] = 0;
            for (int k = 1; k < len; k++)
                this.TimeVector[k] = this.TimeVector[k - 1] + (1 / this.Fs);
        }

        private void CreateOFDMTimeVector(double fsSig)
        {
            int len = Convert.ToInt32(fsSig * (this.FFTLength - 1));
            this.TimeVector = new double[len];
            this.TimeVector[0] = 0;
            for (int k = 0; k < len; k++)
                this.TimeVector[k] = this.TimeVector[k - 1] + (1 / fsSig);
        }

        private void CreateSine()
        {
            this.CreateSineTimeVector();
            this.I = new double[this.TimeVector.Length];
            this.Q = new double[this.TimeVector.Length];
            for (int k = 0; k < this.TimeVector.Length; k++)
            {
                var theta = 2 * Math.PI * this.Freq * this.TimeVector[k] + this.Phoff;
                this.I[k] = Math.Cos(theta);
                this.Q[k] = Math.Sin(theta);
            }
        }

        private void CreateOFDM()
        {

        }

        private void CreateOFDMSymbol()
        {

        }

        private void ApplyGain()
        {
            this.X = new Complex[this.TimeVector.Length];
            this.Freqs = new double[this.TimeVector.Length];
            for (int k = 0; k < this.TimeVector.Length; k++)
                this.Freqs[k] = Math.Sqrt(this.I[k] * this.I[k] + this.Q[k] * this.Q[k]);
            this.FGain = 20 * Math.Log10(MathNet.Numerics.Statistics.Statistics.RootMeanSquare(this.Freqs)) - this.RMS;
            for (int k = 0; k < this.TimeVector.Length; k++)
            {
                this.I[k] *= Math.Pow(10, ((this.FGain * (-1)) / 20));
                this.I[k] *= Math.Pow(2, (this.Bitwidth - 1));
                this.Q[k] *= Math.Pow(10, ((this.FGain * (-1)) / 20));
                this.Q[k] *= Math.Pow(2, (this.Bitwidth - 1));
                this.X[k] = new Complex(this.I[k], this.Q[k]);
            }
        }

        private void ComputeFFT()
        {
            Complex[] data = this.X;
            MathNet.Numerics.IntegralTransforms.Fourier.Forward(data);
            this.Freqs = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
                this.Freqs[i] = data[i].Magnitude;
        }

        public void GenerateWaveform()
        {
            if (this.Type.Contains("Sine"))
            {
                this.CreateSine();
            }

            this.ApplyGain();
            this.ComputeFFT();
        }

        public WaveformDesigner()
        {
            this.Gain = 1;
            this.Length = 0.001;
            this.OS = 1;
            this.Bitwidth = 16;
            this.fftLength = 512;
            this.NSymbols = 1;
        }
    }
}
