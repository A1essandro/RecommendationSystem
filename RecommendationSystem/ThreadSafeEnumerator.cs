using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace RecommendationSystem
{
    /// <summary>
    /// I find this class there: http://theburningmonk.com/2010/03/thread-safe-enumeration-in-csharp/
    /// A thread-safe IEnumerator implementation.
    /// See: http://www.codeproject.com/KB/cs/safe_enumerable.aspx
    /// </summary>
    internal class SafeEnumerator<T> : IEnumerator<T>
    {
        // this is the (thread-unsafe)
        // enumerator of the underlying collection
        private readonly IEnumerator<T> _inner;

        // this is the object we shall lock on.
        private readonly ReaderWriterLockSlim _lock;

        public SafeEnumerator(IEnumerator<T> inner, ReaderWriterLockSlim @lock)
        {
            _inner = inner;
            _lock = @lock;

            // entering lock in constructor
            _lock.EnterReadLock();
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            // .. and exiting lock on Dispose()
            // This will be called when foreach loop finishes
            //Monitor.Exit(_lock);
            _lock.ExitReadLock();
        }

        /// <remarks>
        /// we just delegate actual implementation
        /// to the inner enumerator, that actually iterates
        /// over some collection
        /// </remarks>
        public bool MoveNext()
        {
            return _inner.MoveNext();
        }

        public void Reset()
        {
            _inner.Reset();
        }
    }
}