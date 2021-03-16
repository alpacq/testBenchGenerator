using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Common;
using testBenchGenerator.WaveformDesignerAndAnalyzer.Model;

namespace testBenchGenerator.WaveformDesignerAndAnalyzer.ViewModel
{
    public class WaveformAnalyzerViewModel : WaveformProcessorViewModel, IImportable
    {        
        private string problemToolTip;

        private WaveformAnalyzer model;

        public override WaveformProcessor Model
        {
            get { return this.model; }
            set { this.model = (WaveformAnalyzer)value; }
        }

        public string Path
        {
            get { return this.model.Path; }
            set { this.model.Path = value; OnPropertyChanged("Path"); this.CanDosRecompute(); }
        }

        public int LinesIgnore
        {
            get { return this.model.LinesIgnore; }
            set { this.model.LinesIgnore = value; OnPropertyChanged("LinesIgnore"); this.CanDosRecompute(); }
        }

        public double RMSEFs
        {
            get { return this.model.RMSEFs; }
            set { this.model.RMSEFs = value; OnPropertyChanged("RMSEFs"); }
        }

        public double RMSElsbs
        {
            get { return this.model.RMSElsbs; }
            set { this.model.RMSElsbs = value; OnPropertyChanged("RMSElsbs"); }
        }

        public double PRMSE
        {
            get { return this.model.PRMSE; }
            set { this.model.PRMSE = value; OnPropertyChanged("PRMSE"); }
        }

        public double GainE
        {
            get { return this.model.GainE; }
            set { this.model.GainE = value; OnPropertyChanged("GainE"); }
        }

        public double DCReal
        {
            get { return this.model.DCReal; }
            set { this.model.DCReal = value; OnPropertyChanged("DCReal"); }
        }

        public double DCImag
        {
            get { return this.model.DCImag; }
            set { this.model.DCImag = value; OnPropertyChanged("DCImag"); }
        }       

        public string ProblemToolTip
        {
            get { return this.problemToolTip; }
            set { this.problemToolTip = value; OnPropertyChanged("ProblemToolTip"); }
        }

        public bool CanImport
        {
            get
            {
                bool toRet = true;
                string ptt = string.Empty;
                if (this.Type == null || this.Type == String.Empty)
                {
                    ptt += "One of waveform types must be selected.\n";
                    toRet = false;
                }
                else
                {
                    if (this.FFTLength <= 0)
                    {
                        ptt += "FFT length cannot be negative or equal to zero.\n";
                        toRet = false;
                    }
                    if (this.Fs <= 0)
                    {
                        ptt += "Sampling frequency cannot be negative or equal to zero.\n";
                        toRet = false;
                    }
                    if (this.Bitwidth <= 0)
                    {
                        ptt += "Bitwidth cannot be negative or equal to zero.\n";
                        toRet = false;
                    }
                    if(this.LinesIgnore < 0)
                    {
                        ptt += "Lines to ignore cannot be negative.\n";
                        toRet = false;
                    }
                    if (this.Type.Contains("Sine"))
                    {
                        if (this.InputMag <= 0)
                        {
                            ptt += "Input magnitude cannot be negative or equal to zero.\n";
                            toRet = false;
                        }
                    }
                    else if(this.Type.Contains("OFDM"))
                    {
                        if(this.NSymbols <= 0)
                        {
                            ptt += "Number of symbols cannot be negative or equal to zero.\n";
                            toRet = false;
                        }
                    }
                }
                if (ptt.EndsWith("\n"))
                    ptt = ptt.Remove(ptt.Length - 1);
                this.ProblemToolTip = ptt;
                return toRet;
            }
        }

        public bool CanImportN
        {
            get { return !this.CanImport; }
        }        

        public WaveformAnalyzerViewModel(WaveformAnalyzer model)
        {
            this.model = model;
            this.IPoints = new List<DataPoint>();
            this.QPoints = new List<DataPoint>();
            this.FPoints = new List<DataPoint>();
            this.Radixes = new List<string>()
            {
                "Decimal", "Hexadecimal", "Floating Point"
            };
            this.Delimiters = new List<string>()
            {
                "' '","','"
            };
            OnPropertyChanged("Radixes");
            OnPropertyChanged("Delimiters");
            OnPropertyChanged("IPoints");
            OnPropertyChanged("QPoints");
            OnPropertyChanged("FPoints");
        }

        public void Import()
        {
            this.model.ReadFile();

            this.IPoints = new List<DataPoint>();
            this.QPoints = new List<DataPoint>();
            this.FPoints = new List<DataPoint>();
            for (int i = 0; i < this.Length; i++)
            {
                this.IPoints.Add(new DataPoint(i/this.Fs, this.I[i]));
                this.QPoints.Add(new DataPoint(i/this.Fs, this.Q[i]));
            }
            for (int i = 0; i < this.Freqs.Length; i++)
            {
                this.FPoints.Add(new DataPoint((this.Fs * i / this.Freqs.Length), this.Freqs[i]));
            }            
            OnPropertyChanged("IPoints");
            OnPropertyChanged("QPoints");
            OnPropertyChanged("FPoints");
        }

        public void Analyze()
        {
            this.model.AnalyzeSignal();
            OnPropertyChanged("Freq");
            OnPropertyChanged("Phoff");
            OnPropertyChanged("RMS");
            OnPropertyChanged("RMSElsbs");
            OnPropertyChanged("RMSEFs");
            OnPropertyChanged("PRMSE");
            OnPropertyChanged("GainE");
            OnPropertyChanged("DCReal");
            OnPropertyChanged("DCImag");
        }
    }
}
