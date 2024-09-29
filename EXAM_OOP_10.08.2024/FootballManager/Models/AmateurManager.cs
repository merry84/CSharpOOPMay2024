﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Models
{
    public class AmateurManager : Manager
    {
        //It has an initial ranking value of 15.
        //Amateur manager will multiply the value of the parameter passed to the RankingUpdate() by 0.75 whether it is a positive or negative number.
        public AmateurManager(string name) 
            : base(name, 15)
        {
        }

        public override void RankingUpdate(double updateValue)
            => Ranking += updateValue * 0.75;

    }
}
