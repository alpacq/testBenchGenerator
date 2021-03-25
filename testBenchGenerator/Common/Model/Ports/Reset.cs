namespace FPGADeveloperTools.Common.Model.Ports
{
    public class Reset : Port
    {
        private bool polarization;

        public bool Polarization
        {
            get { return this.polarization; }
            set { this.polarization = value; }
        }

        public Reset(string name, bool pol = true) : base(name)
        { 
            this.Polarization = pol;
        }

        public Reset(Port port, bool pol = true) : base(port.Name, port.Bitwidth)
        {
            this.Polarization = pol;
        }
    }
}
