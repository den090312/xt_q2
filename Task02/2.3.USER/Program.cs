using System;

namespace _2._3.USER
{
    class Program
    {
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

            Console.WriteLine("Пользователь № 1");
            WriteUserInfo(myUser);

            //создаем пользователя через конструктор ФИО
            var myUser2 = new User("Иванов", "Сидор", "Пупкиевич");
            Console.WriteLine("Пользователь № 2");
            WriteUserInfo(myUser2);
        }

        private static void WriteUserInfo(User user)
        {
            Console.WriteLine($"Фамилия: {user.LastName}");
            Console.WriteLine($"Имя: {user.FirstName}");
            Console.WriteLine($"Отчество: {user.SecondName}");
            Console.WriteLine($"Дата рождения: {user.BirthDate.ToString("D")}");
            Console.WriteLine($"Возраст: {user.Age}");
            Console.WriteLine();
        }

        private static string GetNameFromConsole(string nameType)
        {
            Console.WriteLine($"Введите {nameType}:");

            return Console.ReadLine();
        }
    }
}
