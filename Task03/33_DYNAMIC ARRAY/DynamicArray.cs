
using System;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    class DynamicArray<T>
    {
        private T[] dynamicArray;

        public DynamicArray()
        {
            dynamicArray = new T[0];
        }

        public DynamicArray(int userCapacity)
        {
            if (userCapacity > 0)
            {
                dynamicArray = new T[userCapacity];
            }
            else
            {
                throw new ArgumentException($"{nameof(userCapacity)} < 0. Емкость массива не может быть отрицательной!");
            }
        }

        public DynamicArray(IEnumerable<T> userIEnum)
        {
            NullCheck(userIEnum);

            int capacity = 0;

            foreach (T element in userIEnum)
            {
                capacity++;
            }

            dynamicArray = new T[capacity];

            int i = 0;

            foreach (T element in userIEnum)
            {
                dynamicArray[i] = element;
            }
        }

        private static void NullCheck(IEnumerable<T> iEnumerable)
        {
            if (iEnumerable is null)
            {
                throw new ArgumentException($"{nameof(iEnumerable)} is null!");
            }
        }
    }
}
