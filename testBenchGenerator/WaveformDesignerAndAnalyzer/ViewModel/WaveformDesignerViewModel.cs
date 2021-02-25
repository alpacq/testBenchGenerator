using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using testBenchGenerator.WaveformDesignerAndAnalyzer.Model;
using System.ComponentModel;
using OxyPlot;
using testBenchGenerator.TestbenchGenerator.Model;

namespace testBenchGenerator.WaveformDesignerAndAnalyzer.ViewModel
{
    public class WaveformDesignerViewModel : INotifyPropertyChanged
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

        public double[] TimeVector
        {
            get { return this.model.TimeVector; }
            set { this.model.TimeVector = value; OnPropertyChanged("TimeVector"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double[] I
        {
            get { return this.model.I; }
            set { this.model.I = value; OnPropertyChanged("I"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double[] Q
        {
            get { return this.model.Q; }
            set { this.model.Q = value; OnPropertyChanged("Q"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public Complex[] X
        {
            get { return this.model.X; }
            set { this.model.X = value; OnPropertyChanged("X"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double FGain
        {
            get { return this.model.FGain; }
            set { this.model.FGain = value; OnPropertyChanged("FGain"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double[] Freqs
        {
            get { return this.model.Freqs; }
            set { this.model.Freqs = value; OnPropertyChanged("IQOut"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double[] OFDMTimeSym
        {
            get { return this.model.OFDMTimeSym; }
            set { this.model.OFDMTimeSym = value; OnPropertyChanged("OFDMTimeSym"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public string Type
        {
            get { return this.model.Type; }
            set { this.model.Type = value; OnPropertyChanged("Type"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double Fs
        {
            get { return this.model.Fs; }
            set { this.model.Fs = value; OnPropertyChanged("Fs"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
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

        public double Length
        {
            get { return this.model.Length; }
            set { this.model.Length = value; OnPropertyChanged("Length"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double Freq
        {
            get { return this.model.Freq; }
            set { this.model.Freq = value; OnPropertyChanged("Freq"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double Phoff
        {
            get { return this.model.Phoff; }
            set { this.model.Phoff = value; OnPropertyChanged("Phoff"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public string Modulation
        {
            get { return this.model.Modulation; }
            set { this.model.Modulation = value; OnPropertyChanged("Modulation"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double Fsoff
        {
            get { return this.model.Fsoff; }
            set { this.model.Fsoff = value; OnPropertyChanged("Fsoff"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double OS
        {
            get { return this.model.OS; }
            set { this.model.OS = value; OnPropertyChanged("OS"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double OFDMN
        {
            get { return this.model.OFDMN; }
            set { this.model.OFDMN = value; OnPropertyChanged("OFDMN"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double FFTLength
        {
            get { return this.model.FFTLength; }
            set { this.model.FFTLength = value; OnPropertyChanged("FFTLength"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double Distance
        {
            get { return this.model.Distance; }
            set { this.model.Distance = value; OnPropertyChanged("Distance"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double Seed
        {
            get { return this.model.Seed; }
            set { this.model.Seed = value; OnPropertyChanged("Seed"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double CPLength
        {
            get { return this.model.CPLength; }
            set { this.model.CPLength = value; OnPropertyChanged("CPLength"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double NSymbols
        {
            get { return this.model.NSymbols; }
            set { this.model.NSymbols = value; OnPropertyChanged("NSymbols"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public int Bitwidth
        {
            get { return this.model.Bitwidth; }
            set { this.model.Bitwidth = value; OnPropertyChanged("Bitwidth"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
        }

        public double RMS
        {
            get { return this.model.RMS; }
            set { this.model.RMS = value; OnPropertyChanged("RMS"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanDesignN"); OnPropertyChanged("CanExportN"); }
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
                        if(this.CPLength <= 0)
                        {
                            ptt += "Cyclic prefix length cannot be negative or equal to zero.\n";
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
                            ptt += "OFDM N cannot be negative or equal to zero.\n";
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
            for (int i = 0; i < this.X.Length; i++)
            {
                this.IPoints.Add(new DataPoint(this.TimeVector[i], this.I[i]));
                this.QPoints.Add(new DataPoint(this.TimeVector[i], this.Q[i]));
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
