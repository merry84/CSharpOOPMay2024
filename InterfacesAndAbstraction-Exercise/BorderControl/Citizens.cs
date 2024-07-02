using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public class Citizens : IId
    {
        public Citizens(string name, int age, string id)
        {
            this.Name = name;
            this.Age = age;
            Id = id;
        }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }

        public bool IsValidId(string fakeId)
        {
            return !Id.EndsWith(fakeId);

        }
    }
}
