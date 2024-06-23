namespace CustomRandomList
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            RandomList list = new();
            list.Add("1");
            list.Add("5");
            list.Add("8");
            list.Add("2");
            list.Add("6");
            Console.WriteLine(list.RandomString());
        }
    }
}
