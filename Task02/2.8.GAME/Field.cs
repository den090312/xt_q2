using _2._1.ROUND;
using System;
using System.Collections.Generic;

namespace _2._8.GAME
{
    public class Field
    {
        public List<Subject> Subjects { get; private set; } = new List<Subject>();

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Field(int width, int height)
        {
            if (width < 0)
            {
                throw new Exception("Ширина поля не может быть меньше нуля!");
            }

            if (height < 0)
            {
                throw new Exception("Высота поля не может быть меньше нуля!");
            }

            Width = width;
            Height = height;
        }

        public void AddSubject(Subject subject)
        {
            Subjects.Add(subject);
        }

        public void ChangeSubjectLocation(Subject sub)
        {
            throw new NotImplementedException();
        }

        internal bool NoBonuses()
        {
            throw new NotImplementedException();
        }
    }
}
