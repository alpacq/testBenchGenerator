using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.TestbenchGenerator.Model
{
    public class VHDLModuleFile : ModuleFile
    {
        public VHDLModuleFile(string path) : base(path)
        {

        }

        protected override void Read()
        {

        }

        protected override void ReadInputs()
        {
            throw new NotImplementedException();
        }

        protected override void ReadOutputs()
        {
            throw new NotImplementedException();
        }

        protected override void ReadParameters()
        {
            throw new NotImplementedException();
        }

        protected override void RecognizeClocks()
        {
            throw new NotImplementedException();
        }

        protected override void RecognizeIns()
        {
            throw new NotImplementedException();
        }

        protected override void RecognizeResets()
        {
            throw new NotImplementedException();
        }
    }
}
