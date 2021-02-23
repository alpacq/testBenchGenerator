using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using testBenchGenerator.WaveformDesignerAndAnalyzer.Model;
using System.ComponentModel;
using OxyPlot;

namespace testBenchGenerator.WaveformDesignerAndAnalyzer.ViewModel
{
    public class WaveformDesignerViewModel : INotifyPropertyChanged
    {
        private WaveformDesigner model;
        public double[] TimeVector
        {
            get { return this.model.TimeVector; }
            set { this.model.TimeVector = value; OnPropertyChanged("TimeVector"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double[] I
        {
            get { return this.model.I; }
            set { this.model.I = value; OnPropertyChanged("I"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double[] Q
        {
            get { return this.model.Q; }
            set { this.model.Q = value; OnPropertyChanged("Q"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public Complex[] X
        {
            get { return this.model.X; }
            set { this.model.X = value; OnPropertyChanged("X"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double FGain
        {
            get { return this.model.FGain; }
            set { this.model.FGain = value; OnPropertyChanged("FGain"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double[] Freqs
        {
            get { return this.model.Freqs; }
            set { this.model.Freqs = value; OnPropertyChanged("IQOut"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double[] OFDMTimeSym
        {
            get { return this.model.OFDMTimeSym; }
            set { this.model.OFDMTimeSym = value; OnPropertyChanged("OFDMTimeSym"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public string Type
        {
            get { return this.model.Type; }
            set { this.model.Type = value; OnPropertyChanged("Type"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double Fs
        {
            get { return this.model.Fs; }
            set { this.model.Fs = value; OnPropertyChanged("Fs"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double Gain
        {
            get { return this.model.Gain; }
            set { this.model.Gain = value; OnPropertyChanged("Gain"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double Length
        {
            get { return this.model.Length; }
            set { this.model.Length = value; OnPropertyChanged("Length"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double Freq
        {
            get { return this.model.Freq; }
            set { this.model.Freq = value; OnPropertyChanged("Freq"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double Phoff
        {
            get { return this.model.Phoff; }
            set { this.model.Phoff = value; OnPropertyChanged("Phoff"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public string Modulation
        {
            get { return this.model.Modulation; }
            set { this.model.Modulation = value; OnPropertyChanged("Modulation"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double Fsoff
        {
            get { return this.model.Fsoff; }
            set { this.model.Fsoff = value; OnPropertyChanged("Fsoff"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double OS
        {
            get { return this.model.OS; }
            set { this.model.OS = value; OnPropertyChanged("OS"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double OFDMN
        {
            get { return this.model.OFDMN; }
            set { this.model.OFDMN = value; OnPropertyChanged("OFDMN"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double FFTLength
        {
            get { return this.model.FFTLength; }
            set { this.model.FFTLength = value; OnPropertyChanged("FFTLength"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double Distance
        {
            get { return this.model.Distance; }
            set { this.model.Distance = value; OnPropertyChanged("Distance"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double Seed
        {
            get { return this.model.Seed; }
            set { this.model.Seed = value; OnPropertyChanged("Seed"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double CPLength
        {
            get { return this.model.CPLength; }
            set { this.model.CPLength = value; OnPropertyChanged("CPLength"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double NSymbols
        {
            get { return this.model.NSymbols; }
            set { this.model.NSymbols = value; OnPropertyChanged("NSymbols"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public int Bitwidth
        {
            get { return this.model.Bitwidth; }
            set { this.model.Bitwidth = value; OnPropertyChanged("Bitwidth"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public double RMS
        {
            get { return this.model.RMS; }
            set { this.model.RMS = value; OnPropertyChanged("RMS"); OnPropertyChanged("CanDesign"); OnPropertyChanged("CanExport"); }
        }

        public bool CanDesign
        {
            get 
            {
                if (this.Type == null || this.Type == String.Empty)
                {
                    return false;
                }
                else
                {
                    if(this.Type.Contains("Sine") || this.Type.Contains("Ramp") || this.Type.Contains("Const"))
                    {
                        return (this.Fs != 0 && this.Gain != 0 && this.Length != 0);
                    }
                    else if(this.Type.Contains("OFDM"))
                    {
                        return (this.Fs != 0 && this.Gain != 0 && this.Length != 0 && this.FFTLength != 0 && this.CPLength != 0 && this.Distance != 0 && this.Modulation != null && this.Modulation != String.Empty && this.NSymbols != 0 && this.OFDMN != 0 && this.OS != 0);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool CanExport
        {
            get
            {
                return this.CanDesign && this.X != null && !this.X.All(x => x == 0);
            }
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

        public void Export()
        {

        }
    }
}
