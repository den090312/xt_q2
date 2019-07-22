using System;
using System.Collections;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    public class DynamicArray<T> : IEnumerable, IEnumerable<T>
    {
        private T[] dynamicArray = new T[0];

        public int Capacity { get; private set; } = 0;

        public int Length => dynamicArray.Length;

        public DynamicArray() => Capacity = 8;

        public DynamicArray(int userCapacity)
        {
            if (userCapacity > 0)
            {
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
            FillDynamicArrayFromIEnumerable(userIEnum);
        }

        public T this[int i]
        {
            get
            {
                IndexCheck(i);
                return dynamicArray[i];
            }

            set
            {
                IndexCheck(i);
                dynamicArray[i] = value;
            }
        }

        public void Add(T element)
        {
            if (Length == Capacity)
            {
                if (Capacity > 0)
                {
                    SetNewCapacity(Capacity * 2);
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
            FillDynamicArrayFromIEnumerable(userIEnum);
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

            bool inserted = false;

            if (Length == Capacity)
            {
                SetNewCapacity(Capacity + 1);
            }

            var tempArray = dynamicArray;
            dynamicArray = new T[Capacity];

            int j = 0;

            for (int i = 0; i < Capacity; i++)
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
            int newCapacity = Capacity;

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

        private void FillDynamicArrayFromIEnumerable(IEnumerable<T> userIEnum)
        {
            var startIndex = Length;

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
            //if (index < 0)
            //{
            //    throw new ArgumentException($"{nameof(index)} < 0: индекс не может быть отрицательным!");
            //}

            //if (index > Length - 1)
            //{
            //    throw new ArgumentOutOfRangeException($"{nameof(index)}: индекс находится за границами массива!");
            //}

            if (index < -1 * Length & index > Length - 1)
            {
                throw new ArgumentOutOfRangeException($"{nameof(index)}: индекс находится за границами массива!");
            }
        }

        public IEnumerator GetEnumerator() => dynamicArray.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)dynamicArray).GetEnumerator();
    }
}
