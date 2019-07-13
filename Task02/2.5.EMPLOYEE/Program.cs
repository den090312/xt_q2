using System;
using _2._3.USER;

namespace _2._5.EMPLOYEE
{
    public class Program
    {
        private static void Main(string[] args)
        {
            User myEmployee = new Employee();

            myEmployee.LastName = _2._3.USER.Program.GetNameFromConsole("фамилию");
            myEmployee.FirstName = _2._3.USER.Program.GetNameFromConsole("имя");
            myEmployee.SecondName = _2._3.USER.Program.GetNameFromConsole("отчество");
            Console.WriteLine();
            myEmployee.SetBirthDate(_2._3.USER.Program.GetBirthDateFromConsole());

            _2._3.USER.Program.WriteUserInfo(myEmployee);
        }
    }
}
