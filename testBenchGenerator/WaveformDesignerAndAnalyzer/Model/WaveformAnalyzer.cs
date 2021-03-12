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
        private Signal errorSignal;

        private int linesIgnore;
        private double rmsEfs;
        private double rmsElsbs;
        private double pRmsE;
        private double gainE;
        private double dcReal;
        private double dcImag;

        public Signal RefSignal
        {
            get { return this.refSignal; }
            set { this.refSignal = value; }
        }

        public Signal ErrorSignal
        {
            get { return this.errorSignal; }
            set { this.errorSignal = value; }
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

        private double MeasurePhaseOffset()
        {
            double[] dataAngle = new double[this.FFT.Length];
            double threshold = this.Freqs.Max() / 1000;

            for (int m = 0; m < this.Freqs.Length; m++)
                if (this.Freqs[m] < threshold)
                    this.FFT[m] = new Complex(0, 0);

            for (int m = 0; m < dataAngle.Length; m++)
                dataAngle[m] = Math.Atan2(this.FFT[m].Imaginary, this.FFT[m].Real);

            return dataAngle.Max() * 180 / Math.PI;
        }

        private void Unwrap(double[] ph)
        {
            double[] diffs = new double[ph.Length - 1];
            double[] diffsPi = new double[ph.Length - 1];
            double[] phCorr = new double[ph.Length - 1];
            double[] acc = new double[ph.Length - 1];
            for (int m = 0; m < diffs.Length; m++)
            {
                diffs[m] = ph[m + 1] - ph[m];
                diffsPi[m] = (diffs[m] + Math.PI) - Math.Floor((diffs[m] + Math.PI) / (2 * Math.PI)) * (2 * Math.PI) - Math.PI;
                if (diffsPi[m] == ((-1) * Math.PI) && diffs[m] > 0)
                    diffsPi[m] = Math.PI;
                phCorr[m] = diffsPi[m] - diffs[m];
                if (Math.Abs(diffs[m]) < Math.PI)
                    phCorr[m] = 0;
            }
            acc[0] = phCorr[0];
            for (int m = 1; m < acc.Length; m++)
                acc[m] = acc[m - 1] + phCorr[m];
            for (int m = 1; m < ph.Length; m++)
                ph[m] += acc[m - 1];
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

        private void CreateErrorSignal()
        {
            this.ErrorSignal = new Signal(this.Fs, this.Length, this.LengthTime, this.OS, this.FFTLength, this.Bitwidth);
            this.ErrorSignal.I = new double[this.I.Length];
            this.ErrorSignal.Q = new double[this.Q.Length];
            this.ErrorSignal.X = new Complex[this.X.Length];
            for(int m = 0; m < this.I.Length; m++)
            {
                this.ErrorSignal.I[m] = this.I[m] - (this.RefSignal.I[m] / (Math.Pow(2, this.Bitwidth - 1)));
                this.ErrorSignal.Q[m] = this.Q[m] - (this.RefSignal.Q[m] / (Math.Pow(2, this.Bitwidth - 1)));
                this.ErrorSignal.X[m] = new Complex(this.ErrorSignal.I[m], this.ErrorSignal.Q[m]);
            }
        }

        private void ComputeErrors()
        {
            double[] errorMag = new double[this.ErrorSignal.X.Length];

            for (int m = 0; m < errorMag.Length; m++)
                errorMag[m] = Math.Pow(this.ErrorSignal.X[m].Magnitude, 2);

            this.RMSElsbs = Math.Sqrt(errorMag.ToList().Average());
            this.RMSEFs = 20 * Math.Log10(this.RMSElsbs / (Math.Pow(2, this.Bitwidth)));

            double[] phaseAngsSig = new double[this.I.Length];
            double[] phaseAngsRef = new double[this.RefSignal.I.Length];

            for (int m = 0; m < phaseAngsSig.Length; m++)
                phaseAngsSig[m] = Math.Atan2(this.Q[m], this.I[m]);

            for (int m = 0; m < phaseAngsRef.Length; m++)
                phaseAngsRef[m] = Math.Atan2(this.RefSignal.Q[m], this.RefSignal.I[m]);

            this.Unwrap(phaseAngsSig);
            this.Unwrap(phaseAngsRef);

            double[] phaseError = new double[this.I.Length];

            for (int m = 0; m < phaseError.Length; m++)
                phaseError[m] = phaseAngsSig[m] - phaseAngsRef[m];

            this.PRMSE = Math.Sqrt(phaseError.Select(pe => Math.Pow((Math.Abs(pe * 180 / Math.PI)), 2)).Average());

            double[] mags = new double[this.I.Length];
            for (int m = 0; m < mags.Length; m++)
                mags[m] = (this.I[m] * this.I[m]) + (this.Q[m] * this.Q[m]);

            double actual = mags.Select(x => Math.Sqrt(x)).Average();

            this.GainE = 20 * Math.Log10(actual / this.InputMag);

            this.DCReal = 20 * Math.Log10(Math.Abs(this.ErrorSignal.I.Average()) / Math.Pow(2, (this.Bitwidth - 1)));
            this.DCImag = 20 * Math.Log10(Math.Abs(this.ErrorSignal.Q.Average()) / Math.Pow(2, (this.Bitwidth - 1)));
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
        }

        public void AnalyzeSignal()
        {
            this.RMS = this.Signal.ComputeSignalRMS();

            double fsSig = this.Fs * this.OS;

            if (this.Type.Contains("Sine"))
            {
                this.Freq = this.EstimateSineFrequency(fsSig);
                (this.RefSignal as SineSignal).Freq = this.Freq;
                this.Phoff = this.MeasurePhaseOffset();
                (this.RefSignal as SineSignal).Phoff = this.Phoff;
                this.CreateRefSignal();
                this.CreateErrorSignal();
                this.ComputeErrors();
            }
        }

        public WaveformAnalyzer()
        {
            this.Signal = new Signal();
            this.InputMag = 1;
        }
    }
}
