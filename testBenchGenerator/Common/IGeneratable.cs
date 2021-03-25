namespace FPGADeveloperTools.Common
{
    public interface IGeneratable
    {
        bool CanGenerate { get; }

        bool Generate();
    }
}
