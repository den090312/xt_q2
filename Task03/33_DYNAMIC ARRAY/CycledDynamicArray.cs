using System.Collections;
using System.Collections.Generic;

namespace _33_DYNAMIC_ARRAY
{
    public class CycledDynamicArray<T> : IEnumerable, IEnumerable<T>
    {
        private DynamicArray<T> dynamicArray;

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)dynamicArray.ToArray()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)dynamicArray.ToArray()).GetEnumerator();
        }
    }
}
