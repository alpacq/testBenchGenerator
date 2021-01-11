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

        public ModuleFile ModuleFile
        {
            get { return this.moduleFile; }
            set { this.moduleFile = value; }
        }

        public GeneratorViewModel(string modulePath)
        {
            this.ModuleFile = new ModuleFile(modulePath);
        }

        public bool GenerateFile()
        {
            string tbFilePath = this.ModuleFile.Path.Replace(".sv", "_tb.sv");

            FileGen file = new FileGen(this.ModuleFile, tbFilePath);

            return file.GenerateFile();
        }
    }
}
