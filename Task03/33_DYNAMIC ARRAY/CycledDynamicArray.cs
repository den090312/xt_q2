using System.Collections;

namespace _33_DYNAMIC_ARRAY
{
    public class CycledDynamicArray<T> : IEnumerable
    {
        private readonly DynamicArray<T> baseDynamicArray;

        public CycledDynamicArray(DynamicArray<T> userDynamicArray) => baseDynamicArray = userDynamicArray;

        public IEnumerator GetEnumerator()
        {
            if (!GetEnumerator().MoveNext())
            {
                GetEnumerator().Reset();
            }

            return baseDynamicArray.GetEnumerator();
        }
    }
}
