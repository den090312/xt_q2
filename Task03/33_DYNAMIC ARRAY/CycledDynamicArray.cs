using System.Collections;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    public class CycledDynamicArray<T> : DynamicArray<T>, IEnumerable, IEnumerable<T>, IEnumerator
    {
        private T[] cycledDynamicArray = new T[0];
        private int position = -1;

        public CycledDynamicArray(T[] userArray)
        {
            cycledDynamicArray = new T[userArray.Length];
            (userArray as ICollection).CopyTo(cycledDynamicArray, 0);
        }

        public object Current => Current;

        public new IEnumerator GetEnumerator()
        {
            if (!MoveNext())
            {
                Reset();
            }

            return cycledDynamicArray.GetEnumerator();
        }

        public bool MoveNext()
        {
            position++;
            return (position < cycledDynamicArray.Length);
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
