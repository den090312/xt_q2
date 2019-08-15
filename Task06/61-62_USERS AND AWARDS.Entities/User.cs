using System;
using System.Collections;

namespace _61_62_USERS_AND_AWARDS.Entities
{
    public class User
    {
        private readonly DateTime currentDateTime = DateTime.Now.Date;

        private string name = string.Empty;

        private DateTime dateOfBirth;

        public string UserId { get; set; } = string.Empty;

        public string IdAward { get; set; } = string.Empty;

        public string Name
        {
            get => name;
            set
            {
                CheckName(value, "Имя");
                name = value;
            }
        }

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                if (value > currentDateTime)
                {
                    throw new ArgumentException("Date of birth can't be more than current date!");
                }

                if (value == currentDateTime)
                {
                    throw new ArgumentException("Welcome to our world!");
                }

                dateOfBirth = value;
            }
        }

        public int Age
        {
            get
            {
                if (DateOfBirth.Year == currentDateTime.Year)
                {
                    throw new ArgumentException("Age can't be less than 1 year!");
                }

                var userAge = currentDateTime.AddYears(-DateOfBirth.Year).Year;

                if (currentDateTime.Month < DateOfBirth.Month)
                {
                    userAge--;
                }

                return userAge;
            }
        }

        public User(string name, DateTime dateOfBirth)
        {
            NullCheck(name);

            UserId = Guid.NewGuid().ToString();
            IdAward = string.Empty;
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        private static void CheckName(string userName, string nameType)
        {
            NullCheck(userName);
            NullCheck(nameType);

            var userCharArray = userName.ToCharArray();

            if (char.IsLower(userCharArray[0]))
            {
                throw new ArgumentException($"Filed '{nameType}' must begin from upper case!");
            }
        }

        private static void NullCheck(string userString)
        {
            if (userString is null)
            {
                throw new ArgumentNullException($"{nameof(userString)} is null!");
            }
        }
    }
}
