using System;
using System.Text;

namespace _2._4.MY_STRING
{
    public class MyString
    {
        public char[] CharArray { get; private set; }

        public MyString(string userString) => CharArray = userString.ToCharArray();

        public MyString(char[] userCharArray) => CharArray = userCharArray;

        public char[] MyStringToCharArray(MyString myString)
        {
            CharArray = new char[10];

            return CharArray;
        }

        public MyString CharArrayToMyString(char[] userCharArray)
        {
            MyString myString = new MyString(userCharArray);

            return myString;
        }

        public static bool operator ==(MyString myString1, MyString myString2)
        {
            if (myString1.CharArray.Length != myString2.CharArray.Length)
            {
                return false;
            }

            for (int i = 0; i <= myString1.CharArray.Length - 1; i++)
            {
                if (myString1.CharArray[i] != myString1.CharArray[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(MyString myString1, MyString myString2)
        {
            if (myString1.CharArray.Length != myString2.CharArray.Length)
            {
                return true;
            }

            for (int i = 0; i <= myString1.CharArray.Length - 1; i++)
            {
                if (myString1.CharArray[i] != myString1.CharArray[i])
                {
                    return true;
                }
            }

            return false;
        }

        public static bool operator >(MyString myString1, MyString myString2) => myString1.CharArray.Length > myString2.CharArray.Length;
        public static bool operator <(MyString myString1, MyString myString2) => myString1.CharArray.Length < myString2.CharArray.Length;
        public static string operator +(MyString myString1, MyString myString2)
        {
            StringBuilder mySB = new StringBuilder();

            foreach (char element in myString1.CharArray)
            {
                mySB.Append(element);
            }

            foreach (char element in myString2.CharArray)
            {
                mySB.Append(element);
            }

            return mySB.ToString();
        }
    }
}
