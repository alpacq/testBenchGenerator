using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.WaveformDesignerAndAnalyzer.Model;

namespace testBenchGenerator.WaveformDesignerAndAnalyzer.ViewModel
{
    public class WaveformAnalyzerViewModel : INotifyPropertyChanged
    {
        private WaveformAnalyzer model;

        public string Path
        {
            get { return this.model.Path; }
            set { this.model.Path = value; OnPropertyChanged("Path"); }
        }

        public double[] I
        {
            get { return this.model.I; }
            set { this.model.I = value; OnPropertyChanged("I"); }
        }

        public double[] Q
        {
            get { return this.model.Q; }
            set { this.model.Q = value; OnPropertyChanged("Q"); }
        }

        public Complex[] X
        {
            get { return this.model.X; }
            set { this.model.X = value; OnPropertyChanged("X"); }
        }

        public double[] Freqs
        {
            get { return this.model.Freqs; }
            set { this.model.Freqs = value; OnPropertyChanged("IQOut"); }
        }

        public double[] OFDMTimeSym
        {
            get { return this.model.OFDMTimeSym; }
            set { this.model.OFDMTimeSym = value; OnPropertyChanged("OFDMTimeSym"); }
        }

        public string Type
        {
            get { return this.model.Type; }
            set { this.model.Type = value; OnPropertyChanged("Type"); }
        }

        public double Fs
        {
            get { return this.model.Fs; }
            set { this.model.Fs = value; OnPropertyChanged("Fs"); }
        }

        public double Gain
        {
            get { return this.model.Gain; }
            set { this.model.Gain = value; OnPropertyChanged("Gain"); }
        }

        public int Length
        {
            get { return this.model.Length; }
            set { this.model.Length = value; OnPropertyChanged("Length"); }
        }

        public double Freq
        {
            get { return this.model.Freq; }
            set { this.model.Freq = value; OnPropertyChanged("Freq"); }
        }

        public double Phoff
        {
            get { return this.model.Phoff; }
            set { this.model.Phoff = value; OnPropertyChanged("Phoff"); }
        }

        public string Modulation
        {
            get { return this.model.Modulation; }
            set { this.model.Modulation = value; OnPropertyChanged("Modulation"); }
        }

        public double Fsoff
        {
            get { return this.model.Fsoff; }
            set { this.model.Fsoff = value; OnPropertyChanged("Fsoff"); }
        }

        public double OS
        {
            get { return this.model.OS; }
            set { this.model.OS = value; OnPropertyChanged("OS"); }
        }

        public double OFDMN
        {
            get { return this.model.OFDMN; }
            set { this.model.OFDMN = value; OnPropertyChanged("OFDMN"); }
        }

        public double FFTLength
        {
            get { return this.model.FFTLength; }
            set { this.model.FFTLength = value; OnPropertyChanged("FFTLength"); }
        }

        public double Distance
        {
            get { return this.model.Distance; }
            set { this.model.Distance = value; OnPropertyChanged("Distance"); }
        }

        public double Seed
        {
            get { return this.model.Seed; }
            set { this.model.Seed = value; OnPropertyChanged("Seed"); }
        }

        public double CPLength
        {
            get { return this.model.CPLength; }
            set { this.model.CPLength = value; OnPropertyChanged("CPLength"); }
        }

        public double NSymbols
        {
            get { return this.model.NSymbols; }
            set { this.model.NSymbols = value; OnPropertyChanged("NSymbols"); }
        }

        public int Bitwidth
        {
            get { return this.model.Bitwidth; }
            set { this.model.Bitwidth = value; OnPropertyChanged("Bitwidth"); }
        }

        public double RMS
        {
            get { return this.model.RMS; }
            set { this.model.RMS = value; OnPropertyChanged("RMS"); }
        }

        public double NyquistFrequency
        {
            get { return this.Fs / 2; }
        }

        public IList<DataPoint> IPoints { get; private set; }

        public IList<DataPoint> QPoints { get; private set; }

        public IList<DataPoint> FPoints { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public WaveformAnalyzerViewModel(WaveformAnalyzer model)
        {
            this.model = model;
            this.IPoints = new List<DataPoint>();
            this.QPoints = new List<DataPoint>();
            this.FPoints = new List<DataPoint>();
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
                this.FPoints.Add(new DataPoint((this.NyquistFrequency * i / this.Freqs.Length), this.Freqs[i]));
            }
            OnPropertyChanged("IPoints");
            OnPropertyChanged("QPoints");
            OnPropertyChanged("FPoints");
        }

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
