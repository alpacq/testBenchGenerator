namespace FPGADeveloperTools.Common.Model.Ports
{
    public class Parameter
    {
        private string name;
        private string value;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public Parameter(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
