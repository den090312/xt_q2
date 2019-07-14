using System;
using _2._3.USER;

namespace _2._5.EMPLOYEE
{
    public class Employee
    {
        private int workExperience;

        public Employee(User employeeUser, int employeeWorkExperience, string employeePosition)
        {
            if (employeeUser.Age < 18)
            {
                throw new ArgumentException("Официальное трудоустройство возможно только с 18 лет!");
            }

            User = employeeUser;
            WorkExperience = employeeWorkExperience;
            Position = employeePosition;
        }

        public string Position { get; set; }

        public User User { get; }

        public int WorkExperience
        {
            get => workExperience;
            set
            {
                if (workExperience >= User.Age)
                {
                    throw new ArgumentException("Опыт работы не может быть больше или равен возрасту!");
                }

                workExperience = value;
            }
        }

    }
}
