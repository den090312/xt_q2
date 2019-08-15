using _61_62_USERS_AND_AWARDS.Interfaces;
using System;
using System.Collections.Generic;

namespace _61_62_USERS_AND_AWARDS.Entities
{
    class User : IStorable, IDeleteble
    {
        private readonly DateTime currentDateTime = DateTime.Now.Date;

        public string Id { get; } = string.Empty; 

        public string Name { get; } = string.Empty;

        public DateTime DateOfBirth { get; }

        public int Age
        {
            get
            {
                if (DateOfBirth.Year == DateOfBirth.Year)
                {
                    throw new ArgumentException("Возраст не может быть меньше года!");
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

        public void PrintObjects()
        {
            foreach(var user in Users)
            {
                Console.WriteLine(user);
            }
        }

        public void Create()
        {
            throw new System.NotImplementedException();
        }

        public void Delete()
        {
            throw new System.NotImplementedException();
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
