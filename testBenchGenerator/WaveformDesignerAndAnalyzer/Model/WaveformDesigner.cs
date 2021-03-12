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
    public class WaveformDesigner : WaveformProcessor
    {
        public void GenerateWaveform()
        {
            this.Signal.Create();

            this.Signal.ApplyGain();
            this.Signal.ComputeFFT();

            this.Signal.Length = this.X.Length;
            double[] mags = new double[this.I.Length];
            for (int m = 0; m < mags.Length; m++)
                mags[m] = (this.I[m] * this.I[m]) + (this.Q[m] * this.Q[m]);

            this.InputMag = mags.Select(x => Math.Sqrt(x)).Average() / (Math.Pow(2, (this.Bitwidth - 1)));             
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
