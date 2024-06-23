using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Kitten : Cat
    {
        public override string AnimalType => "Kitten";
        public Kitten(string name, int age) : base(name, age, "Female")
        {
        }
        public override string ProduceSound()
       => "Meow";
    }
}
