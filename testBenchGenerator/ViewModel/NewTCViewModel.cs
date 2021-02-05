using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Model;

namespace testBenchGenerator.ViewModel
{
    public class NewTCViewModel : INotifyPropertyChanged
    {
        private ModuleFileViewModel moduleFileVM;
        private TestCaseViewModel tcVM;
        private string inputPath;
        private List<PortSelViewModel> ins;
        private List<PortSelViewModel> outs;
        private bool loop;
        private ClockViewModel clock;
        private string radix;
        private string seq;
        private PortViewModel validIn;
        private PortViewModel validOut;
        private string addText;
        private List<string> seqs;
        private List<string> radixes;

        public ModuleFileViewModel ModuleFileVM
        {
            get { return this.moduleFileVM; }
        }

        public TestCaseViewModel TCVM
        {
            get { return this.tcVM; }
            set { this.tcVM = value; OnPropertyChanged("TCVM"); }
        }

        public List<string> Seqs
        {
            get { return this.seqs; }
            set { this.seqs = value; OnPropertyChanged("Seqs"); }
        }

        public List<string> Radixes
        {
            get { return this.radixes; }
            set { this.radixes = value; OnPropertyChanged("Radixes"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public List<ClockViewModel> Clocks
        {
            get { return this.ModuleFileVM.Clocks; }
        }

        public List<PortViewModel> Inputs
        {
            get { return this.ModuleFileVM.Ins; }
        }

        public List<PortViewModel> Outputs
        {
            get { return this.ModuleFileVM.Outputs; }
        }

        public List<PortSelViewModel> Ins
        {
            get { return this.ins; }
            set { this.ins = value; OnPropertyChanged("Ins"); }
        }

        public List<PortSelViewModel> Outs
        {
            get { return this.outs; }
            set { this.outs = value; OnPropertyChanged("Outs"); }
        }

        public string InputPath
        {
            get { return this.inputPath; }
            set { this.inputPath = value; OnPropertyChanged("InputPath"); }
        }

        public bool Loop
        {
            get { return this.loop; }
            set { this.loop = value; OnPropertyChanged("Loop"); }
        }

        public ClockViewModel Clock
        {
            get { return this.clock; }
            set { this.clock = value; OnPropertyChanged("Clock"); }
        }

        public string Radix
        {
            get { return this.radix; }
            set { this.radix = value; OnPropertyChanged("Radix"); }
        }

        public string Seq
        {
            get { return this.seq; }
            set { this.seq = value; OnPropertyChanged("Seq"); }
        }

        public PortViewModel ValidIn
        {
            get { return this.validIn; }
            set { this.validIn = value; OnPropertyChanged("ValidIn"); }
        }

        public PortViewModel ValidOut
        {
            get { return this.validOut; }
            set { this.validOut = value; OnPropertyChanged("ValidOut"); }
        }

        public string AddText
        {
            get { return this.addText; }
            set { this.addText = value; OnPropertyChanged("AddText"); }
        }

        public NewTCViewModel(ModuleFileViewModel moduleFileVM)
        {
            this.Seqs = new List<string>()
            {
                "1/1", "1/2", "1/4", "1/8", "1/16", "1/32", "1/64", "1/128", "1/256", "1/512"
            };
            this.Radixes = new List<string>()
            {
                "Decimal", "Hexadecimal"
            };
            this.moduleFileVM = moduleFileVM;
            this.Ins = new List<PortSelViewModel>();
            foreach (PortViewModel port in this.Inputs)
            {
                this.Ins.Add(new PortSelViewModel(new PortSel(port.Port)));
            }
            this.Outs = new List<PortSelViewModel>();
            foreach(PortViewModel port in this.Outputs)
            {
                this.Outs.Add(new PortSelViewModel(new PortSel(port.Port)));
            }
            this.AddText = "Add";
        }

        public NewTCViewModel(ModuleFileViewModel moduleFileVM, TestCaseViewModel tcVM)
        {
            this.Seqs = new List<string>()
            {
                "1/1", "1/2", "1/4", "1/8", "1/16", "1/32", "1/64", "1/128", "1/256", "1/512"
            };
            this.Radixes = new List<string>()
            {
                "Decimal", "Hexadecimal"
            };
            this.moduleFileVM = moduleFileVM;
            this.tcVM = tcVM;
            this.Ins = new List<PortSelViewModel>();
            foreach (PortViewModel port in this.Inputs)
            {
                PortSel ps = new PortSel(port.Port);
                if (this.TCVM.DataIns.Contains(ps.Port))
                    ps.IsSel = true;
                this.Ins.Add(new PortSelViewModel(ps));
            }
            this.Outs = new List<PortSelViewModel>();
            foreach (PortViewModel port in this.Outputs)
            {
                PortSel ps = new PortSel(port.Port);
                if (this.TCVM.DataOuts.Contains(ps.Port))
                    ps.IsSel = true;
                this.Ins.Add(new PortSelViewModel(ps));
            }
            this.Clock = this.ModuleFileVM.Clocks.Where(c => c.Name == this.TCVM.ClockSync.Name).FirstOrDefault();
            this.Seq = this.SVStringToSeq();
            this.Loop = this.TCVM.Loop;
            this.Radix = this.TCVM.Radix == Model.Radix.Decimal ? "Decimal" : "Hexadecimal";
            this.ValidIn = this.ModuleFileVM.Ins.Where(p => p.Name == this.TCVM.ValidIn.Name).FirstOrDefault();
            this.ValidOut = this.ModuleFileVM.Outputs.Where(p => p.Name == this.TCVM.ValidOut.Name).FirstOrDefault();
            this.InputPath = this.TCVM.DataVector;

            this.AddText = "Save";
        }

        public void Add()
        {
            if (this.TCVM == null)
            {
                TestCaseViewModel tc = new TestCaseViewModel(new TestCase(), this.ModuleFileVM);

                tc.ClockSync = this.Clock.Clock;
                tc.DataVector = this.InputPath;
                tc.DataIns = (from input in this.Ins
                              where input.IsSel
                              select input.Port).ToList<Port>();
                tc.DataOuts = (from output in this.Outs
                               where output.IsSel
                               select output.Port).ToList<Port>();
                tc.Loop = this.Loop;
                tc.Radix = this.Radix == "Decimal" ? Model.Radix.Decimal : Model.Radix.Hexadecimal;
                tc.ValidIn = this.ValidIn.Port;
                tc.ValidOut = this.ValidOut.Port;
                tc.VldSeq = this.SeqtoSVString();

                this.ModuleFileVM.TestCases.Add(tc);
            }
            else
            {
                this.TCVM.ClockSync = this.Clock.Clock;
                this.TCVM.DataVector = this.InputPath;
                this.TCVM.DataIns = (from input in this.Ins
                              where input.IsSel
                              select input.Port).ToList<Port>();
                this.TCVM.DataOuts = (from output in this.Outs
                               where output.IsSel
                               select output.Port).ToList<Port>();
                this.TCVM.Loop = this.Loop;
                this.TCVM.Radix = this.Radix == "Decimal" ? Model.Radix.Decimal : Model.Radix.Hexadecimal;
                this.TCVM.ValidIn = this.ValidIn.Port;
                this.TCVM.ValidOut = this.ValidOut.Port;
                this.TCVM.VldSeq = this.SeqtoSVString();
            }
        }

        public string SeqtoSVString()
        {
            switch(this.Seq)
            {
                case "1/1":
                    return "11111111";
                case "1/2":
                    return "10101010";
                case "1/4":
                    return "10001000";
                case "1/8":
                    return "10000000";
                case "1/16":
                    return "1000000000000000";
                case "1/32":
                    return "10000000000000000000000000000000";
                case "1/64":
                    return "1000000000000000000000000000000000000000000000000000000000000000";
                case "1/128":
                    return "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
                case "1/256":
                    return "1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
                case "1/512":
                    return "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
                default:
                    return null;
            }
        }

        public string SVStringToSeq()
        {
            switch(this.TCVM.VldSeq)
            {
                case "11111111":
                    return "1/1";
                case "10101010":
                    return "1/2";
                case "10001000":
                    return "1/4";
                case "10000000":
                    return "1/8";
                case "1000000000000000":
                    return "1/16";
                case "10000000000000000000000000000000":
                    return "1/32";
                case "1000000000000000000000000000000000000000000000000000000000000000":
                    return "1/64";
                case "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000":
                    return "1/128";
                case "1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000":
                    return "1/256";
                case "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000":
                    return "1/512";
                default:
                    return null;
            }
        }
    }
}
