using FPGADeveloperTools.Common.Model.ModuleFiles;

namespace FPGADeveloperTools.Common.ViewModel.ModuleFiles
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
