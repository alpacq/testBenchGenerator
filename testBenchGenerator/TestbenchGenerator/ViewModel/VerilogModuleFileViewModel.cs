using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Common;
using testBenchGenerator.TestbenchGenerator.Model;

namespace testBenchGenerator.TestbenchGenerator.ViewModel
{
    public class VerilogModuleFileViewModel : ModuleFileViewModel
    {
        private VerilogModuleFile moduleFile;        

        public override ModuleFile ModuleFile
        {
            get { return this.moduleFile; }
        }        

        public VerilogModuleFileViewModel(VerilogModuleFile moduleFile) : base()
        {
            this.moduleFile = moduleFile;
        }
    }
}
