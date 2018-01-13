using System;
using System.Collections;
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
    public class Cluster<T> : IReadOnlyDictionary<int, T>
    {

        private SortedList<int, T> _items;
        private object _lock = new object();

        public Cluster()
        {
            _items = new SortedList<int, T>(new DuplicateKeyComparer<int>());
        }

        public T this[int index]
        {
            get
            {
                return _items[index];
            }
            set
            {
                Add(index, value);
            }
        }

        /// <summary>
        /// Add item to cluster
        /// </summary>
        /// <param name="key">Priority of item</param>
        /// <param name="item">Item</param>
        public void Add(int key, T item)
        {
            lock (_lock)
            {
                _items.Add(key, item);
            }
        }

        public IEnumerable<int> Keys => _items.Keys;

        public IEnumerable<T> Values => _items.Values;

        public int Count => _items.Count;
        
        public bool ContainsKey(int key) => _items.ContainsKey(key);

        public IEnumerator<KeyValuePair<int, T>> GetEnumerator() => _items.GetEnumerator();

        public bool TryGetValue(int key, out T value) => _items.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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