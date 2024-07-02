namespace FoodShortage
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*On the first line of the input you will receive an integer N - the number of people, 
             * on each of the next N lines you will receive information in one of the following formats "<name> <age> <id> <birthdate>" 
             * for a Citizen or "<name> <age><group>" for a Rebel.
             * After the N lines, until the command "End" is received, you will receive names of people who bought food, each on a new line. 
             * Note that not all names may be valid, in case of an incorrect name - nothing should happen.*/

            int numberOfPeople = int.Parse(Console.ReadLine());
            List<IBuyer> list = new List<IBuyer>();

            for (int i = 0; i < numberOfPeople; i++) 
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (input[0]== "Citizen")
                {
                    list.Add(new Citizen(input[1], int.Parse(input[2]), input[3], input[4]));
                }
                if(input[0]== "Rebel")
                {
                    list.Add(new Rebel(input[1], int.Parse(input[2]), input[3]));
                }
            }
            string action;
            while ((action = Console.ReadLine()) != "End") 
            {
               
                var buyer = list.FirstOrDefault(x=>x.Name == action);
                //The output consists of only one line on which you should print the total amount of food purchased.
                if (buyer != null)
                {
                    buyer.BuyFood();
                }
            }
            Console.WriteLine(list.Sum(x => x.Food));
        }
    }
}
