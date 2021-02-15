using System;
using System.Collections.Generic;
using System.Text;
using App.Business.Tests.TestData;

namespace App.Business.Tests.Security
{
    /// <summary>
    /// Class-helper to convert <see cref="ExpectedAccessLevel"/> to <see cref="TestAccessLevel"/> for read check tests
    /// </summary>
    public static class TestAccessLevelReadConverter
    {
        /// <summary>
        /// Converts <see cref="ExpectedAccessLevel"/> to <see cref="TestAccessLevel"/> for read check tests
        /// </summary>
        /// <param name="expectedAccessLevel"></param>
        /// <returns></returns>
        public static TestAccessLevel Convert(ExpectedAccessLevel expectedAccessLevel)
        {
            return expectedAccessLevel switch
            {
                ExpectedAccessLevel.Full => TestAccessLevel.All,
                ExpectedAccessLevel.Write => TestAccessLevel.Check,
                ExpectedAccessLevel.WriteOwn => TestAccessLevel.Own,
                ExpectedAccessLevel.Read => TestAccessLevel.Check,
                ExpectedAccessLevel.ReadOwn => TestAccessLevel.Own,
                ExpectedAccessLevel.ReadAll => TestAccessLevel.All,
                ExpectedAccessLevel.Skip => TestAccessLevel.Skip,
                _ => TestAccessLevel.No,
            };
        }
    }

    /// <summary>
    /// Class-helper to convert <see cref="ExpectedAccessLevel"/> to <see cref="TestAccessLevel"/> for write check tests
    /// </summary>
    public static class TestAccessLevelWriteConverter
    {
        /// <summary>
        /// /// Converts <see cref="ExpectedAccessLevel"/> to <see cref="TestAccessLevel"/> for write check tests
        /// </summary>
        /// <param name="expectedAccessLevel"></param>
        /// <returns></returns>
        public static TestAccessLevel Convert(ExpectedAccessLevel expectedAccessLevel)
        {
            return expectedAccessLevel switch
            {
                ExpectedAccessLevel.Full => TestAccessLevel.All,
                ExpectedAccessLevel.Write => TestAccessLevel.Check,
                ExpectedAccessLevel.WriteOwn => TestAccessLevel.Own,
                ExpectedAccessLevel.Read => TestAccessLevel.No,
                ExpectedAccessLevel.ReadOwn => TestAccessLevel.No,
                ExpectedAccessLevel.ReadAll => TestAccessLevel.No,
                ExpectedAccessLevel.Skip => TestAccessLevel.Skip,
                _ => TestAccessLevel.No,
            };
        }
    }
}
