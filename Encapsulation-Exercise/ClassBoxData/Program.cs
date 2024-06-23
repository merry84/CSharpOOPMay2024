namespace ClassBoxData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //•	On the first three lines, you will get the length, width, and height. 
            double length = double.Parse(Console.ReadLine());
            double width = double.Parse(Console.ReadLine());
            double height = double.Parse(Console.ReadLine());
            try
            {
                Box box = new(length, width, height);
                Console.WriteLine(box);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
