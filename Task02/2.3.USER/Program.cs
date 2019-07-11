using System;
using System.Globalization;

namespace _2._3.USER
{
    class Program
    {
        private const string Format = "dd.MM.yyyy";

        private static void Main(string[] args) 
        {
            //создаем пользователя через конструктор по умолчанию
            var myUser = new User();

            myUser.LastName = GetNameFromConsole("фамилию");
            Console.WriteLine();

            myUser.FirstName = GetNameFromConsole("имя");
            Console.WriteLine();

            myUser.SecondName = GetNameFromConsole("отчество");
            Console.WriteLine();

            myUser.BirthDate = GetBirthDateFromConsole();
            Console.WriteLine();

            Console.WriteLine("Пользователь № 1");
            WriteUserInfo(myUser);
        }

        private static void WriteUserInfo(User user)
        {
            Console.WriteLine($"Фамилия: {user.LastName}");
            Console.WriteLine($"Имя: {user.FirstName}");
            Console.WriteLine($"Отчество: {user.SecondName}");
            Console.WriteLine($"Дата рождения: {user.BirthDate.ToString(Format)}");
            Console.WriteLine($"Возраст: {user.Age}");
            Console.WriteLine();
        }

        private static string GetNameFromConsole(string nameType)
        {
            Console.WriteLine($"Введите {nameType}:");

            return Console.ReadLine();
        }

        private static DateTime GetBirthDateFromConsole()
        {
            DateTime birthDate;

            bool isDate;
            do
            {
                Console.WriteLine($"Введите дату в формате: {Format}:");
                isDate = DateTime.TryParseExact(Console.ReadLine(), Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            }
            while (isDate == false);

            return birthDate;
        }
    }
}
