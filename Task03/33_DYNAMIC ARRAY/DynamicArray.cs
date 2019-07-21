
using System;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    class CreateArrayFromCapacity<T>
    {
        private T[] dynamicArray;

        public CreateArrayFromCapacity()
        {
            dynamicArray = new T[0];
        }

        public CreateArrayFromCapacity(int userCapacity)
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

        public CreateArrayFromCapacity(IEnumerable<T> userIEnum)
        {
            NullCheck(userIEnum);

            dynamicArray = new T[GetDynamicArrayCapacity(userIEnum)];

            int i = 0;

            foreach (T element in userIEnum)
            {
                dynamicArray[i] = element;
            }
        }

        private int GetDynamicArrayCapacity(IEnumerable<T> userIEnum)
        {
            int capacity = 0;

            foreach (T element in userIEnum)
            {
                capacity++;
            }

            return capacity;
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
