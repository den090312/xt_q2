using System.Text;

namespace _2._4.MY_STRING
{
    public class MyString
    {
        private readonly char[] charArray;

        public int Length => charArray.Length;
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

        public MyString Reverse()
        {
            var lastIndex = charArray.Length - 1;
            var swapStep = charArray.Length % 2 == 0 ? lastIndex / 2 : (lastIndex + 1) / 2;
            char buffer;

            for (int i = 0; i <= swapStep; i++)
            {
                buffer = charArray[i];
                charArray[i] = charArray[lastIndex - i];
                charArray[lastIndex - i] = buffer;
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
        public static MyString operator +(MyString myString1, MyString myString2)
        {
            var charArray1 = myString1.charArray;
            var charArray2 = myString2.charArray;
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
