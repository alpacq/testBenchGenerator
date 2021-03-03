using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
{
    public class SineSignalViewModel : SignalViewModel
    {
        public double Freq
        {
            get { return this.Model.Freq; }
            set { this.Model.Freq = value; OnPropertyChanged("Freq"); }
        }

        public double Phoff
        {
            get { return this.Model.Phoff; }
            set { this.Model.Phoff = value; OnPropertyChanged("Phoff"); }
        }

        public SineSignal Model
        {
            get { return (SineSignal)this.model; }
        }

        public SineSignalViewModel(SineSignal signal) : base(signal)
        {            
        }
    }
}
