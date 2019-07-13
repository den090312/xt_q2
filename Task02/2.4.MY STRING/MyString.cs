using System.Text;

namespace _2._4.MY_STRING
{
    public class MyString
    {
        public char[] CharArray { get; set; }
        public int Length => CharArray.Length;
        public MyString(string userString) => CharArray = StringToCharArray(userString);
        public MyString(char[] userCharArray) => CharArray = userCharArray;

        public bool TryFind(char userChar, out int index)
        {
            index = 0;
            bool found = false;

            foreach (char element in CharArray)
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

            for (int i = 0; i < CharArray.Length - 1; i++)
            {
                for (int j = i + 1; j < CharArray.Length; j++)
                {
                    if (CharArray[j] < CharArray[i])
                    {
                        buffer = CharArray[i];
                        CharArray[i] = CharArray[j];
                        CharArray[j] = buffer;
                    }
                }
            }

            return this;
        }

        public MyString Reverse()
        {
            var lastIndex = CharArray.Length - 1;
            var swapStep = CharArray.Length % 2 == 0 ? lastIndex / 2 : (lastIndex + 1) / 2;
            char buffer;

            for (int i = 0; i <= swapStep; i++)
            {
                buffer = CharArray[i];
                CharArray[i] = CharArray[lastIndex - i];
                CharArray[lastIndex - i] = buffer;
            }

            return this;
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

        public override bool Equals(object userObject)
        {
            var myString = userObject as MyString;

            return myString == null ? false : CharArray.Equals(myString.CharArray);
        }

        public override int GetHashCode() => CharArray.GetHashCode();
        public static bool operator >(MyString myString1, MyString myString2) => myString1.CharArray.Length > myString2.CharArray.Length;
        public static bool operator <(MyString myString1, MyString myString2) => myString1.CharArray.Length < myString2.CharArray.Length;
        public static MyString operator +(MyString myString1, MyString myString2)
        {
            var charArray1 = myString1.CharArray;
            var charArray2 = myString2.CharArray;
            var length1 = charArray1.Length;
            var length3 = length1 + charArray2.Length;
            var resultCharArray = new char[length3];

            for (int i = 0; i < length1; i++)
            {
                resultCharArray[i] = charArray1[i];
            }

            int j = 0;

            for (int i = length1; i < length3; i++)
            {
                resultCharArray[i] = charArray2[j];
                j++;
            }

            return new MyString(resultCharArray);
        }

        private static char[] StringToCharArray(string thoseString)
        {
            char[] myCharArray = new char[thoseString.Length];
            var lastIndex = thoseString.Length - 1;

            for (int i = 0; i <= lastIndex; i++)
            {
                myCharArray[i] = thoseString[i];
            }

            return myCharArray;
        }
    }
}
