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
    public class WaveformDesignerViewModel : ViewModelBase
    {
        private WaveformDesigner model;
        private List<string> radixes;
        private List<string> delimiters;
        private string problemDesignToolTip;
        private string problemExportToolTip;

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

        public List<string> Radixes
        {
            get { return this.radixes; }
            set { this.radixes = value; OnPropertyChanged("Radixes"); }
        }

        public List<string> Delimiters
        {
            get { return this.delimiters; }
            set { this.delimiters = value; OnPropertyChanged("Delimiters"); }
        }

        public Signal Signal
        {
            get { return this.model.Signal; }
            set { this.model.Signal = value; OnPropertyChanged("Signal"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double[] TimeVector
        {
            get { return this.Signal.TimeVector; }
            set { this.Signal.TimeVector = value; OnPropertyChanged("TimeVector"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double[] I
        {
            get { return this.Signal.I; }
            set { this.Signal.I = value; OnPropertyChanged("I"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double[] Q
        {
            get { return this.Signal.Q; }
            set { this.Signal.Q = value; OnPropertyChanged("Q"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public Complex[] X
        {
            get { return this.Signal.X; }
            set { this.Signal.X = value; OnPropertyChanged("X"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double[] Freqs
        {
            get { return this.Signal.Freqs; }
            set { this.Signal.Freqs = value; OnPropertyChanged("IQOut"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double Fs
        {
            get { return this.Signal.Fs; }
            set { this.Signal.Fs = value; OnPropertyChanged("Fs"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public Radix Radix
        {
            get { return this.model.Radix; }
            set { this.model.Radix = value; OnPropertyChanged("Radix"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public Delimiter Delimiter
        {
            get { return this.model.Delimiter; }
            set { this.model.Delimiter = value; OnPropertyChanged("Delimiter"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public int Length
        {
            get { return this.Signal.Length; }
            set { this.Signal.Length = value; OnPropertyChanged("Length"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double LengthTime
        {
            get { return this.Signal.LengthTime; }
            set { this.Signal.LengthTime = value; OnPropertyChanged("LengthTime"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double Freq
        {
            get { return this.Signal.Freq; }
            set { this.Signal.Freq = value; OnPropertyChanged("Freq"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double Phoff
        {
            get { return this.Signal.Phoff; }
            set { this.Signal.Phoff = value; OnPropertyChanged("Phoff"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public string Modulation
        {
            get { return this.model.Modulation; }
            set { this.model.Modulation = value; OnPropertyChanged("Modulation"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double Fsoff
        {
            get { return this.Signal.Fsoff; }
            set { this.Signal.Fsoff = value; OnPropertyChanged("Fsoff"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double OS
        {
            get { return this.Signal.OS; }
            set { this.Signal.OS = value; OnPropertyChanged("OS"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double OFDMN
        {
            get { return this.Signal.OFDMN; }
            set { this.Signal.OFDMN = value; OnPropertyChanged("OFDMN"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public int FFTLength
        {
            get { return this.Signal.FFTLength; }
            set { this.Signal.FFTLength = value; OnPropertyChanged("FFTLength"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double Distance
        {
            get { return this.Signal.Distance; }
            set { this.Signal.Distance = value; OnPropertyChanged("Distance"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public int CPLength
        {
            get { return this.Signal.CPLength; }
            set { this.Signal.CPLength = value; OnPropertyChanged("CPLength"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public int NSymbols
        {
            get { return this.Signal.NSymbols; }
            set { this.Signal.NSymbols = value; OnPropertyChanged("NSymbols"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public int Bitwidth
        {
            get { return this.Signal.Bitwidth; }
            set { this.Signal.Bitwidth = value; OnPropertyChanged("Bitwidth"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double RMS
        {
            get { return this.Signal.RMS; }
            set { this.Signal.RMS = value; OnPropertyChanged("RMS"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }
        public string Type
        {
            get { return this.model.Type; }
            set { this.model.Type = value; OnPropertyChanged("Type"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

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
                        ptt += "Sampling frequency cannot be less or equal to zero.\n";
                        toRet = false;
                    }
                    if(this.Length <= 0)
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
                    ptt = ptt.Remove(ptt.Length - 2);
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
                    ptt = ptt.Remove(ptt.Length - 2);
                this.ProblemExportToolTip = ptt;
                return toRet;
            }
        }

        public bool CanExportN
        {
            get { return !this.CanExport; }
        }

        public double NyquistFrequency
        {
            get { return this.Fs / 2; }
        }

        public IList<DataPoint> IPoints { get; private set; }

        public IList<DataPoint> QPoints { get; private set; }

        public IList<DataPoint> FPoints { get; private set; }

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
