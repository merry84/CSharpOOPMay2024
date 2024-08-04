using System;
using System.Collections.Generic;
using System.Text;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        /*•	BoothId – int the booth number
           •	Capacity - int the booth capacity
            It can’t be less or equal to zero. In these cases, throw an ArgumentException with message: "Capacity has to be greater than 0!"
           •	DelicacyMenu – DelicacyRepository all available to order delicacies
           •	CocktailMenu – CocktailRepository all available to order cocktails
           •	CurrentBill – double, set initial value to zero and increase the CurrentBill after every successful order (UpdateCurrentBill method)
           •	Turnover – double, set initial value to zero the Turnover should be increased, after paying the CurrentBill upon leaving the Booth
           o	If no orders have been made to the specific Booth, return zero.
           •	IsReserved - boolean returns true if the Booth is reserved, otherwise returns false. Set its initial value to False.
           */
        private int bootId;
        private int capacity;
        private readonly IRepository<IDelicacy> delicacyMenu;
        private readonly IRepository<ICocktail>cocktailMenu;
        private double currentBill;
       
        private double turnover;

        public Booth(int bootId,int capacity)
        {
            Capacity = capacity;
            BoothId = bootId;
            delicacyMenu = new DelicacyRepository();
            cocktailMenu = new CocktailRepository();
            currentBill = 0;
            //isReserved = false;
            turnover = 0;

        }

        public int BoothId
        {
            get => bootId;
            private set
            {
                bootId = value;
            }
        }

        public int Capacity
        {
            get=>capacity;
            private set
            {
                // It can’t be less or equal to zero. In these cases, throw an ArgumentException with message: "Capacity has to be greater than 0!"
                if (value < 1)
                {
                    string.Format(ExceptionMessages.CapacityLessThanOne);
                }
                capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => delicacyMenu;
        public IRepository<ICocktail> CocktailMenu => cocktailMenu;
        public double CurrentBill =>currentBill;
        
        public double Turnover => this.turnover;
        public bool IsReserved { get;private set; }

        public void UpdateCurrentBill(double amount)
            => currentBill += amount;

        public void Charge()
        {
            turnover += currentBill;
            currentBill = 0;
        }

        public void ChangeStatus()
        {
            if (IsReserved)
            {
                IsReserved =false;
                return;
            }
            IsReserved =true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            /*"Booth: {boothId}
               Capacity: {boothCapacity}
               Turnover: {turnoverAmount - formatted to the second decimal place} lv
               -Cocktail menu:
               --{cocktail1.ToString()}
               --{cocktail2.ToString()}
               …
               --{cocktailN.ToString()}
               -Delicacy menu:
               --{delicacy1.ToString()}
               --{delicacy2.ToString()}
               */
            sb.AppendLine($"Booth: {BoothId}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Turnover: {Turnover:f2} lv");
            sb.AppendLine("-Cocktail menu:");
            foreach (var cocktail in cocktailMenu.Models)
            {
                sb.AppendLine($"--{cocktail}");
            }

            sb.AppendLine($"-Delicacy menu:");
            foreach (var delicacy in delicacyMenu.Models)
            {
                sb.AppendLine($"--{delicacy}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
