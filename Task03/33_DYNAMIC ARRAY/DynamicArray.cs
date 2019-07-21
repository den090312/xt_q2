
using System;

namespace _33_DYNAMIC_ARRAY
{
    class DynamicArray<T>
    {
        private char[] charArray = new char[0]; 

        public DynamicArray()
        {
            charArray = new char[8];
        }

        public DynamicArray(int userCapacity)
        {
            if (userCapacity > 0)
            {
                charArray = new char[userCapacity];
            }
            else
            {
                throw new ArgumentException($"{nameof(userCapacity)} < 0. Емкость массива не может быть отрицательной!");
            }

        }
    }
}
