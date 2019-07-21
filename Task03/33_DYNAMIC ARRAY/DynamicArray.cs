
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
                SetNewCapacity(Capacity * 2);
            }

            dynamicArray[nextIndex] = element;
        }

        public void AddRange(IEnumerable<T> userIEnum)
        {
            var startIndex = Length + 1;
            SetNewCapacity(CapacityAdjusment(GetIEnumerableLength(userIEnum)));
            FillDynamicArrayFromIEnumerable(userIEnum, startIndex);
        }

        public bool Remove(T element)
        {
            TypeCheck(element);

            if (!TryFind(element, out int index))
            {
                return false;
            }
            else
            {
                var tempArray = dynamicArray;
                dynamicArray = new T[0];

                for (int i = 0; i < Length; i++)
                {
                    if (i != index)
                    {
                        dynamicArray[i] = tempArray[i];
                    }
                }

                return true;
            }
        }

        public bool Insert(T element, int index)
        {
            TypeCheck(element);

            bool inserted = false;



            return inserted;
        }

        private bool TryFind(T searchableElement, out int index)
        {
            index = 0;
            bool found = false;

            foreach (T element in dynamicArray)
            {
                if (element.Equals(searchableElement))
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

        private int CapacityAdjusment(int userIEnumLength)
        {
            int newCapacity = 0;

            if (Length == Capacity)
            {
                newCapacity += userIEnumLength;
            }

            if (Length < Capacity)
            {
                var freeCellsQuantity = Capacity - Length;

                if (freeCellsQuantity < userIEnumLength)
                {
                    newCapacity += userIEnumLength - freeCellsQuantity;
                }
            }

            return newCapacity;
        }

        private void SetNewCapacity(int newCapacity)
        {
            var newArray = new T[newCapacity];

            dynamicArray.CopyTo(newArray, 0);
            dynamicArray = newArray;
            Capacity = newCapacity;
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

        private static void TypeCheck(T element)
        {
            if (element.GetType() != typeof(T))
            {
                throw new ArgumentException($"{nameof(element)} содержит неверный тип данных!");
            }
        }
    }
}
