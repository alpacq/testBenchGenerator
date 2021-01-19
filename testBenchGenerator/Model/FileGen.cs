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
        #region constant SystemVerilog strings, other often used strings
        private const string timescale = "`timescale 1ns / 1ps";
        private const string nettypeBegin = "`default_nettype none";
        private const string nettypeEnd = "`default_nettype wire";
        private const string always = "\talways @(posedge ";
        private const string endmodule = "endmodule";
        private const string initial = "\tinitial begin";
        private const string forever = "\t\tforever begin";
        private const string end = "\tend";
        private const string generatedComment = "//GENERATED CODE";
        private const string generatedToAdaptComment = "//GENERATED CODE TO ADAPT";
        #endregion
        #region private variables
        private ModuleFile moduleFile;
        private string tbFileName;
        private InputFile inFile;
        private string outFileName;
        private List<string> lines;
        #endregion
        #region public properties
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

        public InputFile InFile
        {
            get { return this.inFile; }
            set { this.inFile = value; }
        }

        public string OutFileName
        {
            get { return this.outFileName; }
            set { this.outFileName = value; }
        }
        #endregion
        #region helper methods

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
        #region lines create methods

        private void AddHeader()
        {
            this.lines.Add("////////////////////////////////////////////////////////////////////////////////");
            this.lines.Add("//                                                                            //");
            this.lines.Add("//                                                                            //");
            this.lines.Add("//                        FILE GENERATED BY tbGen TOOL                        //");
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
            this.lines.Add("//USER CODE");
            this.lines.Add("//Place for your stimulus & verification logic");
            this.lines.Add("");
            this.lines.Add("");
            this.lines.Add(endmodule);
            this.lines.Add(nettypeEnd);
        }

        private void AddParamsAndConnections()
        {
            if (this.ModuleFile.Parameters.Count > 0)
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
            foreach (Clock clk in this.ModuleFile.Clocks)
            {
                if (clk.Frequency != 0.0)
                    this.lines.Add("\tlocalparam " + clk.Name.ToUpper() + "_PERIOD = " + (1000 / clk.Frequency).ToString("N3").Replace(",",".") + "ns;");
            }
            this.lines.Add("");
            this.lines.Add(generatedComment);
            this.lines.Add("//Connections of Device Under Test");
            foreach (Port conn in this.ModuleFile.Inputs)
            {
                string lineToAdd = "\tlogic\t";
                if (conn.Bitwidth != null)
                {
                    lineToAdd += conn.Bitwidth;
                    for (int i = this.ModuleFile.BwLen - (conn.Bitwidth.Length / 4); i > 0; i--)
                        lineToAdd += "\t";
                }
                else
                {
                    for (int i = this.ModuleFile.BwLen; i > 0; i--)
                        lineToAdd += "\t";
                }
                this.lines.Add(lineToAdd + "\t" + conn.Name + " = '0;");
            }
            foreach (Port conn in this.ModuleFile.Outputs)
            {
                string lineToAdd = "\tlogic\t";
                if (conn.Bitwidth != null)
                {
                    lineToAdd += conn.Bitwidth;
                    for (int i = this.ModuleFile.BwLen - (conn.Bitwidth.Length / 4); i > 0; i--)
                        lineToAdd += "\t";
                }
                else
                {
                    for (int i = this.ModuleFile.BwLen; i > 0; i--)
                        lineToAdd += "\t";
                }
                this.lines.Add(lineToAdd + "\t" + conn.Name + ";");
            }
            if(this.InFile != null)
            {
                string lineToAdd = "\tlogic\t";
                for (int i = this.ModuleFile.BwLen; i > 0; i--)
                    lineToAdd += "\t";
                this.lines.Add(lineToAdd + "\t" + "eof = '0;");
            }
            this.lines.Add("");
        }

        private void AddDut()
        {
            this.lines.Add(generatedComment);
            this.lines.Add("//Device Under Test instatiation");
            if (this.ModuleFile.Parameters.Count > 0)
            {
                this.lines.Add("\t" + this.ModuleFile.Name + " #(");
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
            foreach (Port conn in this.ModuleFile.Inputs)
            {
                this.lines.Add("\t\t." + conn.Name + "(" + conn.Name + "),");
            }
            foreach (Port conn in this.ModuleFile.Outputs)
            {
                if (conn == this.ModuleFile.Outputs.Last())
                    this.lines.Add("\t\t." + conn.Name + "(" + conn.Name + ")");
                else
                    this.lines.Add("\t\t." + conn.Name + "(" + conn.Name + "),");
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
            foreach (Clock clk in this.ModuleFile.Clocks)
            {
                if (clk.Frequency != 0.0)
                {
                    this.lines.Add("\t\t\t#(" + clk.Name.ToUpper() + "_PERIOD / 2) " + clk.Name + " <= ~" + clk.Name + ";");
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
            foreach (Reset rst in this.ModuleFile.Resets)
            {
                this.lines.Add("\t\t" + rst.Name + " = " + (rst.Polarization ? "1'b1;" : "1'b0;"));
            }
            this.lines.Add("\t\t#100ns;");
            foreach (Reset rst in this.ModuleFile.Resets)
            {
                this.lines.Add("\t\t" + rst.Name + " = " + (rst.Polarization ? "1'b0;" : "1'b1;"));
            }
            this.lines.Add(end);
            this.lines.Add("");
        }

        private void AddInputsInit()
        {
            this.lines.Add(generatedToAdaptComment);
            this.lines.Add("//Inputs stimulus - fill in proper values");
            this.lines.Add(initial);
            foreach (Port input in this.ModuleFile.Inputs)
            {
                if ((!this.ModuleFile.Resets.Any(r => r.Name == input.Name)) && !(this.ModuleFile.Clocks.Any(r => r.Name == input.Name)))
                {
                    this.lines.Add("\t\t" + input + " = '0;");
                }
            }
            this.lines.Add(end);
            this.lines.Add("");
        }

        private void AddFileIDs()
        {
            if (this.InFile != null)
            {
                this.lines.Add(generatedComment);
                this.lines.Add("//Opening files for input/output data");
                this.lines.Add("\tinteger\t\tdata_in_file;");
                this.lines.Add("\tinteger\t\tdata_out_file;");
                this.lines.Add("");
                this.lines.Add(initial);
                this.lines.Add("\t\tdata_in_file = $fopen(\"" + this.InFile.Path.Replace("\\","/") + "\",\"r\");");
                this.lines.Add("\t\tdata_out_file = $fopen(\"" + this.OutFileName.Replace("\\", "/") + "\",\"w\");");
                this.lines.Add(end);
                this.lines.Add("");
            }
        }

        private void AddFileIO()
        {
            if (this.InFile != null)
            {
                this.lines.Add(generatedToAdaptComment);
                this.lines.Add("//Reading from input data file and writing to output data file - modify for your needs");
                this.lines.Add("\t//Check for proper clock signal for this process");
                this.lines.Add(always + this.ModuleFile.Clocks.FirstOrDefault().Name + ") begin");
                this.lines.Add("\t\t//Check for proper reset signal");
                this.lines.Add("\t\tif(!" + this.ModuleFile.Resets.FirstOrDefault().Name + ") begin");
                this.lines.Add("\t\t\tif(!$feof(data_in_file)) begin");
                this.lines.Add("\t\t\t\t//Use proper valid signal for data input and proper data input port");
                this.lines.Add("\t\t\t\tif(valid_in) begin"); 
                this.lines.Add("\t\t\t\t\t$fscanf(data_in_file, \"%d %d\\n\", data_in);");
                this.lines.Add("\t\t\t" + end);
                this.lines.Add("\t\t" + end + " else begin");
                this.lines.Add("\t\t\t\t$fclose(data_in_file);");
                this.lines.Add("\t\t\t\teof <= 1'b1;");
                this.lines.Add("\t\t" + end);                
                this.lines.Add("\t\t\t//Use proper valid signal for data output and proper data output port");
                this.lines.Add("\t\t\tif(valid_out) begin");
                this.lines.Add("\t\t\t\t$fwrite(data_out_file, \"%d %d\\n\", $signed(data_out));");
                this.lines.Add("\t\t" + end);
                this.lines.Add("\t" + end);
                this.lines.Add(end);
                this.lines.Add("");
            }
        }

        #endregion

        public FileGen(ModuleFile moduleFile, string tbFileName, InputFile inputFile = null)
        {
            this.ModuleFile = moduleFile;
            this.TbFileName = tbFileName;
            this.InFile = inputFile;
            if(this.InFile != null)
                this.OutFileName = this.ModuleFile.Path.Replace(".sv", "_" + this.InFile.Name + "_out.txt");
            this.lines = new List<string>();
        }

        public bool GenerateFile()
        {
            this.lines = new List<string>();

            //create all lines of code
            this.AddHeader();
            this.AddParamsAndConnections();
            this.AddDut();
            this.AddClocks();
            this.AddResets();
            this.AddFileIDs();
            this.AddInputsInit();
            this.AddFileIO();
            this.AddFooter();

            //put code in tb file
            return this.SaveFile();
        }
    }
}
