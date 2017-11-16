using System.Collections.Generic;
using System;

namespace Generics
{
    [System.Serializable] public class CustomList<T>
    {
        public T[] list;
        public int capacity { get; }
        public int amount { get; private set; }
        public CustomList() { }

        public T this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        // Adds item to the end of the CustomList<T>
        public void Add (T item)
        {
            // Create a new array of amount + 1
            T[] cache = new T[amount + 1];

            // Check intialize
            if (list != null)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    cache[i] = list[i];
                }
            }

            // Place new item at end of index
            cache[amount] = item;

            // Replace old array with new array
            list = cache;

            // Increment amount
            amount++;
        }

        // Find
        static void Find ()
        {
            // Contains, Exists, Find, FindAll, FindIndex, FindLast, FindLastIndex, IndexOf, LastIndexOf
            List<int> myPartyAges = new List<int> { 35, 39, 42, 88, 42, 99 };
            int fortyTwosIndex = myPartyAges.IndexOf(42);
            Console.WriteLine(fortyTwosIndex);
        }
    }

    // AddRange
    public static class CollectionHelpers
    {
        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            List<T> list = destination as List<T>;

            if (list != null)
            {
                list.AddRange(source);
            }
            else
            {
                foreach (T item in source)
                {
                    destination.Add(item);
                }
            }
        }
    }
}
