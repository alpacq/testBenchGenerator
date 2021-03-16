using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using testBenchGenerator.WaveformDesignerAndAnalyzer.Model;
using System.ComponentModel;
using OxyPlot;
using testBenchGenerator.Common;

namespace testBenchGenerator.WaveformDesignerAndAnalyzer.ViewModel
{
    public class WaveformDesignerViewModel : WaveformProcessorViewModel, IDesignable, IExportable
    {
        private WaveformDesigner model;

        private string problemDesignToolTip;
        private string problemExportToolTip;

        public override WaveformProcessor Model
        {
            get { return this.model; }
            set { this.model = (WaveformDesigner)value; }
        }

        public string ProblemDesignToolTip
        {
            get { return this.problemDesignToolTip; }
            set { this.problemDesignToolTip = value; OnPropertyChanged("ProblemDesignToolTip"); }
        }

        public string ProblemExportToolTip
        {
            get { return this.problemExportToolTip; }
            set { this.problemExportToolTip = value; OnPropertyChanged("ProblemExportToolTip"); }
        }        

        public string Modulation
        {
            get { return (this.Signal is OFDMSignal) ? (this.Signal as OFDMSignal).Modulation : null; }
            set 
            {
                if (this.Signal is OFDMSignal)
                {
                    (this.Signal as OFDMSignal).Modulation = value; 
                    OnPropertyChanged("Modulation");
                    this.CanDosRecompute();
                }
            }
        }

        public double Fsoff
        {
            get { return (this.Signal is OFDMSignal) ? (this.Signal as OFDMSignal).Fsoff : 0.0; }
            set
            {
                if (this.Signal is OFDMSignal)
                {
                    (this.Signal as OFDMSignal).Fsoff = value;
                    OnPropertyChanged("Fsoff");
                    this.CanDosRecompute();
                }
            }
        }

        public int OFDMN
        {
            get { return (this.Signal is OFDMSignal) ? (this.Signal as OFDMSignal).OFDMN : 0; }
            set
            {
                if (this.Signal is OFDMSignal)
                {
                    (this.Signal as OFDMSignal).OFDMN = value;
                    OnPropertyChanged("OFDMN");
                    this.CanDosRecompute();
                }
            }
        }

        public double Distance
        {
            get { return (this.Signal is OFDMSignal) ? (this.Signal as OFDMSignal).Distance : 0.0; }
            set
            {
                if (this.Signal is OFDMSignal)
                {
                    (this.Signal as OFDMSignal).Distance = value;
                    OnPropertyChanged("Distance");
                    this.CanDosRecompute();
                }
            }
        }

        public int CPLength
        {
            get { return (this.Signal is OFDMSignal) ? (this.Signal as OFDMSignal).CPLength : 0; }
            set
            {
                if (this.Signal is OFDMSignal)
                {
                    (this.Signal as OFDMSignal).CPLength = value;
                    OnPropertyChanged("CPLength");
                    this.CanDosRecompute();
                }
            }
        }

        //public bool IsRand
        //{
        //    get { return (this.Signal is OFDMSignal) ? (this.Signal as OFDMSignal).IsRand : false; }
        //    set
        //    {
        //        if(this.Signal is OFDMSignal)
        //        {
        //            (this.Signal as OFDMSignal).IsRand = value;
        //            OnPropertyChanged("IsRand");
        //            this.CanDosRecompute();
        //        }
        //    }
        //}

        //public double Seed
        //{
        //    get { return (this.Signal is OFDMSignal) ? (this.Signal as OFDMSignal).Seed : 0.0; }
        //    set
        //    {
        //        if(this.Signal is OFDMSignal)
        //        {
        //            (this.Signal as OFDMSignal).Seed = value;
        //            OnPropertyChanged("Seed");
        //            this.CanDosRecompute();
        //        }
        //    }
        //}

