// --------------------------------------------------------------------
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// Copyright (c) 2017  Ivan Taturevich
// --------------------------------------------------------------------

using System;
using Xunit;
using static IvanT.Utilities.Extensions;

namespace UtilitiesTest.Extensions
{
    public class ExtensionsTests
    {
        [Fact]
        public void RepeatAction_WhenActionProvided_ShouldRepeatProvidedAction()
        {
            // Arrange
            var testingValue = 0;

            // Act
            RepeatAction(() => testingValue++, 10);

            // Assert
            Assert.Equal(testingValue, 10);
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
