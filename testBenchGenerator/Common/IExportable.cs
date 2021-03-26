using System.Windows.Input;

namespace FPGADeveloperTools.Common
{
    public interface IExportable
    {
        bool CanExport { get; }

        ICommand ExportCommand { get; set; }

        void Export(object obj);
    }
}
