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
        private Port validIn;
        private string dataVector;
        private string vldSeq;
        private Port validOut;
        private Port dataOut;

        public Clock ClockSync
        {
            get { return this.clockSync; }
            set { this.clockSync = value; }
        }

        public Port ValidIn
        {
            get { return this.validIn; }
            set { this.validIn = value; }
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

        public Port ValidOut
        {
            get { return this.validOut; }
            set { this.validOut = value; }
        }

        public Port DataOut
        {
            get { return this.dataOut; }
            set { this.dataOut = value; }
        }

        public DataInput(string name, string bitwidth = null) : base(name, bitwidth)
        {            
        }

        public DataInput(Port port) : base(port.Name, port.Bitwidth)
        {
        }
    }
}
