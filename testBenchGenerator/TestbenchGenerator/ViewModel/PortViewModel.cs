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
