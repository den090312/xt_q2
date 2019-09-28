using System;

namespace Entities
{
    public class User
    {
        public Guid Guid { get; }

        public string Name { get; }

        public DateTime DateOfBirth { get; }

        public readonly static string DateFormat = "dd.MM.yyyy";

        public int Age
        {
            get
            {
                var currentDateTime = DateTime.Now.Date;

                if (currentDateTime.Year == DateOfBirth.Year)
                {
                    return 0;
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
            Guid = Guid.NewGuid();
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public User(Guid guid, string name, DateTime dateOfBirth)
        {
            Guid = guid;
            Name = name;
            DateOfBirth = dateOfBirth;
        }
    }
}