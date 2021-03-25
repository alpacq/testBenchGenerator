namespace FPGADeveloperTools.Common.Model.Ports
{
    public class Port
    {
        private string name;
        private string bitwidth;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Bitwidth
        {
            get { return this.bitwidth; }
            set { this.bitwidth = value; }
        }

        public Port(string name, string bitwidth = null)
        {
            this.Name = name;
            this.Bitwidth = bitwidth;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
