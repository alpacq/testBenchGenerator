using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using testBenchGenerator.Common;

namespace testBenchGenerator.TestbenchGenerator.Model
{
    public class FileGen
    {
        #region constant SystemVerilog strings, other often used strings
        private const string timescale = "`timescale 1ns / 1ps";
        private const string nettypeBegin = "`default_nettype none";
        private const string nettypeEnd = "`default_nettype wire";
        private const string always = "\talways @(posedge ";
        private const string task = "\ttask ";
        private const string endtask = "\tendtask";
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
        private List<string> lines;
        private string formatOut;
        private string formatIn;
        private string dos;
        private string dis;
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

        private void FormatsForFileIO(TestCase di)
        {
            this.formatIn = String.Empty;
            this.formatOut = String.Empty;
            this.dis = String.Empty;
            this.dos = String.Empty;
            foreach(Port inp in di.DataIns)
            {
                this.formatIn += di.Radix == Radix.Decimal ? "%d" : "%h";
                this.dis += inp.Name;
                if (inp == di.DataIns.LastOrDefault())
                {
                    this.formatIn += "\\n";
                    this.dis += "));";
                }
                else
                {
                    this.formatIn += " ";
                    this.dis += ", ";
                }
            }
            foreach (Port outp in di.DataOuts)
            {
                formatOut += di.Radix == Radix.Decimal ? "%d" : "%h";
                dos += outp.Name;
                if (outp == di.DataOuts.LastOrDefault())
                {
                    dos += ");";
                }
                else
                {
                    formatOut += " ";
                    dos += ", ";
                }
            }
        }

