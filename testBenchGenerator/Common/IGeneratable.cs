using System.Windows.Input;

namespace FPGADeveloperTools.Common
{
    public interface IGeneratable
    {
        bool CanGenerate { get; }

        ICommand GenerateCommand { get; set; }

        void Generate(object obj);
    }
}
