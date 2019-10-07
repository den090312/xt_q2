using Common;
using Entities;
using System;
using System.Collections.Specialized;

namespace WebPL.Models
{
    public class Feedback
    {
        public static NameValueCollection Forms { get; set; }

        public static string Message { get; private set; }

        static Feedback() => Message = string.Empty;

        public static bool Run(NameValueCollection forms)
        {
            Forms = forms;

            if (TryAddFeedback())
            {
                return true;
            }
            else
            {
                Message = string.Empty;
            }

            return false;
        }

        private static bool TryAddFeedback()
        {
            var name = Forms["feedbackName"];
            var text = Forms["feedbackText"];

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(text))
            {
                return false;
            }

            if (Dependencies.FeedbackLogic.Add(name, text))
            {
                Message = "Отзыв добавлен";
            }
            else
            {
                Message = $"Ошибка добавления отзыва, имя - '{name}', текст - '{text}'!";
            }

            return true;
        }
    }
}