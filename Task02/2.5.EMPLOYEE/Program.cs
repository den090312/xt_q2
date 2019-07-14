using _2._3.USER;
using System;

namespace _2._5.EMPLOYEE
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var myUser = new User
            (
                _3.USER.Program.GetStringValueFromConsole("фамилию"),
                _3.USER.Program.GetStringValueFromConsole("имя"),
                _3.USER.Program.GetStringValueFromConsole("отчество")
            );

            myUser.SetBirthDate(_3.USER.Program.GetBirthDateFromConsole(myUser));

            Console.WriteLine("Ввод стажа работы:");

            var myEmployee = new Employee(myUser)
            {
                WorkExperience = GetPositiveIntFromConsole(),
                Position = _3.USER.Program.GetStringValueFromConsole("должность")
            };

            Console.WriteLine("РАБОТНИК");
            _2._3.USER.Program.WriteInfo(myUser);
            Console.WriteLine($"Стаж работы: {myEmployee.WorkExperience}");
            Console.WriteLine($"Должность: {myEmployee.Position}");
        }

        private static int GetPositiveIntFromConsole()
        {
            int intValue;
            bool isInt;

            do
            {
                Console.WriteLine($"Введите целое положительное число меньше или равно {int.MaxValue}:");
                isInt = int.TryParse(Console.ReadLine(), out intValue);

                if (isInt)
                {
                    isInt = intValue > 0;
                }
            }
            while (isInt == false);

            return intValue;
        }
    }
}
