namespace FPGADeveloperTools.Common
{
    public interface IImportable
    {
        bool CanImport { get; }

        void Import();
    }
}
