using System;

namespace Entities
{
    public class User
    {
        public string UserID { get; } = string.Empty;

        public string Name { get; } = string.Empty;

        public DateTime DateOfBirth { get; }

        public int Age
        {
            get
            {
                var currentDateTime = DateTime.Now.Date;
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
                case "Name":
                    return 1;
                case "DateOfBirth":
                    return 2;
                case "Age":
                    return 3;
                default:
                    return -1;
            }
        }
    }
}
