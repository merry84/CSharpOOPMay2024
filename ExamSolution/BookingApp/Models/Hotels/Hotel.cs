using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;

namespace BookingApp.Models.Hotels
{
    public class Hotel :IHotel
    {
        private string fullName;
        private int category;

        public Hotel(string fullName,int category)
        {
            FullName = fullName;
            Category = category;
            Rooms = new RoomRepository();
            Bookings = new BookingRepository();

        }
        public string FullName
        {
            get=>fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.HotelNameNullOrEmpty);
                }
                fullName = value;
            }
        }

        //o	If the category is less than 1 or greater than 5, throw an ArgumentException with the message:
        // "Category should be between 1 and 5 stars!"

        public int Category
        {
            get=>category;
            private set
            {
                if (value < 1 || value > 5)
                {
                    string.Format(ExceptionMessages.InvalidCategory);
                }
                category = value;
            }
        }
        //o	Returns the Sum of all booking amounts(ResidenceDuration*PricePerNight) paid in the Hotel, rounded to the second decimal place
        public double Turnover => Math.Round(Bookings.All().Sum(x => x.ResidenceDuration * x.Room.PricePerNight), 2);
        public IRepository<IRoom> Rooms { get; set; }
        public IRepository<IBooking> Bookings { get; set; }
    }
}
