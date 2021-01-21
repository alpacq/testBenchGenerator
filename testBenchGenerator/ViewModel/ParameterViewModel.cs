using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Model;

namespace testBenchGenerator.ViewModel
{
    public class ParameterViewModel : INotifyPropertyChanged
    {
        private Parameter parameter;

        public Parameter Parameter
        {
            get { return this.parameter; }
        }

        public string Name
        {
            get { return this.Parameter.Name; }
            set { this.Parameter.Name = value; OnPropertyChanged("Name"); }
        }

        public string Value
        {
            get { return this.Parameter.Value; }
            set { this.Parameter.Value = value; OnPropertyChanged("Value"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ParameterViewModel(Parameter param)
        {
            this.parameter = param;
        }
    }
}
