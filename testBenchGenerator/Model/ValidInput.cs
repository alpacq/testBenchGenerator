using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Model
{
    public class ValidInput : Port
    {
        public ValidInput(string name, string bitwidth = null) : base(name, bitwidth)
        {
        }

        public ValidInput(Port port) : base(port.Name, port.Bitwidth)
        {
        }
    }
}
