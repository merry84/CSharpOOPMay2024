using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationAttributes.Attributes
{
    public class MyRangeAttribute : MyValidationAttribute
    {
        /*•	Its constructor should accept two parameters - int minValue, int maxValue, 
         * which represent a range of integer numbers
        •	It should contain two fields: int minValue and int maxValue
        •	It should implement the bool IsValid(object obj) method and its
        logic should validate whether the passed object obj parameter is within the set range
        */
        private int minValue = 12;
        private int maxValue = 90;
        public MyRangeAttribute(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
        public override bool IsValid(object obj)
            => (int)obj >= minValue && (int)obj <= maxValue;

    }
}
