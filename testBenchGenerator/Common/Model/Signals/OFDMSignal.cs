using System;
using System.Numerics;

namespace FPGADeveloperTools.Common.Model.Signals
{
    public class OFDMSignal : PhoffSignal
    {
        #region variables and fields
        private string modulation;
        private double fsoff;
        private int ofdmn;
        private double distance;
        private int cpLength;
        private int nSymbols;
        //private bool isRand;
        //private double seed;

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

        public int OFDMN
        {
            get { return this.ofdmn; }
            set { this.ofdmn = value; }
        }


        public double Distance
        {
            get { return this.distance; }
            set { this.distance = value; }
        }

        public int CPLength
        {
            get { return this.cpLength; }
            set { this.cpLength = value; }
        }

        public int NSymbols
        {
            get { return this.nSymbols; }
            set { this.nSymbols = value; }
        }

        //public bool IsRand
        //{
        //    get { return this.isRand; }
        //    set { this.isRand = value; }
        //}

        //public double Seed
        //{
        //    get { return this.seed; }
        //    set { this.seed = value; }
        //}
        #endregion

        private void CreateOFDMTimeVector(double fsSig)
        {
            this.TimeVector = new double[this.FFTLength];
            for (int k = 0; k < this.FFTLength; k++)
                this.TimeVector[k] = k * (1 / fsSig);
        }

        private Tuple<double[], double[]> CreateOFDMSymbol(int k)
        {
            double fsSig = this.Fs / this.OS;
            double fSpac = fsSig / this.FFTLength;

            this.CreateOFDMTimeVector(fsSig);

            double[] sigI = new double[this.FFTLength];
            double[] sigQ = new double[this.FFTLength];

            for (int c = (int)(this.OFDMN / (-2)); c < (int)((this.OFDMN / 2) + 1); c++)
            {
                if (c != 0)
                {
                    if (this.Distance != 0 && (c % (int)(this.Distance) == 0))
                    {
                        double[] cI = new double[this.TimeVector.Length];
                        double[] cQ = new double[this.TimeVector.Length];
                        for (int m = 0; m < this.TimeVector.Length; m++)
                        {
                            var theta = 2 * Math.PI * c * fSpac * this.TimeVector[m] + (this.Phoff * Math.PI / 180.0);
                            cI[m] = Math.Cos(theta);
                            cQ[m] = Math.Sin(theta);
                        }
                        if (c < -300 || c > 300)
                        {
                            for (int m = 0; m < this.TimeVector.Length; m++)
                            {
                                cI[m] *= 2;
                                cQ[m] *= 2;
                            }
                        }
                        for (int m = 0; m < this.FFTLength; m++)
                        {
                            sigI[m] += cI[m];
                            sigQ[m] += cQ[m];
                        }
                    }
                }
            }

            Complex[] fft = new Complex[this.FFTLength];
            for (int m = 0; m < this.FFTLength; m++)
                fft[m] = new Complex(sigI[m], sigQ[m]);
            MathNet.Numerics.IntegralTransforms.Fourier.Forward(fft);
            for (int m = 0; m < this.FFTLength; m++)
                fft[m] /= this.FFTLength;
            Complex[] fftShift = new Complex[this.FFTLength];
            for (int m = 0; m < this.FFTLength / 2; m++)
            {
                fftShift[m] = fft[m + (this.FFTLength / 2)];
                fftShift[m + (this.FFTLength / 2)] = fft[m];
            }
            MathNet.Numerics.IntegralTransforms.Fourier.Inverse(fftShift);

            if (this.CPLength > 0)
            {
                Complex[] cp = new Complex[this.CPLength];
                for (int m = 0; m < this.CPLength; m++)
                    cp[m] = fftShift[this.FFTLength - (this.CPLength + m)];
                for (int m = 0; m < this.CPLength; m++)
                    fftShift[m] = cp[m];
                for (int m = 0; m < (this.FFTLength - this.CPLength); m++)
                    fftShift[m + this.CPLength] = fftShift[m];
            }
            double[] ofdmI = new double[fftShift.Length];
            double[] ofdmQ = new double[fftShift.Length];
            for (int m = 0; m < fftShift.Length; m++)
            {
                ofdmI[m] = fftShift[m].Real;
                ofdmQ[m] = fftShift[m].Imaginary;
            }
            return new Tuple<double[], double[]>(ofdmI, ofdmQ);
        }

        public override void Create()
        {
            //this.I = new double[this.NSymbols * this.FFTLength * 2];
            //this.Q = new double[this.NSymbols * this.FFTLength * 2];
            //for (int k = 0; k < this.NSymbols; k++)
            //{
            //    Tuple<double[], double[]> tp = this.CreateOFDMSymbol(k);
            //    for (int m = 0; m < tp.Item1.Length; m++)
            //    {
            //        this.I[(k * 2 * this.FFTLength) + m] = tp.Item1[m];
            //        this.I[(k * 2 * this.FFTLength) + m + tp.Item1.Length] = tp.Item1[m];
            //        this.Q[(k * 2 * this.FFTLength) + m] = tp.Item2[m];
            //        this.Q[(k * 2 * this.FFTLength) + m + tp.Item2.Length] = tp.Item2[m];
            //    }
            //}

            this.I = new double[this.FFTLength * 2];
            this.Q = new double[this.FFTLength * 2];
            for(int k = 0; k < this.NSymbols; k++)
            {
                Tuple<double[], double[]> tp = this.CreateOFDMSymbol(k);
                for(int m = 0; m < this.FFTLength; m++)
                {
                    this.I[m] += tp.Item1[m];
                    this.I[m + this.FFTLength] += tp.Item1[m];
                    this.Q[m] += tp.Item2[m];
                    this.Q[m + this.FFTLength] += tp.Item2[m];
                }
            }
        }

        public OFDMSignal() : base()
        {            
            this.NSymbols = 1;
            this.Distance = 1;
        }

        public OFDMSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double rms, double phoff = 0.0) : base(fs, length, lengthTime, os, fftLength, bitwidth, rms, phoff)
        {

        }

        public OFDMSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double phoff = 0.0) : base(fs, length, lengthTime, os, fftLength, bitwidth, phoff)
        {

        }
    }
}
