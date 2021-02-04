using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Model;

namespace testBenchGenerator.ViewModel
{
    public class PortSelViewModel : INotifyPropertyChanged
    {
        private PortSel portSel;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

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
