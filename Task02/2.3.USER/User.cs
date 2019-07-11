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
                CheckNameForNumbers(value);
                firstName = value;
            }
        }
        public string SecondName
        {
            get => secondName;
            set
            {
                CheckNameForNumbers(value);
                secondName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                CheckNameForNumbers(value);
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
                    throw new ArgumentException("Возраст не может быть отрицательным или меньше нуля!");
                }
                return userAge;
            }
        }

        private static void CheckNameForNumbers(string userName)
        {
            var userCharArray = userName.ToCharArray();
            foreach (char element in userCharArray)
            {
                if (char.IsDigit(element))
                {
                    throw new ArgumentException("Цифры в ФИО недопустимы!");
                }
            }
        }
    }
}
