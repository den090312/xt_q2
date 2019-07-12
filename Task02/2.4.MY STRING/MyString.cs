using System;

namespace _2._4.MY_STRING
{
    public class MyString
    {
        private readonly int innerStringLength;
        private string innerString;
        private char[] innerCharArray;

        public MyString(string userString)
        {
            innerString = userString;
            innerStringLength = userString.Length;
            innerCharArray = userString.ToCharArray();
        }
        public MyString(char[] userCharArray)
        {
            innerString = userCharArray.ToString();
            innerStringLength = innerString.Length;
            innerCharArray = userCharArray;
        }

        public int Length => innerStringLength;

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
    }
}
