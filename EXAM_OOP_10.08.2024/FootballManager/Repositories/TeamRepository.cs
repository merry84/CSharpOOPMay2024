using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballManager.Models.Contracts;
using FootballManager.Repositories.Contracts;

namespace FootballManager.Repositories
{
    public class TeamRepository :IRepository<ITeam>
    {
        private List<ITeam> teams;

        public TeamRepository()
        {
            teams = new List<ITeam>();
        }
        public IReadOnlyCollection<ITeam> Models => teams.AsReadOnly();
        public int Capacity { get; } = 10;
        public void Add(ITeam model)
        {
            if (Capacity <= teams.Count)
            {
                return;
            }
            teams.Add(model);
        }

        public bool Remove(string name)
            => teams.Remove(teams.FirstOrDefault(x => x.Name == name));

        public bool Exists(string name)
            => teams.Any(x => x.Name == name);

        public ITeam Get(string name)
            => teams.FirstOrDefault(x => x.Name == name);
    }
}
