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

        public List<Port> Inputs
        {
            get { return this.ModuleFile.Inputs; }
            set { this.ModuleFile.Inputs = value; OnPropertyChanged("Inputs"); }
        }

        public List<Port> Outputs
        {
            get { return this.ModuleFile.Outputs; }
            set { this.ModuleFile.Outputs = value; OnPropertyChanged("Outputs"); }
        }

        public Dictionary<string, string> Parameters
        {
            get { return this.ModuleFile.Parameters; }
            set { this.ModuleFile.Parameters = value; OnPropertyChanged("Parameters"); }
        }

        public List<Reset> Resets
        {
            get { return this.ModuleFile.Resets; }
            set { this.ModuleFile.Resets = value; OnPropertyChanged("Resets"); }
        }

        public List<Clock> Clocks
        {
            get { return this.ModuleFile.Clocks; }
            set { this.ModuleFile.Clocks = value; OnPropertyChanged("Clocks"); }
        }

        public List<DataInput> DataInputs
        {
            get { return this.ModuleFile.DataInputs; }
            set { this.ModuleFile.DataInputs = value; OnPropertyChanged("DataInputs"); }
        }

        public List<ValidInput> ValidInputs
        {
            get { return this.ModuleFile.ValidInputs; }
            set { this.ModuleFile.ValidInputs = value; OnPropertyChanged("ValidInputs"); }
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
    }
}
