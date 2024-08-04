using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Core.Contracts
{
    public class Controller :IController
    {
        //126/150
        private readonly BoothRepository booths;

        public Controller()
        {
            booths = new BoothRepository();
        }
        public string AddBooth(int capacity)
        {
            /*Booth constructor will be expecting as first parameter boothId.
             So it should be created by taking the count of the already added booths in the BoothRepository + 1.               
               Creates a new  Booth with the given capacity. Adds the newly created Booth to the BoothRepository and returns:
               "Added booth number {boothId} with capacity {capacity} in the pastry shop!"
               */
            int boothId = booths.Models.Count + 1; 
            IBooth booth = new Booth(boothId,capacity);
            booths.AddModel(booth);
            return string.Format(OutputMessages.NewBoothAdded, boothId, capacity);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            /*Creates a new IDelicacy from the proper type with the given name.
               •	If the given delicacyType is not supported in the application, return the following message:
            "Delicacy type {type} is not supported in our application!"
               •	If a Delicacy with the given delicacyName already exists in the delicacy repository, 
            return the following message "{delicacyName} is already added in the pastry shop!"
               •	If the delicacy is created successfully, it is added to the DelicacyMenu of the Booth with the given boothId.
            Returns the following message:
               "{delicacyTypeName} {delicacyName} added to the pastry shop!"  */
            if (delicacyTypeName != nameof(Gingerbread) && delicacyTypeName != nameof(Stolen))
            {
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            if (booths.Models.Any(x => x.DelicacyMenu.Models.Any(x => x.Name == delicacyName)))
            {
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }

            IDelicacy delicacy;
            if (delicacyTypeName == nameof(Gingerbread))
            {
                delicacy = new Gingerbread(delicacyName);
            }
            else
            {
                delicacy = new Stolen(delicacyName);
            }

            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            booth.DelicacyMenu.AddModel(delicacy);
            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);

        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            /*Creates a new ICocktail from the proper type with the given name.
               •	If the given cocktailType is not supported in the application, return the following message:
            "Cocktail type {type} is not supported in our application!"
               •	If the given size is different from the supported in the application ("Small", "Middle", "Large"), 
            return the following message: "{size} is not recognized as valid cocktail size!"
               •	If a Cocktail with the given cocktailName && size already exists in the cocktail repository, 
            return the following message "{size} {cocktailName} is already added in the pastry shop!"
               •	If the Cocktail is created successfully, , it is added to the CocktailMenu of the Booth with the given boothId 
            and returns the following message:
               "{size} {cocktailName} {cocktailTypeName} added to the pastry shop!" */
            if (cocktailTypeName != nameof(Hibernation) && cocktailTypeName != nameof(MulledWine))
            {
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }

            if (size != "Small" && size != "Middle" && size != "Large")
            {
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            }
            if(booths.Models.Any(x=>x.CocktailMenu.Models.Any(x=>x.Name == cocktailName 
                                    && x.Size == size)))
            {
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }

            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            ICocktail cocktail;
            if (cocktailTypeName == nameof(Hibernation))
            {
                cocktail = new Hibernation(cocktailName, size);
            }
            else
            {
                cocktail = new MulledWine(cocktailName, size);
            }
            booth.CocktailMenu.AddModel(cocktail);
            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string ReserveBooth(int countOfPeople)
        {
            /*•	Order all booths from the BoothRepository,
             which are not reserved && their capacity is enough for the number of people provided, by capacity ascending, and the by boothId, decsending
               •	Take the first available Booth.
               •	If there is no such booth returns: "No available booth for {numberOfPeople} people!"
               •	If an available Booth is found, sets the IsReserved status to true and returns the following message:
               "Booth {boothId} has been reserved for {numberOfPeople} people!"
               */
            var orderedBooths = booths.Models.Where(x => !x.IsReserved && x.Capacity >= countOfPeople)
                .OrderBy(x=>x.Capacity)
                .ThenByDescending(x=>x.BoothId)
                .FirstOrDefault();
            if (orderedBooths is null)
            {
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }
            orderedBooths.ChangeStatus();
            return string.Format(OutputMessages.BoothReservedSuccessfully, orderedBooths.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            /*The second prameter (order) will be a string sequence, seperated by "/":
               •	The first element of the sequence will be the itemTypeName
               •	The second element will be itemName
               •	The third element will be the count of the ordered pieces
               •	The fourth will exist only if the item is an ICocktail. The element (if such exists) will be the size of the Cocktail.
               Finds the booth with the given boothId and finds the item from the given type with the given name.
            Before confirming the order, the method must make the following validations, in the following order:
               •	If the given itemTypeName is not existing in our application, return the following message:
            "{itemTypeName} is not recognized type!"
               •	If an item with the given itemName is not added in the according IRepository yet, return the following message:
            "There is no {itemTypeName} {itemName} available!"
               If all validations pass, try to order the given item:
               •	If the item is cocktail:
               o	Check if cocktail from the given itemTypeName, with the given itemName and the desired size is available:
               	If not, return the following message: "There is no {size} {itemName} available!"
               o	If all the validations pass, the CurrentBill is increased with the price of the desired item,
            multiplied by the desired pieces and the following message is returned:
               "Booth {boothId} ordered {pieces} {itemName}!"
               •	If the item is delicacy:
               o	Check if delicacy from the given itemTypeName and the given itemName is available:
               	If not, return the following message: "There is no {itemTypeName} {itemName} available!"
               o	If all the validations pass, the CurrentBill is increased with the price of the desired item,
            multiplied by the desired pieces and the following message is returned:
               "Booth {boothId} ordered {pieces} {itemName}!"
               */
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            var orders = order.Split("/");
            string itemTypeName = orders[0];
            string itemName = orders[1];
            
            bool isCocktail = false;

            if (itemTypeName != nameof(Stolen)
                && itemTypeName != nameof(Gingerbread)
                && itemTypeName != nameof(Hibernation)
                && itemTypeName != nameof(MulledWine))
            {
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName);

            }

            if (!booth.CocktailMenu.Models.Any(x => x.Name == itemName)
                && !booth.DelicacyMenu.Models.Any((x => x.Name == itemName)))
            {
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
            }

            int countPieces = int.Parse(orders[2]);
            //•	If the item is cocktail:
            if (itemTypeName == nameof(Hibernation)
                || itemTypeName == nameof(MulledWine))
            {
                isCocktail = true;
            }

            //Check if cocktail from the given itemTypeName, with the given itemName and the desired size is available:
            if (isCocktail)
            {
                string size = orders[3];
                ICocktail cocktailDesired = booth.CocktailMenu.Models
                    .FirstOrDefault(x => x.GetType().Name == itemTypeName
                                         && x.Size == size
                                         && x.Name == itemName);

                if (cocktailDesired == null)
                {
                    //There is no {size} {itemName} available!"
                    return string.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                }

                //o	If all the validations pass, the CurrentBill is increased with the price of the desired item
                booth.UpdateCurrentBill(cocktailDesired.Price * countPieces);
                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, countPieces, itemName);
            }
            else
            {
                //o	Check if delicacy from the given itemTypeName and the given itemName is available:
                IDelicacy delicacyDesired = booth.DelicacyMenu.Models
                    .FirstOrDefault(x => x.GetType().Name == itemTypeName
                                         && x.Name == itemName);
                if (delicacyDesired == null)
                {
                    //"There is no {itemTypeName} {itemName} available!"
                    return string.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                }
                booth.UpdateCurrentBill(delicacyDesired.Price * countPieces);
                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, countPieces, itemName);
            }
          
        }

        public string LeaveBooth(int boothId)
        {
            /*•	Finds the Booth with the given boothId. 
               •	Gets the CurrentBill and adds it to the Turnover of the Booth. Sets the CurrentBill to zero. This can be done through the Charge() method
               •	Sets the IsReserved status to false. This can be done through the ChangeStatus() method
               •	After all returns the following message:
               "Bill {currentBill:f2} lv"
               "Booth {boothId} is now available!"
               */
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            var sb = new StringBuilder();
            //"Bill {currentBill:f2} lv"
            // "Booth {boothId} is now available!"
            sb.AppendLine($"Bill {booth.CurrentBill:f2} lv");
            booth.Charge();
            booth.ChangeStatus();
            sb.AppendLine($"Booth {boothId} is now available!");
            return sb.ToString().TrimEnd();
        }

        public string BoothReport(int boothId)
        {
            /*"Booth: {boothId}
               Capacity: {boothCapacity}
               Turnover: {turnover:f2} lv
               -Cocktail menu:
               --{cocktail1.ToString()}
               --{cocktail2.ToString()}
               …
               --{cocktailN.ToString()}
               -Delicacy menu:
               --{delicacy1.ToString()}
               --{delicacy2.ToString()}
               
             */
            return this.booths.Models
                .FirstOrDefault(x => x.BoothId == boothId)
                .ToString()
                .TrimEnd();

        }
    }
}
