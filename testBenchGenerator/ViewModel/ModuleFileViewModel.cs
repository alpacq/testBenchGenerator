using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Model;

namespace testBenchGenerator.ViewModel
{
    public class ModuleFileViewModel : INotifyPropertyChanged
    {
        private ModuleFile moduleFile;
        private List<PortViewModel> inputs;
        private List<PortViewModel> outputs;
        private List<ParameterViewModel> parameters;
        private List<ResetViewModel> resets;
        private List<ClockViewModel> clocks;
        private List<DataInputViewModel> dataInputs;
        private List<ValidInputViewModel> validInputs;

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

        public List<DataInputViewModel> DataInputs
        {
            get
            {
                this.dataInputs = new List<DataInputViewModel>();
                foreach(DataInput di in this.ModuleFile.DataInputs)
                {
                    this.dataInputs.Add(new DataInputViewModel(di));
                }
                return this.dataInputs;
            }
            set
            {
                this.ModuleFile.DataInputs = new List<DataInput>();
                foreach(DataInputViewModel di in this.dataInputs)
                {
                    this.ModuleFile.DataInputs.Add(di.DataInput);
                }
                OnPropertyChanged("DataInputs");
            }
        }

        public List<ValidInputViewModel> ValidInputs
        {
            get
            {
                this.validInputs = new List<ValidInputViewModel>();
                foreach(ValidInput vi in this.ModuleFile.ValidInputs)
                {
                    this.validInputs.Add(new ValidInputViewModel(vi));
                }
                return this.validInputs;
            }
            set
            {
                this.ModuleFile.ValidInputs = new List<ValidInput>();
                foreach(ValidInputViewModel vi in this.validInputs)
                {
                    this.ModuleFile.ValidInputs.Add(vi.ValidInput);
                }
                OnPropertyChanged("ValidInputs");
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
        }
    }
}
