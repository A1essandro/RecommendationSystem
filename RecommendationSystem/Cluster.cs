using System;
using System.Collections.Generic;

namespace RecommendationSystem
{
    public class Cluster<T>
    {

        private SortedList<int, T> _items;

        /// <summary>
        /// All items in cluster must be sorted by rank (descending)
        /// Items can contain the same keys (rank) <see cref="DuplicateKeyComparer<TKey>"/>
        /// </summary>
        /// <returns></returns>
        public SortedList<int, T> Items { get { return _items; } }

        public Cluster()
        {
            _items = new SortedList<int, T>(new DuplicateKeyComparer<int>());
        }

        /// <summary>
        /// Add item to cluster
        /// </summary>
        /// <param name="key">Rank of item</param>
        /// <param name="item">Item</param>
        public void Add(int key, T item) => _items.Add(key, item);

        private class DuplicateKeyComparer<TKey> : IComparer<TKey>
            where TKey : IComparable
        {

            public int Compare(TKey x, TKey y)
            {
                int result = x.CompareTo(y);
                return result == 0 ? -1 : -result;
            }

        }

    }
}