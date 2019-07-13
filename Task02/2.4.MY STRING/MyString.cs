﻿using System.Text;

namespace _2._4.MY_STRING
{
    public class MyString
    {
        private readonly char[] charArray;

        public MyString(string userString) => charArray = StringToCharArray(userString);
        public MyString(char[] userCharArray) => charArray = userCharArray;

        public char[] ToCharArray() => charArray;

        public bool TryFind(char userChar, out int index)
        {
            index = 0;
            bool found = false;

            foreach (char element in charArray)
            {
                if (element == userChar)
                {
                    return true;
                }
                else
                {
                    index++;
                }
            }

            return found;
        }

        public MyString Sort()
        {
            char buffer;
            for (int i = 0; i < charArray.Length - 1; i++)
            {
                for (int j = i + 1; j < charArray.Length; j++)
                {
                    if (charArray[j] < charArray[i])
                    {
                        buffer = charArray[i];
                        charArray[i] = charArray[j];
                        charArray[j] = buffer;
                    }
                }
            }

            return this;
        }

        public static bool operator ==(MyString myString1, MyString myString2)
        {
            if (myString1.charArray.Length != myString2.charArray.Length)
            {
                return false;
            }

            for (int i = 0; i <= myString1.charArray.Length - 1; i++)
            {
                if (myString1.charArray[i] != myString1.charArray[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(MyString myString1, MyString myString2)
        {
            if (myString1.charArray.Length != myString2.charArray.Length)
            {
                return true;
            }

            for (int i = 0; i <= myString1.charArray.Length - 1; i++)
            {
                if (myString1.charArray[i] != myString1.charArray[i])
                {
                    return true;
                }
            }

            return false;
        }

        public override bool Equals(object userObject)
        {
            var myString = userObject as MyString;

            return myString == null ? false : charArray.Equals(myString.charArray);
        }

        public override int GetHashCode() => charArray.GetHashCode();

        public static bool operator >(MyString myString1, MyString myString2) => myString1.charArray.Length > myString2.charArray.Length;
        public static bool operator <(MyString myString1, MyString myString2) => myString1.charArray.Length < myString2.charArray.Length;
        public static MyString operator +(MyString myString1, MyString myString2) => new MyString(CharArrayToString(myString1.charArray) + CharArrayToString(myString2.charArray));

        private static string CharArrayToString(char[] thoseCharArray)
        {
            StringBuilder mySB = new StringBuilder();

            foreach (char element in thoseCharArray)
            {
                mySB.Append(element);
            }

            return mySB.ToString();
        }

        private char[] StringToCharArray(string thoseString)
        {
            char[] myCharArray = new char[thoseString.Length];

            for (int i = 0; i <= thoseString.Length - 1; i++)
            {
                myCharArray[i] = thoseString[i];
            }

            return myCharArray;
        }
    }
}
