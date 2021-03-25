using System;

namespace FPGADeveloperTools.Common.Model.Signals
{
    public class SineSignal : FreqSignal
    {
        public override void Create()
        {
            this.CreateTimeVector();
            this.I = new double[this.TimeVector.Length];
            this.Q = new double[this.TimeVector.Length];
            for (int k = 0; k < this.TimeVector.Length; k++)
            {
                var theta = 2 * Math.PI * this.Freq * this.TimeVector[k] + (this.Phoff * Math.PI / 180.0);
                this.I[k] = Math.Cos(theta);
                this.Q[k] = Math.Sin(theta);
            }
        }

        public SineSignal() : base()
        {

        }

        public SineSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double rms, double phoff = 0.0, double freq = 0.0) : base(fs, length, lengthTime, os, fftLength, bitwidth, rms, phoff, freq)
        {
        }

        public SineSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double phoff = 0.0, double freq = 0.0) : base(fs, length, lengthTime, os, fftLength, bitwidth, phoff, freq)
        {
        }
    }
}
