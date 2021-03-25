using FPGADeveloperTools.Common.Model.Ports;

namespace FPGADeveloperTools.Common.ViewModel.Ports
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
