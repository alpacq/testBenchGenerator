using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Common
{
    public class SignalViewModel : ViewModelBase
    {
        protected Signal model;

        public double[] TimeVector
        {
            get { return this.model.TimeVector; }
            set { this.model.TimeVector = value; OnPropertyChanged("TimeVector");  }
        }

        public double[] I
        {
            get { return this.model.I; }
            set { this.model.I = value; OnPropertyChanged("I");  }
        }

        public double[] Q
        {
            get { return this.model.Q; }
            set { this.model.Q = value; OnPropertyChanged("Q");  }
        }

        public Complex[] X
        {
            get { return this.model.X; }
            set { this.model.X = value; OnPropertyChanged("X");  }
        }

        public double Gain
        {
            get { return this.model.Gain; }
            set { this.model.Gain = value; OnPropertyChanged("FGain");  }
        }

        public double[] Freqs
        {
            get { return this.model.Freqs; }
            set { this.model.Freqs = value; OnPropertyChanged("IQOut");  }
        }

        public double Fs
        {
            get { return this.model.Fs; }
            set { this.model.Fs = value; OnPropertyChanged("Fs");  }
        }

        public int FFTLength
        {
            get { return this.model.FFTLength; }
            set { this.model.FFTLength = value; OnPropertyChanged("FFTLength"); }
        }

        public int Length
        {
            get { return this.model.Length; }
            set { this.model.Length = value; OnPropertyChanged("Length");  }
        }

        public double LengthTime
        {
            get { return this.model.LengthTime; }
            set { this.model.LengthTime = value; OnPropertyChanged("LengthTime"); }
        }

        public double OS
        {
            get { return this.model.OS; }
            set { this.model.OS = value; OnPropertyChanged("OS"); }
        }

        public int Bitwidth
        {
            get { return this.model.Bitwidth; }
            set { this.model.Bitwidth = value; OnPropertyChanged("Bitwidth");  }
        }

        public double RMS
        {
            get { return this.model.RMS; }
            set { this.model.RMS = value; OnPropertyChanged("RMS");  }
        }

        public SignalViewModel(Signal signal)
        {
            this.model = signal;
        }
    }
}
