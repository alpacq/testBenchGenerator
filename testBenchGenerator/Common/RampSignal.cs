using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
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
    }
}
