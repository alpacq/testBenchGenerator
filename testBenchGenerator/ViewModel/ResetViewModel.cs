﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testBenchGenerator.Model;

namespace testBenchGenerator.ViewModel
{
    public class ResetViewModel : PortViewModel, INotifyPropertyChanged
    {
        private Reset reset;

        public Reset Reset
        {
            get { return this.reset; }
        }

        public bool Polarization
        {
            get { return this.Reset.Polarization; }
            set { this.Reset.Polarization = value; OnPropertyChanged("Polarization"); }
        }

        public ResetViewModel(Reset reset) : base((Port)reset)
        {
            this.reset = reset;
        }
    }
}