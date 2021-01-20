using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Model;

namespace testBenchGenerator.ViewModel
{
    public class ValidInputViewModel : PortViewModel, INotifyPropertyChanged
    {
        private ValidInput validInput;

        public ValidInput ValidInput
        {
           get { return this.validInput; }
        }

        public ValidInputViewModel(ValidInput validInput) : base((Port)validInput)
        {
            this.validInput = validInput;
        }
    }
}
