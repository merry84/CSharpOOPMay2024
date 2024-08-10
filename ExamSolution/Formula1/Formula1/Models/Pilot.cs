using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Formula1.Models.Contracts;
using Formula1.Utilities;

namespace Formula1.Models
{
    public class Pilot : IPilot
    {
        private string fullName;
        private IFormulaOneCar car;
        private int wins;
        public Pilot(string fullName)
        {
            FullName = fullName;
            CanRace = false;
        }

        //o	If the pilot's full name is null, white space or the length is less than 5 symbols,
        //throw an ArgumentException with the  message: "Invalid pilot name: { fullName }."
        public string FullName
        {
            get=>fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    string.Format(ExceptionMessages.InvalidPilot, value);
                }
                fullName = value;
            }
        }
        public IFormulaOneCar Car
        {
            get => car;
            private set
            {
                if (value == null)
                {
                    throw new NullReferenceException(ExceptionMessages.InvalidCarForPilot);
                }
                car = value;
            }
        }
        public int NumberOfWins
        {
            get => wins;
            private set => wins = value;
        }

        public bool CanRace { get; private set; }
        public void AddCar(IFormulaOneCar car)
        {
           
            //Sets a car to the pilot, and sets CanRace to true.
            Car = car;
            CanRace = true;
        }

        //The WinRace method increases the NumberOfWins by one (1) every time a pilot wins a race.
        public void WinRace()
            => NumberOfWins++;

        public override string ToString()
       =>$"Pilot {FullName} has {NumberOfWins} wins.";
    }
}
