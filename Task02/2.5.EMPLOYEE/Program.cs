using System;

namespace _2._5.EMPLOYEE
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var myEmployee = new Employee
            {
                LastName = _3.USER.Program.GetStringValueFromConsole("фамилию"),
                FirstName = _3.USER.Program.GetStringValueFromConsole("имя"),
                SecondName = _3.USER.Program.GetStringValueFromConsole("отчество")
            };

            myEmployee.SetBirthDate(_3.USER.Program.GetBirthDateFromConsole(myEmployee));

            Console.WriteLine("Ввод стажа работы:");
            myEmployee.WorkExperience = GetIntFromConsole();
            myEmployee.Position = _3.USER.Program.GetStringValueFromConsole("должность"); ;
            Console.WriteLine();

            Console.WriteLine("РАБОТНИК");
            _2._3.USER.Program.WriteInfo(myEmployee);
            Console.WriteLine($"Стаж работы: {myEmployee.WorkExperience}");
            Console.WriteLine($"Должность: {myEmployee.Position}");
        }

        private static int GetIntFromConsole()
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
