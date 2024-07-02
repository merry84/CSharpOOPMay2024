using MilitaryElite.Contracts;

namespace MilitaryElite
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Dictionary<int, Private> privates = new();
            List<ISolder> solders = new();

            string input;
            while ((input = Console.ReadLine())!= "End")
            {
                string[] tokens = input.Split(" ",StringSplitOptions.RemoveEmptyEntries);
                string type = tokens[0];
                int id = int.Parse(tokens[1]);
                string firstName = tokens[2];
                string lastName = tokens[3];
                if(type == "Private")
                {
                    decimal salary = decimal.Parse(tokens[4]);
                    LieutenantGeneral general = new LieutenantGeneral(id,firstName,lastName,salary);

                    for (int i = 5; i < tokens.Length; i++)
                    {
                        int privateId = int.Parse(tokens[i]);
                        general.AddPrivate(privates[privateId]);
                    }
                    solders.Add(general);
                }
                if(type == "Engineer")
                {
                    decimal salary = decimal.Parse(tokens[4]);
                    string corps = tokens[5];
                    try
                    {
                        Engineer engineer = new Engineer(id, firstName, lastName, salary, corps);

                        for (int i = 6;i< tokens.Length; i += 2)
                        {
                            string partName = tokens[i];
                            int hourWorked = int.Parse(tokens[i + 1]);
                            engineer.AddRepair(new Repair(partName, hourWorked));
                        }
                        solders.Add(engineer);
                    }
                    catch(Exception ex) 
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if(type == "Commando")
                {
                    decimal salary = decimal.Parse(tokens[4]);
                    string corps = tokens[5];
                    try
                    {
                        Commando commando = new (id, firstName, lastName, salary, corps);
                        for (int i = 6; i < tokens.Length; i += 2)
                        {
                            string codeName = tokens[i];
                            string state = tokens[i + 1];
                            commando.AddMission(new Mission(codeName,state));
                        }
                        solders.Add(commando);
                    }
                    catch (Exception ex) 
                    {
                    Console.WriteLine(ex.Message);
                    }
                }
                if(type == "Spy")
                {
                    int codeNuber = int.Parse(tokens[4]);
                    solders.Add(new Spy(id,firstName,lastName, codeNuber));
                }
            }
            foreach (var s in solders)
            {
                Console.WriteLine(s);
            }
        }
    }
}
