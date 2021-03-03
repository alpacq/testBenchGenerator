using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
{
    public class OFDMSignalViewModel : SignalViewModel
    {
        public string Modulation
        {
            get { return this.Model.Modulation; }
            set { this.Model.Modulation = value; OnPropertyChanged("Modulation"); }
        }

        public double Fsoff
        {
            get { return this.Model.Fsoff; }
            set { this.Model.Fsoff = value; OnPropertyChanged("Fsoff"); }
        }       

        public double OFDMN
        {
            get { return this.Model.OFDMN; }
            set { this.Model.OFDMN = value; OnPropertyChanged("OFDMN"); }
        }       

        public double Distance
        {
            get { return this.Model.Distance; }
            set { this.Model.Distance = value; OnPropertyChanged("Distance"); }
        }

        public int CPLength
        {
            get { return this.Model.CPLength; }
            set { this.Model.CPLength = value; OnPropertyChanged("CPLength"); }
        }

        public int NSymbols
        {
            get { return this.Model.NSymbols; }
            set { this.Model.NSymbols = value; OnPropertyChanged("NSymbols"); }
        }
        public OFDMSignal Model
        {
            get { return (OFDMSignal)this.model; }
        }

        public OFDMSignalViewModel(OFDMSignal signal) : base(signal)
        {
        }
    }
}
