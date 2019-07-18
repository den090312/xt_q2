using System;
using _2._3.USER;

namespace _2._5.EMPLOYEE
{
    public class Employee
    {
        private string position = String.Empty;
        private int workExperience;

        public User User { get; }

        public Employee(User employeeUser)
        {
            UserNullCheck(employeeUser);
            AgeCheck(employeeUser);

            User = employeeUser;
        }

        public string Position
        {
            get => position;
            set
            {
                foreach (char element in value)
                {
                    if (!char.IsLetter(element))
                    {
                        throw new ArgumentException($"Недопустимый символ в поле 'должность'!");
                    }
                }

                position = value;
            }
        }

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
            UserNullCheck(employeeUser);

            if (employeeWorkExperience >= employeeUser.Age)
            {
                throw new ArgumentException("Опыт работы не может быть больше или равен возрасту!");
            }
        }

        private void AgeCheck(User employeeUser)
        {
            UserNullCheck(employeeUser);

            if (employeeUser.Age < 18)
            {
                throw new ArgumentException("Официальное трудоустройство возможно только с 18 лет!");
            }
        }

        private static void UserNullCheck(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException($"{nameof(user)} is null!");
            }
        }
    }
}
