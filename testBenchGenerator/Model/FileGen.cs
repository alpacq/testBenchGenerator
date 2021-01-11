﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace testBenchGenerator.Model
{
    public class FileGen
    {
        private const string timescale = "`timescale 1ns / 1ps";
        private const string nettypeBegin = "`default_nettype none";
        private const string nettypeEnd = "`default_nettype wire";
        private const string endmodule = "endmodule";
        private const string initial = "\tinitial begin";
        private const string forever = "\t\tforever begin";
        private const string end = "\tend";
        private const string generatedComment = "//GENERATED CODE";
        private const string generatedToAdaptComment = "//GENERATED CODE TO ADAPT";
        private ModuleFile moduleFile;
        private string tbFileName;
        private string inFileName;
        private string outFileName;
        private List<string> lines;

        public ModuleFile ModuleFile
        {
            get { return this.moduleFile; }
            set { this.moduleFile = value; }
        }

        public string TbFileName
        {
            get { return this.tbFileName; }
            set { this.tbFileName = value; }
        }

        public string InFileName
        {
            get { return this.inFileName; }
            set { this.inFileName = value; }
        }

        public string OutFileName
        {
            get { return this.outFileName; }
            set { this.outFileName = value; }
        }

        public FileGen(ModuleFile moduleFile, string tbFileName, string inFileName = null, string outFileName = null)
        {
            this.ModuleFile = moduleFile;
            this.TbFileName = tbFileName;
            this.InFileName = inFileName;
            this.OutFileName = outFileName;
            this.lines = new List<string>();
        }

        #region Helper Methods
        
        private bool SaveFile()
        {
            try
            {
                if (File.Exists(this.TbFileName))
                {
                    File.Delete(this.TbFileName);
                }

                // Create a new file     
                using (FileStream fs = File.Create(this.TbFileName))
                {
                }
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(this.TbFileName))
                {
                    foreach (string line in this.lines)
                    {
                        file.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        #endregion

        #region Lines Create Methods

        private void AddHeader()
        {
            this.lines.Add("////////////////////////////////////////////////////////////////////////////////");
            this.lines.Add("//                                                                            //");
            this.lines.Add("//                                                                            //");
            this.lines.Add("//                         FILE GENERATED BY TBG TOOL                         //");
            this.lines.Add("//                 (C) Copyright by Vigogne Software Ltd. 2020                //");
            this.lines.Add("//                                                                            //");
            this.lines.Add("//                                                                            //");
            this.lines.Add("//  Please review the content of this file before compilation and execution.  //");
            this.lines.Add("//                                                                            //");
            this.lines.Add("//                                                                            //");
            this.lines.Add("////////////////////////////////////////////////////////////////////////////////");
            this.lines.Add(timescale);
            this.lines.Add(nettypeBegin);
            this.lines.Add("");

            this.lines.Add("module " + this.TbFileName.Split('\\').ToList<string>().LastOrDefault().Replace(".sv", "();"));
            this.lines.Add("");
        }

        private void AddFooter()
        {
            this.lines.Add(endmodule);
            this.lines.Add(nettypeEnd);
        }

        private void AddParamsAndConnections()
        {
            if(this.ModuleFile.Parameters.Count > 0)
            {
                this.lines.Add(generatedToAdaptComment);
                this.lines.Add("//Parameters of Device Under Test - change values if needed");
            }
            foreach (string par in this.ModuleFile.Parameters.Keys)
            {
                this.lines.Add("\tlocalparam " + par + " = " + this.ModuleFile.Parameters[par] + ";");
            }
            this.lines.Add("");
            this.lines.Add(generatedToAdaptComment);
            this.lines.Add("//Clock signals period - change for proper values");
            foreach(string clk in this.ModuleFile.Clocks.Keys)
            {
                if (this.ModuleFile.Clocks[clk] != 0.0)
                    this.lines.Add("\tlocalparam " + clk.ToUpper() + "_PERIOD = " + (1000 / this.ModuleFile.Clocks[clk]) + "ns;");
            }
            this.lines.Add("");
            this.lines.Add(generatedComment);
            this.lines.Add("//Connections of Device Under Test");
            foreach (string conn in this.ModuleFile.Inputs.Keys)
            {
                string lineToAdd = "\tlogic\t";
                if (this.ModuleFile.Inputs[conn] != null)
                {
                    lineToAdd += this.ModuleFile.Inputs[conn];
                    for (int i = this.ModuleFile.BwLen - (this.ModuleFile.Inputs[conn].Length / 4); i > 0; i--)
                        lineToAdd += "\t";
                }
                else
                {
                    for (int i = this.ModuleFile.BwLen; i > 0; i--)
                        lineToAdd += "\t";
                }
                this.lines.Add(lineToAdd + "\t" + conn + " = '0;");
            }
            foreach (string conn in this.ModuleFile.Outputs.Keys)
            {
                string lineToAdd = "\tlogic\t";
                if (this.ModuleFile.Outputs[conn] != null)
                {
                    lineToAdd += this.ModuleFile.Outputs[conn];
                    for (int i = this.ModuleFile.BwLen - (this.ModuleFile.Outputs[conn].Length / 4); i > 0; i--)
                        lineToAdd += "\t";
                }
                else
                {
                    for (int i = this.ModuleFile.BwLen; i > 0; i--)
                        lineToAdd += "\t";
                }
                this.lines.Add(lineToAdd + "\t" + conn + ";");
            }

            this.lines.Add("");
        }

        private void AddDut()
        {
            this.lines.Add(generatedComment);
            this.lines.Add("//Device Under Test instatiation");
            if (this.ModuleFile.Parameters.Count > 0)
            {
                this.lines.Add("\t" + this.ModuleFile.Name +  " #(");
                foreach (string par in this.ModuleFile.Parameters.Keys)
                {
                    if (par == this.ModuleFile.Parameters.Keys.Last())
                        this.lines.Add("\t\t." + par + "(" + par + ")");
                    else
                        this.lines.Add("\t\t." + par + "(" + par + "),");
                }
                this.lines.Add("\t) dut(");
            }
            else
            {
                this.lines.Add("\t" + this.ModuleFile.Name + " dut(");
            }
            foreach (string conn in this.ModuleFile.Inputs.Keys)
            {
                this.lines.Add("\t\t." + conn + "(" + conn + "),");
            }
            foreach (string conn in this.ModuleFile.Outputs.Keys)
            {
                if (conn == this.ModuleFile.Outputs.Keys.Last())
                    this.lines.Add("\t\t." + conn + "(" + conn + ")");
                else
                    this.lines.Add("\t\t." + conn + "(" + conn + "),");
            }
            this.lines.Add("\t);");
            this.lines.Add("");
        }

        private void AddClocks()
        {
            this.lines.Add(generatedComment);
            this.lines.Add("//Clocks generation");
            this.lines.Add(initial);
            this.lines.Add(forever);
            foreach(string clk in this.ModuleFile.Clocks.Keys)
            {
                if(this.ModuleFile.Clocks[clk] != 0.0)
                {
                    this.lines.Add("\t\t\t#(" + clk.ToUpper() + "_PERIOD / 2) " + clk + " <= ~" + clk + ";");
                }
            }
            this.lines.Add("\t" + end);
            this.lines.Add(end);
            this.lines.Add("");
        }

        private void AddResets()
        {
            this.lines.Add(generatedComment);
            this.lines.Add("//Initial reset state");
            this.lines.Add(initial);
            foreach(string rst in this.ModuleFile.Resets.Keys)
            {
                this.lines.Add("\t\t" + rst + " <= " + (this.ModuleFile.Resets[rst] ? "1'b1;" : "1'b0;"));
            }
            this.lines.Add("\t\t#100ns;");
            foreach(string rst in this.ModuleFile.Resets.Keys)
            {
                this.lines.Add("\t\t" + rst + " <= " + (this.ModuleFile.Resets[rst] ? "1'b0;" : "1'b1;"));
            }
            this.lines.Add(end);
            this.lines.Add("");
        }

        private void AddInputsInit()
        {
            this.lines.Add(generatedToAdaptComment);
            this.lines.Add("//Inputs stimulus - fill in proper values");
            this.lines.Add(initial);
            foreach(string input in this.ModuleFile.Inputs.Keys)
            {
                if(!this.ModuleFile.Resets.Keys.Contains(input) && !(this.ModuleFile.Clocks.Keys.Contains(input)))
                {
                    this.lines.Add("\t\t" + input + " <= '0;");
                }
            }
            this.lines.Add(end);
            this.lines.Add("");
        }

        #endregion

        public bool GenerateFile()
        {
            this.lines = new List<string>();

            //create all lines of code
            this.AddHeader();
            this.AddParamsAndConnections();
            this.AddDut();
            this.AddClocks();
            this.AddResets();
            this.AddInputsInit();
            //TODO
            this.AddFooter();

            //put code in tb file
            return this.SaveFile();
        }
    }
}
