namespace FPGADeveloperTools.Common
{
    public interface IDesignable
    {
        bool CanDesign { get; }

        void Design();
    }
}
