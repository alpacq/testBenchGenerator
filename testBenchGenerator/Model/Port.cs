using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Model
{
    public class Port
    {
        private string name;
        private string bitwidth;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Bitwidth
        {
            get { return this.bitwidth; }
            set { this.bitwidth = value; }
        }

        public Port(string name, string bitwidth = null)
        {
            this.Name = name;
            this.Bitwidth = bitwidth;
        }
    }
}
