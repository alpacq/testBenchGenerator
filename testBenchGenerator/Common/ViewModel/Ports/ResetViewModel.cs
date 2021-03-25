using FPGADeveloperTools.Common.Model.Ports;

namespace FPGADeveloperTools.Common.ViewModel.Ports
{
    public class ResetViewModel : PortViewModel
    {
        private Reset reset;

        public Reset Reset
        {
            get { return this.reset; }
        }

        public bool Polarization
        {
            get { return this.Reset.Polarization; }
            set { this.Reset.Polarization = value; OnPropertyChanged("Polarization"); }
        }

        public ResetViewModel(Reset reset) : base((Port)reset)
        {
            this.reset = reset;
        }
    }
}
