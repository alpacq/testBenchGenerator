﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Model;

namespace testBenchGenerator.ViewModel
{
    public class ClockViewModel : PortViewModel, INotifyPropertyChanged
    {
        private Clock clock;

        public Clock Clock
        {
            get { return this.clock; }
        }

        public double Frequency
        {
            get { return this.Clock.Frequency; }
            set { this.Clock.Frequency = value; OnPropertyChanged("Frequency"); }
        }

        public ClockViewModel(Clock clock) : base((Port)clock)
        {
            this.clock = clock;
        }
    }
}