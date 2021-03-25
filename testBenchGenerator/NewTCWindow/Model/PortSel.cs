using FPGADeveloperTools.Common.Model.Ports;

namespace FPGADeveloperTools.NewTCWindow.Model
{
    public class PortSel
    {
        private Port port;
        private bool isSel;

        public Port Port
        {
            get { return this.port; }
        }

        public string Name
        {
            get { return this.port.Name; }
        }

        public bool IsSel
        {
            get { return this.isSel; }
            set { this.isSel = value; }
        }

        public PortSel(Port port)
        {
            this.port = port;
            this.isSel = false;
        }
    }
}
