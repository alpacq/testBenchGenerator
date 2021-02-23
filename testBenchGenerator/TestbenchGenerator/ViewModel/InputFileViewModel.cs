using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.TestbenchGenerator.Model;

namespace testBenchGenerator.TestbenchGenerator.ViewModel
{
    public class InputFileViewModel : INotifyPropertyChanged
    {
        private InputFile inputFile;

        public InputFile InputFile
        {
            get { return this.inputFile; }
        }

        public string Path
        {
            get { return this.InputFile.Path; }
            set { this.InputFile.Path = value; OnPropertyChanged("Path"); }
        }

        public string Name
        {
            get { return this.InputFile.Name; }
            set { this.InputFile.Name = value; OnPropertyChanged("Name"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public InputFileViewModel(InputFile inputFile)
        {
            this.inputFile = inputFile;
        }
    }
}
