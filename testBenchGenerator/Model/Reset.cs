using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Model
{
    public class Reset : Port
    {
        private bool polarization;

        public bool Polarization
        {
            get { return this.polarization; }
            set { this.polarization = value; }
        }

        public Reset(string name, bool pol = true) : base(name)
        { 
            this.Polarization = pol;
        }

        public Reset(Port port, bool pol = true) : base(port.Name, port.Bitwidth)
        {
            this.Polarization = pol;
        }
    }
}
