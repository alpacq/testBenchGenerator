using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.TestbenchGenerator.Model
{
    public abstract class ModuleFile
    {
        #region variables and properties
        private string path;
        private string name;        
        private List<Port> inputs;
        private List<Port> outputs;
        private List<Parameter> parameters;
        private List<Reset> resets;
        private List<Clock> clocks;
        private List<TestCase> testCases;
        private List<Port> ins;
        private int bwLen;

        protected string[] dutLines;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }

        public List<Port> Inputs
        {
            get { return this.inputs; }
            set { this.inputs = value; }
        }

        public List<Port> Outputs
        {
            get { return this.outputs; }
            set { this.outputs = value; }
        }

        public List<Parameter> Parameters
        {
            get { return this.parameters; }
            set { this.parameters = value; }
        }

        public List<Reset> Resets
        {
            get { return this.resets; }
            set { this.resets = value; }
        }

        public List<Clock> Clocks
        {
            get { return this.clocks; }
            set { this.clocks = value; }
        }

        public List<TestCase> TestCases
        {
            get { return this.testCases; }
            set { this.testCases = value; }
        }

        public List<Port> Ins
        {
            get { return this.ins; }
            set { this.ins = value; }
        }

        public int BwLen
        {
            get { return this.bwLen; }
            set { this.bwLen = value; }
        }
        #endregion

        public ModuleFile(string path)
        {
            this.Path = path;
            this.testCases = new List<TestCase>();
            this.Read();
        }

        protected abstract void Read();
        protected abstract void ReadInputs();
        protected abstract void ReadOutputs();
        protected abstract void ReadParameters();
        protected abstract void RecognizeResets();
        protected abstract void RecognizeClocks();
        protected abstract void RecognizeIns();
    }
}