        public bool CanDesign
        {
            get 
            {
                bool toRet = true;
                string ptt = String.Empty;
                if (this.Type == null || this.Type == String.Empty)
                {
                    ptt += "One of waveform types must be selected.\n";
                    toRet = false;
                }
                else
                {
                    if(this.Fs <= 0)
                    {
                        ptt += "Sampling frequency cannot be negative or equal to zero.\n";
                        toRet = false;
                    }
                    if(this.LengthTime <= 0)
                    {
                        ptt += "Signal length cannot be negative or equal to zero.\n";
                        toRet = false;
                    }
                    if(this.Bitwidth <= 0)
                    {
                        ptt += "Bitwidth cannot be negative or equal to zero.\n";
                        toRet = false;
                    }
                    if(this.Type.Contains("OFDM"))
                    {
                        if(this.FFTLength <= 0)
                        {
                            ptt += "FFT length cannot be negative or equal to zero.\n";
                            toRet = false;
                        }
                        if(this.CPLength < 0)
                        {
                            ptt += "Cyclic prefix length cannot be negative.\n";
                            toRet = false;
                        }
                        if(this.Distance <= 0)
                        {
                            ptt += "Distance cannot be negative or equal to zero.\n";
                            toRet = false;
                        }
                        if (this.Modulation == null || this.Modulation == String.Empty)
                        {
                            ptt += "One of modulation types must be selected.\n";
                            toRet = false;
                        }
                        if(this.NSymbols <= 0)
                        {
                            ptt += "Number of symbols cannot be negative or equal to zero.\n";
                            toRet = false;
                        }
                        if(this.OFDMN <= 0)
                        {
                            ptt += "Number of OFDM carriers cannot be negative or equal to zero.\n";
                            toRet = false;
                        }
                        if(this.OS <= 0)
                        {
                            ptt += "Oversampling value cannot be negative or equal to zero.\n";
                            toRet = false;
                        }
                    }
                }
                if (ptt.EndsWith("\n"))
                    ptt = ptt.Remove(ptt.Length - 1);
                this.ProblemDesignToolTip = ptt;
                return toRet;
            }
        }

        public bool CanDesignN
        {
            get { return !this.CanDesign; }
        }

        public bool CanExport
        {
            get
            {
                bool toRet = true;
                string ptt = String.Empty;
                ptt += this.ProblemDesignToolTip;
                if (ptt != null && ptt != String.Empty)
                {
                    ptt += "\n";
                    toRet = false;
                }
                if (this.X == null || this.X.Length == 0)
                {
                    ptt += "Waveform is not yet generated.\n";
                    toRet = false;
                }
                if(this.X != null && this.X.All(x => x == 0))
                {
                    ptt += "Waveform is constant zero signal.\n";
                    toRet = false;
                }
                if (ptt.EndsWith("\n"))
                    ptt = ptt.Remove(ptt.Length - 1);
                this.ProblemExportToolTip = ptt;
                return toRet;
            }
        }

        public bool CanExportN
        {
            get { return !this.CanExport; }
        }

        public WaveformDesignerViewModel(WaveformDesigner model)
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
            OnPropertyChanged("CanDesign");
            OnPropertyChanged("CanExport");
        }

        public void Design()
        {
            this.model.GenerateWaveform();
            this.IPoints = new List<DataPoint>();
            this.QPoints = new List<DataPoint>();
            this.FPoints = new List<DataPoint>();
            if (this.Type.Contains("OFDM"))
            {
                for (int i = 0; i < this.I.Length; i++)
                {
                    this.IPoints.Add(new DataPoint(this.Fs * i, this.I[i]));
                    this.QPoints.Add(new DataPoint(this.Fs * i, this.Q[i]));
                }
            }
            else
            {
                for (int i = 0; i < this.X.Length; i++)
                {
                    this.IPoints.Add(new DataPoint(this.TimeVector[i], this.I[i]));
                    this.QPoints.Add(new DataPoint(this.TimeVector[i], this.Q[i]));
                }
            }
            for(int i = 0; i < this.Freqs.Length; i++)
            {
                this.FPoints.Add(new DataPoint((this.Fs * i/this.Freqs.Length), this.Freqs[i]));
            }
            OnPropertyChanged("InputMag");
            OnPropertyChanged("IPoints");
            OnPropertyChanged("QPoints");
            OnPropertyChanged("FPoints");
            OnPropertyChanged("CanDesign");
            OnPropertyChanged("CanExport");
        }

        public void Export(string path)
        {
            this.model.CreateAndSaveFile(path);
        }
    }
}
