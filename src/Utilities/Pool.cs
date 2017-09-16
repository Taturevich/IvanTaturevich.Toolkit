using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Utilities
{
    /// <summary>
    /// Simple implementation of object pool technic
    /// </summary>
    public class Pool<T>
    {
        private Func<T> _objectFactory;
        private ConcurrentBag<T> _objects;

        /// <summary>
        /// Initialize object of <see cref="Pool"/> class
        /// </summary>
        /// <param name="objectFactory">creation object function</param>
        public Pool(Func<T> objectFactory)
        {
            if (objectFactory == null)
            {
                throw new ArgumentNullException("Object initializer factory cannot be null");
            }

            _objectFactory = objectFactory;
            _objects = new ConcurrentBag<T>();
        }

        /// <summary>
        /// Initialize object of <see cref="Pool"/> class with initial set of values
        /// </summary>
        /// <param name="objectFactory">creation object function</param>
        /// <param name="initialObjectNumber">number of initialized objects in pool</param>
        public Pool(Func<T> objectFactory, int initialObjectNumber)
        {
            if (objectFactory == null)
            {
                throw new ArgumentNullException("Object initializer factory cannot be null");
            }

            _objectFactory = objectFactory;
            _objects = new ConcurrentBag<T>();
            for(int i = 0; i < initialObjectNumber; i++)
            {
                Put(objectFactory());
            }
        }

        /// <summary>
        /// Try get object from pool or create new one
        /// </summary>
        /// <returns>instance of type <see cref="T"/></returns>
        public T Get()
        {
            if (_objects.TryTake(out T item))
            {
                return item;
            }

            return _objectFactory();
        }

        /// <summary>
        /// Put object into the Pool
        /// </summary>
        /// <param name="item">pooled object</param>
        public void Put(T item)
        {
            _objects.Add(item);
        }
    }
}