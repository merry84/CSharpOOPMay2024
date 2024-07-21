using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories.Contracts;

namespace TheContentDepartment.Repositories
{
    public class MemberRepository : IRepository<ITeamMember>
    {
        private List<ITeamMember> members;
        public MemberRepository()
        {
            members = new List<ITeamMember>();
        }
        public IReadOnlyCollection<ITeamMember> Models => members.AsReadOnly();

        public void Add(ITeamMember model)
        {
            if (!members.Contains(model))
            {
                members.Add(model);
            }
        }

        public ITeamMember TakeOne(string modelName)
        => members.FirstOrDefault(x => x.Name == modelName);
    }
}
