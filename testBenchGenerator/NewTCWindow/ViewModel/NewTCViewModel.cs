using FPGADeveloperTools.Common.Model.Ports;
using FPGADeveloperTools.Common.Model.TestCases;
using FPGADeveloperTools.Common.ViewModel;
using FPGADeveloperTools.Common.ViewModel.ModuleFiles;
using FPGADeveloperTools.Common.ViewModel.Ports;
using FPGADeveloperTools.Common.ViewModel.TestCases;
using FPGADeveloperTools.NewTCWindow.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPGADeveloperTools.NewTCWindow.ViewModel
{
    public class NewTCViewModel : ViewModelBase
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
        private List<string> seqs;
        private List<string> radixes;
        private string problemToolTip;

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
            set { this.ins = value; OnPropertyChanged("Ins"); OnPropertyChanged("CanSave"); OnPropertyChanged("CanSaveN"); }
        }

        public List<PortSelViewModel> Outs
        {
            get { return this.outs; }
            set { this.outs = value; OnPropertyChanged("Outs"); OnPropertyChanged("CanSave"); OnPropertyChanged("CanSaveN"); }
        }

        public string InputPath
        {
            get { return this.inputPath; }
            set { this.inputPath = value; OnPropertyChanged("InputPath"); OnPropertyChanged("CanSave"); OnPropertyChanged("CanSaveN"); }
        }

        public bool Loop
        {
            get { return this.loop; }
            set { this.loop = value; OnPropertyChanged("Loop"); OnPropertyChanged("CanSave"); OnPropertyChanged("CanSaveN"); }
        }

        public ClockViewModel Clock
        {
            get { return this.clock; }
            set { this.clock = value; OnPropertyChanged("Clock"); OnPropertyChanged("CanSave"); OnPropertyChanged("CanSaveN"); }
        }

        public string Radix
        {
            get { return this.radix; }
            set { this.radix = value; OnPropertyChanged("Radix"); OnPropertyChanged("CanSave"); OnPropertyChanged("CanSaveN"); }
        }

        public string Seq
        {
            get { return this.seq; }
            set { this.seq = value; OnPropertyChanged("Seq"); OnPropertyChanged("CanSave"); OnPropertyChanged("CanSaveN"); }
        }

        public PortViewModel ValidIn
        {
            get { return this.validIn; }
            set { this.validIn = value; OnPropertyChanged("ValidIn"); OnPropertyChanged("CanSave"); OnPropertyChanged("CanSaveN"); }
        }

        public PortViewModel ValidOut
        {
            get { return this.validOut; }
            set { this.validOut = value; OnPropertyChanged("ValidOut"); OnPropertyChanged("CanSave"); OnPropertyChanged("CanSaveN"); }
        }

        public string ProblemToolTip
        {
            get { return this.problemToolTip; }
            set { this.problemToolTip = value; OnPropertyChanged("ProblemToolTip"); }
        }

        public bool CanSave
        {
            get 
            {
                bool toRet = true;
                string ptt = String.Empty;
                if(this.Clock == null)
                {
                    ptt += "Clock to synchronize always block is not selected.\n";
                    toRet = false;
                }
                if(this.InputPath == null)
                {
                    ptt += "Data input file is not selected.\n";
                    toRet = false;
                }
                if(this.Radix == null)
                {
                    ptt += "Data input file radix is not set.\n";
                    toRet = false;
                }
                if(this.Seq == null)
                {
                    ptt += "Data input valid sequence is not selected.\n";
                    toRet = false;
                }
                if(!this.Ins.Any(i => i.IsSel))
                {
                    ptt += "Data input ports are not selected.\n";
                    toRet = false;
                }
                if(!this.Outs.Any(i => i.IsSel))
                {
                    ptt += "Data output ports are not selected.\n";
                    toRet = false;
                }
                if(this.ValidIn == null)
                {
                    ptt += "Valid input port is not selected.\n";
                    toRet = false;
                }
                if(this.ValidOut == null)
                {
                    ptt += "Valid output port is not selected.\n";
                    toRet = false;
                }
                if (ptt.EndsWith("\n"))
                    ptt = ptt.Remove(ptt.Length - 1);
                this.ProblemToolTip = ptt;
                return toRet;
            }
        }

        public bool CanSaveN
        {
            get { return !this.CanSave; }
        }
        public NewTCViewModel(ModuleFileViewModel moduleFileVM)
        {
            this.Seqs = new List<string>()
            {
                "1/1", "1/2", "1/4", "1/8", "1/16", "1/32", "1/64", "1/128", "1/256", "1/512"
            };
            this.Radixes = new List<string>()
            {
                "Decimal", "Hexadecimal", "Floating Point"
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
        }

        public NewTCViewModel(ModuleFileViewModel moduleFileVM, TestCaseViewModel tcVM)
        {
            this.Seqs = new List<string>()
            {
                "1/1", "1/2", "1/4", "1/8", "1/16", "1/32", "1/64", "1/128", "1/256", "1/512"
            };
            this.Radixes = new List<string>()
            {
                "Decimal", "Hexadecimal", "Floating Point"
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
                this.Outs.Add(new PortSelViewModel(ps));
            }
            this.Clock = this.ModuleFileVM.Clocks.Where(c => c.Name == this.TCVM.ClockSync.Name).FirstOrDefault();
            this.Seq = this.TCVM.VldSeq;
            this.Loop = this.TCVM.Loop;
            this.Radix = this.TCVM.Radix == Common.Radix.Decimal ? "Decimal" : this.TCVM.Radix == Common.Radix.Hexadecimal ? "Hexadecimal" : "Floating Point";
            this.ValidIn = this.ModuleFileVM.Ins.Where(p => p.Name == this.TCVM.ValidIn.Name).FirstOrDefault();
            this.ValidOut = this.ModuleFileVM.Outputs.Where(p => p.Name == this.TCVM.ValidOut.Name).FirstOrDefault();
            this.InputPath = this.TCVM.DataVector;
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
                tc.Radix = this.Radix == "Decimal" ? Common.Radix.Decimal : this.Radix == "Hexadecimal" ? Common.Radix.Hexadecimal : Common.Radix.FloatingPoint;
                tc.ValidIn = this.ValidIn.Port;
                tc.ValidOut = this.ValidOut.Port;
                tc.VldSeq = this.Seq;
                if (this.ModuleFileVM.TestCases.Count > 0 && this.ModuleFileVM.TestCases.Last().Loop)
                {
                    tc.Order = this.ModuleFileVM.TestCases.Last().Order;
                    if (!this.Loop)
                        foreach (TestCaseViewModel tcvm in this.ModuleFileVM.TestCases)
                            if (tcvm.Loop)
                                tcvm.Order += 1;
                }
                else
                {
                    tc.Order = this.ModuleFileVM.TestCases.Count + 1;
                }

                this.ModuleFileVM.TestCases.Add(tc);
                this.ModuleFileVM.TestCases = this.ModuleFileVM.TestCases.OrderBy(t => t.Order).ToList<TestCaseViewModel>();
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
                this.TCVM.Radix = this.Radix == "Decimal" ? Common.Radix.Decimal : this.Radix == "Hexadecimal" ? Common.Radix.Hexadecimal : Common.Radix.FloatingPoint;
                this.TCVM.ValidIn = this.ValidIn.Port;
                this.TCVM.ValidOut = this.ValidOut.Port;
                this.TCVM.VldSeq = this.Seq;
            }
        }

        public void CheckIfCanSave()
        {
            OnPropertyChanged("CanSave"); OnPropertyChanged("CanSaveN");
        }
    }
}
