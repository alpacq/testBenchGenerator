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
    public class WaveformAnalyzerViewModel : ViewModelBase
    {
        private WaveformAnalyzer model;
        private string problemToolTip;
        private List<string> radixes;
        private List<string> delimiters;

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
        public string Path
        {
            get { return this.model.Path; }
            set { this.model.Path = value; OnPropertyChanged("Path"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        }

        public double[] I
        {
            get { return this.model.I; }
            set { this.model.I = value; OnPropertyChanged("I"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        }

        public double[] Q
        {
            get { return this.model.Q; }
            set { this.model.Q = value; OnPropertyChanged("Q"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        }

        public Complex[] X
        {
            get { return this.model.X; }
            set { this.model.X = value; OnPropertyChanged("X"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        }

        public double[] Freqs
        {
            get { return this.model.Freqs; }
            set { this.model.Freqs = value; OnPropertyChanged("IQOut"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        }

        public string Type
        {
            get { return this.model.Type; }
            set { this.model.Type = value; OnPropertyChanged("Type"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        }

        public double Fs
        {
            get { return this.model.Fs; }
            set { this.model.Fs = value; OnPropertyChanged("Fs"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        }

        public int Length
        {
            get { return this.model.Length; }
            set { this.model.Length = value; OnPropertyChanged("Length"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        }

        public double Freq
        {
            get { return this.model.Freq; }
            set { this.model.Freq = value; OnPropertyChanged("Freq"); }
        }

        public double OS
        {
            get { return this.model.OS; }
            set { this.model.OS = value; OnPropertyChanged("OS"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        }

        public int FFTLength
        {
            get { return this.model.FFTLength; }
            set { this.model.FFTLength = value; OnPropertyChanged("FFTLength"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        }

        //todo solve
        //public double NSymbols
        //{
        //    get { return this.model.NSymbols; }
        //    set { this.model.NSymbols = value; OnPropertyChanged("NSymbols"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        //}

        public int Bitwidth
        {
            get { return this.model.Bitwidth; }
            set { this.model.Bitwidth = value; OnPropertyChanged("Bitwidth"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        }

        public double RMS
        {
            get { return this.model.RMS; }
            set { this.model.RMS = value; OnPropertyChanged("RMS"); }
        }

        public double InputMag
        {
            get { return this.model.InputMag; }
            set { this.model.InputMag = value; OnPropertyChanged("InputMag"); }
        }

        public double LinesIgnore
        {
            get { return this.model.LinesIgnore; }
            set { this.model.LinesIgnore = value; OnPropertyChanged("LinesIgnore"); }
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

        public double RMSEc
        {
            get { return this.model.RMSEc; }
            set { this.model.RMSEc = value; OnPropertyChanged("RMSEc"); }
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

        public Radix Radix
        {
            get { return this.model.Radix; }
            set { this.model.Radix = value; OnPropertyChanged("Radix"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        }

        public Delimiter Delimiter
        {
            get { return this.model.Delimiter; }
            set { this.model.Delimiter = value; OnPropertyChanged("Delimiter"); OnPropertyChanged("CanImport"); OnPropertyChanged("CanImportN"); }
        }

        public double NyquistFrequency
        {
            get { return this.Fs / 2; }
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
                        ptt += "Sampling frequency cannot be less or equal to zero.\n";
                        toRet = false;
                    }
                    if (this.Bitwidth <= 0)
                    {
                        ptt += "Bitwidth cannot be negative or equal to zero.\n";
                        toRet = false;
                    }
                }
                if (ptt.EndsWith("\n"))
                    ptt = ptt.Remove(ptt.Length - 2);
                this.ProblemToolTip = ptt;
                return toRet;
            }
        }

        public bool CanImportN
        {
            get { return !this.CanImport; }
        }

        public IList<DataPoint> IPoints { get; private set; }

        public IList<DataPoint> QPoints { get; private set; }

        public IList<DataPoint> FPoints { get; private set; }

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
    }
}
