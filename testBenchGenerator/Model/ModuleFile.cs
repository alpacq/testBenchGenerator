using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Model
{
    public class ModuleFile
    {
        private string path;
        private string name;
        private string[] dutLines;
        private Dictionary<string, string> inputs;
        private Dictionary<string, string> outputs;
        private Dictionary<string, string> parameters;
        private Dictionary<string, bool> resets;
        private Dictionary<string, double> clocks;
        private int bwLen;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }

        public Dictionary<string, string> Inputs
        {
            get { return this.inputs; }
            set { this.inputs = value; }
        }

        public Dictionary<string, string> Outputs
        {
            get { return this.outputs; }
            set { this.outputs = value; }
        }

        public Dictionary<string, string> Parameters
        {
            get { return this.parameters; }
            set { this.parameters = value; }
        }

        public Dictionary<string, bool> Resets
        {
            get { return this.resets; }
            set { this.resets = value; }
        }

        public Dictionary<string, double> Clocks
        {
            get { return this.clocks; }
            set { this.clocks = value; }
        }

        public int BwLen
        {
            get { return this.bwLen; }
            set { this.bwLen = value; }
        }

        public ModuleFile(string path)
        {
            this.Path = path;
            this.Read();
        }

        private void Read()
        {
            this.Name = this.Path.Split('\\').ToList<string>().LastOrDefault().Replace(".sv", "");

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

            this.ReadInputs(this.dutLines);
            this.ReadOutputs(this.dutLines);
            this.ReadParameters(this.dutLines);
            this.RecognizeResets();
            this.RecognizeClocks();

            this.bwLen = 0;
            foreach (string bw in this.inputs.Values)
            {
                if (bw != null)
                    if (bw.Length > this.bwLen)
                        this.BwLen = bw.Length;
            }
            foreach (string bw in this.outputs.Values)
            {
                if (bw != null)
                    if (bw.Length > this.bwLen)
                        this.BwLen = bw.Length;
            }
            this.BwLen /= 4;
        }

        private void ReadInputs(string[] codeLines)
        {
            this.Inputs = new Dictionary<string, string>();

            foreach (string codeLine in codeLines)
            {
                if (codeLine.Contains("input"))
                {
                    string connName = codeLine.Split(']').ToList<string>().LastOrDefault().Split(' ').ToList<string>().LastOrDefault().Split('\t').ToList<string>().LastOrDefault().Replace(" ", "").Replace(",", "");

                    if (codeLine.Contains("["))
                    {
                        string bitwidth = String.Join(String.Empty, codeLine.Split(']').ToList<string>().Where(s => s.Contains("["))).Replace(" ", "").Replace("input", "").Replace("output", "").Replace("wire", "").Replace("reg", "").Replace("logic", "").Replace(",", "");
                        bitwidth = bitwidth.Replace("0[", "0][") + "]";
                        this.Inputs.Add(connName, bitwidth);
                    }
                    else
                    {
                        this.Inputs.Add(connName, null);
                    }
                }
            }
        }

        private void ReadOutputs(string[] codeLines)
        {
            this.Outputs = new Dictionary<string, string>();

            foreach (string codeLine in codeLines)
            {
                if (codeLine.Contains("output"))
                {
                    string connName = codeLine.Split(']').ToList<string>().LastOrDefault().Split(' ').ToList<string>().LastOrDefault().Split('\t').ToList<string>().LastOrDefault().Replace(" ", "").Replace(",", "");

                    if (codeLine.Contains("["))
                    {
                        string bitwidth = String.Join(String.Empty, codeLine.Split(']').ToList<string>().Where(s => s.Contains("["))).Replace(" ", "").Replace("input", "").Replace("output", "").Replace("wire", "").Replace("reg", "").Replace("logic", "").Replace(",", "");
                        bitwidth = bitwidth.Replace("0[", "0][") + "]";
                        this.Outputs.Add(connName, bitwidth);
                    }
                    else
                    {
                        this.Outputs.Add(connName, null);
                    }
                }
            }
        }

        private void ReadParameters(string[] codeLines)
        {
            this.Parameters = new Dictionary<string, string>();

            foreach (string codeLine in codeLines)
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
                        this.Parameters.Add(line[1], line[3].Replace(",", ""));
                    }
                }
            }
        }

        private void RecognizeResets()
        {
            this.Resets = new Dictionary<string, bool>();
            foreach(string input in this.Inputs.Keys)
            {
                if(input.Contains("rst") || input.Contains("reset"))
                {
                    if (input.Contains("n"))
                        this.Resets.Add(input, false); //rst = 0
                    else
                        this.Resets.Add(input, true); //rst = 1
                }
            }
        }

        private void RecognizeClocks()
        {
            this.Clocks = new Dictionary<string, double>();
            foreach(string input in this.Inputs.Keys)
            {
                if(input.Contains("clk") || input.Contains("clock"))
                {
                    this.Clocks.Add(input, 0.0);
                }
            }
        }
    }
}
