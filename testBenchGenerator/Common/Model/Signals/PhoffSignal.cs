namespace FPGADeveloperTools.Common.Model.Signals
{
    public class PhoffSignal : Signal
    {
        private double phoff;

        public double Phoff
        {
            get { return this.phoff; }
            set { this.phoff = value; }
        }

        public PhoffSignal(double phoff = 0.0) : base()
        {
            this.Phoff = phoff;
        }

        public PhoffSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double rms, double phoff = 0.0) : base(fs, length, lengthTime, os, fftLength, bitwidth, rms)
        {
            this.Phoff = phoff;
        }

        public PhoffSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double phoff = 0.0) : base(fs, length, lengthTime, os, fftLength, bitwidth)
        {
            this.Phoff = phoff;
        }
    }
}
