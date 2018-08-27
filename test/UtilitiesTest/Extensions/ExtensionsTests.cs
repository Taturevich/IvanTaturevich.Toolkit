// --------------------------------------------------------------------
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// Copyright (c) 2017  Ivan Taturevich
// --------------------------------------------------------------------

using System;
using Xunit;
using Xunit.Abstractions;
using static IvanT.Utilities.Extensions;

namespace UtilitiesTest.Extensions
{
    public class ExtensionsTests
    {
        private readonly ITestOutputHelper _output;

        public ExtensionsTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void RepeatAction_WhenActionProvided_ShouldRepeatProvidedAction()
        {
            // Arrange
            var testingValue = 0;

            // Act
            RepeatAction(() => testingValue++, 10);

            // Assert
            Assert.Equal(10, testingValue);
        }

        [Fact]
        public void RepeatAction_WhenNullActionProvided_ShouldThrowException()
        {
            // Arrange

            // Act
            void TestingAction() => RepeatAction(null, 100);

            // Assert
            Assert.Throws<ArgumentException>((Action)TestingAction);
        }
    }
}
