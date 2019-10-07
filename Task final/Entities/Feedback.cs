using System;

namespace Entities
{
    public class Feedback
    {
        public int Id { get; set; }

        public string Name { get; }

        public DateTime Date { get; }

        public string Text { get; }

        public Feedback(int id, string name, DateTime date, string text)
        {
            Id   = id;
            Name = name;
            Date = date;
            Text = text;
        }
    }
}