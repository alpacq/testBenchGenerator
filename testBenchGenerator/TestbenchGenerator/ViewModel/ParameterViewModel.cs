using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Common;
using testBenchGenerator.TestbenchGenerator.Model;

namespace testBenchGenerator.TestbenchGenerator.ViewModel
{
    public class ParameterViewModel : ViewModelBase
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

        public ParameterViewModel(Parameter param)
        {
            this.parameter = param;
        }
    }
}
