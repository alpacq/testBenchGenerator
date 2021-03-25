using System.Collections.Generic;
using FPGADeveloperTools.Common.Model.Ports;

namespace FPGADeveloperTools.Common.Model.TestCases
{
    public class TestCase 
    {
        private List<Port> dataIns;
        private Clock clockSync;
        private Port validIn;
        private string dataVector;
        private string dataOutVector;
        private string vldSeq;
        private Port validOut;
        private List<Port> dataOuts;
        private bool loop;
        private Radix radix;
        private int order;

        public List<Port> DataIns
        {
            get { return this.dataIns; }
            set { this.dataIns = value; }
        }
        
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

        public string DataOutVector
        {
            get { return this.dataOutVector; }
            set { this.dataOutVector = value; }
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

        public List<Port> DataOuts
        {
            get { return this.dataOuts; }
            set { this.dataOuts = value; }
        }

        public bool Loop
        {
            get { return this.loop; }
            set { this.loop = value; }
        }

        public Radix Radix
        {
            get { return this.radix; }
            set { this.radix = value; }
        }

        public int Order
        {
            get { return this.order; }
            set { this.order = value; }
        }

        public TestCase()
        {
            this.dataIns = new List<Port>();
            this.dataOuts = new List<Port>();
        }
    }
}
