﻿namespace Stealer
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            Spy spy = new Spy();
            string result = spy.CollectGetterAndSetter("Stealer.Hacker");
            Console.WriteLine(result);

        }
    }
}
