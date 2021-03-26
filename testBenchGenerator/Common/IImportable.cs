using System.Windows.Input;

namespace FPGADeveloperTools.Common
{
    public interface IImportable
    {
        bool CanImport { get; }

        ICommand ImportCommand { get; set; }

        void Import(object obj);
    }
}
