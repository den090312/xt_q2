using System;

namespace _2._3.USER
{
    public class Program
    {
        private const string Format = "dd.MM.yyyy";
        private static void Main(string[] args) 
        {
            var myUser = new User();

            myUser.LastName = GetNameFromConsole("фамилию");
            myUser.FirstName = GetNameFromConsole("имя");
            myUser.SecondName = GetNameFromConsole("отчество");
            Console.WriteLine();
            myUser.SetBirthDate(GetBirthDateFromConsole());

            WriteUserInfo(myUser);
        }

        public static void WriteUserInfo(User user)
        {
            Console.WriteLine();
            Console.WriteLine("ПОЛЬЗОВАТЕЛЬ");
            Console.WriteLine($"Фамилия: {user.LastName}");
            Console.WriteLine($"Имя: {user.FirstName}");
            Console.WriteLine($"Отчество: {user.SecondName}");
            Console.WriteLine($"Дата рождения: {user.BirthDate.ToString(Format)}");
            Console.WriteLine($"Возраст: {user.Age}");
            Console.WriteLine();
        }

        public static string GetNameFromConsole(string nameType)
        {
            Console.WriteLine();
            Console.WriteLine($"Введите {nameType}:");

            return Console.ReadLine();
        }

        public static string GetBirthDateFromConsole()
        {
            Console.WriteLine($"Введите дату рождения в формате: {Format}:");

            return Console.ReadLine();
        }
    }
}
