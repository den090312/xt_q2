using System;

namespace _61_62_USERS_AND_AWARDS.Entities
{
    public class User
    {
        private readonly DateTime currentDateTime = DateTime.Now.Date;

        public string UserID { get; } = string.Empty;

        public string AwardID { get; } = string.Empty;

        public string Name { get; } = string.Empty;

        public DateTime DateOfBirth { get; }

        public int Age
        {
            get
            {
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
            CheckName(name);
            CheckDateOfBirth(dateOfBirth);

            UserID = Guid.NewGuid().ToString();
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public static int GetFieldIndex(string fieldName)
        {
            switch (fieldName)
            {
                case "UserID":
                    return 0;
                case "AwardID":
                    return 1;
                case "Name":
                    return 2;
                case "DateOfBirth":
                    return 3;
                case "Age":
                    return 4;
                default:
                    return -1;
            }
        }

        private static void CheckName(string name)
        {
            var userCharArray = name.ToCharArray();

            if (char.IsLower(userCharArray[0]))
            {
                throw new ArgumentException($"Filed '{name}' must begin from upper case!");
            }
        }

        private void CheckDateOfBirth(DateTime date)
        {
            if (date > currentDateTime)
            {
                throw new ArgumentException("Date of birth can't be more than current date!");
            }

            if (date == currentDateTime)
            {
                throw new ArgumentException("Welcome to our world!");
            }
        }
    }
}
