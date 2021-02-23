using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.TestbenchGenerator.Model;

namespace testBenchGenerator.TestbenchGenerator.ViewModel
{
    public class ModuleFileViewModel : INotifyPropertyChanged
    {
        private ModuleFile moduleFile;
        private List<PortViewModel> inputs;
        private List<PortViewModel> ins;
        private List<PortViewModel> outputs;
        private List<ParameterViewModel> parameters;
        private List<ResetViewModel> resets;
        private List<ClockViewModel> clocks;
        private List<TestCaseViewModel> testCases;

        public ModuleFile ModuleFile
        {
            get { return this.moduleFile; }
        }

        public string Path
        {
            get { return this.ModuleFile.Path; }
            set { this.ModuleFile.Path = value; OnPropertyChanged("Path"); }
        }

        public string Name
        {
            get { return this.ModuleFile.Name; }
            set { this.ModuleFile.Name = value; OnPropertyChanged("Name"); }
        }

        public List<PortViewModel> Ins
        {
            get
            {
                this.ins = new List<PortViewModel>();
                foreach (Port input in this.ModuleFile.Ins)
                {
                    this.ins.Add(new PortViewModel(input));
                }
                return this.ins;
            }
            set
            {
                this.ModuleFile.Ins = new List<Port>();
                foreach (PortViewModel input in this.ins)
                {
                    this.ModuleFile.Ins.Add(input.Port);
                }
                OnPropertyChanged("Ins");
            }
        }

        public List<PortViewModel> Inputs
        {
            get 
            {
                this.inputs = new List<PortViewModel>();
                foreach(Port input in this.ModuleFile.Inputs)
                {
                    this.inputs.Add(new PortViewModel(input));
                }
                return this.inputs; 
            }
            set 
            {
                this.ModuleFile.Inputs = new List<Port>();
                foreach (PortViewModel input in this.inputs)
                {
                    this.ModuleFile.Inputs.Add(input.Port);
                }
                OnPropertyChanged("Inputs"); 
            }
        }

        public List<PortViewModel> Outputs
        {
            get
            {
                this.outputs = new List<PortViewModel>();
                foreach (Port output in this.ModuleFile.Outputs)
                {
                    this.outputs.Add(new PortViewModel(output));
                }
                return this.outputs;
            }
            set
            {
                this.ModuleFile.Outputs = new List<Port>();
                foreach (PortViewModel output in this.outputs)
                {
                    this.ModuleFile.Outputs.Add(output.Port);
                }
                OnPropertyChanged("Outputs");
            }
        }

        public List<ParameterViewModel> Parameters
        {
            get
            {
                this.parameters = new List<ParameterViewModel>();
                foreach (Parameter param in this.ModuleFile.Parameters)
                {
                    this.parameters.Add(new ParameterViewModel(param));
                }
                return this.parameters;
            }
            set
            {
                this.ModuleFile.Parameters = new List<Parameter>();
                foreach (ParameterViewModel param in this.parameters)
                {
                    this.ModuleFile.Parameters.Add(param.Parameter);
                }
                OnPropertyChanged("Parameters");
            }
        }

        public List<ResetViewModel> Resets
        {
            get
            {
                this.resets = new List<ResetViewModel>();
                foreach(Reset reset in this.ModuleFile.Resets)
                {
                    this.resets.Add(new ResetViewModel(reset));
                }
                return this.resets;
            }
            set
            {
                this.ModuleFile.Resets = new List<Reset>();
                foreach(ResetViewModel reset in this.resets)
                {
                    this.ModuleFile.Resets.Add(reset.Reset);
                }
                OnPropertyChanged("Resets");
            }
        }

        public List<ClockViewModel> Clocks
        {
            get
            {
                this.clocks = new List<ClockViewModel>();
                foreach(Clock clk in this.ModuleFile.Clocks)
                {
                    this.clocks.Add(new ClockViewModel(clk));
                }
                return this.clocks;
            }
            set
            {
                this.ModuleFile.Clocks = new List<Clock>();
                foreach(ClockViewModel clk in this.clocks)
                {
                    this.ModuleFile.Clocks.Add(clk.Clock);
                }
                OnPropertyChanged("Clocks");
            }
        }

        public List<TestCaseViewModel> TestCases
        {
            get
            {
                return this.testCases;
            }
            set
            {
                this.testCases = value;
                this.ModuleFile.TestCases = new List<TestCase>();
                foreach(TestCaseViewModel tc in this.testCases)
                {
                    this.ModuleFile.TestCases.Add(tc.TestCase);
                }
                OnPropertyChanged("TestCases");
            }
        }

        public int BwLen
        {
            get { return this.ModuleFile.BwLen; }
            set { this.ModuleFile.BwLen = value; OnPropertyChanged("BwLen"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ModuleFileViewModel(ModuleFile moduleFile)
        {
            this.moduleFile = moduleFile;
            this.testCases = new List<TestCaseViewModel>();
        }
    }
}
