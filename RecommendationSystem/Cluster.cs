using System;
using System.Collections.Generic;

namespace RecommendationSystem
{
    public class Cluster<T>
    {

        SortedList<int, T> _items;

        public Cluster()
        {
            _items = new SortedList<int, T>(new DuplicateKeyComparer<int>());
        }

        public void Add(int key, T item)
        {
            _items.Add(key, item);
        }

        public class DuplicateKeyComparer<TKey> : IComparer<TKey>
            where TKey : IComparable
        {

            public int Compare(TKey x, TKey y)
            {
                int result = x.CompareTo(y);
                return result == 0 ? 1 : result;
            }

        }

    }
}