using System.Collections;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    public class CycledDynamicArray<T> : IEnumerable, IEnumerable<T>, IEnumerator
    {
        private DynamicArray<T> baseDynamicArray;

        int position = -1;

        public CycledDynamicArray(DynamicArray<T> userDynamicArray)
        {
            baseDynamicArray = userDynamicArray;
        }

        public object Current => Current;

        public bool MoveNext()
        {
            position++;

            if (position >= baseDynamicArray.Length)
            {
                position = 0;
            }

            return (position < baseDynamicArray.Length);
        }

        public void Reset() => position = -1;

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)baseDynamicArray).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)baseDynamicArray).GetEnumerator();
        }
    }
}
