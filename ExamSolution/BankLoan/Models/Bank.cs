using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private int capacity;
        private List<ILoan> loans;
        private List<IClient> clients;

        protected Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            loans = new List<ILoan>();
            clients = new List<IClient>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.BankNameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public int Capacity
        {
            get => capacity;
            private set
            {
                capacity = value;
            }

        }

        public IReadOnlyCollection<ILoan> Loans => loans.AsReadOnly();

        public IReadOnlyCollection<IClient> Clients => clients.AsReadOnly();

        public void AddClient(IClient Client)
        {
            /*Adds a Client in the Bank if there is a capacity for it.
            If there is not enough capacity to add the Client to the Bank, 
            throw an ArgumentException with the following message:
            •	"Not enough capacity for this client."  */
            if (Capacity < clients.Count)
            {
                string.Format(ExceptionMessages.NotEnoughCapacity);
            }
            clients.Add(Client);
        }

        public void AddLoan(ILoan loan)
            => loans.Add(loan);

        public string GetStatistics()
        {
            /*Returns a string with information about the Bank in the format below. 
                "Name: {bankName}, Type: {bankTypeName}
                Clients: {clientName1}, {clientName2} ... / Clients: none
                Loans: {loansCount}, Sum of Rates: {sumOfInterestRates}"
                */
            var sb = new StringBuilder();//??
            sb.AppendLine($"Name: {Name}, Type: {GetType().Name}");
            sb.Append($"Clients: ");
            {
                if (clients.Count == 0)
                {
                    sb.AppendLine("none");
                }
                else
                {
                    sb.AppendLine(string.Join(", ", clients.Select(x => x.Name)));
                }
            }
            sb.AppendLine($"Loans: {loans.Count}, Sum of Rates: {SumRates()}");
            return sb.ToString().TrimEnd();
        }

        public void RemoveClient(IClient Client)
             => clients.Remove(Client);

        public double SumRates()//??
              => loans.Sum(x => x.InterestRate);
    }
}
