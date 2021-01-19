using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Model
{
    public class DataInput : Port
    {
        
        private Clock clockSync;
        private ValidInput valid;
        private string dataVector;
        private string vldSeq;

        public Clock ClockSync
        {
            get { return this.clockSync; }
            set { this.clockSync = value; }
        }

        public ValidInput Valid
        {
            get { return this.valid; }
            set { this.valid = value; }
        }

        public string DataVector
        {
            get { return this.dataVector; }
            set { this.dataVector = value; }
        }

        public string VldSeq
        {
            get { return this.vldSeq; }
            set { this.vldSeq = value; }
        }

        public DataInput(string name, string bitwidth = null) : base(name, bitwidth)
        {            
        }

        public DataInput(Port port) : base(port.Name, port.Bitwidth)
        {
        }
    }
}
