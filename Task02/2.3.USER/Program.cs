using System;

namespace _2._3.USER
{
    public class Program
    {
        private static void Main(string[] args) 
        {
            var myUser = new User();

            myUser.LastName = GetStringValueFromConsole("фамилию");
            myUser.FirstName = GetStringValueFromConsole("имя");
            myUser.SecondName = GetStringValueFromConsole("отчество");
            myUser.SetBirthDate(GetBirthDateFromConsole(myUser));
            Console.WriteLine();

            Console.WriteLine("ПОЛЬЗОВАТЕЛЬ");
            WriteInfo(myUser);
        }

        public static void WriteInfo(User user)
        {
            Console.WriteLine($"Фамилия: {user.LastName}");
            Console.WriteLine($"Имя: {user.FirstName}");
            Console.WriteLine($"Отчество: {user.SecondName}");
            Console.WriteLine($"Дата рождения: {user.BirthDate.ToString(user.Format)}");
            Console.WriteLine($"Возраст: {user.Age}");
        }

        public static string GetStringValueFromConsole(string valueType)
        {
            Console.WriteLine($"Введите {valueType}:");

            return Console.ReadLine();
        }

        public static string GetBirthDateFromConsole(User myUser)
        {
            Console.WriteLine($"Введите дату рождения в формате: {myUser.Format}:");

            return Console.ReadLine();
        }
    }
}
