using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.TestbenchGenerator.Model;

namespace testBenchGenerator.TestbenchGenerator.ViewModel
{
    public class VHDLModuleFileViewModel : ModuleFileViewModel
    {
        private VHDLModuleFile moduleFile;

        public override ModuleFile ModuleFile
        {
            get { return this.moduleFile; }
        }

        public VHDLModuleFileViewModel(VHDLModuleFile moduleFile) : base()
        {
            this.moduleFile = moduleFile;
        }
    }
}
