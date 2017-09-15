using System;
using System.Collections.Concurrent;

namespace Utilities
{
    public class Pool<T>
    {
        private Func<T> _objectFactory;
        private ConcurrentBag<T> _objects;

        public Pool(Func<T> objectFactory)
        {
            if (objectFactory == null) 
            {
                throw new ArgumentNullException("objectGenerator");
            }

            _objects = new ConcurrentBag<T>();
            _objectFactory = objectFactory;
        }

        public T Get()
        {
            T item;
            if (_objects.TryTake(out item)) 
            {
                return item;
            }

            return _objectFactory();
        }

        public void Put(T item)
        {
            _objects.Add(item);
        }
    }
}