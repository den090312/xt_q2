using System;
using System.Collections;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY // включает в себя задание '3.4.DYNAMIC ARRAY (HARDCORE MODE)'
{
    public class DynamicArray<T> : IEnumerable<T>, ICloneable
    {
        private T[] dynamicArray = new T[0];
        private int capacity = 0;

        public int Capacity
        {
            get => capacity;
            set
            {
                CapacityCheck(value);

                if (value < capacity)
                {
                    var tempArray = dynamicArray;

                    dynamicArray = new T[value];

                    for (int i = 0; i < value; i++)
                    {
                        dynamicArray[i] = tempArray[i];
                    }
                }

                capacity = value;
            }
        }

        public int Length => dynamicArray.Length;

        public DynamicArray()
        {
            capacity = 8;
            dynamicArray = new T[capacity];
        }

        public DynamicArray(int userCapacity)
        {
            IntMaxValueCheck(userCapacity);
            CapacityCheck(userCapacity);

            capacity = userCapacity;
            dynamicArray = new T[capacity];
        }

        public DynamicArray(IEnumerable<T> userIEnum)
        {
            NullCheck(userIEnum);

            capacity = GetIEnumerableLength(userIEnum);
            dynamicArray = new T[capacity];

            var startIndex = 0;
            FillDynamicArrayFromIEnumerable(userIEnum, startIndex);
        }

        public T this[int i]
        {
            get
            {
                IndexCheck(i);

                return dynamicArray[TryParseNegativeIndex(i)];
            }

            set
            {
                IndexCheck(i);
                dynamicArray[TryParseNegativeIndex(i)] = value;
            }
        }

        public T[] ToArray()
        {
            var newArray = new T[capacity]; 
            dynamicArray.CopyTo(newArray, 0);

            return newArray;
        }

        public void Add(T element)
        {
            if (Length == capacity)
            {
                if (capacity > 0)
                {
                    capacity *= 2;
                }
                else
                {
                    capacity = 8;
                }

                ResizeArray(Length + 1);
            }

            dynamicArray[Length - 1] = element;
        }

        public void AddRange(IEnumerable<T> userIEnum)
        {
            NullCheck(userIEnum);

            var startIndex = Length;
            var iEnumLength = GetIEnumerableLength(userIEnum);
            capacity = GetAdjusmentedCapacity(iEnumLength) + 8;

            ResizeArray(capacity);

            FillDynamicArrayFromIEnumerable(userIEnum, startIndex);
        }

        public bool Remove(T element)
        {
            if (!TryFind(element, out int index))
            {
                return false;
            }
            else
            {
                var tempArray = dynamicArray;
                var newLength = tempArray.Length;
                dynamicArray = new T[newLength - 1];

                int j = 0;

                for (int i = 0; i < newLength; i++)
                {
                    if (i != index)
                    {
                        dynamicArray[j] = tempArray[i];
                    }
                    else
                    {
                        j--;
                    }

                    j++;
                }

                return true;
            }
        }

        public bool Insert(T element, int index)
        {
            IndexCheck(index);

            index = TryParseNegativeIndex(index);

            bool inserted = false;

            if (Length == capacity)
            {
                capacity += 8;
            }

            var tempArray = dynamicArray;
            dynamicArray = new T[Length + 1];

            int j = 0;

            for (int i = 0; i < capacity; i++)
            {
                if (i == index)
                {
                    dynamicArray[i] = element;
                    j--;
                }
                else
                {
                    dynamicArray[i] = tempArray[j];
                }

                j++;
            }

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

        private int GetAdjusmentedCapacity(int userIEnumLength)
        {
            int newCapacity = capacity;

            if (Length == capacity)
            {
                newCapacity += userIEnumLength;
            }

            if (Length < capacity)
            {
                var freeCellsQuantity = capacity - Length;

                if (freeCellsQuantity < userIEnumLength)
                {
                    newCapacity += userIEnumLength - freeCellsQuantity;
                }
            }

            return newCapacity;
        }

        private void ResizeArray(int newSize)
        {
            var newArray = new T[newSize];

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
            int iEnumerableLength = 0;

            foreach (T element in userIEnum)
            {
                iEnumerableLength++;
            }

            return iEnumerableLength;
        }

        private static void NullCheck(IEnumerable<T> userIEnum)
        {
            if (userIEnum is null)
            {
                throw new ArgumentException($"{nameof(userIEnum)} is null!");
            }
        }

        private void IndexCheck(int index)
        {
            if (index < -1 * Length || index > Length - 1)
            {
                throw new ArgumentOutOfRangeException($"{nameof(index)}: индекс находится за границами массива!");
            }
        }

        private void CapacityCheck(int userCapacity)
        {
            if (userCapacity > 0)
            {
                capacity = userCapacity;
            }
            else
            {
                throw new ArgumentException($"{nameof(userCapacity)} < 0. Емкость массива не может быть отрицательной!");
            }
        }

        private void IntMaxValueCheck(int userInt)
        {
            if (userInt > int.MaxValue)
            {
                throw new ArgumentException($"{nameof(userInt)}: Превышено максимальное значение int!");
            }
        }

        private int TryParseNegativeIndex(int index) => index < 0 ? (index += Length) : index;

        public IEnumerator GetEnumerator() => dynamicArray.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)dynamicArray).GetEnumerator();

        public object Clone() => new DynamicArray<T> { dynamicArray = dynamicArray, capacity = capacity };
    }
}
