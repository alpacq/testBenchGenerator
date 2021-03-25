namespace FPGADeveloperTools.Common
{
    public interface IExportable
    {
        bool CanExport { get; }

        void ExportTxt(string path);
    }
}
