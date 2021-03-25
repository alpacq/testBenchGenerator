namespace FPGADeveloperTools.Common.Model.Signals
{
    public class FreqSignal : PhoffSignal
    {
        private double freq;

        public double Freq
        {
            get { return this.freq; }
            set { this.freq = value; }
        }

        public FreqSignal(double freq = 0.0) : base()
        {
            this.Freq = freq;
        }

        public FreqSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double rms, double phoff = 0.0, double freq = 0.0) : base(fs, length, lengthTime, os, fftLength, bitwidth, rms, phoff)
        {
            this.Freq = freq;
        }

        public FreqSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double phoff = 0.0, double freq = 0.0) : base(fs, length, lengthTime, os, fftLength, bitwidth, phoff)
        {
            this.Freq = freq;
        }
    }
}
