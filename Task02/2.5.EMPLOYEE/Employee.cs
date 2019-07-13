using System;
using _2._3.USER;

namespace _2._5.EMPLOYEE
{
    public class Employee : User
    {
        private int workExperience;

        public Employee(string userLastName, string userFirstName, string userSecondName) : base(userLastName, userFirstName, userSecondName)
        {
        }

        public int WorkExperience
        {
            get => workExperience;
            set
            {
                if (workExperience >= Age)
                {
                    throw new ArgumentException("Опыт работы не может быть больше или равен возрасту!");
                }

                workExperience = value;
            }
        }
        public string Position { get; set; }
    }
}
