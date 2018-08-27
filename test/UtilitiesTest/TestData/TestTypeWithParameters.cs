// --------------------------------------------------------------------
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// Copyright (c) 2017  Ivan Taturevich
// --------------------------------------------------------------------

namespace UtilitiesTest.TestData
{
    public class TestTypeWithParameters
    {
        public TestTypeWithParameters(TestHolder testHolder, TestPocoClass testPocoClass)
        {
            TestHolder = testHolder;
            TestPocoClass = testPocoClass;
        }

        public TestHolder TestHolder { get; }

        public TestPocoClass TestPocoClass { get; }
    }
}
