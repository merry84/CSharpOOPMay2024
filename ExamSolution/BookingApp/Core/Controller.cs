using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;

namespace BookingApp.Core
{
    //123/150
    public class Controller : IController
    {
        private readonly IRepository<IHotel> hotels;

        public Controller()
        {
            hotels = new HotelRepository();
        }
        public string AddHotel(string hotelName, int category)
        {
            /*Creates a Hotel with the given name and star category. The method should return one of the following messages:
               •	If the hotel with the given name exists return: "Hotel {hotelName} is already registered in our platform."
               •	If the hotel is successfully created, store the hotel in the appropriate collection and return:
            "{category} stars hotel {hotelName} is registered in our platform and expects room availability to be uploaded."
               */
            IHotel hotel;
            if (hotels.Select(hotelName) != null)
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }
            hotel = new Hotel(hotelName, category);
            hotels.AddNew(hotel);
            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);


        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            //Uploads new room type for the given hotel.
            // If a hotel with such a name doesn't exist, returns: "Profile {hotelName} doesn’t exist!"
            if (hotels.Select(hotelName) == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            // If the given type is already created, returns: "Room type is already created!"
            IHotel hotel = hotels.Select(hotelName);
            if (hotel.Rooms.Select(roomTypeName) != default)
            {
                return string.Format(OutputMessages.RoomTypeAlreadyCreated);
            }

            // If the room type is not correct, throw a new !!!ArgumentException !!!!with the message: "Incorrect room type!"

            // If all the given data is correct, create a room from the given type and add it to the RoomRepository of the Hotel with the given name,
            IRoom room;
            if (roomTypeName == nameof(Studio))
            {
                room = new Studio();
            }
            else if (roomTypeName == nameof(Apartment))
            {
                room = new Apartment();
            }
            else if (roomTypeName == nameof(DoubleBed))
            {
                room = new DoubleBed();
            }
            else
            {
                return string.Format(ExceptionMessages.RoomTypeIncorrect);
            }
            hotel.Rooms.AddNew(room);

            return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            /*Sets prices to the given room type for the given hotel.
               •	If a hotel with such a name doesn't exist, returns: "Profile {hotelName} doesn't exist!"
               •	If the room type is not correct, throw a new ArgumentException with the message: "Incorrect room type!"
               •	If the given type is not created yet, returns: "Room type is not created yet!"
               •	You can set the room price only once. If it is already set, throw a new InvalidOperationException with the 
               message: "Price is already set!"
               •	If the price is set successfully, return the message: "Price of {roomType} room type in {hotelName} hotel is set!"
               */
            //IHotel hotel =hotels.Select(hotelName);
            //if (hotel == null)
            //{
            //    return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            //}

            //if (hotel.Rooms.Select(roomTypeName) != default)
            //{
            //    return string.Format(ExceptionMessages.RoomTypeIncorrect);
            //}

            //if (hotel.Rooms.Select(roomTypeName) == null)
            //{
            //    return string.Format(OutputMessages.RoomTypeNotCreated);
            //}
            //IRoom room = hotel.Rooms.Select(roomTypeName);
            //if (room.PricePerNight>0)
            //{
            //    return string.Format(ExceptionMessages.CannotResetInitialPrice);
            //}
            //room.SetPrice(price);
            //return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);

            //Sets prices to the given room type for the given hotel.

            // •	If the price is set successfully, return the message: "Price of {roomType} room type in {hotelName} hotel is set!"
            if (hotels.Select(hotelName) == null)
            {//	If a hotel with such a name doesn't exist, returns: "Profile {hotelName} doesn't exist!"
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            // If the room type is not correct, throw a new ArgumentException with the message: "Incorrect room type!"
            IHotel hotel = hotels.Select(hotelName);
            if (roomTypeName != nameof(Studio) && roomTypeName != nameof(DoubleBed) && roomTypeName != nameof(Apartment))
            {
                return string.Format(ExceptionMessages.RoomTypeIncorrect);
            }

            // If the given type is not created yet, returns: "Room type is not created yet!"
            if (hotel.Rooms.Select(roomTypeName) == null)
            {
                return string.Format(OutputMessages.RoomTypeNotCreated);
            }
            IRoom room = hotel.Rooms.Select(roomTypeName);

            // •	You can set the room price only once. If it is already set, throw a new InvalidOperationException with the 
            // message: "Price is already set!"
            if (room.PricePerNight > 0)
            {
                return string.Format(ExceptionMessages.CannotResetInitialPrice);
            }

            //If the price is set successfully, return the message: "Price of {roomType} room type in {hotelName} hotel is set!"
            room.SetPrice(price);
            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
        }



        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            /*A reservation is made in the first room, which answers all the following conditions:
               •	First, order all the hotels by FullName alphabetically
               •	Second, take only the rooms which have their PricePerNight set (PricePerNight > 0 )
               •	Third, order all taken rooms from the previous step by bed capacity ascending,
               •	Finally, choose from the ordered rooms, the room with the lowest capacity where the guests will fit
                If none of the available hotels corresponds to the given category, returns: "{category} star hotel is not available in our platform."
               If none of the rooms can fit the requested guests, return message: "We cannot offer appropriate room for your request."
               If the booking is successful, the method returns message: "Booking number {bookingNumber} for {hotelName} hotel is successful!"
               Also for successful booking, you should add the new Booking in the BookingRepository of the selected hotel. Every new Booking should have a booking number equal to the total number of the already added bookings to the selected hotel increased by one: 
               bookingNumber = totalBookingAppBookingsCount + 1; 
               */
            if (hotels.All().All(x => x.Category != category))
            {
                return string.Format(OutputMessages.CategoryInvalid, category);
            }
            var selectedHotels = hotels.All()
                .Where(x => x.Category == category)
                .OrderBy(x => x.Turnover)
                .ThenBy((x => x.FullName));

            foreach (var hotel in selectedHotels)
            {
                var selectedRoom = hotel.Rooms.All()
                    .Where(x => x.PricePerNight > 0)
                    .Where(y => y.BedCapacity >= adults + children)
                    .OrderBy(z => z.BedCapacity)
                    .FirstOrDefault();

                if (selectedRoom != null)
                {
                    int bookingNumber = this.hotels.All().Sum(x => x.Bookings.All().Count) + 1;
                    IBooking booking = new Booking(selectedRoom, duration, adults, children, bookingNumber);
                    hotel.Bookings.AddNew(booking);
                    return string.Format(OutputMessages.BookingSuccessful, bookingNumber, hotel.FullName);
                }
            }

            return string.Format(OutputMessages.RoomNotAppropriate);
        }

        public string HotelReport(string hotelName)
        {
            IHotel hotel = hotels.Select(hotelName);
            if (hotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            var sb = new StringBuilder();
            sb.AppendLine($"Hotel name: {hotel.FullName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
            sb.AppendLine("--Bookings:");
            sb.AppendLine();
            if (hotel.Bookings.All().Count == 0)
            {
                sb.AppendLine("none");
            }
            else
            {
                foreach (var booking in hotel.Bookings.All())
                {
                    sb.AppendLine($"{booking.BookingSummary()}");
                    sb.AppendLine();
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
