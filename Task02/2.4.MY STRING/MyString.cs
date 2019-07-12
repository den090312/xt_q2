using System;

namespace _2._4.MY_STRING
{
    public class MyString
    {
        private string innerString;
        private char[] innerCharArray;

        public MyString(string userString)
        {
            innerString = userString;
            innerCharArray = userString.ToCharArray();
        }

        public MyString(char[] userCharArray)
        {
            innerString = userCharArray.ToString();
            innerCharArray = userCharArray;
        }

        public char[] MyStringToCharArray(MyString myString)
        {
            innerCharArray = myString.innerString.ToCharArray();

            return innerCharArray;
        }

        public MyString CharArrayToMyString(char[] userCharArray)
        {
            MyString myString = new MyString(userCharArray);

            return myString;
        }

        public static bool operator ==(MyString myString1, MyString myString2) => myString1.innerString == myString2.innerString;

        public static bool operator !=(MyString myString1, MyString myString2) => myString1.innerString != myString2.innerString;
    }
}
