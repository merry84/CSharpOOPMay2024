﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Models
{
    public class Williams : FormulaOneCar
    {
        public Williams(string model, int horsepower, double engineDisplacement)
                : base(model, horsepower, engineDisplacement)
        {
        }
    }
}
