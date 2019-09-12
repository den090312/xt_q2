using System;

namespace Entities
{
    public class User
    {
        public Guid UserGuid { get; }

        public string Name { get; } = string.Empty;

        public DateTime DateOfBirth { get; }

        public static string DateFormat { get; } = "dd.MM.yyyy";

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
            UserGuid = Guid.NewGuid();
            Name = name;
            DateOfBirth = dateOfBirth;
        }
    }
}