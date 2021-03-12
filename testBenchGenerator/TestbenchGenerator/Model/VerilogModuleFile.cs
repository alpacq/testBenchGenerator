using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.TestbenchGenerator.Model
{
    public class VerilogModuleFile : ModuleFile
    {
        public VerilogModuleFile(string path) : base(path)
        {
            
        }

        protected override void Read()
        {
            this.Name = this.Path.Split('\\').ToList<string>().LastOrDefault().Replace(".sv", "").Replace(".v","");

            this.dutLines = System.IO.File.ReadAllLines(this.Path);

            for (int i = 0; i < this.dutLines.Length; i++)
            {
                if (this.dutLines[i].Contains("//"))
                {
                    int index = this.dutLines[i].IndexOf("//");
                    if (index > 0)
                        this.dutLines[i] = this.dutLines[i].Substring(0, index);
                }
                this.dutLines[i] = this.dutLines[i].Trim();
            }

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
                if (codeLine.Contains("input"))
                {
                    string connName = codeLine.Split(']').ToList<string>().LastOrDefault().Replace(" ","").Replace(",", "").Replace("input", "").Replace("output", "").Replace("wire", "").Replace("reg", "").Replace("logic", "");

                    if (codeLine.Contains("["))
                    {
                        string bitwidth = String.Join(String.Empty, codeLine.Split(']').ToList<string>().Where(s => s.Contains("["))).Replace(" ", "").Replace("input", "").Replace("output", "").Replace("wire", "").Replace("reg", "").Replace("logic", "").Replace(",", "");
                        bitwidth = bitwidth.Replace("0[", "0][") + "]";
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
                if (codeLine.Contains("output"))
                {
                    
                    string connName = codeLine.Replace("=", "").Replace("'0,", "").Replace("'0", "").Trim().Split(']').ToList<string>().LastOrDefault().Replace(" ", "").Replace(",", "").Replace("input", "").Replace("output", "").Replace("wire", "").Replace("reg", "").Replace("logic", "");

                    if (codeLine.Contains("["))
                    {
                        string bitwidth = String.Join(String.Empty, codeLine.Split(']').ToList<string>().Where(s => s.Contains("["))).Replace(" ", "").Replace("input", "").Replace("output", "").Replace("wire", "").Replace("reg", "").Replace("logic", "").Replace(",", "");
                        bitwidth = bitwidth.Replace("0[", "0][") + "]";
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

            foreach (string codeLine in this.dutLines)
            {
                if (codeLine.Contains("parameter"))
                {
                    List<string> line = codeLine.Split(' ').ToList<string>();
                    List<string> toRemove = new List<string>();
                    for (int i = 0; i < line.Count; i++)
                    {
                        if (line[i] == " " || line[i] == "\t" || string.IsNullOrEmpty(line[i]))
                            toRemove.Add(line[i]);
                    }
                    foreach (string wrd in toRemove)
                        line.Remove(wrd);
                    if (line.Count > 3)
                    {
                        this.Parameters.Add(new Parameter(line[1], line[3].Replace(",", "")));
                    }
                }
            }
        }
    }
}
