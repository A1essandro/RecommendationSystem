using System;
using System.Collections.Generic;

namespace RecommendationSystem
{

    /// <summary>
    /// Cluster is a set of items, sorted by priority.
    /// </summary>
    /// <example>
    /// Suppose we have users who rate the movies.
    /// For specified user, the cluster must be a set of other users
    /// with similar preferences of movies.
    /// In clusters, you can combine not only users, but also movies.
    /// </example>
    public class Cluster<T>
    {

        private SortedList<int, T> _items;

        /// <summary>
        /// All items in cluster must be sorted by priority (descending)
        /// Items can contain the same keys (priority) <see cref="DuplicateKeyComparer<TKey>"/>
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
        /// <param name="key">Priority of item</param>
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