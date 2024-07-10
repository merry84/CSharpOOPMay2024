namespace MoneyTransactions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //"You will receive on the first line a collection of bank accounts,
            //consisting of an account number (integer) and its balance (double),
            //in the following format:{account number}-{account balance},{account number}-{account balance},…"

            /*After that, until the "End" command, you will receive commands, 
             * which should manipulate the given account`s balance:
         •	"Deposit {account number} {sum}" – Add the given sum to the given account`s balance. 
         •	"Withdraw {account number} {sum}" – Subtract the given sum from the account`s balance.
         Print the following messages from the exceptions which can be produced from your program:
         */
            /*1-45.67,2-3256.09,3-97.34
           Deposit 1 50
           Withdraw 3 100
           End
           */
            Dictionary<int, double> bankAccounts = new();
            string[] infoAccounts = Console.ReadLine()
                .Split(",", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < infoAccounts.Length; i++)
            {
                string[] data = infoAccounts[i].Split("-", StringSplitOptions.RemoveEmptyEntries);
                int accountNumber = int.Parse(data[0]);
                double accountBalance = double.Parse(data[1]);
                bankAccounts.Add(accountNumber, accountBalance);
            }
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string command = tokens[0];
                int bankAccount = int.Parse(tokens[1]);
                double money = double.Parse(tokens[2]);
                try
                {
                    if (command == "Deposit")
                    {
                        bankAccounts[bankAccount] += money;
                        Console.WriteLine($"Account {bankAccount} has new balance: {bankAccounts[bankAccount]:f2}");
                    }
                    else if (command == "Withdraw")
                    {
                        //•	If you receive the "Withdraw" command with the sum, which is bigger than the balance:
                        //"Insufficient balance!"
                        if (bankAccounts[bankAccount] < money)
                        {
                            throw new InvalidOperationException("Insufficient balance!");

                        }

                        bankAccounts[bankAccount] -= money;
                        Console.WriteLine($"Account {bankAccount} has new balance: {bankAccounts[bankAccount]:f2}");
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid command!");
                    }
                }
                catch (InvalidOperationException io)
                {
                    Console.WriteLine(io.Message);
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("Invalid account!");
                }
                finally
                {
                    Console.WriteLine("Enter another command");
                }

            }
        }
    }
}
