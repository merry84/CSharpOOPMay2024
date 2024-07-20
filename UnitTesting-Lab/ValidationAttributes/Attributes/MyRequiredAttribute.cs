using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationAttributes.Attributes
{
    public class MyRequiredAttribute : MyValidationAttribute
    {
        //•	It should implement the bool IsValid(object obj)
        //method and its logic should validate whether a property has the attribute or not
        public override bool IsValid(object obj)
        => obj is not null;
    }
}
