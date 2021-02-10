using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Model;

namespace testBenchGenerator.ViewModel
{
    public class TestCaseViewModel : INotifyPropertyChanged
    {
        private TestCase testCase;
        private ModuleFileViewModel module;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public TestCase TestCase
        {
            get { return this.testCase; }
        }

        public List<Port> DataIns
        {
            get { return this.TestCase.DataIns; }
            set { this.TestCase.DataIns = value; OnPropertyChanged("DataIns"); }
        }

        public Clock ClockSync
        {
            get { return this.TestCase.ClockSync; }
            set { this.TestCase.ClockSync = value; OnPropertyChanged("ClockSync"); }
        }

        public Port ValidIn
        {
            get { return this.TestCase.ValidIn; }
            set { this.TestCase.ValidIn = value; OnPropertyChanged("ValidIn"); }
        }

        public string DataVector
        {
            get { return this.TestCase.DataVector; }
            set 
            { 
                this.TestCase.DataVector = value; 
                OnPropertyChanged("DataVector"); 
                this.DataOutVector = this.module.Path.Replace(".sv", "_" + value.Split('\\').LastOrDefault().Replace(".txt","_out.txt")); 
            }
        }

        public string DataOutVector
        {
            get { return this.TestCase.DataOutVector; }
            set { this.TestCase.DataOutVector = value; OnPropertyChanged("DataOutVector"); }
        }

        public string VldSeq
        {
            get { return this.TestCase.VldSeq; }
            set { this.TestCase.VldSeq = value; OnPropertyChanged("VldSeq"); }
        }

        public Port ValidOut
        {
            get { return this.TestCase.ValidOut; }
            set { this.TestCase.ValidOut = value; OnPropertyChanged("ValidOut"); }
        }

        public List<Port> DataOuts
        {
            get { return this.TestCase.DataOuts; }
            set { this.TestCase.DataOuts = value; OnPropertyChanged("DataOuts"); }
        }

        public string DIs
        {
            get
            {
                string dis = String.Empty;
                foreach(Port port in this.DataIns)
                {
                    dis += port.Name;
                    if (!(port == this.DataIns.LastOrDefault()))
                        dis += ", ";
                }
                return dis;
            }
        }

        public string DOs
        {
            get
            {
                string dos = String.Empty;
                foreach (Port port in this.DataOuts)
                {
                    dos += port.Name;
                    if (!(port == this.DataOuts.LastOrDefault()))
                        dos += ", ";
                }
                return dos;
            }
        }

        public bool Loop
        {
            get { return this.TestCase.Loop; }
            set { this.TestCase.Loop = value; OnPropertyChanged("Loop"); }
        }

        public Radix Radix
        {
            get { return this.TestCase.Radix; }
            set { this.TestCase.Radix = value; OnPropertyChanged("Radix"); }
        }

        public int Order
        {
            get { return this.TestCase.Order; }
            set { this.TestCase.Order = value; OnPropertyChanged("Order"); }
        }

        public string Description
        {
            get { return "@" + this.ClockSync.Name + " | " + this.DIs + " | " + this.DataVector.Split('\\').LastOrDefault().Replace(".txt", "") + " | " + this.DOs; }
        }

        public TestCaseViewModel(TestCase testCase) 
        {
            this.testCase = testCase;
        }

        public TestCaseViewModel(TestCase testCase, ModuleFileViewModel module)
        {
            this.testCase = testCase;
            this.module = module;
        }
    }
}
