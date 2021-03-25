using System;
using System.Collections.Generic;
using FPGADeveloperTools.Common;
using FPGADeveloperTools.Common.Model.ModuleFiles;
using FPGADeveloperTools.Common.ViewModel;
using FPGADeveloperTools.Common.ViewModel.ModuleFiles;
using FPGADeveloperTools.Common.ViewModel.Ports;
using FPGADeveloperTools.Common.ViewModel.TestCases;
using FPGADeveloperTools.TestbenchGenerator.Model;

namespace FPGADeveloperTools.TestbenchGenerator.ViewModel
{
    public class GeneratorViewModel : ViewModelBase, IGeneratable
    {
        private ModuleFileViewModel moduleFile;
        private TestCaseViewModel selectedTC;
        private string problemAddToolTip;
        private string problemRemoveToolTip;
        private string problemGenerateToolTip;

        public string ProblemAddToolTip
        {
            get { return this.problemAddToolTip; }
            set { this.problemAddToolTip = value; OnPropertyChanged("ProblemAddToolTip"); }
        }

        public string ProblemRemoveToolTip
        {
            get { return this.problemRemoveToolTip; }
            set { this.problemRemoveToolTip = value; OnPropertyChanged("ProblemRemoveToolTip"); }
        }

        public string ProblemGenerateToolTip
        {
            get { return this.problemGenerateToolTip; }
            set { this.problemGenerateToolTip = value; OnPropertyChanged("ProblemGenerateToolTip"); }
        }

        public ModuleFileViewModel ModuleFile
        {
            get { return this.moduleFile; }
            set { this.moduleFile = value; this.OnPropertyChanged("CanAdd"); this.OnPropertyChanged("CanAddN"); this.OnPropertyChanged("CanGenerate"); this.OnPropertyChanged("CanGenerateN"); }
        }

        public List<PortViewModel> Inputs
        {
            get { if (this.ModuleFile != null) return this.ModuleFile.Ins; else return null; }
            set { this.ModuleFile.Ins = value; this.OnPropertyChanged("Inputs"); }
        }

        public List<PortViewModel> Outputs
        {
            get { if (this.ModuleFile != null) return this.ModuleFile.Outputs; else return null; }
            set { this.ModuleFile.Outputs = value; this.OnPropertyChanged("Outputs"); }
        }

        public List<ParameterViewModel> Parameters
        {
            get { if (this.ModuleFile != null) return this.ModuleFile.Parameters; else return null; }
            set { this.ModuleFile.Parameters = value; this.OnPropertyChanged("Parameters"); }
        }

        public List<ClockViewModel> Clocks
        {
            get { if (this.ModuleFile != null) return this.ModuleFile.Clocks; else return null; }
            set { this.ModuleFile.Clocks = value; this.OnPropertyChanged("Clocks"); }
        }

        public List<ResetViewModel> Resets
        {
            get { if (this.ModuleFile != null) return this.ModuleFile.Resets; else return null; }
            set { this.ModuleFile.Resets = value; this.OnPropertyChanged("Resets"); }
        }

        public List<TestCaseViewModel> TestCases
        {
            get { if (this.ModuleFile != null) return this.ModuleFile.TestCases; else return null; }
            set { this.ModuleFile.TestCases = value; this.OnPropertyChanged("TestCases"); this.OnPropertyChanged("CanRemove"); this.OnPropertyChanged("CanRemoveN"); }
        }

        public TestCaseViewModel SelectedTestCase
        {
            get { return this.selectedTC; }
            set { this.selectedTC = value; this.OnPropertyChanged("SelectedTestCase"); this.OnPropertyChanged("CanRemove"); this.OnPropertyChanged("CanRemoveN"); }
        }

        public bool CanAdd
        {
            get 
            { 
                if(this.ModuleFile == null)
                {
                    this.ProblemAddToolTip = "No module under test source file loaded.";
                    return false;
                }
                this.ProblemAddToolTip = string.Empty;
                return true;
            }
        }

        public bool CanAddN
        {
            get { return !this.CanAdd; }
        }

        public bool CanRemove
        {
            get 
            {
                string ptt = string.Empty;
                bool toRet = true;
                if(this.TestCases == null || this.TestCases.Count == 0)
                {
                    ptt += "There are no testcases added.\n";
                    toRet = false;
                }
                if(this.SelectedTestCase == null)
                {
                    ptt += "No testcase is selected.\n";
                    toRet = false;
                }
                if (ptt.EndsWith("\n"))
                    ptt = ptt.Remove(ptt.Length - 1);
                this.ProblemRemoveToolTip = ptt;
                return toRet;
            }
        }

        public bool CanRemoveN
        {
            get { return !this.CanRemove; }
        }

        public bool CanGenerate
        {
            get
            {
                if (this.ModuleFile == null)
                {
                    this.ProblemGenerateToolTip = "No module under test source file loaded.";
                    return false;
                }
                this.ProblemGenerateToolTip = string.Empty;
                return true;
            }
        }

        public bool CanGenerateN
        {
            get { return !this.CanGenerate; }
        }

        public GeneratorViewModel(string modulePath)
        {            
            if (modulePath != null && modulePath != String.Empty)
            {
                if (modulePath.EndsWith(".sv") || modulePath.EndsWith(".v"))
                    this.ModuleFile = new VerilogModuleFileViewModel(new VerilogModuleFile(modulePath));
                else if (modulePath.EndsWith(".vhd"))
                    this.ModuleFile = new VHDLModuleFileViewModel(new VHDLModuleFile(modulePath));
                this.OnPropertyChanged("Clocks");
                this.OnPropertyChanged("Resets");
                this.OnPropertyChanged("Inputs");
                this.OnPropertyChanged("Outputs");
                this.OnPropertyChanged("Parameters");
            }
            this.OnPropertyChanged("CanAdd"); this.OnPropertyChanged("CanAddN");
            this.OnPropertyChanged("CanRemove"); this.OnPropertyChanged("CanRemoveN");
            this.OnPropertyChanged("CanGenerate"); this.OnPropertyChanged("CanGenerateN");
        }

        public bool Generate()
        {
            string tbFilePath = String.Empty;
            if (this.ModuleFile is VerilogModuleFileViewModel)
                tbFilePath = this.ModuleFile.Path.Replace(".sv", "_tb.sv").Replace(".v", "_tb.sv");
            else
                tbFilePath = this.ModuleFile.Path.Replace(".vhd", "_tb.sv");

            FileGen file = new FileGen(this.ModuleFile.ModuleFile, tbFilePath);

            return file.GenerateFile();
        }
    }
}
