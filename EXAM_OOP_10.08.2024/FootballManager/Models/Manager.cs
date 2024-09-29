using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballManager.Models.Contracts;
using FootballManager.Utilities.Messages;

namespace FootballManager.Models
{
    public abstract class Manager : IManager
    {
        private string name;
        private double ranking;

        protected Manager(string name,double ranking)
        {
            Name = name;
            Ranking = ranking;
        }
        public string Name
        {
            get=>name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ManagerNameNull);
                }
                name = value;
            }
        }

        public double Ranking
        {
            get => ranking;
            protected set => ranking = Math.Max(0, Math.Min(100, value)); 
        }

        public abstract void RankingUpdate(double updateValue);
        public override string ToString()
        {
            return $"{Name} - {GetType().Name} (Ranking: {Ranking:F2})";
        }
    }
}
