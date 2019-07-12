using System;

namespace _2._4.MY_STRING
{
    public class MyString
    {
        private string innerString;

        public char[] CharArray { get; private set; }

        public MyString(string userString)
        {
            innerString = userString;
            CharArray = userString.ToCharArray();
        }

        public MyString(char[] userCharArray)
        {
            innerString = userCharArray.ToString();
            CharArray = userCharArray;
        }

        public char[] MyStringToCharArray(MyString myString)
        {
            CharArray = myString.innerString.ToCharArray();

            return CharArray;
        }

        public MyString CharArrayToMyString(char[] userCharArray)
        {
            MyString myString = new MyString(userCharArray);

            return myString;
        }

        public static bool operator ==(MyString myString1, MyString myString2) => myString1.innerString == myString2.innerString;
        public static bool operator !=(MyString myString1, MyString myString2) => myString1.innerString != myString2.innerString;
        public static bool operator >(MyString myString1, MyString myString2) => myString1.CharArray.Length > myString2.CharArray.Length;
        public static bool operator <(MyString myString1, MyString myString2) => myString1.CharArray.Length < myString2.CharArray.Length;
    }
}
