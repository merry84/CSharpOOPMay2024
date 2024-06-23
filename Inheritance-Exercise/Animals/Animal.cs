using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Animal
    {
        private string name;
        private int age;
        private string gender;

        public Animal(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Invalid input!");

                }
                name = value;
            }
        }
        public int Age
        {
            get => age;
            private set
            {
                if (value < 0) { throw new ArgumentException("Invalid input!"); }
                age = value;
            }
        }
        public string Gender
        {
            get => gender;
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException("Invalid input!"); }
                gender = value;
            }
        }
        public virtual string AnimalType => default;

        public virtual string ProduceSound()
        => "";
        /*•	Print the information for each animal on three lines. On the first line, print: "{AnimalType}"
        •	On the second line print: "{Name} {Age} {Gender}"
        •	On the third line print the sounds it produces: "{ProduceSound()}"
        */
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{AnimalType}");
            sb.AppendLine($"{Name} {Age} {Gender}");
            sb.AppendLine($"{ProduceSound()}");
            return sb.ToString().TrimEnd();
        }
    }
}
