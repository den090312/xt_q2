using System;
using System.Globalization;

namespace _2._3.USER
{
    public class User
    {
        private string lastName;
        private string firstName;
        private string secondName;
        private readonly DateTime currentDateTime = DateTime.Now.Date;

        public readonly string Format = "dd.MM.yyyy";

        public User(string userLastName, string userFirstName, string userSecondName)
        {
            CheckName(userLastName, "Фамилия");
            CheckName(userFirstName, "Имя");
            CheckName(userSecondName, "Отчество");

            LastName = userLastName;
            FirstName = userFirstName;
            SecondName = userSecondName;
        }

        public DateTime BirthDate { get; private set; }

        public string FirstName
        {
            get => firstName;
            set
            {
                CheckName(value, "Имя");
                firstName = value;
            }
        }

        public string SecondName
        {
            get => secondName;
            set
            {
                CheckName(value, "Отчество");
                secondName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                CheckName(value, "Фамилия");
                lastName = value;
            }
        }

        public void SetBirthDate(string userDate)
        {
            NullCheck(userDate);
            bool isDate = DateTime.TryParseExact(userDate, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime userBirthDate);

            if (!isDate)
            {
                throw new ArgumentException($"Дата рождения должна быть в формате: {Format}:");
            }

            if (userBirthDate > currentDateTime)
            {
                throw new ArgumentException("Дата рождения не может быть больше текущей даты!");
            }

            if (userBirthDate == currentDateTime)
            {
                throw new ArgumentException("Добро пожаловать в наш бренный мир!");
            }

            BirthDate = userBirthDate;
        }

        public int Age
        {
            get
            {
                if (BirthDate.Year == currentDateTime.Year)
                {
                    throw new ArgumentException("Возраст не может быть меньше года!");
                }

                var userAge = currentDateTime.AddYears(-BirthDate.Year).Year;

                if (currentDateTime.Month < BirthDate.Month)
                {
                    userAge--;
                }

                return userAge;
            }
        }

        private static void CheckName(string userName, string nameType)
        {
            NullCheck(userName);
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

        private static void NullCheck(string userString)
        {
            if (userString == null)
            {
                throw new ArgumentNullException($"{nameof(userString)} is null!");
            }
        }
    }
}
