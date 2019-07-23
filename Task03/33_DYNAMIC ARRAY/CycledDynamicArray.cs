using System;
using System.Collections;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    public class CycledDynamicArray<T> : DynamicArray<T>
    {
        private T[] cycledDynamicArray = new T[0];

        public CycledDynamicArray(T[] userArray)
        {
            cycledDynamicArray = new T[userArray.Length];
            (userArray as ICollection).CopyTo(cycledDynamicArray, 0);
        }

        public new IEnumerator<T> GetEnumerator()
        {
            return new CycledEnumerator(cycledDynamicArray);
        }

        class CycledEnumerator : IEnumerator<T>
        {
            private T[] cycledDynamicArray = new T[0];
            private int position = -1;

            public CycledEnumerator(T[] userArray)
            {
                cycledDynamicArray = userArray;
            }

            public T Current => cycledDynamicArray[position];

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (position < cycledDynamicArray.Length - 1)
                {
                    position++;
                }
                else
                {
                    Reset();                
                }

                return true;
            }

            public void Reset()
            {
                position = -1;
            }

            public void Dispose() => throw new NotImplementedException();
        }
    }
}

