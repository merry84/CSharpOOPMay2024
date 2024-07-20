using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationAttributes.Attributes
{
    public abstract class MyValidationAttribute : Attribute
    {
        //Create a validation attribute: MyValidationAttribute. Its purpose is to validate properties. 
        //It should contain the following method: public abstract bool IsValid(object obj)

        public abstract bool IsValid(object obj);
    }
}
