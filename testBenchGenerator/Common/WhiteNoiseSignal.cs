using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
{
    public class WhiteNoiseSignal : Signal
    {
        public override void Create()
        {
            Random rand = new Random();
            this.CreateTimeVector();
            this.I = new double[this.TimeVector.Length];
            this.Q = new double[this.TimeVector.Length];
            for (int k = 0; k < this.TimeVector.Length; k++)
            {
                this.I[k] = rand.NextDouble();
                this.Q[k] = rand.NextDouble();
            }
        }

        public WhiteNoiseSignal() : base()
        {

        }

        public WhiteNoiseSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double rms) : base(fs, length, lengthTime, os, fftLength, bitwidth, rms)
        {

        }

        public WhiteNoiseSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth) : base(fs, length, lengthTime, os, fftLength, bitwidth)
        {

        }
    }
}
