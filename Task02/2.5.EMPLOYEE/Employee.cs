using System;
using _2._3.USER;

namespace _2._5.EMPLOYEE
{
    public class Employee
    {
        private int workExperience;

        public Employee(User employeeUser, int employeeWorkExperience, string employeePosition)
        {
            AgeCheck(employeeUser);
            WorkExperienceCheck(employeeWorkExperience, employeeUser);

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
                WorkExperienceCheck(value, User);
                workExperience = value;
            }
        }

        private void WorkExperienceCheck(int employeeWorkExperience, User employeeUser)
        {
            if (employeeWorkExperience >= employeeUser.Age)
            {
                throw new ArgumentException("Опыт работы не может быть больше или равен возрасту!");
            }
        }

        private void AgeCheck(User employeeUser)
        {
            if (employeeUser.Age < 18)
            {
                throw new ArgumentException("Официальное трудоустройство возможно только с 18 лет!");
            }
        }
    }
}
