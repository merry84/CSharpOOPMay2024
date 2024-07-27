using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Repositories.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private IRepository<ILoan> loans;
        private IRepository<IBank> banks;
        public Controller()
        {
            banks = new BankRepository();
            loans = new LoanRepository();
        }
        public string AddBank(string bankTypeName, string name)
        {
            /*Creates a bank from the appropriate type and adds it to the BankRepository.
             If the bankTypeName is an invalid type in the application, 
            throw an ArgumentException with the following message:
             •	"Invalid bank type."
             If the Bank is added successfully, the method should return the following string:
             •	"{bankTypeName} is successfully added."
             */
            IBank bank;
            if (bankTypeName == nameof(BranchBank))
            {
                bank = new BranchBank(name);
            }
            else if (bankTypeName == nameof(CentralBank))
            {
                bank = new CentralBank(name);
            }
            else
            {
                return string.Format(ExceptionMessages.BankTypeInvalid);
            }
            banks.AddModel(bank);
            return string.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            /*•If the given clientTypeName is not recognized as a valid type in the application, 
             * the method should throw an ArgumentException with the following message:
              "Invalid client type."
              •	Select from the BankRepository the bank with the given bankName.
              o	If the given clientTypeName is NOT a valid client type for the selected bank,
            the following message is returned: 
              "Unsuitable bank."
              o	Otherwise creates and adds client from the appropriate type to the Bank with the given name.
            The following message should be returned: 
              "{clientTypeName} successfully added to {bankName}."
              */
            IClient client;
            if (clientTypeName == nameof(Adult))
            {
                client = new Adult(clientName, id, income);
            }
            else if (clientTypeName == nameof(Student))
            {
                client = new Student(clientName, id, income);
            }
            else
            {
                return string.Format(ExceptionMessages.ClientTypeInvalid);
            }

            IBank bank = banks.FirstModel(bankName);
            if ((bank.GetType().Name == nameof(BranchBank) && clientTypeName != nameof(Student)) ||
                (bank.GetType().Name == nameof(CentralBank) && clientTypeName != nameof(Adult)))
            {
                return string.Format(OutputMessages.UnsuitableBank);
            }

            bank.AddClient(client);
            return string.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);
        }



        public string AddLoan(string loanTypeName)
        {
            /*Creates a loan from the appropriate type and adds it to the LoanRepository. 
                If the loanTypeName is an invalid type in the application,
            throw an ArgumentException with the following message:
                •	"Invalid loan type."
                If the Loan is added successfully, the method should return the following string:
                •	"{loanTypeName} is successfully added."
                */
            ILoan loan;
            if (loanTypeName == nameof(MortgageLoan))
            {
                loan = new MortgageLoan();
            }
            else if (loanTypeName == nameof(StudentLoan))
            {
                loan = new StudentLoan();
            }
            else
            {
                return string.Format(ExceptionMessages.LoanTypeInvalid);
            }
            loans.AddModel(loan);
            return string.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);

        }

        public string FinalCalculation(string bankName)
        {
            /*Calculates all funds that have passed through the Bank with the given name.
             * It is calculated from the sum of all income from clients and amount from loans in the Bank.
             Return a string in the following format:
             •	"The funds of bank {bankName} are {funds}."
             o	The funds should be formatted to the 2nd decimal place!
             */
            IBank bank = banks.Models.FirstOrDefault(x => x.Name == bankName);
            var clientsSum = bank.Clients.Sum(x => x.Income);
            var loanSum = bank.Loans.Sum(x => x.Amount);
            var funds = (clientsSum + loanSum).ToString("0.00");
            return $"The funds of bank {bankName} are {funds}.";
        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            /*Adds the appropriate ILoan, returned by a client, to the Bank with the given name.
             * You have to remove the Loan from the LoanRepository if the insert is successful.
             It is important to note that the bank referenced by the bankName parameter 
            will always exist in the BankRepository. 
            Therefore, you can assume that the specified bank is valid and present.
             If there is no such loan, you have to throw an ArgumentException with the following message:
             •	"Loan of type {loanTypeName} is missing."
             If no exceptions are thrown, return the String:
             •	"{loanTypeName} successfully added to {bankName}."
             */
            ILoan loan = loans.FirstModel(loanTypeName);
            if (loan == null)
            {
                return string.Format(ExceptionMessages.MissingLoanFromType, loanTypeName);
            }
            IBank bank = banks.FirstModel(bankName);
            bank.AddLoan(loan);
            loans.RemoveModel(loan);
            return string.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName, bankName);
        }

        public string Statistics()
        {
            var sb = new StringBuilder();

            foreach (var bank in this.banks.Models)
            {
                sb.AppendLine(bank.GetStatistics());
            }

            return sb.ToString().TrimEnd();

        }
    }
}
