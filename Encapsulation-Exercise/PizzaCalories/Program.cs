namespace PizzaCalories
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Pizza Meatless
            //Dough Wholegrain Crispy 100
            //Topping Veggies 50
            //Topping Cheese 50
            //END
            try
            {
                string pizzaName = Console.ReadLine()
                .Split()[1];
                string[] arguments = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string flour = arguments[1];
                string bakingType = arguments[2];
                double grams = double.Parse(arguments[3]);
                Dough dough = new Dough(flour, bakingType, grams);
                Pizza pizza = new Pizza(pizzaName, dough);

                string command;
                while ((command = Console.ReadLine()) != "END")
                {
                    string type = command.Split()[1];
                    double toppingGrams = double.Parse(command.Split()[2]);
                    Topping topping = new Topping(type, toppingGrams);
                    pizza.AddTopping(topping);
                }
                Console.WriteLine(pizza);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
