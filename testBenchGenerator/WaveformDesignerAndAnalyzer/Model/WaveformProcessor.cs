using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Common;

namespace testBenchGenerator.WaveformDesignerAndAnalyzer.Model
{
    public abstract class WaveformProcessor
    {
        #region variables and fields
        private Radix radix;
        private Delimiter delimiter;
        private Signal signal;
        private string type;
        private double inputMag;

        public Signal Signal
        {
            get { return this.signal; }
            set { this.signal = value; }
        }

        public double[] Freqs
        {
            get { return this.Signal.Freqs; }
            set { this.Signal.Freqs = value; }
        }

        public Radix Radix
        {
            get { return this.radix; }
            set { this.radix = value; }
        }

        public Delimiter Delimiter
        {
            get { return this.delimiter; }
            set { this.delimiter = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public double Fs
        {
            get { return this.Signal.Fs; }
            set { this.Signal.Fs = value; }
        }

        public double Freq
        {
            get { return (this.Signal is FreqSignal) ? (this.Signal as FreqSignal).Freq : 0.0; }
            set
            {
                if (this.Signal is FreqSignal)
                {
                    (this.Signal as FreqSignal).Freq = value;
                }
            }
        }

        public int OS
        {
            get { return this.Signal.OS; }
            set { this.Signal.OS = value; }
        }

        public int FFTLength
        {
            get { return this.Signal.FFTLength; }
            set { this.Signal.FFTLength = value; }
        }

        public double Phoff
        {
            get { return (this.Signal is PhoffSignal) ? (this.Signal as PhoffSignal).Phoff : 0.0; }
            set 
            {
                if (this.Signal is PhoffSignal)
                {
                    (this.Signal as PhoffSignal).Phoff = value;
                }
            }
        }

        public int NSymbols
        {
            get { return (this.Signal is OFDMSignal) ? (this.Signal as OFDMSignal).NSymbols : 0; }
            set 
            {
                if (this.Signal is OFDMSignal)
                {
                    (this.Signal as OFDMSignal).NSymbols = value;
                }
            }
        }

        public int Bitwidth
        {
            get { return this.Signal.Bitwidth; }
            set { this.Signal.Bitwidth = value; }
        }

        public double RMS
        {
            get { return this.Signal.RMS; }
            set { this.Signal.RMS = value; }
        }

        public int Length
        {
            get { return this.Signal.Length; }
            set { this.Signal.Length = value; }
        }

        public double LengthTime
        {
            get { return this.Signal.LengthTime; }
            set { this.Signal.LengthTime = value; }
        }

        public double[] I
        {
            get { return this.Signal.I; }
            set { this.Signal.I = value; }
        }

        public double[] Q
        {
            get { return this.Signal.Q; }
            set { this.Signal.Q = value; }
        }

        public Complex[] X
        {
            get { return this.Signal.X; }
            set { this.Signal.X = value; }
        }

        public Complex[] FFT
        {
            get { return this.Signal.FFT; }
            set { this.Signal.FFT = value; }
        }

        public double InputMag
        {
            get { return this.inputMag; }
            set { this.inputMag = value; }
        }
        #endregion
    }
}
