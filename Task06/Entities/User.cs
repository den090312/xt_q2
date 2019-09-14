using System;

namespace Entities
{
    public class User
    {
        public Guid UserGuid { get; }

        public string Name { get; } = string.Empty;

        public DateTime DateOfBirth { get; }

        public readonly static string DateFormat = "dd.MM.yyyy";

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
            NullCheck(name);

            UserGuid = Guid.NewGuid();
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public User(Guid guid, string name, DateTime dateOfBirth)
        {
            NullCheck(name);

            UserGuid = guid;
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        private static void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}