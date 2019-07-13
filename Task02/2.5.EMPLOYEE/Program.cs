using System;
using _2._3.USER;

namespace _2._5.EMPLOYEE
{
    public class Program
    {
        private static void Main(string[] args)
        {
            User myUser = new Employee();

            myUser.LastName = _3.USER.Program.GetStringValueFromConsole("фамилию");
            myUser.FirstName = _3.USER.Program.GetStringValueFromConsole("имя");
            myUser.SecondName = _3.USER.Program.GetStringValueFromConsole("отчество");
            myUser.SetBirthDate(_2._3.USER.Program.GetBirthDateFromConsole(myUser));

            Employee myEmployee = (Employee)myUser;
            Console.WriteLine();
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
