﻿using HighwayToPeak.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Models
{
    public class BaseCamp : IBaseCamp
    {
        private List<string> residents;
        public BaseCamp()
        {
            residents = new List<string>();
        }
        public IReadOnlyCollection<string> Residents => residents.AsReadOnly();

        public void ArriveAtCamp(string climberName)
        {
           residents.Add(climberName);
            residents = residents.OrderBy(x => x).ToList();
        }

        public void LeaveCamp(string climberName)
        => residents.Remove(climberName);
    }
}
