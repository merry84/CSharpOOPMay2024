using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public class User : IUser
    {
        private string firstName;
        private string lastName;
        private double rating;
        private string drivingLicenseNumber;
        private bool isBlocked;

        public User(string firstName, string lastName, string drivingLicenseNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            rating = 0;
            DrivingLicenseNumber = drivingLicenseNumber;
            isBlocked = false;
        }

        public string FirstName
        {
            get
            => firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.FirstNameNull);
                }
                firstName = value;
            }

        }

        public string LastName
        {
            get
            => lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.LastNameNull);
                }
                lastName = value;
            }

        }

        public double Rating
        =>rating;

        public string DrivingLicenseNumber
        {
            get
            => drivingLicenseNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string.Format(ExceptionMessages.DrivingLicenseRequired);
                }
                drivingLicenseNumber = value;
            }
        }

        public bool IsBlocked
        =>isBlocked;
        public void DecreaseRating()
        {
            if (rating < 2)
            {
                rating = 0;
                isBlocked = true;
            }
            else
            {
                rating -= 2;
            }
        }

        public void IncreaseRating()
        {
            if (rating < 10)
            { rating += 0.5; }
        }
        public override string ToString()
        => $"{FirstName} {LastName} Driving license: {drivingLicenseNumber} Rating: {rating}";

    }
}
