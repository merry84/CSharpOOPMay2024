namespace Operations
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            MathOperations operations = new MathOperations();

            Console.WriteLine(operations.Add(2, 4));
            Console.WriteLine(operations.Add(2.50, 3.25, 6.93));
            Console.WriteLine(operations.Add(2.2m, 4.69m, 3.56m));
        }
    }
}
