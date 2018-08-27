// --------------------------------------------------------------------
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// Copyright (c) 2017  Ivan Taturevich
// --------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Linq;
using IvanT.Utilities;
using IvanT.Utilities.Caching;
using Xunit;

namespace UtilitiesTest.Caching
{
    public class LazyListTest
    {
        [Fact]
        public void LazyList_WhenIteratedTwiceThroughForeach_ShouldFullyIterateTwice()
        {
            // Arrange
            var counter = 0;
            var range = Enumerable.Range(1, 100);
            var lazy = new LazyList<int>(range);

            // Act
            foreach (var x in lazy)
            {
                foreach (var y in lazy)
                {
                    counter++;
                }
            }

            // Asssert
            Assert.Equal(10000, counter);
        }

        [Fact]
        public void LazyList_WhenIteratedTwiceThroughLinq_ShouldFullyIterateTwice()
        {
            // Arrange
            var range = Enumerable.Range(1, 100);
            var lazy = new LazyList<int>(range);

            // Act
            var counter = lazy.SelectMany(x => lazy).Count();

            // Asssert
            Assert.Equal(10000, counter);
        }

        [Fact]
        public void LazyList_WhenSourceIsNull_ShouldThrowException()
        {
            // Arrange

            // Act
            void Action()
            {
                var lazy = new LazyList<int>(null);
                Debug.Write(lazy);
            }

            // Asssert
            Assert.Throws<ArgumentNullException>((Action)Action);
        }

        [Fact]
        public void LazyList_WhenCachingInvolved_ShouldCacheValues()
        {
            // Arrange
            var lazyCounter = 0;
            var commonCounter = 0;
            var numbers = Enumerable.Range(1, 35);
            var enumerable = numbers as int[] ?? numbers.ToArray();
            var lazyQuery = enumerable.Select(x =>
            {
                lazyCounter++;
                return x;
            }).ToLazyList();
            var commonQuery = enumerable.Select(x =>
            {
                commonCounter++;
                return x;
            }).ToList();

            // Act
            var lazyEvaluating = lazyQuery
                .Take(5)
                .Concat(lazyQuery.Take(15))
                .Concat(lazyQuery.Take(12)).Sum();
            var commonEvaluating = commonQuery
                .Take(5)
                .Concat(commonQuery.Take(15))
                .Concat(commonQuery.Take(12)).Sum();

            // Assert
            Assert.Equal(5, lazyCounter);
            Assert.Equal(35, commonCounter);
            Assert.Equal(45, lazyEvaluating);
            Assert.Equal(213, commonEvaluating);
        }
    }
}
