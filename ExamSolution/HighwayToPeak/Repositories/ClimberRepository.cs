using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Repositories
{
    public class ClimberRepository : IRepository<IClimber>
    {
        private List<IClimber> list = new();
        public IReadOnlyCollection<IClimber> All => list;

        public void Add(IClimber model)
        =>list.Add(model);

        public IClimber Get(string name)
       => list.FirstOrDefault(x => x.Name == name);
    }
}
