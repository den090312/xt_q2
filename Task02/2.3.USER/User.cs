using System;
using System.Globalization;

namespace _2._3.USER
{
    public class User
    {
        private const string Format = "dd.MM.yyyy";

        private string firstName;
        private string secondName;
        private string lastName;
        private DateTime birthDate;
        private readonly DateTime currentDateTime = DateTime.Now;

        public string FirstName
        {
            get => firstName;
            set
            {
                CheckNameForCorrect(value, "Имя");
                firstName = value;
            }
        }
        public string SecondName
        {
            get => secondName;
            set
            {
                CheckNameForCorrect(value, "Отчество");
                secondName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                CheckNameForCorrect(value, "Фамилия");
                lastName = value;
            }
        }

        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                bool isDate = DateTime.TryParseExact(value.ToString(), Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out value);
                if (!isDate)
                {
                    throw new ArgumentException($"Дата должна быть формата '{Format}'!");
                }
                else
                {
                    if (value > currentDateTime)
                    {
                        throw new ArgumentException("Дата рождения не может быть больше текущей даты!");
                    }
                }

                birthDate = value;
            }
        }
        public int Age
        {
            get
            {
                var userAge = currentDateTime.AddYears(-birthDate.Year).Year;

                if (currentDateTime.Month < birthDate.Month)
                {
                    userAge--;
                }

                if (userAge <= 0)
                {
                    throw new ArgumentException("Возраст не может быть меньше или равен нулю!");
                }

                return userAge;
            }
        }

        public User()
        {

        }

        public User(string userLastName, string userFirstName, string userSecondName)
        {
            CheckNameForCorrect(userLastName, "Фамилия");
            CheckNameForCorrect(userFirstName, "Имя");
            CheckNameForCorrect(userSecondName, "Отчество");

            lastName = userLastName;
            firstName = userFirstName;
            secondName = userSecondName;
        }

        private static void CheckNameForCorrect(string userName, string nameType)
        {
            var userCharArray = userName.ToCharArray();
            if (char.IsLower(userCharArray[0]))
            {
                throw new ArgumentException($"Поле '{nameType}' должно начинаться с заглавной буквы!");
            }

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
