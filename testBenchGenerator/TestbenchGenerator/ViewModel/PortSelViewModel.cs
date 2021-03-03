using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Common;
using testBenchGenerator.TestbenchGenerator.Model;

namespace testBenchGenerator.TestbenchGenerator.ViewModel
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
