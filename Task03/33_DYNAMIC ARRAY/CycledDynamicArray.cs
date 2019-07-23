using System;
using System.Collections;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    public class CycledDynamicArray<T> : DynamicArray<T>, IEnumerable<T>, IEnumerator<T>
    {
        private T[] cycledDynamicArray = new T[0];
        private int position = -1;

        public CycledDynamicArray(T[] userArray)
        {
            cycledDynamicArray = new T[userArray.Length];
            (userArray as ICollection).CopyTo(cycledDynamicArray, 0);
        }

        public T Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
