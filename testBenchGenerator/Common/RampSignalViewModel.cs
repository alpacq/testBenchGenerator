using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
{
    public class RampSignalViewModel : SignalViewModel
    {
        public RampSignal Model
        {
            get { return (RampSignal)this.model; }
        }

        public RampSignalViewModel(RampSignal signal) : base(signal)
        {
        }
    }
}
