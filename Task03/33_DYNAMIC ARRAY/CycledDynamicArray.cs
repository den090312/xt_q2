using System.Collections;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    public class CycledDynamicArray<T> : IEnumerable<T>, IEnumerable, IEnumerator
    {
        private readonly DynamicArray<T> baseDynamicArray;
        private int position = -1;

        public CycledDynamicArray(DynamicArray<T> userDynamicArray) => baseDynamicArray = userDynamicArray;

        object IEnumerator.Current => baseDynamicArray.GetEnumerator().Current;

        bool IEnumerator.MoveNext()
        {
            position++;

            return (position < baseDynamicArray.Length);
        }

        void IEnumerator.Reset() => position = -1;

        public IEnumerator GetEnumerator()
        {
            return baseDynamicArray.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)baseDynamicArray).GetEnumerator();
        }
    }
}
