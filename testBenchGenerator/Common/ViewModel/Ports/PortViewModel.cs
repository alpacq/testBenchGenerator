using FPGADeveloperTools.Common.Model.Ports;

namespace FPGADeveloperTools.Common.ViewModel.Ports
{
    public class PortViewModel : ViewModelBase
    {
        private Port port;

        public Port Port
        { 
            get { return this.port; } 
        }

        public string Name
        {
            get { return this.Port.Name; }
            set { this.Port.Name = value; OnPropertyChanged("Name"); }
        }

        public string Bitwidth
        {
            get { return this.Port.Bitwidth; }
            set { this.Port.Bitwidth = value; OnPropertyChanged("Bitwidth"); }
        }

        public PortViewModel(Port port)
        {
            this.port = port;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
