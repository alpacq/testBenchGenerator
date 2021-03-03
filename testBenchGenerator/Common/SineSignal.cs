using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
{
    public class SineSignal : Signal
    {
        private double phoff;
        private double freq;

        public double Phoff
        {
            get { return this.phoff; }
            set { this.phoff = value; }
        }

        public double Freq
        {
            get { return this.freq; }
            set { this.freq = value; }
        }
        public override void Create()
        {
            this.CreateTimeVector();
            this.I = new double[this.TimeVector.Length];
            this.Q = new double[this.TimeVector.Length];
            for (int k = 0; k < this.TimeVector.Length; k++)
            {
                var theta = 2 * Math.PI * this.Freq * this.TimeVector[k] + this.Phoff;
                this.I[k] = Math.Cos(theta);
                this.Q[k] = Math.Sin(theta);
            }
        }

        public SineSignal() : base()
        {

        }
    }
}
