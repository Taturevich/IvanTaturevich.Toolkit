// --------------------------------------------------------------------
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// Copyright (c) 2017  Ivan Taturevich
// --------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace UtilitiesTest.TestData
{
    /// <summary>
    /// Test Poco Class
    /// </summary>
    public class TestPocoClass
    {
        /// <summary>
        /// Gets or sets poco int value
        /// </summary>
        public int PocoInt { get; set; }

        /// <summary>
        /// Gets or sets poco char value
        /// </summary>
        public char PocoChar { get; set; }

        /// <summary>
        /// Gets or sets poco float value
        /// </summary>
        public float PocoFloat { get; set; }

        /// <summary>
        /// Gets or sets poco double value
        /// </summary>
        public double PocoDouble { get; set; }

        /// <summary>
        /// Gets or sets poco string value
        /// </summary>
        public string PocoString { get; set; }

        /// <summary>
        /// Gets or sets poco decimal value
        /// </summary>
        public decimal PocoDecimal { get; set; }

        /// <summary>
        /// Gets or sets poco DateTime value
        /// </summary>
        public DateTime PocoDateTime { get; set; }

        /// <summary>
        /// Gets or sets poco TestHolder collection
        /// </summary>
        public List<TestHolder> TestHolders { get; set; } = new List<TestHolder>();
    }
}
