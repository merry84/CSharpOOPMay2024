using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Models
{
    public class ProfessionalManager : Manager
    {
        //It has an initial ranking value of 60.
        // Professional manager multiplies the value of the parameter passed to the RankingUpdate() by 1.5 whether it is a positive or negative number.
        // 
        public ProfessionalManager(string name)
            : base(name, 60)
        {
        }

        public override void RankingUpdate(double updateValue)
            => Ranking += updateValue * 1.5;
    }
}
