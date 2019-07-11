using System;

namespace _2._3.USER
{
    class Program
    {
        private static void Main(string[] args) 
        {
            var user = new User("Болотин", "Денис", "Михайлович");
            WriteUserInfo(user);
        }

        private static void WriteUserInfo(User user)
        {
            Console.WriteLine();
            Console.WriteLine($"Фамилия: {user.LastName}");
            Console.WriteLine($"Имя: {user.FirstName}");
            Console.WriteLine($"Отчество: {user.SecondName}");

            //Console.WriteLine($"Дата рождения: {user.BirthDate}");
            //Console.WriteLine($"Возраст: {user.Age}");
            Console.WriteLine();
        }
    }
}
