using System;
using System.Collections.Generic;
using System.Text;
using App.Business.Tests.TestData;

namespace App.Business.Tests.Security
{
    /// <summary>
    /// Class-helper to convert incoming data to specific data object needed for tests
    /// </summary>
    public static class DataUnitToReadEntityTestCaseConverter
    {
        /// <summary>
        /// Converts data test unit to specific obect[]
        /// </summary>
        /// <param name="dataUnit">Common data test unit</param>
        /// <returns>Specific object[] with elements needed for ReadEntity tests as parameters</returns>
        public static object[] Convert(SecurityTestDataUnit dataUnit)
        {
            return new object[]
            {
                dataUnit.EntityType,
                TestAccessLevelReadConverter.Convert(dataUnit.ExpectedAccessLevel)
            };
        }
    }

    /// <summary>
    /// Class-helper to convert incoming data to specific data object needed for tests
    /// </summary>
    public static class DataUnitToReadDtoTestCaseConverter
    {
        /// <summary>
        /// Converts data test unit to specific obect[]
        /// </summary>
        /// <param name="dataUnit">Common data test unit</param>
        /// <returns>Specific object[] with elements needed for ReadDto tests as parameters</returns>
        public static object[] Convert(SecurityTestDataUnit dataUnit)
        {
            return new object[]
            {
                dataUnit.DtoType,
                TestAccessLevelReadConverter.Convert(dataUnit.ExpectedAccessLevel)
            };
        }
    }

    /// <summary>
    /// Class-helper to convert incoming data to specific data object needed for tests
    /// </summary>
    public static class DataUnitToWriteEntityTestCaseConverter
    {
        /// <summary>
        /// Converts data test unit to specific obect[]
        /// </summary>
        /// <param name="dataUnit">Common data test unit</param>
        /// <returns>Specific object[] with elements needed for AddEntity tests as parameters</returns>
        public static object[] Convert(SecurityTestDataUnit dataUnit)
        {
            return new object[]
            {
                dataUnit.EntityType,
                TestAccessLevelWriteConverter.Convert(dataUnit.ExpectedAccessLevel)
            };
        }
    }

    /// <summary>
    /// Class-helper to convert incoming data to specific data object needed for tests
    /// </summary>
    public static class DataUnitToWriteDtoTestCaseConverter
    {
        /// <summary>
        /// Converts data test unit to specific obect[]
        /// </summary>
        /// <param name="dataUnit">Common data test unit</param>
        /// <returns>Specific object[] with elements needed for AddDto tests as parameters</returns>
        public static object[] Convert(SecurityTestDataUnit dataUnit)
        {
            return new object[]
            {
                dataUnit.DtoType,
                dataUnit.EntityType,
                TestAccessLevelWriteConverter.Convert(dataUnit.ExpectedAccessLevel)
            };
        }
    }
}
