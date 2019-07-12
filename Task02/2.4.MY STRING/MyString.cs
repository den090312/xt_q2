using System;
using System.Text;

namespace _2._4.MY_STRING
{
    public class MyString
    {
        private char[] charArray;

        public MyString(string userString) => charArray = StringToCharArray(userString);
        public MyString(char[] userCharArray) => charArray = userCharArray;

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

        public static bool operator >(MyString myString1, MyString myString2) => myString1.charArray.Length > myString2.charArray.Length;
        public static bool operator <(MyString myString1, MyString myString2) => myString1.charArray.Length < myString2.charArray.Length;
        public static string operator +(MyString myString1, MyString myString2) => CharArrayToString(myString1.charArray) + CharArrayToString(myString2.charArray);

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

        public char[] ToCharArray(MyString mystring) => mystring.charArray;

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
    }
}
