using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationAttributes.Attributes;

namespace ValidationAttributes
{
    public class Person
    {
        /*Create a class Person. It should have a constructor, which accepts two parameters:
         * string fullName, int age.
        It should have two properties:
        •	string FullName - the property is required. Apply the MyRequiredAttribute
        •	int Age - the age should be between 12 and 90.
        Apply the MyRangeAttribute and set the right values for minimum and maximum age
        */
        private const int MinAge = 12;
        private const int MaxAge = 90;
        [MyRequired]
        public string Name { get; set; }
        [MyRange(MinAge, MaxAge)]
        public int Age { get; set; }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

    }
}
