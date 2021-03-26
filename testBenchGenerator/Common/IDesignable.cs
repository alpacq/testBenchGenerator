using System.Windows.Input;

namespace FPGADeveloperTools.Common
{
    public interface IDesignable
    {
        bool CanDesign { get; }

        ICommand DesignCommand { get; set; }

        void Design(object obj);
    }
}
