using System;
using System.Collections.Generic;
using System.Text;

namespace App.Business.Tests.TestData
{
    /// <summary>
    /// Represents one data unit to test
    /// </summary>
    /// <remarks>
    /// Planned test cases:
    ///   Read entity, read dto, add entity, add dto
    /// </remarks>
    public class SecurityTestDataUnit
    {
        /// <summary>
        /// Test entity type. Can be null if class is used for dto tests
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// Test dto type. Can be null if is class used for entity tests
        /// </summary>
        public Type DtoType { get; set; }

        /// <summary>
        /// Expected access level
        /// </summary>
        public ExpectedAccessLevel ExpectedAccessLevel { get; set; }
    }
}
