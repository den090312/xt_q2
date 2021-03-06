﻿using System;
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
            SubjectNullCheck(subject);

            Subjects.Add(subject);
        }

        public void RemoveSubject(Subject subject)
        {
            SubjectNullCheck(subject);

            Subjects.Remove(subject);
        }

        public bool NoBonuses() => Subjects.Exists(element => element.GetType() == typeof(Bonus));

        private static void SubjectNullCheck(Subject subject)
        {
            if (subject is null)
            {
                throw new ArgumentNullException($"{nameof(subject)} is null!");
            }
        }
    }
}
