using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class Goalkeeper : Player
    {
        /*It has initial rating value of 2.5.
        Goalkeeper will IncreaseRating() by 0.75 and DecreaseRating() by 1.25.
        The Constructor of the Goalkeeper should take the following parameters upon initialization:
        string name
        */
        private const double rating = 2.5;
        private const double increaseRating = 0.75;
        private const double decreaseRating = 1.25;
        public Goalkeeper(string name) : base(name, rating)
        {
        }

        public override void DecreaseRating()
        {
            // The minimum value of the Rating is 1, do not drop below. 
            base.Rating-=decreaseRating;

            if (base.Rating < 1)
            { base.Rating = 1; }
        }

        public override void IncreaseRating()
        {
            // The maximum value of the Rating is 10, do not exceed it. 
           base.Rating +=increaseRating;

            if (base.Rating > 10)
            { base.Rating = 10; }
        }
    }
}
