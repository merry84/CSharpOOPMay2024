using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Contracts
{
    public interface ISolder
    {
        int Id { get; set; }
        string FirstName {  get; set; }
        string LastName { get; set; }
    }
}
