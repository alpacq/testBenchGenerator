﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testBenchGenerator.Model
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
    }
}
