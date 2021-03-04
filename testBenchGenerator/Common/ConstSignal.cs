using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
{
    public class ConstSignal : Signal
    {
        public override void Create()
        {
            this.CreateTimeVector();
            this.I = new double[this.TimeVector.Length];
            this.Q = new double[this.TimeVector.Length];
            for (int k = 0; k < this.TimeVector.Length; k++)
            {
                I[k] = 1;
                Q[k] = 1;
            }
        }

        public ConstSignal() : base()
        {

        }

        public ConstSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth, double rms) : base(fs, length, lengthTime, os, fftLength, bitwidth, rms)
        {

        }

        public ConstSignal(double fs, int length, double lengthTime, int os, int fftLength, int bitwidth) : base(fs, length, lengthTime, os, fftLength, bitwidth)
        {

        }
    }
}
