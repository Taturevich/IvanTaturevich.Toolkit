// --------------------------------------------------------------------
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// Copyright (c) 2017  Ivan Taturevich
// --------------------------------------------------------------------

namespace IvanT.Utilities.Pool
{
    /// <summary>
    /// Simple implementation of object pool technic.
    /// type of object specified in <see cref="T"/>
    /// </summary>
    public interface IPool<T>
    {
        /// <summary>
        /// Try get object from pool or create new one
        /// </summary>
        /// <returns>instance of type <see cref="T"/></returns>
        T Get();

        /// <summary>
        /// Put object into the Pool
        /// </summary>
        /// <param name="item">pooled object</param>
        void Put(T item);
    }
}
