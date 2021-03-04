using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
{
    public class Signal
    {
        #region variables and fields
        private double[] i;
        private double[] q;
        private Complex[] x;
        private double[] timeVector;
        private int length;
        private double gain;
        private double lengthTime;
        private double[] freqs;
        private double fs;        
        private int os;
        private int fftLength;
        private int bitwidth;
        private double rms;

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

        public double[] TimeVector
        {
            get { return this.timeVector; }
            set { this.timeVector = value; }
        }

        public double[] Freqs
        {
            get { return this.freqs; }
            set { this.freqs = value; }
        }

        public double Fs
        {
            get { return this.fs; }
            set { this.fs = value; }
        }        

        public int OS
        {
            get { return this.os; }
            set { this.os = value; }
        }

        public int FFTLength
        {
            get { return this.fftLength; }
            set { this.fftLength = value; }
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

        public double Gain
        {
            get { return this.gain; }
            set { this.gain = value; }
        }

        public int Length
        {
            get { return this.length; }
            set { this.length = value; }
        }

        public double LengthTime
        {
            get { return this.lengthTime; }
            set { this.lengthTime = value; }
        }
        #endregion
        public void ApplyGain()
        {
            this.X = new Complex[this.I.Length];
            this.Freqs = new double[this.I.Length];
            for (int k = 0; k < this.I.Length; k++)
                this.Freqs[k] = Math.Sqrt(this.I[k] * this.I[k] + this.Q[k] * this.Q[k]);
            this.Gain = 20 * Math.Log10(MathNet.Numerics.Statistics.Statistics.RootMeanSquare(this.Freqs)) - this.RMS;
            for (int k = 0; k < this.I.Length; k++)
            {
                this.I[k] *= Math.Pow(10, ((this.Gain * (-1)) / 20));
                this.I[k] *= Math.Pow(2, (this.Bitwidth - 1));
                this.Q[k] *= Math.Pow(10, ((this.Gain * (-1)) / 20));
                this.Q[k] *= Math.Pow(2, (this.Bitwidth - 1));
                this.X[k] = new Complex(this.I[k], this.Q[k]);
            }
        }

        public void ComputeFFT()
        {
            Complex[] data = this.X;
            MathNet.Numerics.IntegralTransforms.Fourier.Forward(data);
            this.Freqs = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
                this.Freqs[i] = data[i].Magnitude;
        }

        public double ComputeSignalRMS()
        {
            return 20 * Math.Log10(Math.Sqrt(this.X.Average(c => (c.Magnitude * c.Magnitude))));
        }

        protected void CreateTimeVector()
        {
            int len = Convert.ToInt32(this.LengthTime * this.Fs);
            this.TimeVector = new double[len];
            for (int k = 0; k < len; k++)
                this.TimeVector[k] = k * (1 / this.Fs);
        }

        public virtual void Create()
        {
        }

        public Signal()
        {
            this.LengthTime = 0.001;
            this.OS = 1;
            this.Bitwidth = 16;
            this.fftLength = 512;
        }

        public Signal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double rms)
        {
            this.Fs = fs;
            this.Length = length;
            this.LengthTime = lengthTime;
            this.OS = os;
            this.FFTLength = fftLength;
            this.Bitwidth = bitwidth;
            this.RMS = rms;
        }

        public Signal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth)
        {
            this.Fs = fs;
            this.Length = length;
            this.LengthTime = lengthTime;
            this.OS = os;
            this.FFTLength = fftLength;
            this.Bitwidth = bitwidth;
        }
    }
}
