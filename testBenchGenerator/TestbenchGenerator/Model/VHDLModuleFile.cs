using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.TestbenchGenerator.Model
{
    public class VHDLModuleFile : ModuleFile
    {
        public VHDLModuleFile(string path) : base(path)
        {
            
        }

        protected override void Read()
        {
            this.Name = this.Path.Split('\\').ToList<string>().LastOrDefault().Replace(".vhd", "");

            this.dutLines = System.IO.File.ReadAllLines(this.Path);
            int entInd = 0;
            int endInd = 0;
            string nm = String.Empty;

            for (int i = 0; i < this.dutLines.Length; i++)
            {
                if (this.dutLines[i].Contains("--"))
                {
                    int index = this.dutLines[i].IndexOf("--");
                    if (index > 0)
                        this.dutLines[i] = this.dutLines[i].Substring(0, index);
                }
                this.dutLines[i] = this.dutLines[i].Trim();
                if (this.dutLines[i].Contains("entity"))
                {
                    nm = this.dutLines[i].Split(' ')[1];
                    entInd = i;
                }
                if(this.dutLines[i].Contains("end " + nm + ";"))
                {
                    endInd = i;
                }
            }

            //only lines between entity ... end
            if(endInd > entInd)
                this.dutLines = this.dutLines.ToList().GetRange(entInd, (endInd - entInd)).ToArray();

            this.ReadInputs();
            this.ReadOutputs();
            this.ReadParameters();
            this.RecognizeResets();
            this.RecognizeClocks();
            this.RecognizeIns();

            this.BwLen = 0;
            foreach (Port input in this.Inputs)
            {
                if (input.Bitwidth != null)
                    if (input.Bitwidth.Length > this.BwLen)
                        this.BwLen = input.Bitwidth.Length;
            }
            foreach (Port output in this.Outputs)
            {
                if (output.Bitwidth != null)
                    if (output.Bitwidth.Length > this.BwLen)
                        this.BwLen = output.Bitwidth.Length;
            }
            this.BwLen /= 4;
        }

        protected override void ReadInputs()
        {
            this.Inputs = new List<Port>();

            foreach (string codeLine in this.dutLines)
            {
                if (codeLine.Contains("in "))
                {
                    string connName = codeLine.Split(':').FirstOrDefault().Trim();

                    if (codeLine.Contains(" downto ") || codeLine.Contains(" to "))
                    {
                        string bitwidth = codeLine.Substring(codeLine.IndexOf("(")).Replace("(", "[").Replace(")", "]").Replace(" downto ", ":").Replace(" to ", ":").Replace(";", "").Trim();
                        this.Inputs.Add(new Port(connName, bitwidth));
                    }
                    else
                    {
                        this.Inputs.Add(new Port(connName));
                    }
                }
            }
        }

        protected override void ReadOutputs()
        {
            this.Outputs = new List<Port>();

            foreach (string codeLine in this.dutLines)
            {
                if (codeLine.Contains("out "))
                {

                    string connName = codeLine.Split(':').FirstOrDefault().Trim();

                    if (codeLine.Contains(" downto ") || codeLine.Contains(" to "))
                    {
                        string bitwidth = codeLine.Substring(codeLine.IndexOf("(")).Replace("(", "[").Replace(")", "]").Replace(" downto ", ":").Replace(" to ", ":").Replace(";", "").Trim();
                        this.Outputs.Add(new Port(connName, bitwidth));
                    }
                    else
                    {
                        this.Outputs.Add(new Port(connName));
                    }
                }
            }
        }

        protected override void ReadParameters()
        {
            this.Parameters = new List<Parameter>();
        }
    }
}
