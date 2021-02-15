using System;
using System.Collections.Generic;
using System.Text;

namespace App.Business.Tests.TestData
{
    /// <summary>
    /// Represents possible test right access levels
    /// </summary>
    public enum ExpectedAccessLevel
    {
        // check there is no rights at all
        No,
        // access should be checked without comparing records count
        Read,
        // for rls check, compare that only allowed records can be read
        ReadOwn,
        // for rls check, compare that all records can be read
        ReadAll,
        // access should be checked without comparing records count
        Write,
        // for rls check, compare that only allowed records can be added
        WriteOwn,
        // for rls check, compare that all records can be added/modified
        Full,
        // skip test
        Skip
    }
}
