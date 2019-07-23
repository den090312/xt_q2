using System;
using System.Collections;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY // включает в себя задание 3.4.	* DYNAMIC ARRAY (HARDCORE MODE)
{
    public class DynamicArray<T> : IEnumerable<T>, ICloneable
    {
        protected T[] dynamicArray = new T[0];
        private int capacity = 0;

        public int Capacity
        {
            get => capacity;
            set
            {
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

        public DynamicArray() => capacity = 8;

        public DynamicArray(int userCapacity)
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

        public T[] ToArray() => dynamicArray;

        public void Add(T element)
        {
            if (Length == capacity)
            {
                if (capacity > 0)
                {
                    SetNewCapacity(capacity * 2);
                }
                else
                {
                    SetNewCapacity(1);
                }
            }

            dynamicArray[Length] = element;
        }

        public void AddRange(IEnumerable<T> userIEnum)
        {
            NullCheck(userIEnum);
            SetNewCapacity(CapacityAdjusment(GetIEnumerableLength(userIEnum)));
            var startIndex = Length;
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
                dynamicArray = new T[newLength];

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
                SetNewCapacity(capacity + 1);
            }

            var tempArray = dynamicArray;
            dynamicArray = new T[capacity];

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

        private int CapacityAdjusment(int userIEnumLength)
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

        private void SetNewCapacity(int newCapacity)
        {
            var newArray = new T[newCapacity];

            dynamicArray.CopyTo(newArray, 0);
            dynamicArray = newArray;
            capacity = newCapacity;
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
            if (index < -1 * Length & index > Length - 1)
            {
                throw new ArgumentOutOfRangeException($"{nameof(index)}: индекс находится за границами массива!");
            }
        }

        private int TryParseNegativeIndex(int index) => index < 0 ? (index += Length) : index;

        public IEnumerator GetEnumerator() => dynamicArray.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)dynamicArray).GetEnumerator();

        public object Clone() => new DynamicArray<T> { dynamicArray = dynamicArray, capacity = capacity };
    }
}
