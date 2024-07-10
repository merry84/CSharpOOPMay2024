namespace SquareRoot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numer = int.Parse(Console.ReadLine());
            try
            {
                if (numer < 0)
                {
                    throw new ArgumentException("Invalid number.");
                }
                else { Console.WriteLine(Math.Sqrt(numer)); }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            finally
            {
                Console.WriteLine("Goodbye.");
            }
        }
    }
}
