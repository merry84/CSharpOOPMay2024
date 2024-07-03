namespace Shapes
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            /*o	height and width for Rectangle
            o	radius for Circle
            */
            double height = double.Parse(Console.ReadLine());
            double width = double.Parse(Console.ReadLine());

            double radius = double.Parse(Console.ReadLine());
            Shape rectangle = new Rectangle(height, width);
            Shape circle = new Circle(radius);

            Console.WriteLine($"{rectangle.CalculatePerimeter():f2}");
            Console.WriteLine($"{rectangle.CalculateArea():f2}");
            Console.WriteLine($"{rectangle.Draw()}");

            Console.WriteLine($"{circle.CalculatePerimeter():f2}");
            Console.WriteLine($"{circle.CalculateArea():f2}");
            Console.WriteLine($"{circle.Draw()}");
        }
    }
}
