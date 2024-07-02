using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public interface IId
    {
        string Id { get; set; }
        bool IsValidId(string fakeId);

    }
}
