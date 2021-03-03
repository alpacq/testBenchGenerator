using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
{
    public class ConstSignalViewModel : SignalViewModel
    {
        public ConstSignal Model
        {
            get { return (ConstSignal)this.model; }
        }

        public ConstSignalViewModel(ConstSignal signal) : base(signal)
        { 
        }
    }
}
