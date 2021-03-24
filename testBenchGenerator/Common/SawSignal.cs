using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
{
    public class SawSignal : FreqSignal
    {
        public override void Create()
        {
            this.CreateTimeVector();
            this.I = new double[this.TimeVector.Length];
            this.Q = new double[this.TimeVector.Length];
            for (int k = 0; k < this.TimeVector.Length; k++)
            {
                this.I[k] = (2 / Math.PI) * (this.Freq * Math.PI * (this.TimeVector[k] % (1.0 / this.Freq)) - (Math.PI / 2.0));
                this.Q[k] = 0.0;
            }
        }

        public SawSignal() : base()
        {

        }

        public SawSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double rms, double phoff = 0.0, double freq = 0.0) : base(fs, length, lengthTime, os, fftLength, bitwidth, rms, phoff, freq)
        {
            
        }

        public SawSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double phoff = 0.0, double freq = 0.0) : base(fs, length, lengthTime, os, fftLength, bitwidth, phoff, freq)
        {

        }
    }
}
