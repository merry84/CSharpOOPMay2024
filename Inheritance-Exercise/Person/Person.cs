using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    public class Person
    {
       
        private int age;
        public virtual string Name { get; set; }
        public virtual int Age
        {
            get => age; set
            {
                if (value > 0) age = value;
            }

        }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
        
        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }
}
