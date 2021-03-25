using OxyPlot;
using System.Collections.Generic;
using System.Numerics;
using FPGADeveloperTools.Common;
using FPGADeveloperTools.WaveformDesignerAndAnalyzer.Model;
using FPGADeveloperTools.Common.Model.Signals;
using FPGADeveloperTools.Common.ViewModel;

namespace FPGADeveloperTools.WaveformDesignerAndAnalyzer.ViewModel
{
    public abstract class WaveformProcessorViewModel : ViewModelBase
    {        
        private List<string> radixes;
        private List<string> delimiters;

        public List<string> Radixes
        {
            get { return this.radixes; }
            set { this.radixes = value; OnPropertyChanged("Radixes"); this.CanDosRecompute(); }
        }

        public abstract WaveformProcessor Model
        {
            get; set;
        }

        public double[] I
        {
            get { return this.Model.I; }
            set { this.Model.I = value; OnPropertyChanged("I"); this.CanDosRecompute(); }
        }

        public double[] Q
        {
            get { return this.Model.Q; }
            set { this.Model.Q = value; OnPropertyChanged("Q"); this.CanDosRecompute(); }
        }

        public Complex[] X
        {
            get { return this.Model.X; }
            set { this.Model.X = value; OnPropertyChanged("X"); this.CanDosRecompute(); }
        }

        public double[] Freqs
        {
            get { return this.Model.Freqs; }
            set { this.Model.Freqs = value; OnPropertyChanged("IQOut"); this.CanDosRecompute(); }
        }

        public string Type
        {
            get { return this.Model.Type; }
            set 
            { 
                this.Model.Type = value; 
                OnPropertyChanged("Type"); 
                if(value.Contains("Sine"))
                {
                    SineSignal sine = new SineSignal(this.Fs, this.Length, this.LengthTime, this.OS, this.FFTLength, this.Bitwidth, this.RMS, this.Phoff, this.Freq);
                    this.Signal = sine;

                }
                else if(value.Contains("OFDM"))
                {
                    OFDMSignal ofdm = new OFDMSignal(this.Fs, this.Length, this.LengthTime, this.OS, this.FFTLength, this.Bitwidth, this.RMS, this.Phoff);
                    this.Signal = ofdm;
                }
                else if(value.Contains("Ramp"))
                {
                    RampSignal ramp = new RampSignal(this.Fs, this.Length, this.LengthTime, this.OS, this.FFTLength, this.Bitwidth, this.RMS);
                    this.Signal = ramp;
                }
                else if(value.Contains("Const"))
                {
                    ConstSignal cst = new ConstSignal(this.Fs, this.Length, this.LengthTime, this.OS, this.FFTLength, this.Bitwidth, this.RMS);
                    this.Signal = cst;
                }
                else if(value.Contains("Square"))
                {
                    SquareSignal square = new SquareSignal(this.Fs, this.Length, this.LengthTime, this.OS, this.FFTLength, this.Bitwidth, this.RMS, this.Phoff, this.Freq);
                    this.Signal = square;
                }
                else if (value.Contains("Triangle"))
                {
                    TriangleSignal triangle = new TriangleSignal(this.Fs, this.Length, this.LengthTime, this.OS, this.FFTLength, this.Bitwidth, this.RMS, this.Phoff, this.Freq);
                    this.Signal = triangle;
                }
                else if (value.Contains("Saw"))
                {
                    SawSignal saw = new SawSignal(this.Fs, this.Length, this.LengthTime, this.OS, this.FFTLength, this.Bitwidth, this.RMS, this.Phoff, this.Freq);
                    this.Signal = saw;
                }
                else if (value.Contains("White"))
                {
                    WhiteNoiseSignal wn = new WhiteNoiseSignal(this.Fs, this.Length, this.LengthTime, this.OS, this.FFTLength, this.Bitwidth, this.RMS);
                    this.Signal = wn;
                }
                this.CanDosRecompute(); 
            }
        }

        public double Fs
        {
            get { return this.Model.Fs; }
            set { this.Model.Fs = value; OnPropertyChanged("Fs"); this.CanDosRecompute(); }
        }

        public int Length
        {
            get { return this.Model.Length; }
            set { this.Model.Length = value; OnPropertyChanged("Length"); this.CanDosRecompute(); }
        }

        public double LengthTime
        {
            get { return this.Model.LengthTime; }
            set { this.Model.LengthTime = value; OnPropertyChanged("LengthTime"); this.CanDosRecompute(); }
        }

        public double Freq
        {
            get { return this.Model.Freq; }
            set { this.Model.Freq = value; OnPropertyChanged("Freq"); this.CanDosRecompute(); }
        }

        public int OS
        {
            get { return this.Model.OS; }
            set { this.Model.OS = value; OnPropertyChanged("OS"); this.CanDosRecompute(); }
        }

        public int FFTLength
        {
            get { return this.Model.FFTLength; }
            set { this.Model.FFTLength = value; OnPropertyChanged("FFTLength"); this.CanDosRecompute(); }
        }

        public int NSymbols
        {
            get { return this.Model.NSymbols; }
            set { this.Model.NSymbols = value; OnPropertyChanged("NSymbols"); this.CanDosRecompute(); }
        }

        public int Bitwidth
        {
            get { return this.Model.Bitwidth; }
            set { this.Model.Bitwidth = value; OnPropertyChanged("Bitwidth"); this.CanDosRecompute(); }
        }

        public double Phoff
        {
            get { return this.Model.Phoff; }
            set { this.Model.Phoff = value; OnPropertyChanged("Phoff"); this.CanDosRecompute(); }
        }

        public double RMS
        {
            get { return this.Model.RMS; }
            set { this.Model.RMS = value; OnPropertyChanged("RMS"); this.CanDosRecompute(); }
        }

        public List<string> Delimiters
        {
            get { return this.delimiters; }
            set { this.delimiters = value; OnPropertyChanged("Delimiters"); this.CanDosRecompute(); }
        }

        public Radix Radix
        {
            get { return this.Model.Radix; }
            set { this.Model.Radix = value; OnPropertyChanged("Radix"); this.CanDosRecompute(); }
        }

        public Delimiter Delimiter
        {
            get { return this.Model.Delimiter; }
            set { this.Model.Delimiter = value; OnPropertyChanged("Delimiter"); this.CanDosRecompute(); }
        }

        public double NyquistFrequency
        {
            get { return this.Fs / 2; }
        }

        public Signal Signal
        {
            get { return this.Model.Signal; }
            set { this.Model.Signal = value; OnPropertyChanged("Signal"); this.CanDosRecompute(); }
        }

        public double[] TimeVector
        {
            get { return this.Signal.TimeVector; }
            set { this.Signal.TimeVector = value; OnPropertyChanged("TimeVector"); this.CanDosRecompute(); }
        }

        public double InputMag
        {
            get { return this.Model.InputMag; }
            set { this.Model.InputMag = value; OnPropertyChanged("InputMag"); this.CanDosRecompute(); }
        }

        protected void CanDosRecompute()
        {
            OnPropertyChanged("CanImport");
            OnPropertyChanged("CanImportN");
            OnPropertyChanged("CanDesign");
            OnPropertyChanged("CanExport");
            OnPropertyChanged("CanDesignN");
            OnPropertyChanged("CanExportN");
        }

        public IList<DataPoint> IPoints { get; protected set; }

        public IList<DataPoint> QPoints { get; protected set; }

        public IList<DataPoint> FPoints { get; protected set; }
    }
}
