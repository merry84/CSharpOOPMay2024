using WildFarm.Models.Animals;

namespace WildFarm
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*Mouse Jerry 0.5 Anywhere
              Fruit 1000
              Owl Tom 2.5 30
              Meat 5
              End
              */
            List<Animal> animals = new ();
            string input;

            while((input = Console.ReadLine()) != "End")
            {
                string[] animalsInfo = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string[] foodInfo = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string animalType = animalsInfo[0];
                string animalName = animalsInfo[1];
                double weight = double.Parse(animalsInfo[2]);

                string foodName = foodInfo[0];
                int quantity = int.Parse(foodInfo[1]);
                /*•	Hen   •	Owl 
                •	Mouse •	Cat
                •	Dog   •	Tiger 
                */
                try
                {
                    if(animalType == "Cat")
                    {
                        Cat cat = new (animalName, weight, animalsInfo[3], animalsInfo[4]);
                        Console.WriteLine(cat.ProduseSound());
                        animals.Add(cat);
                        cat.Eaten(foodName, quantity);
                    }
                    else if(animalType == "Tiger")
                    {
                        Tiger tiger = new(animalName, weight, animalsInfo[3], animalsInfo[4]);
                        Console.WriteLine(tiger.ProduseSound());
                        animals.Add(tiger);
                        tiger.Eaten(foodName, quantity);    
                    }
                    else if (animalType == "Dog")
                    {
                        Dog dog = new(animalName, weight, animalsInfo[3]);
                        Console.WriteLine(dog.ProduseSound());
                        animals.Add(dog);
                        dog.Eaten(foodName, quantity);
                    }
                    else if (animalType == "Owl")
                    {
                        Owl оwl = new(animalName, weight, double.Parse(animalsInfo[3]));
                        Console.WriteLine(оwl.ProduseSound());
                        animals.Add(оwl);
                        оwl.Eaten(foodName, quantity);
                    }
                    else if (animalType == "Hen")
                    {
                        Hen hen = new(animalName, weight, double.Parse(animalsInfo[3]));
                        Console.WriteLine(hen.ProduseSound());
                        animals.Add(hen);
                        hen.Eaten(foodName, quantity);
                    }
                    else if (animalType == "Mouse")
                    {
                        Mouse mouse = new(animalName, weight, animalsInfo[3]);
                        Console.WriteLine(mouse.ProduseSound());
                        animals.Add(mouse);
                        mouse.Eaten(foodName, quantity);
                    }
                }
                catch  (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}
