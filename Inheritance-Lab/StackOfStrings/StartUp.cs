namespace CustomStack
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            StackOfStrings stack = new ();
            Console.WriteLine(stack.IsEmpty());
            for (int i = 0; i < 5; i++)
            {
                stack.Push(i.ToString());
            }
            Console.WriteLine(stack.IsEmpty());
            Console.WriteLine(stack.Count);
            StackOfStrings stackString = new ();
            stackString.AddRange(stack);

        }
    }
}
