
using System;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    class DynamicArray<T>
    {
        private List<T> dynamicArray = new List<T>(0);

        public DynamicArray()
        {
            dynamicArray = new List<T>(0);
        }

        public DynamicArray(int userCapacity)
        {
            if (userCapacity > 0)
            {
                dynamicArray = new List<T>(userCapacity);
            }
            else
            {
                throw new ArgumentException($"{nameof(userCapacity)} < 0. Емкость массива не может быть отрицательной!");
            }
        }

        public DynamicArray(IEnumerable<T> userIEnum)
        {
            NullCheck(userIEnum);

            foreach (T element in userIEnum)
            {
                dynamicArray.Add(element);
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
