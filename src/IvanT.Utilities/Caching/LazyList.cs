// --------------------------------------------------------------------
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// Copyright (c) 2017  Ivan Taturevich
// --------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IvanT.Utilities.Caching
{
    /// <summary>
    /// List which cache once evaluated
    /// </summary>
    /// <typeparam name="T">containing type</typeparam>
    public class LazyList<T> : IEnumerable<T>, IDisposable
    {
        private readonly IList<T> _cache;
        private IEnumerator<T> _sourceEnumerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyList{T}"/> class.
        /// </summary>
        /// <param name="source">enumerable source</param>
        public LazyList(IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (_cache == null)
            {
                _cache = new List<T>();
                _sourceEnumerator = source.GetEnumerator();
            }
            else
            {
                IsAllElementsAreCached = true;
                _sourceEnumerator = Enumerable.Empty<T>().GetEnumerator();
            }
        }

        /// <summary>
        /// Gets a value indicating whether check if all elements were cached
        /// </summary>
        public bool IsAllElementsAreCached { get; private set; }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return IsAllElementsAreCached ? _cache.GetEnumerator() : new LazyListEnumerator(this);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposing by flag
        /// </summary>
        /// <param name="disposing">disposing flag</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (_sourceEnumerator == null)
            {
                return;
            }

            _sourceEnumerator.Dispose();
            _sourceEnumerator = null;
        }

        private sealed class LazyListEnumerator : IEnumerator<T>
        {
            private const int StartIndex = -1;

            private readonly LazyList<T> _lazyList;
            private readonly object _lock = new object();
            private int _index = StartIndex;

            public LazyListEnumerator(LazyList<T> lazyList)
            {
                _lazyList = lazyList;
            }

            /// <inheritdoc cref="IEnumerator{T}.Current"/>
            public T Current { get; private set; }

            object IEnumerator.Current => Current;

            /// <summary>
            /// Gets a value indicating whether check if index in cache
            /// </summary>
            private bool IsIndexItemIsInCache => _index < _lazyList._cache.Count;

            public bool MoveNext()
            {
                var result = true;
                _index++;
                if (IsIndexItemIsInCache)
                {
                    SetCurrentToIndex();
                }
                else
                {
                    lock (_lock)
                    {
                        if (IsIndexItemIsInCache)
                        {
                            SetCurrentToIndex();
                        }
                        else
                        {
                            result = !_lazyList.IsAllElementsAreCached && _lazyList._sourceEnumerator != null && _lazyList._sourceEnumerator.MoveNext();
                            if (result)
                            {
                                Current = _lazyList._sourceEnumerator.Current;
                                _lazyList._cache.Add(_lazyList._sourceEnumerator.Current);
                            }
                            else if (!_lazyList.IsAllElementsAreCached)
                            {
                                _lazyList.IsAllElementsAreCached = true;
                                _lazyList?._sourceEnumerator?.Dispose();
                            }
                        }
                    }
                }

                return result;
            }

            public void Reset()
            {
                _index = StartIndex;
            }

            /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
            public void Dispose()
            {
                _lazyList?.Dispose();
            }

            private void SetCurrentToIndex()
            {
                Current = _lazyList._cache[_index];
            }
        }
    }
}
