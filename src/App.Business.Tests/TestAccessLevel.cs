using System;
using System.Collections.Generic;
using System.Text;

namespace App.Business.Tests
{
    public enum TestAccessLevel
    {
        // check there is no rights at all
        No,
        // for rls check, compare total record count with count read
        Own,
        // access should be checked without comparing records count
        Check,
        // checks, that all records in database can be read
        All,
        // skip test
        Skip
    }
}
