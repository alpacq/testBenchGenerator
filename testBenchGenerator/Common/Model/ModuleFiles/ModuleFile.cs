using System.Collections.Generic;
using FPGADeveloperTools.Common.Model.Ports;
using FPGADeveloperTools.Common.Model.TestCases;

namespace FPGADeveloperTools.Common.Model.ModuleFiles
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
        protected void RecognizeResets()
        {
            this.Resets = new List<Reset>();
            foreach (Port input in this.Inputs)
            {
                if (input.Name.Contains("rst") || input.Name.Contains("reset"))
                {
                    if (input.Name.Contains("n"))
                        this.Resets.Add(new Reset(input, false)); //rst = 0
                    else
                        this.Resets.Add(new Reset(input, true)); //rst = 1
                }
            }
        }
        protected void RecognizeClocks()
        {
            this.Clocks = new List<Clock>();
            foreach (Port input in this.Inputs)
            {
                if (input.Name.Contains("clk") || input.Name.Contains("clock"))
                {
                    this.Clocks.Add(new Clock(input, 491.52));
                }
            }
        }
        protected void RecognizeIns()
        {
            this.Ins = new List<Port>();
            foreach (Port input in this.Inputs)
            {
                if (!input.Name.Contains("clk") && !input.Name.Contains("clock") && !input.Name.Contains("rst") && !input.Name.Contains("reset"))
                {
                    this.Ins.Add(input);
                }
            }
        }
    }
}
