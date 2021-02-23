using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.TestbenchGenerator.Model;

namespace testBenchGenerator.TestbenchGenerator.ViewModel
{
    public class GeneratorViewModel : INotifyPropertyChanged
    {
        private ModuleFileViewModel moduleFile;
        private TestCaseViewModel selectedTC;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ModuleFileViewModel ModuleFile
        {
            get { return this.moduleFile; }
            set { this.moduleFile = value; this.OnPropertyChanged("CanAdd"); this.OnPropertyChanged("CanGenerate"); }
        }

        public List<PortViewModel> Inputs
        {
            get { if (this.ModuleFile != null) return this.ModuleFile.Ins; else return null; }
            set { this.ModuleFile.Ins = value; this.OnPropertyChanged("Inputs"); }
        }

        public List<PortViewModel> Outputs
        {
            get { if (this.ModuleFile != null) return this.ModuleFile.Outputs; else return null; }
            set { this.ModuleFile.Outputs = value; this.OnPropertyChanged("Outputs"); }
        }

        public List<ParameterViewModel> Parameters
        {
            get { if (this.ModuleFile != null) return this.ModuleFile.Parameters; else return null; }
            set { this.ModuleFile.Parameters = value; this.OnPropertyChanged("Parameters"); }
        }

        public List<ClockViewModel> Clocks
        {
            get { if (this.ModuleFile != null) return this.ModuleFile.Clocks; else return null; }
            set { this.ModuleFile.Clocks = value; this.OnPropertyChanged("Clocks"); }
        }

        public List<ResetViewModel> Resets
        {
            get { if (this.ModuleFile != null) return this.ModuleFile.Resets; else return null; }
            set { this.ModuleFile.Resets = value; this.OnPropertyChanged("Resets"); }
        }

        public List<TestCaseViewModel> TestCases
        {
            get { if (this.ModuleFile != null) return this.ModuleFile.TestCases; else return null; }
            set { this.ModuleFile.TestCases = value; this.OnPropertyChanged("TestCases"); this.OnPropertyChanged("CanRemove"); }
        }

        public TestCaseViewModel SelectedTestCase
        {
            get { return this.selectedTC; }
            set { this.selectedTC = value; this.OnPropertyChanged("SelectedTestCase"); this.OnPropertyChanged("CanRemove"); }
        }

        public bool CanAdd
        {
            get { return this.ModuleFile != null; }
        }

        public bool CanRemove
        {
            get { return this.TestCases != null && this.TestCases.Count > 0 && this.SelectedTestCase != null; }
        }

        public bool CanGenerate
        {
            get { return this.ModuleFile != null; }
        }

        public GeneratorViewModel(string modulePath)
        {            
            if (modulePath != null && modulePath != String.Empty)
            {
                this.ModuleFile = new ModuleFileViewModel(new ModuleFile(modulePath));
                this.OnPropertyChanged("Clocks");
                this.OnPropertyChanged("Resets");
                this.OnPropertyChanged("Inputs");
                this.OnPropertyChanged("Outputs");
                this.OnPropertyChanged("Parameters");
            }
            this.OnPropertyChanged("CanAdd");
            this.OnPropertyChanged("CanRemove");
            this.OnPropertyChanged("CanGenerate");
        }

        public bool GenerateFile()
        {
            string tbFilePath = this.ModuleFile.Path.Replace(".sv", "_tb.sv").Replace(".v", "_tb.sv");

            FileGen file = new FileGen(this.ModuleFile.ModuleFile, tbFilePath);

            return file.GenerateFile();
        }
    }
}