        public string SeqtoSVString(string seq)
        {
            switch (seq)
            {
                case "1/1":
                    return "11111111";
                case "1/2":
                    return "10101010";
                case "1/4":
                    return "10001000";
                case "1/8":
                    return "10000000";
                case "1/16":
                    return "1000000000000000";
                case "1/32":
                    return "10000000000000000000000000000000";
                case "1/64":
                    return "1000000000000000000000000000000000000000000000000000000000000000";
                case "1/128":
                    return "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
                case "1/256":
                    return "1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
                case "1/512":
                    return "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
                default:
                    return null;
            }
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

            this.lines.Add("module " + this.TbFileName.Split('\\').ToList<string>().LastOrDefault().Replace(".sv", "();").Replace(".v", "();"));
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
            foreach (Parameter par in this.ModuleFile.Parameters)
            {
                this.lines.Add("\tlocalparam " + par.Name + " = " + par.Value + ";");
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
            foreach (TestCase tc in this.ModuleFile.TestCases)
            {
                if (tc.DataVector != null && tc.DataIns.FirstOrDefault() != null)
                {
                    string name = tc.DataIns.FirstOrDefault().Name + "_" + tc.DataVector.Split('\\').LastOrDefault().Replace(".txt", "");

                    int len = this.SeqtoSVString(tc.VldSeq).Length;

                    string lineToAdd = "\tlogic\t";
                    for (int i = this.ModuleFile.BwLen; i > 0; i--)
                        lineToAdd += "\t";
                    this.lines.Add(lineToAdd + "\ttest_" + name + "_in_progress = '0;");
                    this.lines.Add(lineToAdd + "\tvalid_" + name + "_pre;");

                    if (len > 1)
                    {
                        lineToAdd = "\tlogic\t[";
                        lineToAdd += (len - 1);
                        lineToAdd += ":0]";
                        for (int i = this.ModuleFile.BwLen - ((len.ToString().Length + 4) / 4); i > 0; i--)
                            lineToAdd += "\t";
                    }
                    this.lines.Add(lineToAdd + "\tvalid_" + name + "_seq = " + len + "'b" + this.SeqtoSVString(tc.VldSeq) + ";");
                    this.lines.Add(lineToAdd + "\tvalid_" + name + "_srl;");                    
                }
            }
            this.lines.Add("");
            foreach(TestCase tc in this.ModuleFile.TestCases)
            {
                if (!tc.Loop)
                {
                    string name = this.ModuleFile.Name + "_" + tc.DataVector.Split('\\').LastOrDefault().Replace(".txt", "");
                    this.lines.Add("\tinteger num_vlds_" + name + " = 0;");
                    this.lines.Add("\tinteger num_vlds_" + name + "_out = 0;");
                }
            }
            this.lines.Add("");
            foreach (TestCase tc in this.ModuleFile.TestCases)
            {
                if (tc.DataVector != null && tc.DataIns.FirstOrDefault() != null)
                {
                    string name = tc.DataIns.FirstOrDefault().Name + "_" + tc.DataVector.Split('\\').LastOrDefault().Replace(".txt", "");

                    int len = this.SeqtoSVString(tc.VldSeq).Length;

                    this.lines.Add("\tassign valid_" + name + "_pre = valid_" + name + "_srl[" + (len - 1) + "];");
                }
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
                foreach (Parameter par in this.ModuleFile.Parameters)
                {
                    if (par == this.ModuleFile.Parameters.Last())
                        this.lines.Add("\t\t." + par.Name + "(" + par.Name + ")");
                    else
                        this.lines.Add("\t\t." + par.Name + "(" + par.Name + "),");
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
            foreach (Clock clk in this.ModuleFile.Clocks)
            {
                this.lines.Add(initial);
                this.lines.Add(forever);
                if (clk.Frequency != 0.0)
                {
                    this.lines.Add("\t\t\t#(" + clk.Name.ToUpper() + "_PERIOD / 2) " + clk.Name + " <= ~" + clk.Name + ";");
                }
                this.lines.Add("\t" + end);
                this.lines.Add(end);
            }            
            this.lines.Add("");
        }

        private void AddResets()
        {
            this.lines.Add("\t\t//Initial resets conditions");
            foreach (Reset rst in this.ModuleFile.Resets)
            {
                this.lines.Add("\t\t" + rst.Name + " = " + (rst.Polarization ? "1'b1;" : "1'b0;"));
            }
            this.lines.Add("\t\t#100ns;");
            foreach (Reset rst in this.ModuleFile.Resets)
            {
                this.lines.Add("\t\t" + rst.Name + " = " + (rst.Polarization ? "1'b0;" : "1'b1;"));
            }
        }

        private void AddInputsInit()
        {
            this.lines.Add("\t\t//Initial inputs states - adapt to your needs");
            foreach (Port input in this.ModuleFile.Inputs)
            {
                if ((!this.ModuleFile.Resets.Any(r => r.Name == input.Name)) && !(this.ModuleFile.Clocks.Any(r => r.Name == input.Name)))
                {
                    this.lines.Add("\t\t" + input.Name + " = '0;");
                }
            }
        }

        private void AddInitialBlock()
        {
            this.lines.Add(generatedToAdaptComment);
            this.lines.Add("//Initial connections' conditions, initial reset state, files' opening and test tasks");
            this.lines.Add(initial);
            this.AddInputsInit();
            this.AddResets();
            if (this.ModuleFile.TestCases.Any(di => di.DataVector != null))
            {
                this.lines.Add("\t\tfork");
                foreach (TestCase di in this.ModuleFile.TestCases)
                {
                    if (di.DataVector != null && di.DataIns.FirstOrDefault() != null)
                    {
                        this.lines.Add("\t\t\t" + di.DataIns.FirstOrDefault().Name + "_" + di.DataVector.Split('\\').LastOrDefault().Replace(".txt", "();"));
                        this.lines.Add("\t\t\t" + this.ModuleFile.Name + "_" + di.DataVector.Split('\\').LastOrDefault().Replace(".txt", "_out();"));
                        if (!di.Loop)
                        {
                            this.lines.Add("\t\tjoin");
                            this.lines.Add("\t\tfork");
                        }
                        
                    }
                }
                this.lines.Add("\t\tjoin");
            }
            this.lines.Add(end);
            this.lines.Add("");
        }

        private void AddFileOutput()
        {
            if (this.ModuleFile.TestCases.Any(di => di.DataVector != null))
            {
                this.lines.Add(generatedToAdaptComment);
                this.lines.Add("//Tasks for writingto output data files - modify for your needs");
                foreach (TestCase di in this.ModuleFile.TestCases)
                {
                    if (di.DataVector != null)
                    {
                        string name = this.ModuleFile.Name + "_" + di.DataVector.Split('\\').LastOrDefault().Replace(".txt", "_out");

                        this.lines.Add(task + name + "();");
                        
                        this.lines.Add("\t\tautomatic integer fid_out_" + name + " = $fopen(\"" + di.DataOutVector.Replace("\\", "/") + "\",\"w\");");
                        this.lines.Add("\t\tif(fid_out_" + name + " == 0) begin");
                        this.lines.Add("\t\t\t$display(\"Error opening file - could not open.\");");
                        this.lines.Add("\t\t\t$stop;");
                        this.lines.Add("\t" + end + " else begin");
                        this.lines.Add("\t\t\t$display(\"File " + di.DataOutVector.Replace("\\", "/") + " opened successfully.\");");
                        this.lines.Add("\t" + end);
                        this.FormatsForFileIO(di);

                        if (di.Loop)
                        {
                            this.lines.Add(forever);
                            this.lines.Add("\t\t\t@(posedge " + di.ClockSync.Name + ");");
                            this.lines.Add("\t\t\tif(" + di.ValidOut.Name + ") begin");                            
                            this.lines.Add("\t\t\t\t$fdisplay(fid_out_" + name + ", \"" + this.formatOut + "\", " + this.dos);
                            this.lines.Add("\t\t" + end);
                        }
                        else
                        {
                            this.lines.Add("\t\twhile(num_vlds_" + name + " == 0 || num_vlds_" + name + " < num_vlds_" + name.Replace("_out","") + ") begin");
                            this.lines.Add("\t\t\t@(posedge " + di.ClockSync.Name + ");");
                            this.lines.Add("\t\t\tif(" + di.ValidOut.Name + ") begin");
                            this.lines.Add("\t\t\t\t$fdisplay(fid_out_" + name + ", \"" + this.formatOut + "\", " + this.dos);
                            this.lines.Add("\t\t\t\tnum_vlds_" + name + " = num_vlds_" + name + " + 1;");
                            this.lines.Add("\t\t" + end);
                        }

                        this.lines.Add("\t" + end);

                        this.lines.Add(endtask + " : " + name);
                        this.lines.Add("");
                    }
                }
            }
        }

        private void AddFileInput()
        {
            if (this.ModuleFile.TestCases.Any(di => di.DataVector != null))
            {
                this.lines.Add(generatedToAdaptComment);
                this.lines.Add("//Tasks for reading from input data files - modify for your needs");
                foreach (TestCase di in this.ModuleFile.TestCases)
                {
                    if (di.DataVector != null && di.DataIns.FirstOrDefault() != null)
                    {
                        string name = di.DataIns.FirstOrDefault().Name + "_" + di.DataVector.Split('\\').LastOrDefault().Replace(".txt", "");
                                      
                        this.lines.Add(task + name + "();");

                        this.lines.Add("\t\tstring line_" + name + ";");
                        this.lines.Add("\t\tautomatic integer fid_" + name + " = $fopen(\"" + di.DataVector.Replace("\\", "/") + "\",\"r\");");
                        this.lines.Add("\t\tif(fid_" + name + " == 0) begin");
                        this.lines.Add("\t\t\t$display(\"Error opening file - could not open.\");");
                        this.lines.Add("\t\t\t$stop;");
                        this.lines.Add("\t" + end + " else begin");
                        this.lines.Add("\t\t\t$display(\"File " + di.DataVector.Replace("\\", "/") + " opened successfully.\");");
                        this.lines.Add("\t\t\ttest_" + name + "_in_progress <= 1'b1;");
                        this.lines.Add("\t" + end);

                        this.FormatsForFileIO(di);

                        if (di.Loop)
                        {
                            this.lines.Add(forever);
                            this.lines.Add("\t\t\t@(posedge " + di.ClockSync.Name + ");");
                            this.lines.Add("\t\t\tif(valid_" + name + "_pre) begin");
                            this.lines.Add("\t\t\t\tif($feof(fid_" + name + ")) begin");
                            this.lines.Add("\t\t\t\t\t$fclose(fid_" + name + ");");
                            this.lines.Add("\t\t\t\t\tfid_" + name + " = $fopen(\"" + di.DataVector.Replace("\\", "/") + "\",\"r\");");
                            this.lines.Add("\t\t\t\t\tif(fid_" + name + " == 0) begin");
                            this.lines.Add("\t\t\t\t\t\t$display(\"Error opening file - could not open.\");");
                            this.lines.Add("\t\t\t\t\t\t$stop;");
                            this.lines.Add("\t\t\t\t" + end + " else begin");
                            this.lines.Add("\t\t\t\t\t\t$display(\"File " + di.DataVector.Replace("\\", "/") + " opened successfully.\");");
                            this.lines.Add("\t\t\t\t" + end);
                            this.lines.Add("\t\t\t" + end + " else if($fgets(line_" + name + ", fid_" + name + ")) begin");
                            this.lines.Add("\t\t\t\t\tvoid'($sscanf(line_" + name + ", \"" + this.formatIn + "\", " + this.dis);
                            this.lines.Add("\t\t\t" + end);
                            this.lines.Add("\t\t" + end + " else begin");
                            foreach(Port inp in di.DataIns)
                                this.lines.Add("\t\t\t\t" + inp.Name + " <= '0;");
                            this.lines.Add("\t\t" + end);
                            this.lines.Add("\t" + end);
                        }
                        else
                        {
                            this.lines.Add("\t\twhile(!$feof(fid_" + name + ")) begin");
                            this.lines.Add("\t\t\t@(posedge " + di.ClockSync.Name + ");");
                            this.lines.Add("\t\t\tif(valid_" + name + "_pre) begin");
                            this.lines.Add("\t\t\t\tif($fgets(line_" + name + ", fid_" + name + ")) begin");
                            this.lines.Add("\t\t\t\t\tvoid'($sscanf(line_" + name + ", \"" + this.formatIn + "\", " + this.dis);
                            this.lines.Add("\t\t\t\t\tnum_vlds_" + this.ModuleFile.Name + "_" + di.DataVector.Split('\\').LastOrDefault().Replace(".txt", "") + " = num_vlds_" + this.ModuleFile.Name + "_" + di.DataVector.Split('\\').LastOrDefault().Replace(".txt", "") + " + 1;");
                            this.lines.Add("\t\t\t" + end + " else begin");
                            foreach (Port inp in di.DataIns)
                                this.lines.Add("\t\t\t\t\t" + inp.Name + " <= '0;");
                            this.lines.Add("\t\t\t" + end);
                            this.lines.Add("\t\t" + end + " else begin");
                            foreach (Port inp in di.DataIns)
                                this.lines.Add("\t\t\t\t" + inp.Name + " <= '0;");
                            this.lines.Add("\t\t" + end);
                            this.lines.Add("\t" + end);
                            this.lines.Add("\t\ttest_" + name + "_in_progress <= 1'b0;");
                            this.lines.Add("\t\t$fclose(fid_" + name + ");");
                        }                                   
                                                                       
                        this.lines.Add(endtask + " : " + name);
                        this.lines.Add("");
                    }
                }
            }
        }

        private void AddValidSeqs()
        {
            if (this.ModuleFile.TestCases.Any(di => di.DataVector != null))
            {                
                if (this.ModuleFile.TestCases != null && this.ModuleFile.TestCases.Count > 0)
                {
                    List<Port> vis = new List<Port>();
                    foreach(TestCase di in this.ModuleFile.TestCases)
                    {
                        if (!vis.Any(v => v.Name == di.ValidIn.Name))
                            vis.Add(di.ValidIn);
                    }
                    this.lines.Add(generatedToAdaptComment);
                    this.lines.Add("//Always blocks for valid input signals generation - modify for your needs");
                    foreach(Port vld in vis)
                    {
                        List<TestCase> tcs = (from TestCase tc in this.ModuleFile.TestCases
                                             where tc.ValidIn.Name == vld.Name
                                             select tc).ToList<TestCase>();
                        tcs = tcs.OrderBy(t => t.Order).ToList();
                        this.lines.Add(always + tcs.FirstOrDefault().ClockSync.Name + ") begin");
                        foreach (TestCase di in tcs)
                        {
                            if (di.DataVector != null && di.DataIns.FirstOrDefault() != null)
                            {
                                string name = di.DataIns.FirstOrDefault().Name + "_" + di.DataVector.Split('\\').LastOrDefault().Replace(".txt", "");
                                int len = this.SeqtoSVString(di.VldSeq).Length;
                                this.lines.Add("\t\tif(!test_" + name + "_in_progress) begin");
                                this.lines.Add("\t\t\tvalid_" + name + "_srl <= valid_" + name + "_seq;");
                                this.lines.Add("\t" + end);
                            }
                        }
                        foreach (TestCase di in tcs)
                        {
                            if (di.DataVector != null && di.DataIns.FirstOrDefault() != null)
                            {
                                string name = di.DataIns.FirstOrDefault().Name + "_" + di.DataVector.Split('\\').LastOrDefault().Replace(".txt", "");
                                int len = this.SeqtoSVString(di.VldSeq).Length;
                                if (di == tcs.FirstOrDefault())
                                    this.lines.Add("\t\tif(test_" + name + "_in_progress) begin");
                                else
                                    this.lines.Add("\t\tend else if(test_" + name + "_in_progress) begin");
                                this.lines.Add("\t\t\tvalid_" + name + "_srl <= {valid_" + name + "_srl[" + (len - 2) + ":0], valid_" + name + "_srl[" + (len - 1) + "]};");
                                this.lines.Add("\t\t\t" + di.ValidIn.Name + " <= valid_" + name + "_srl[" + (len - 1) + "];");
                            }
                        }
                        this.lines.Add("\t" + end + " else begin");
                        this.lines.Add("\t\t\t" + vld.Name + " <= '0;");
                        this.lines.Add("\t" + end);
                        this.lines.Add(end);
                        this.lines.Add("");
                    }
                    #region commentedOut prevVer
                    //Port vi = this.ModuleFile.TestCases.FirstOrDefault().ValidIn;
                    //if (this.ModuleFile.TestCases.All(t => t.ValidIn == vi))
                    //{
                    //    this.lines.Add(always + this.ModuleFile.TestCases.FirstOrDefault().ClockSync.Name + ") begin");
                    //    foreach (TestCase di in this.ModuleFile.TestCases)
                    //    {
                    //        if (di.DataVector != null && di.DataIns.FirstOrDefault() != null)
                    //        {
                    //            string name = di.DataIns.FirstOrDefault().Name + "_" + di.DataVector.Split('\\').LastOrDefault().Replace(".txt", "");
                    //            int len = this.SeqtoSVString(di.VldSeq).Length;
                    //            this.lines.Add("\t\tif(!test_" + name + "_in_progress) begin");
                    //            this.lines.Add("\t\t\tvalid_" + name + "_srl <= valid_" + name + "_seq;");
                    //            this.lines.Add("\t" + end);
                    //        }
                    //    }
                    //    foreach (TestCase di in this.ModuleFile.TestCases)
                    //    {
                    //        if (di.DataVector != null && di.DataIns.FirstOrDefault() != null)
                    //        {
                    //            string name = di.DataIns.FirstOrDefault().Name + "_" + di.DataVector.Split('\\').LastOrDefault().Replace(".txt", "");
                    //            int len = this.SeqtoSVString(di.VldSeq).Length;
                    //            if (di == this.ModuleFile.TestCases.FirstOrDefault())
                    //                this.lines.Add("\t\tif(test_" + name + "_in_progress) begin");
                    //            else
                    //                this.lines.Add("\t\tend else if(test_" + name + "_in_progress) begin");
                    //            this.lines.Add("\t\t\tvalid_" + name + "_srl <= {valid_" + name + "_srl[" + (len - 2) + ":0], valid_" + name + "_srl[" + (len - 1) + "]};");
                    //            this.lines.Add("\t\t\t" + di.ValidIn.Name + " <= valid_" + name + "_srl[" + (len - 1) + "];");
                    //        }
                    //    }
                    //    this.lines.Add("\t" + end + " else begin");
                    //    this.lines.Add("\t\t\t" + vi.Name + " <= '0;");
                    //    this.lines.Add("\t" + end);
                    //    this.lines.Add(end);
                    //    this.lines.Add("");
                    //}
                    //else
                    //{
                    //    foreach (TestCase di in this.ModuleFile.TestCases)
                    //    {
                    //        if (di.DataVector != null && di.DataIns.FirstOrDefault() != null)
                    //        {
                    //            string name = di.DataIns.FirstOrDefault().Name + "_" + di.DataVector.Split('\\').LastOrDefault().Replace(".txt", "");

                    //            int len = this.SeqtoSVString(di.VldSeq).Length;

                    //            this.lines.Add(always + di.ClockSync.Name + ") begin");
                    //            this.lines.Add("\t\tif(!test_" + name + "_in_progress) begin");
                    //            this.lines.Add("\t\t\tvalid_" + name + "_srl <= valid_" + name + "_seq;");
                    //            this.lines.Add("\t" + end + " else begin");
                    //            this.lines.Add("\t\t\tvalid_" + name + "_srl <= {valid_" + name + "_srl[" + (len - 2) + ":0], valid_" + name + "_srl[" + (len - 1) + "]};");
                    //            this.lines.Add("\t\t\t" + di.ValidIn.Name + " <= valid_" + name + "_srl[" + (len - 1) + "];");
                    //            this.lines.Add("\t" + end);

                    //            this.lines.Add(end);
                    //            this.lines.Add("");
                    //        }
                    //    }
                    //}
                    #endregion
                }
            }
        }

        #endregion

        public FileGen(ModuleFile moduleFile, string tbFileName)
        {
            this.ModuleFile = moduleFile;
            this.TbFileName = tbFileName;
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
            this.AddInitialBlock();
            this.AddValidSeqs();
            this.AddFileInput();
            this.AddFileOutput();
            this.AddFooter();

            //put code in tb file
            return this.SaveFile();
        }
    }
}
