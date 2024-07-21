using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public abstract class Diver : IDiver
    {
        private string name;
        private int oxigenLevel;
        private List<string> _catch;
        private double competitionPoints;
        private bool hasHealthIssues;

        protected Diver(string name, int oxygenLevel)
        {
            Name = name;
            OxygenLevel = oxygenLevel;
            _catch = new List<string>();
            competitionPoints = 0;
            hasHealthIssues = false;
        }

        public string Name
        {
            get
            => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.DiversNameNull);
                }
                name = value;
            }
        }

        public int OxygenLevel
        {
            get
            => oxigenLevel;
            protected set
            {
                if(value< 0)
                {
                    oxigenLevel = 0;
                }
                oxigenLevel = value;
            }
        }

        public IReadOnlyCollection<string> Catch => _catch.AsReadOnly();
        //o	Set the initial value of the property to zero. Returns a floating-point number rounded to the first decimal place.
        //Represents the total points accumulated by a diver, based on the type of fish caught during the competition. 
        public double CompetitionPoints => Math.Round(competitionPoints,1);

        public bool HasHealthIssues => hasHealthIssues;

        public  void Hit(IFish fish)
        {
            OxygenLevel -= fish.TimeToCatch;
            _catch.Add(fish.Name);
            competitionPoints += Math.Round(fish.Points, 1, MidpointRounding.AwayFromZero);
            //!!
        }

        //The Miss() is an abstract method that should decrease the diver's OxygenLevel property.
        //When the method is invoked the diver's OxygenLevel is decreased by a certain value,
        //that will depend on the fish that is chased.
        //OxygenLevel -= (int) Math.Round(…, MidpointRounding.AwayFromZero);
        public abstract void Miss(int TimeToCatch);
        //It should be abstract method. The diver's OxygenLevel should be fully replenished to its original or maximum value.
        //This would mean setting the OxygenLevel back to its starting value depending on the diver’s type.
        public abstract void RenewOxy();

        public void UpdateHealthStatus()
        => hasHealthIssues = !hasHealthIssues;
        public override string ToString()
        {
            return $"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {Catch.Count}, Points earned: {CompetitionPoints} ]";
        }
    }
}
