using FPGADeveloperTools.Common.Model.ModuleFiles;

namespace FPGADeveloperTools.Common.ViewModel.ModuleFiles
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
