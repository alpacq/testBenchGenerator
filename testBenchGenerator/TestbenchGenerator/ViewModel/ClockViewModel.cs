using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.TestbenchGenerator.Model;

namespace testBenchGenerator.TestbenchGenerator.ViewModel
{
    public class ClockViewModel : PortViewModel
    {
        private Clock clock;

        public Clock Clock
        {
            get { return this.clock; }
        }

        public double Frequency
        {
            get { return this.Clock.Frequency; }
            set { this.Clock.Frequency = value; OnPropertyChanged("Frequency"); }
        }

        public ClockViewModel(Clock clock) : base((Port)clock)
        {
            this.clock = clock;
        }

        public override string ToString()
        {
            return this.Name + " (" + this.Frequency + " MHz)";
        }
    }
}
