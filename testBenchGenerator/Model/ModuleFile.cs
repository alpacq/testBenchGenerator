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
        private List<Port> inputs;
        private List<Port> outputs;
        private List<Parameter> parameters;
        private List<Reset> resets;
        private List<Clock> clocks;
        private List<TestCase> testCases;
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

        public List<Port> Inputs
        {
            get { return this.inputs; }
            set { this.inputs = value; }
        }

        public List<Port> Outputs
        {
            get { return this.outputs; }
            set { this.outputs = value; }
        }

        public List<Parameter> Parameters
        {
            get { return this.parameters; }
            set { this.parameters = value; }
        }

        public List<Reset> Resets
        {
            get { return this.resets; }
            set { this.resets = value; }
        }

        public List<Clock> Clocks
        {
            get { return this.clocks; }
            set { this.clocks = value; }
        }

        public List<TestCase> TestCases
        {
            get { return this.testCases; }
            set { this.testCases = value; }
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

            this.ReadInputs(this.dutLines);
            this.ReadOutputs(this.dutLines);
            this.ReadParameters(this.dutLines);
            this.RecognizeResets();
            this.RecognizeClocks();

            this.bwLen = 0;
            foreach (Port input in this.inputs)
            {
                if (input.Bitwidth != null)
                    if (input.Bitwidth.Length > this.bwLen)
                        this.BwLen = input.Bitwidth.Length;
            }
            foreach (Port output in this.outputs)
            {
                if (output.Bitwidth != null)
                    if (output.Bitwidth.Length > this.bwLen)
                        this.BwLen = output.Bitwidth.Length;
            }
            this.BwLen /= 4;
        }

        private void ReadInputs(string[] codeLines)
        {
            this.Inputs = new List<Port>();

            foreach (string codeLine in codeLines)
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

        private void ReadOutputs(string[] codeLines)
        {
            this.Outputs = new List<Port>();

            foreach (string codeLine in codeLines)
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

        private void ReadParameters(string[] codeLines)
        {
            this.Parameters = new List<Parameter>();

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
                        this.Parameters.Add(new Parameter(line[1], line[3].Replace(",", "")));
                    }
                }
            }
        }

        private void RecognizeResets()
        {
            this.Resets = new List<Reset>();
            foreach(Port input in this.Inputs)
            {
                if(input.Name.Contains("rst") || input.Name.Contains("reset"))
                {
                    if (input.Name.Contains("n"))
                        this.Resets.Add(new Reset(input, false)); //rst = 0
                    else
                        this.Resets.Add(new Reset(input, true)); //rst = 1
                }
            }
        }

        private void RecognizeClocks()
        {
            this.Clocks = new List<Clock>();
            foreach(Port input in this.Inputs)
            {
                if(input.Name.Contains("clk") || input.Name.Contains("clock"))
                {
                    this.Clocks.Add(new Clock(input, 491.52));
                }
            }
        }
    }
}
