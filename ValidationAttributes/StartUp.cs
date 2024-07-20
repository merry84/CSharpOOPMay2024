using System;

namespace ValidationAttributes
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var person = new Person
             (
                 null,
                 -1
             );
            var person1 = new Person
            (
                "Pesho",
                12
            );
            var person2 = new Person
           (
               "Pesho",
               91
           );

            bool isValidEntity = Validator.IsValid(person);
            bool isValidEntity1 = Validator.IsValid(person1);
            bool isValidEntity2 = Validator.IsValid(person2);

            Console.WriteLine(isValidEntity);
            Console.WriteLine(isValidEntity1);
            Console.WriteLine(isValidEntity2);
        }
    }
}
