namespace Raiding
{
    internal class Program
    {
        static void Main(string[] args)
        {
           List<BaseHero> list = new ();
            int numberOfHeroes = int.Parse(Console.ReadLine());

            while (list.Count < numberOfHeroes)
            {
                string nameHero = Console.ReadLine();
                string typeHero = Console.ReadLine();
                if(typeHero == "Druid")
                {
                    Druid druid = new(nameHero);
                    list.Add(druid);
                }
                else if(typeHero == "Warrior")
                {
                    Warrior warrior = new(nameHero);
                    list.Add(warrior);
                }
                else if (typeHero == "Rogue")
                {
                    Rogue rogue = new(nameHero);
                    list.Add(rogue);
                }
                else if(typeHero == "Paladin")
                {
                    Paladin paladin = new(nameHero);
                        list.Add(paladin);
                }

                else
                {
                    Console.WriteLine("Invalid Hero!");
                }
            }
            int bossPower = int.Parse(Console.ReadLine());

            foreach (var hero in list) 
            {
                Console.WriteLine(hero.CastAbility());
            }
            int powerAllHeroes = list.Sum(x=>x.Power);

            if(powerAllHeroes >= bossPower)
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }
        }
    }
}
