namespace ShoppingSpree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> peoples = new();
            List<Product> products = new();
            try
            {
                string[] peopleInfo = Console.ReadLine()
                    .Split(";", StringSplitOptions.RemoveEmptyEntries);
                foreach (string element in peopleInfo)
                {
                    string[] components = element.Split("=", StringSplitOptions.RemoveEmptyEntries);
                    Person person = new(components[0], int.Parse(components[1]));
                    peoples.Add(person);
                }

                string[] productsInfo = Console.ReadLine()
                    .Split(";", StringSplitOptions.RemoveEmptyEntries);
                foreach (string element in productsInfo)
                {
                    string[] components = element.Split("=", StringSplitOptions.RemoveEmptyEntries);
                    Product product = new(components[0], int.Parse(components[1]));
                    products.Add(product);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] personEndProduct = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string peopleName = personEndProduct[0];
                string productName = personEndProduct[1];
                Person person = peoples.FirstOrDefault(n => n.Name == peopleName);
                Product product = products.FirstOrDefault(p => p.Name == productName);

                if (person is not null && product is not null)
                {
                    Console.WriteLine(person.AddProduct(product));
                }
            }
            foreach (var person in peoples)
            {
                if (person.Products.Count > 0)
                {
                    Console.WriteLine($"{person.Name} - {string.Join(", ", person.Products)}");
                }
                else
                {
                    Console.WriteLine($"{person.Name} - Nothing bought ");
                }
            }

        }
    }
}
