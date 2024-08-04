using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Utilities.Messages;

namespace BookingApp.Models.Rooms
{
    public abstract class Room : IRoom
    {
        private int betCapacity;
        private double pricePerNight = 0;

        protected Room(int betBedCapacity)
        {
            this.betCapacity = betBedCapacity;
            
        }

        public int BedCapacity => betCapacity;

        public double PricePerNight
        {
            get=>pricePerNight;
            private set
            {
                if (value < 0)
                {
                    string.Format(ExceptionMessages.PricePerNightNegative);
                }
                pricePerNight = value;
            }
        }
        public void SetPrice(double price)
        {
           PricePerNight = price;
        }
    }
}
