
using System;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    class DynamicArray<T>
    {
        private T[] dynamicArray;

        public int Capacity { get; private set; } = 0;

        public DynamicArray()
        {
            dynamicArray = new T[0];
            Capacity = 8;
        }

        public DynamicArray(int userCapacity)
        {
            if (userCapacity > 0)
            {
                dynamicArray = new T[userCapacity];
                Capacity = userCapacity;
            }
            else
            {
                throw new ArgumentException($"{nameof(userCapacity)} < 0. Емкость массива не может быть отрицательной!");
            }
        }

        public DynamicArray(IEnumerable<T> userIEnum)
        {
            NullCheck(userIEnum);

            Capacity = GetDynamicArrayCapacity(userIEnum);
            dynamicArray = new T[Capacity];

            int i = 0;

            foreach (T element in userIEnum)
            {
                dynamicArray[i] = element;
            }
        }

        public void Add(T element)
        {
            var nextIndex = dynamicArray.Length + 1;

            if (dynamicArray.Length == Capacity)
            {
                Capacity *= 2;

                var newArray = new T[Capacity];

                dynamicArray.CopyTo(newArray, 0);
                dynamicArray = newArray;
            }

            dynamicArray[nextIndex] = element;
        }

        private static int GetDynamicArrayCapacity(IEnumerable<T> userIEnum)
        {
            int capacity = 0;

            foreach (T element in userIEnum)
            {
                capacity++;
            }

            return capacity;
        }

        private static void NullCheck(IEnumerable<T> userIEnum)
        {
            if (userIEnum is null)
            {
                throw new ArgumentException($"{nameof(userIEnum)} is null!");
            }
        }
    }
}
