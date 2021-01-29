using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Model;

namespace testBenchGenerator.ViewModel
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
            set { this.moduleFile = value; }
        }

        public List<PortViewModel> Outputs
        {
            get { return this.ModuleFile.Outputs; }
            set { this.ModuleFile.Outputs = value; this.OnPropertyChanged("Outputs"); }
        }

        public List<ParameterViewModel> Parameters
        {
            get { return this.ModuleFile.Parameters; }
            set { this.ModuleFile.Parameters = value; this.OnPropertyChanged("Parameters"); }
        }

        public List<ClockViewModel> Clocks
        {
            get { return this.ModuleFile.Clocks; }
            set { this.ModuleFile.Clocks = value; this.OnPropertyChanged("Clocks"); }
        }

        public List<ResetViewModel> Resets
        {
            get { return this.ModuleFile.Resets; }
            set { this.ModuleFile.Resets = value; this.OnPropertyChanged("Resets"); }
        }

        public List<TestCaseViewModel> TestCases
        {
            get { return this.ModuleFile.TestCases; }
            set { this.ModuleFile.TestCases = value; this.OnPropertyChanged("TestCases"); }
        }


        public TestCaseViewModel SelectedTestCase
        {
            get { return this.selectedTC; }
            set { this.selectedTC = value; this.OnPropertyChanged("SelectedTestCase"); }
        }

        public GeneratorViewModel(string modulePath)
        {
            if (modulePath != null && modulePath != String.Empty)
            {
                this.ModuleFile = new ModuleFileViewModel(new ModuleFile(modulePath));
                this.OnPropertyChanged("Clocks");
                this.OnPropertyChanged("Resets");
                this.OnPropertyChanged("DataInputs");
                this.OnPropertyChanged("Outputs");
                this.OnPropertyChanged("Parameters");
            }
        }

        public bool GenerateFile()
        {
            string tbFilePath = this.ModuleFile.Path.Replace(".sv", "_tb.sv").Replace(".v", "_tb.sv");

            FileGen file = new FileGen(this.ModuleFile.ModuleFile, tbFilePath);

            return file.GenerateFile();
        }
    }
}
