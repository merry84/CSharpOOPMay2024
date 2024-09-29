using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Models
{
    public class SeniorManager : Manager
    {
        //It has an initial ranking value of 30.
        // Senior manager will not manipulate the parameter passed to the RankingUpdate() method.
        // 
        public SeniorManager(string name) : base(name, 30)
        {
        }

        public override void RankingUpdate(double updateValue)
            => Ranking += updateValue;
    }
}
