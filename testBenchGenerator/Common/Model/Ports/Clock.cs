﻿namespace FPGADeveloperTools.Common.Model.Ports
{
    public class Clock : Port
    {
        private double frequency;

        public double Frequency
        {
            get { return this.frequency; }
            set { this.frequency = value; }
        }

        public Clock(string name, double freq = 0.0) : base(name)
        {
            this.Frequency = freq;
        }

        public Clock(Port port, double freq = 0.0) : base(port.Name, port.Bitwidth)
        {
            this.Frequency = freq;
        }

        public override string ToString()
        {
            return this.Name + " (" + this.Frequency + " MHz)";
        }
    }
}