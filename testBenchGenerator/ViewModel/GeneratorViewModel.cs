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
        private ModuleFile moduleFile;
        private InputFile inputFile;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ModuleFile ModuleFile
        {
            get { return this.moduleFile; }
            set { this.moduleFile = value; }
        }

        public InputFile InputFile
        {
            get { return this.inputFile; }
            set { this.inputFile = value; }
        }

        public List<Clock> Clocks
        {
            get { return this.ModuleFile.Clocks; }
            set { this.ModuleFile.Clocks = value; this.OnPropertyChanged("Clocks"); }
        }

        public List<Reset> Resets
        {
            get { return this.ModuleFile.Resets; }
            set { this.ModuleFile.Resets = value; this.OnPropertyChanged("Resets"); }
        }

        public List<DataInput> DataInputs
        {
            get { return this.ModuleFile.DataInputs; }
            set { this.ModuleFile.DataInputs = value; this.OnPropertyChanged("DataInputs"); }
        }

        public List<ValidInput> ValidInputs
        {
            get { return this.ModuleFile.ValidInputs; }
            set { this.ModuleFile.ValidInputs = value; this.OnPropertyChanged("ValidInputs"); }
        }

        public GeneratorViewModel(string modulePath, string inputPath = null)
        {
            if (modulePath != null && modulePath != String.Empty)
            {
                this.ModuleFile = new ModuleFile(modulePath);
                this.OnPropertyChanged("Clocks");
                this.OnPropertyChanged("Resets");
                this.OnPropertyChanged("DataInputs");
            }
            if (inputPath != null && inputPath != String.Empty)
                this.InputFile = new InputFile(inputPath);
        }

        public bool GenerateFile()
        {
            string tbFilePath = this.ModuleFile.Path.Replace(".sv", "_tb.sv");

            FileGen file = (this.InputFile != null) ? new FileGen(this.ModuleFile, tbFilePath, this.InputFile) : new FileGen(this.ModuleFile, tbFilePath);

            return file.GenerateFile();
        }
    }
}
