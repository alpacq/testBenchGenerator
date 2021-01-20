using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Model;

namespace testBenchGenerator.ViewModel
{
    public class DataInputViewModel : PortViewModel, INotifyPropertyChanged
    {
        private DataInput dataInput;

        public DataInput DataInput
        {
            get { return this.dataInput; }
        }

        public Clock ClockSync
        {
            get { return this.DataInput.ClockSync; }
            set { this.DataInput.ClockSync = value; OnPropertyChanged("ClockSync"); }
        }

        public Port ValidIn
        {
            get { return this.DataInput.ValidIn; }
            set { this.DataInput.ValidIn = value; OnPropertyChanged("ValidIn"); }
        }

        public string DataVector
        {
            get { return this.DataInput.DataVector; }
            set { this.DataInput.DataVector = value; OnPropertyChanged("DataVector"); }
        }

        public string VldSeq
        {
            get { return this.DataInput.VldSeq; }
            set { this.DataInput.VldSeq = value; OnPropertyChanged("VldSeq"); }
        }

        public Port ValidOut
        {
            get { return this.DataInput.ValidOut; }
            set { this.DataInput.ValidOut = value; OnPropertyChanged("ValidOut"); }
        }

        public Port DataOut
        {
            get { return this.DataInput.DataOut; }
            set { this.DataInput.DataOut = value; OnPropertyChanged("DataOut"); }
        }

        public DataInputViewModel(DataInput dataInput) : base((Port)dataInput)
        {
            this.dataInput = dataInput;
        }
    }
}
