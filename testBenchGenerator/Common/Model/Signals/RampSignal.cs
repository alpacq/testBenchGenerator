namespace FPGADeveloperTools.Common.Model.Signals
{
    public class RampSignal : Signal
    {
        public override void Create()
        {
            this.CreateTimeVector();
            this.I = new double[this.TimeVector.Length];
            this.Q = new double[this.TimeVector.Length];
            for (int k = 0; k < this.TimeVector.Length; k++)
            {
                I[k] = ((double)(k) / (this.TimeVector.Length - 1));
                Q[k] = ((double)(k) / (this.TimeVector.Length - 1));
            }
        }

        public RampSignal() : base()
        {

        }

        public RampSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double rms) : base(fs, length, lengthTime, os, fftLength, bitwidth, rms)
        {

        }

        public RampSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth) : base(fs, length, lengthTime, os, fftLength, bitwidth)
        {

        }
    }
}
