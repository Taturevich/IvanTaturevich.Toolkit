// --------------------------------------------------------------------
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// Copyright (c) 2017  Ivan Taturevich
// --------------------------------------------------------------------

using System;
using System.Collections.Concurrent;

namespace IvanT.Utilities.Pool
{
    /// <inheritdoc cref="IPool{T}"/>
    public class Pool<T> : IPool<T>
    {
        private readonly Func<T> _objectFactory;
        private readonly ConcurrentBag<T> _objects;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pool{T}"/> class.
        /// </summary>
        /// <param name="objectFactory">creation object function</param>
        public Pool(Func<T> objectFactory)
        {
            _objectFactory = objectFactory ?? throw new ArgumentNullException(nameof(objectFactory), "Object initializer factory cannot be null");
            _objects = new ConcurrentBag<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pool{T}"/> class.
        /// </summary>
        /// <param name="objectFactory">creation object function</param>
        /// <param name="initialObjectNumber">number of initialized objects in pool</param>
        public Pool(Func<T> objectFactory, int initialObjectNumber)
        {
            _objectFactory = objectFactory ?? throw new ArgumentNullException(nameof(objectFactory), "Object initializer factory cannot be null");
            _objects = new ConcurrentBag<T>();
            Extensions.RepeatAction(() => Put(objectFactory()), initialObjectNumber);
        }

        /// <inheritdoc />
        public T Get()
        {
            return _objects.TryTake(out var item) ? item : _objectFactory();
        }

        /// <inheritdoc/>
        public void Put(T item)
        {
            _objects.Add(item);
        }

        /// <summary>
        /// Executed action againts initilized pool
        /// </summary>
        /// <param name="poolExecutedFunction">function definition</param>
        public void Execute(Action<T> poolExecutedFunction)
        {
            var instanceFromPool = Get();
            try
            {
                poolExecutedFunction(instanceFromPool);
            }
            finally
            {
                Put(instanceFromPool);
            }
        }
    }
}
