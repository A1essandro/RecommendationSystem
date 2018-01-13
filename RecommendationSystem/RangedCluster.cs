using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace RecommendationSystem
{

    /// <summary>
    /// Cluster sorted by priority.
    /// </summary>
    /// <example>
    /// Suppose we have users who rate the movies.
    /// For specified user, the cluster must be a set of other users
    /// with similar preferences of movies.
    /// In clusters, you can combine not only users, but also movies.
    /// </example>
    public class RangedCluster<T> : ICluster<T>, IDisposable
    {

        private readonly SortedList<int, T> _items;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public RangedCluster()
        {
            _items = new SortedList<int, T>(new DuplicateKeyComparer<int>());
        }

        public T this[int index]
        {
            get
            {
                return _threadSafeRead(() => _items[index]);
            }
        }

        /// <summary>
        /// Add item to cluster
        /// </summary>
        /// <param name="key">Priority of item</param>
        /// <param name="item">Item</param>
        public void Add(int key, T item)
        {
            _lock.EnterWriteLock();
            try
            {
                _items.Add(key, item);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public IEnumerable<int> Keys => _threadSafeRead(() => _items.Keys);

        public IEnumerable<T> Values => _threadSafeRead(() => _items.Values);

        public int Count => _threadSafeRead(() => _items.Count);

        public bool ContainsKey(int key) => _threadSafeRead(() => _items.ContainsKey(key));

        public IEnumerator<KeyValuePair<int, T>> GetEnumerator() => _threadSafeRead(() => _items.GetEnumerator());

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool TryGetValue(int key, out T value)
        {
            _lock.EnterReadLock();
            try
            {
                return _items.TryGetValue(key, out value);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        #region Private

        private class DuplicateKeyComparer<TKey> : IComparer<TKey>
            where TKey : IComparable
        {

            public int Compare(TKey x, TKey y)
            {
                int result = x.CompareTo(y);
                return result == 0 ? -1 : -result;
            }

        }

        private TResult _threadSafeRead<TResult>(Func<TResult> function)
        {
            _lock.EnterReadLock();
            try
            {
                return function();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _lock.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

    }
}