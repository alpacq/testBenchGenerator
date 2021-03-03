using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
{
    public class OFDMSignal : Signal
    {
        private double phoff;
        private string modulation;
        private double fsoff;
        private double ofdmn;
        private double distance;
        private int cpLength;
        private int nSymbols;

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

        public double OFDMN
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
                            var theta = 2 * Math.PI * c * fSpac * this.TimeVector[m] + this.Phoff;
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
                    cp[m] = fftShift[this.FFTLength - (m + 1)];
                fft = new Complex[this.FFTLength + this.CPLength];
                for (int m = 0; m < this.CPLength; m++)
                    fft[m] = cp[m];
                for (int m = 0; m < this.FFTLength; m++)
                    fft[m + this.CPLength] = fftShift[m];

                double[] ofdmI = new double[fft.Length];
                double[] ofdmQ = new double[fft.Length];
                for (int m = 0; m < fft.Length; m++)
                {
                    ofdmI[m] = fft[m].Real;
                    ofdmQ[m] = fft[m].Imaginary;
                }
                return new Tuple<double[], double[]>(ofdmI, ofdmQ);
            }
            else
            {
                double[] ofdmI = new double[fftShift.Length];
                double[] ofdmQ = new double[fftShift.Length];
                for (int m = 0; m < fftShift.Length; m++)
                {
                    ofdmI[m] = fftShift[m].Real;
                    ofdmQ[m] = fftShift[m].Imaginary;
                }
                return new Tuple<double[], double[]>(ofdmI, ofdmQ);
            }
        }

        public override void Create()
        {
            this.I = new double[this.NSymbols * this.FFTLength * 2];
            this.Q = new double[this.NSymbols * this.FFTLength * 2];
            for (int k = 0; k < this.NSymbols; k++)
            {
                Tuple<double[], double[]> tp = this.CreateOFDMSymbol(k);
                for (int m = 0; m < tp.Item1.Length; m++)
                {
                    this.I[(k * 2 * this.FFTLength) + m] = tp.Item1[m];
                    this.I[(k * 2 * this.FFTLength) + m + tp.Item1.Length] = tp.Item1[m];
                    this.Q[(k * 2 * this.FFTLength) + m] = tp.Item2[m];
                    this.Q[(k * 2 * this.FFTLength) + m + tp.Item2.Length] = tp.Item2[m];
                }
            }
        }

        public OFDMSignal() : base()
        {            
            this.NSymbols = 1;
            this.Distance = 1;
        }
    }
}
