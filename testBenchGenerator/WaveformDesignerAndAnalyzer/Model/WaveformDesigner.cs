using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using testBenchGenerator.Common;
using System.Globalization;

namespace testBenchGenerator.WaveformDesignerAndAnalyzer.Model
{
    public class WaveformDesigner
    {
        #region variables and fields
        private Signal signal;
        private string type;
        private Radix radix;
        private Delimiter delimiter;

        public Signal Signal
        {
            get { return this.signal; }
            set { this.signal = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
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
        #endregion

        public void GenerateWaveform()
        {
            this.Signal.Create();

            this.Signal.ApplyGain();
            this.Signal.ComputeFFT();
        }

        public void CreateAndSaveFile(string path)
        {
            List<string> data = new List<string>();
            string rdx = this.Radix == Radix.Hexadecimal ? "x" : this.Radix == Radix.Decimal ? "d" : "f";
            string del = this.Delimiter == Delimiter.Comma ? "," : " ";

            for(int k = 0; k < this.Signal.Length; k++)
            {
                if(rdx == "F")
                    data.Add(this.Signal.I[k].ToString(rdx, CultureInfo.InvariantCulture) + del + this.Signal.Q[k].ToString(rdx, CultureInfo.InvariantCulture));
                else
                    data.Add(Convert.ToInt32(Math.Truncate(this.Signal.I[k])).ToString(rdx, CultureInfo.InvariantCulture) + del + Convert.ToInt32(Math.Truncate(this.Signal.Q[k])).ToString(rdx, CultureInfo.InvariantCulture));
            }

            System.IO.File.WriteAllLines(path, data);
        }

        public WaveformDesigner()
        {
            this.Signal = new Signal();
        }
    }
}
