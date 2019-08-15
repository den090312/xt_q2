using _61_62_USERS_AND_AWARDS.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace _61_62_USERS_AND_AWARDS.Entities
{
    class User : IStorable, IDeleteble
    {
        public readonly string DateFormat = "dd.MM.yyyy";
        private readonly DateTime currentDateTime = DateTime.Now.Date;
        private string name = string.Empty;
        private DateTime dateOfBirth;

        public string Id { get; } = string.Empty;

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
                if (DateOfBirth.Year == DateOfBirth.Year)
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

        public List<User> Users { get; private set; } = new List<User>();

        public User(string name, DateTime dateOfBirth)
        {
            NullCheck(name);

            Id = Guid.NewGuid().ToString();
            Name = name;
            DateOfBirth = dateOfBirth;

            Users.Add(this);
        }

        private DateTime GetDateFromConsole(string userDate)
        {
            NullCheck(userDate);

            bool isDate = DateTime.TryParseExact(userDate, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime userBirthDate);

            if (!isDate)
            {
                throw new ArgumentException($"Date of birth must be in format: {DateFormat}:");
            }

            return userBirthDate;
        }

        public void PrintObjects()
        {
            foreach (var user in Users)
            {
                Console.WriteLine(user);
            }
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
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

            foreach (char element in userCharArray)
            {
                if (!char.IsLetter(element))
                {
                    throw new ArgumentException($"Wrong symbol in field '{nameType}'!");
                }
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
