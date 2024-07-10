namespace Cards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Card> list = new List<Card>();
            string[] elements = Console.ReadLine().Split(", ");

            for (int i = 0; i < elements.Length; i++)
            {
                string face = elements[i].Split().First();
                string suit = elements[i].Split().Last();

                try
                {
                    list.Add(CardCreation(face, suit));
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
            Console.WriteLine(string.Join(" ", list));
        }
        private static Card CardCreation(string face, string suit)
        {
            string[] validFace = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            string[] validSuit = new string[] { "S", "H", "D", "C" };

            if (!validFace.Contains(face) || !validSuit.Contains(suit))
            {
                throw new ArgumentException("Invalid card!");
            }
            /*Use the following UTF code literals to represent the suits:
            •	\u2660 – Spades (♠)
            •	\u2665 – Hearts (♥)
            •	\u2666 – Diamonds (♦)
            •	\u2663 – Clubs (♣)
            */
            string utf = "";

            if (suit == "S") utf = "\u2660";
            else if (suit == "H") utf = "\u2665";
            else if (suit == "D") utf = "\u2666";
            else if (suit == "C") utf = "\u2663";
            return new Card(face, utf);
        }
    }
    public class Card
    {
        public Card(string face, string suit)
        {
            Face = face;
            Suit = suit;
        }

        public string Face { get; set; }
        public string Suit { get; set; }
        public override string ToString() => $"[{Face}{Suit}]";
    }
}
