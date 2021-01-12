using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Model;

namespace testBenchGenerator.ViewModel
{
    public class GeneratorViewModel
    {
        private ModuleFile moduleFile;
        private InputFile inputFile;

        public ModuleFile ModuleFile
        {
            get { return this.moduleFile; }
            set { this.moduleFile = value; }
        }

        public InputFile InputFile
        {
            get { return this.inputFile; }
            set { this.inputFile = value; }
        }

        public GeneratorViewModel(string modulePath, string inputPath = null)
        {
            if(modulePath != null && modulePath != String.Empty)
                this.ModuleFile = new ModuleFile(modulePath);
            if (inputPath != null && inputPath != String.Empty)
                this.InputFile = new InputFile(inputPath);
        }

        public bool GenerateFile()
        {
            string tbFilePath = this.ModuleFile.Path.Replace(".sv", "_tb.sv");

            FileGen file = (this.InputFile != null) ? new FileGen(this.ModuleFile, tbFilePath, this.InputFile) : new FileGen(this.ModuleFile, tbFilePath);

            return file.GenerateFile();
        }
    }
}
