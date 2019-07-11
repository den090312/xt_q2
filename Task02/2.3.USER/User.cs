using System;

namespace _2._3.USER
{
    public class User
    {
        private string firstName;
        private string secondName;
        private string lastName;
        private DateTime birthDate;

        public string FirstName
        {
            get => firstName;
            set
            {
                CheckNameForLetters(value, "Имя");
                firstName = value;
            }
        }
        public string SecondName
        {
            get => secondName;
            set
            {
                CheckNameForLetters(value, "Отчество");
                secondName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                CheckNameForLetters(value, "Фамилия");
                lastName = value;
            }
        }

        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("Дата рождения не может быть больше текущей даты!");
                }

                birthDate = value;
            }
        }
        public int Age
        {
            get
            {
                var userAge = DateTime.Now.AddYears(-birthDate.Year).Year;
                if (userAge <= 0)
                {
                    throw new ArgumentException("Возраст не может быть меньше или равен нулю!");
                }

                return userAge;
            }
        }

        public User(string userLastName, string userFirstName, string userSecondName)
        {
            CheckNameForLetters(lastName, "Фамилия");
            CheckNameForLetters(firstName, "Имя");
            CheckNameForLetters(secondName, "Отчество");

            lastName = userLastName;
            firstName = userFirstName;
            secondName = userSecondName;
        }

        private static void CheckNameForLetters(string userName, string nameType)
        {
            var userCharArray = userName.ToCharArray();
            foreach (char element in userCharArray)
            {
                if (!char.IsLetter(element))
                {
                    throw new ArgumentException($"Недопустимый символ в поле '{nameType}'!");
                }
            }
        }
    }
}
