using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories.Contracts;

namespace TheContentDepartment.Repositories
{
    public class ResourceRepository : IRepository<IResource>
    {
        private List<IResource> resources;
        public ResourceRepository()
        {
            resources = new List<IResource>();
        }
        public IReadOnlyCollection<IResource> Models => resources.AsReadOnly();

        public void Add(IResource model)
        {
            //•	Adds a new IResource to the ResourceRepository.
            resources.Add(model);
        }

        public IResource TakeOne(string modelName)
        {
            //•	Returns a resource with a Name equal to the given modelName from the collection, if there is any.
            //Otherwise, it returns null.
            return resources.FirstOrDefault(x => x.Name == modelName);

        }
    }
}
