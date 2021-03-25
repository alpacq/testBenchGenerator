using FPGADeveloperTools.Common.Model.Ports;
using FPGADeveloperTools.Common.ViewModel;
using FPGADeveloperTools.NewTCWindow.Model;

namespace FPGADeveloperTools.NewTCWindow.ViewModel
{
    public class PortSelViewModel : ViewModelBase
    {
        private PortSel portSel;

        public string Name
        {
            get { return this.portSel.Name; }
        }

        public bool IsSel
        {
            get { return this.portSel.IsSel; }
            set { this.portSel.IsSel = value; OnPropertyChanged("IsSel"); }
        }

        public Port Port
        {
            get { return this.portSel.Port; }
        }

        public PortSelViewModel(PortSel portSel)
        {
            this.portSel = portSel;
        }
    }
}
