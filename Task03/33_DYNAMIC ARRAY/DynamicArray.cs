
using System;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    class DynamicArray<T>
    {
        private T[] dynamicArray = new T[0];

        public int Capacity { get; private set; } = 0;

        public int Length => dynamicArray.Length;

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

            Capacity = GetIEnumerableLength(userIEnum);
            dynamicArray = new T[Capacity];
            FillDynamicArrayFromIEnumerable(userIEnum, 0);
        }

        public T this[int i]
        {
            get => dynamicArray[i];
            set => dynamicArray[i] = value;
        }

        public void Add(T element)
        {
            var nextIndex = Length + 1;

            if (Length == Capacity)
            {
                Capacity *= 2;
                ExpandDynamicArray();
            }

            dynamicArray[nextIndex] = element;
        }

        public void AddRange(IEnumerable<T> userIEnum)
        {
            var nextIndex = Length + 1;
            CapacityAdjusment(GetIEnumerableLength(userIEnum));
            ExpandDynamicArray();
            FillDynamicArrayFromIEnumerable(userIEnum, nextIndex);
        }

        private void CapacityAdjusment(int userIEnumLength)
        {
            if (Length == Capacity)
            {
                Capacity += userIEnumLength;
            }

            if (Length < Capacity)
            {
                var freeCellsQuantity = Capacity - Length;

                if (freeCellsQuantity < userIEnumLength)
                {
                    Capacity += userIEnumLength - freeCellsQuantity;
                }
            }
        }

        private void ExpandDynamicArray()
        {
            var newArray = new T[Capacity];

            dynamicArray.CopyTo(newArray, 0);
            dynamicArray = newArray;
        }

        private void FillDynamicArrayFromIEnumerable(IEnumerable<T> userIEnum, int startIndex)
        {
            foreach (T element in userIEnum)
            {
                dynamicArray[startIndex] = element;
                startIndex++;
            }
        }

        private static int GetIEnumerableLength(IEnumerable<T> userIEnum)
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
