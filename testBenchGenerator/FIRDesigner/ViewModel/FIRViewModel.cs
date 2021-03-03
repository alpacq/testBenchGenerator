using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using testBenchGenerator.Common;
using testBenchGenerator.FIRDesigner.Model;

namespace testBenchGenerator.FIRDesigner.ViewModel
{
    public class FIRViewModel : ViewModelBase
    {
        private FIR fir;
        private string problemToolTip;
        private string problemExportToolTip;
        private string plotType;

        public FIR FIR
        {
            get { return this.fir; }
        }

        public double Fs
        {
            get { return this.FIR.Fs; }
            set { this.FIR.Fs = value; OnPropertyChanged("Fs"); OnPropertyChanged("CanUpdateN"); OnPropertyChanged("CanUpdate"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanExportN"); }
        }

        public int Length
        {
            get { return this.FIR.Length; }
            set { this.FIR.Length = value; OnPropertyChanged("Length"); OnPropertyChanged("CanUpdateN"); OnPropertyChanged("CanUpdate"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanExportN"); }
        }

        public int ShiftSamples
        {
            get { return this.FIR.ShiftSamples; }
            set { this.FIR.ShiftSamples = value; OnPropertyChanged("ShiftSamples"); OnPropertyChanged("CanUpdateN"); OnPropertyChanged("CanUpdate"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanExportN"); }
        }

        public double LowFreq
        {
            get { return this.FIR.LowFreq; }
            set { this.FIR.LowFreq = value; OnPropertyChanged("LowFreq"); OnPropertyChanged("CanUpdateN"); OnPropertyChanged("CanUpdate"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanExportN"); }
        }

        public double HighFreq
        {
            get { return this.FIR.HighFreq; }
            set { this.FIR.HighFreq = value; OnPropertyChanged("HighFreq"); OnPropertyChanged("CanUpdateN"); OnPropertyChanged("CanUpdate"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanExportN"); }
        }

        public int FreqSamples
        {
            get { return this.FIR.FreqSamples; }
            set { this.FIR.FreqSamples = value; OnPropertyChanged("FreqSamples"); OnPropertyChanged("CanUpdateN"); OnPropertyChanged("CanUpdate"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanExportN"); }
        }

        public WindowType WinType
        {
            get { return this.FIR.WinType; }
            set { this.FIR.WinType = value; OnPropertyChanged("WinType"); OnPropertyChanged("CanUpdateN"); OnPropertyChanged("CanUpdate"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanExportN"); }
        }

        public FilterType FiltType
        {
            get { return this.FIR.FiltType; }
            set { this.FIR.FiltType = value; OnPropertyChanged("FiltType"); OnPropertyChanged("CanUpdateN"); OnPropertyChanged("CanUpdate"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanExportN"); OnPropertyChanged("IsLFEnabled"); OnPropertyChanged("IsHFEnabled"); }
        }

        public double Ts
        {
            get { return this.FIR.Ts; }
        }

        public List<double> TimeVector
        {
            get { return this.FIR.TimeVector; }
            set { this.FIR.TimeVector = value; OnPropertyChanged("TimeVector"); OnPropertyChanged("LastTime"); }
        }

        public List<double> ImpulseResponse
        {
            get { return this.FIR.ImpulseResponse; }
            set { this.FIR.ImpulseResponse = value; OnPropertyChanged("ImpulseResponse"); }
        }

        public List<double> StepResponse
        {
            get { return this.FIR.StepResponse; }
            set { this.FIR.StepResponse = value; OnPropertyChanged("StepResponse"); }
        }

        public List<double> Window
        {
            get { return this.FIR.Window; }
            set { this.FIR.Window = value; OnPropertyChanged("Window"); }
        }

        public List<double> WindowedImpulseResponse
        {
            get { return this.FIR.WindowedImpulseResponse; }
            set { this.FIR.WindowedImpulseResponse = value; OnPropertyChanged("WindowedImpulseResponse"); OnPropertyChanged("Coeffs"); OnPropertyChanged("CanUpdateN"); OnPropertyChanged("CanUpdate"); OnPropertyChanged("CanExport"); OnPropertyChanged("CanExportN"); }
        }

        public List<double> WindowedStepResponse
        {
            get { return this.FIR.WindowedStepResponse; }
            set { this.FIR.WindowedStepResponse = value; OnPropertyChanged("WindowedStepResponse"); }
        }

        public List<double> FrequencyVectorHz
        {
            get { return this.FIR.FrequencyVectorHz; }
            set { this.FIR.FrequencyVectorHz = value; OnPropertyChanged("FrequencyVectorHz"); OnPropertyChanged("NyquistFrequency"); }
        }

        public List<double> ImpRespMag
        {
            get { return this.FIR.ImpRespMag; }
            set { this.FIR.ImpRespMag = value; OnPropertyChanged("ImpRespMag"); }
        }

        public List<double> WinRespMag
        {
            get { return this.FIR.WinRespMag; }
            set { this.FIR.WinRespMag = value; OnPropertyChanged("WinRespMag"); }
        }

        public List<double> WinMag
        {
            get { return this.FIR.WinMag; }
            set { this.FIR.WinMag = value; OnPropertyChanged("WinMag"); }
        }

        public string PlotType
        {
            get { return this.plotType; }
            set { this.plotType = value; OnPropertyChanged("PlotType"); }
        }

        public IList<DataPoint> WinRespPoints { get; private set; }
        public IList<DataPoint> WinChartPoints { get; private set; }

        public IList<DataPoint> FIRRespPoints { get; private set; }

        public IList<DataPoint> FIRChartPoints { get; private set; }

        public string Coeffs
        {
            get 
            {
                string coeffs = String.Empty;
                foreach (double coeff in this.WindowedImpulseResponse) coeffs += (coeff.ToString("f8") + ", ");
                if(coeffs.Length > 2)
                    coeffs = coeffs.Remove(coeffs.Length - 2);
                return coeffs;
            }
        }

        public bool CanUpdate
        {
            get
            {
                bool toRet = true;
                string ptt = String.Empty;
                if(this.Length < 1)
                {
                    ptt += "Filter length must be greater than zero.\n";
                    toRet = false;
                }
                if (this.Ts < 0.0)
                {
                    ptt += "Sampling frequency cannot be negative.\n";
                    toRet = false;
                }

                if (this.LowFreq >= 0.5 / this.Ts || this.HighFreq >= 0.5 / this.Ts)
                {
                    ptt += "Cut-off frequency has to be less than the Nyquist frequency (i.e. sampling frequency / 2).\n";
                    toRet = false;
                }

                if (this.Length < 0 || this.ShiftSamples < 0)
                {
                    ptt += "Total number of samples and sample shift number both need to be integers, greater than zero.";
                    toRet = false;
                }
                if (ptt.EndsWith("\n"))
                    ptt = ptt.Remove(ptt.Length - 2);
                this.ProblemToolTip = ptt;
                return toRet;
            }
        }

        public bool CanUpdateN
        {
            get { return !this.CanUpdate; }
        }

        public bool CanExport
        {
            get 
            {
                bool toRet = true;
                string ptt = String.Empty;
                ptt += this.ProblemToolTip;
                if (ptt != null && ptt != String.Empty)
                {
                    ptt += "\n";
                    toRet = false;
                }
                if(this.WindowedImpulseResponse == null || this.WindowedImpulseResponse.Count == 0)
                {
                    ptt += "Coefficients are not yet computed.\n";
                    toRet = false;
                }
                if(this.WindowedImpulseResponse.All(v => v == 0))
                {
                    ptt += "All coefficients are equal to zero.\n";
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

        public bool IsLFEnabled
        {
            get { return this.FiltType != FilterType.LowPass; }
        }

        public bool IsHFEnabled
        {
            get { return this.FiltType != FilterType.HighPass; }
        }

        public string ProblemToolTip
        {
            get { return this.problemToolTip; }
            set { this.problemToolTip = value; OnPropertyChanged("ProblemToolTip"); }
        }

        public string ProblemExportToolTip
        {
            get { return this.problemExportToolTip; }
            set { this.problemExportToolTip = value; OnPropertyChanged("ProblemExportToolTip"); }
        }

        public double NyquistFrequency
        {
            get { return this.FrequencyVectorHz != null ? this.FrequencyVectorHz.LastOrDefault() : 0.0; }
        }

        public double LastTime
        {
            get { return this.TimeVector != null ? this.TimeVector.LastOrDefault() : 0.0; }
        }

        public FIRViewModel(FIR fir)
        {
            this.fir = fir;
            this.WinRespPoints = new List<DataPoint>();
            this.WinChartPoints = new List<DataPoint>();
            this.FIRRespPoints = new List<DataPoint>();
            this.FIRChartPoints = new List<DataPoint>();
            OnPropertyChanged("WinChartPoints");
            OnPropertyChanged("WinRespPoints");
            OnPropertyChanged("FIRRespPoints");
            OnPropertyChanged("FIRChartPoints");
            OnPropertyChanged("WinRespPoints");
            OnPropertyChanged("PlotType");
            OnPropertyChanged("CanUpdateN"); 
            OnPropertyChanged("CanUpdate");
        }

        public void Update()
        {
            if(this.CanUpdate)
                this.FIR.Update();
            this.WinRespPoints = new List<DataPoint>();
            this.WinChartPoints = new List<DataPoint>();
            this.FIRRespPoints = new List<DataPoint>();
            this.FIRChartPoints = new List<DataPoint>();
            for (int i = 0; i < this.Window.Count; i++)
                this.WinRespPoints.Add(new DataPoint(this.TimeVector[i], this.Window[i]));
            for (int i = 0; i < this.WinMag.Count; i++)
                this.WinChartPoints.Add(new DataPoint(this.FrequencyVectorHz[i], this.WinMag[i]));
            for (int i = 0; i < this.WinRespMag.Count; i++)
                this.FIRChartPoints.Add(new DataPoint(this.FrequencyVectorHz[i], this.WinRespMag[i]));
            if (this.PlotType.Contains("Impulse"))
                for (int i = 0; i < this.WindowedImpulseResponse.Count; i++)
                    this.FIRRespPoints.Add(new DataPoint(this.TimeVector[i], this.WindowedImpulseResponse[i]));
            else
                for (int i = 0; i < this.WindowedStepResponse.Count; i++)
                    this.FIRRespPoints.Add(new DataPoint(this.TimeVector[i], this.WindowedStepResponse[i]));
            OnPropertyChanged("WinChartPoints");
            OnPropertyChanged("WinRespPoints");
            OnPropertyChanged("FIRRespPoints");
            OnPropertyChanged("FIRChartPoints");
            OnPropertyChanged("WinRespPoints");
            OnPropertyChanged("Coeffs");
            OnPropertyChanged("CanExport"); OnPropertyChanged("CanExportN");
        }

        public void UpdateWindow()
        {
            if (this.Fs > 0 && this.Length > 0)
            {
                this.FIR.UpdateWindow();
                this.WinRespPoints = new List<DataPoint>();
                this.WinChartPoints = new List<DataPoint>();
                for (int i = 0; i < this.Window.Count; i++)
                    this.WinRespPoints.Add(new DataPoint(this.TimeVector[i], this.Window[i]));
                for (int i = 0; i < this.WinMag.Count; i++)
                    this.WinChartPoints.Add(new DataPoint(this.FrequencyVectorHz[i], this.WinMag[i]));
                OnPropertyChanged("WinChartPoints");
                OnPropertyChanged("WinRespPoints");
            }
        }
    }
}
