using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using App.Business.Tests.TestData;
using Core.Services.Data;
using Xunit.Sdk;

namespace App.Business.Tests.Security
{
    /// <summary>
    /// This class was created for usage as DataAttribute in Theory tests
    /// But somehow not every test launch uses this attribute correctly
    //  So the class is used now as data source for MemberData test attribute
    /// </summary>
    public class SecurityTestExcelDataAttribute: DataAttribute
    {
        // for performance reasons _filePath is the same for all instances
        // this allows to read file only once and save to static variable
        private static readonly string _entityColumnName = "Entity";
        private static readonly string _dtoColumnName = "Dto";
        private static readonly string _filePath = Path.Combine("Data", "AllProfilesSecurityTestData.xlsx");
        // security profile name
        private readonly string _profileName = "";
        // contains all test data read from excel
        private static Dictionary<string, SecurityTestDataUnit[]> TestData = null;

        // dictionary contains relation between Excel expected access level name and the c# actual value
        private static readonly Dictionary<string, ExpectedAccessLevel> _accessLevels = new Dictionary<string, ExpectedAccessLevel>
        {
            { "N", ExpectedAccessLevel.No },
            { "R", ExpectedAccessLevel.Read },
            { "RO", ExpectedAccessLevel.ReadOwn },
            { "RA", ExpectedAccessLevel.ReadAll },
            { "W", ExpectedAccessLevel.Write },
            { "WO", ExpectedAccessLevel.WriteOwn },
            { "WA", ExpectedAccessLevel.Full },
            { "S", ExpectedAccessLevel.Skip }
        };

        static SecurityTestExcelDataAttribute()
        {
            // read excel during initialization to static TestData private variable
            LoadDataFromExcel();
        }

        public SecurityTestExcelDataAttribute(string profileName)
        {
            if (!TestData.TryGetValue(profileName, out var _))
            {
                throw new ArgumentException($"Data for profile {profileName} was not present in test data file");
            }
            _profileName = profileName;
        }

        /// <inheritdoc/>
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            // get data required for concrete method 
            TestData.TryGetValue(_profileName, out var profileData);
            if (testMethod.Name == "CanReadEntity")
            {
                return profileData.Where(x => x.EntityType != null && x.DtoType == null && x.ExpectedAccessLevel != ExpectedAccessLevel.Skip)
                    .Select(datUnit => DataUnitToReadEntityTestCaseConverter.Convert(datUnit));
            }
            else if (testMethod.Name == "CanReadDto")
            {
                return profileData.Where(x => x.DtoType != null && x.ExpectedAccessLevel != ExpectedAccessLevel.Skip)
                    .Select(datUnit => DataUnitToReadDtoTestCaseConverter.Convert(datUnit));
            }
            else if (testMethod.Name == "CanAddEntity")
            {
                return profileData.Where(x => x.EntityType != null && x.DtoType == null && x.ExpectedAccessLevel != ExpectedAccessLevel.Skip)
                    .Select(datUnit => DataUnitToWriteEntityTestCaseConverter.Convert(datUnit));
            }
            else if (testMethod.Name == "CanAddDto")
            {
                return profileData.Where(x => x.DtoType != null && x.EntityType != null && x.ExpectedAccessLevel != ExpectedAccessLevel.Skip)
                    .Select(datUnit => DataUnitToWriteDtoTestCaseConverter.Convert(datUnit));
            }
            else if (testMethod.Name == "SkipTests")
            {
                return profileData.Where(x => x.EntityType != null && x.ExpectedAccessLevel == ExpectedAccessLevel.Skip)
                    .Select(datUnit => DataUnitToWriteDtoTestCaseConverter.Convert(datUnit));
            }
            else
            {
                throw new InvalidOperationException($"Test method {testMethod.Name} is not supported");
            }
        }

        /// <summary>
        /// Loads data from exsel into DataTable and then creates c# test objects from this data
        /// </summary>
        private static void LoadDataFromExcel()
        {
            using var stream = File.OpenRead(_filePath);

            var columns = new Dictionary<string, string>
            {
                { _entityColumnName, _entityColumnName},
                { _dtoColumnName, _dtoColumnName },
                { "AdE" , "AdE" },
                { "AdES", "AdES" },
                { "AdEL", "AdEL" },
                { "AsES", "AsES" },
                { "CD"  , "CD" },
                { "CDE" , "CDE" },
                { "CDES", "CDES" },
                { "R"   , "R" },
                { "RE"  , "RE" },
                { "RES" , "RES" },
                { "FD"  , "FD" },
                { "FDE" , "FDE" },
                { "SES" , "SES" },
                { "HRE" , "HRE" },
                { "HRES", "HRES" }
            };

            var documentData = (new XlsxService()).ReadStream(stream, null, columns);
            ConvertTableDataToTestObjects(documentData);
        }

        /// <summary>
        /// Converts data table that was read from excel to c# test data colections
        /// </summary>
        /// <param name="dataTable"></param>
        private static void ConvertTableDataToTestObjects(DataTable dataTable)
        {
            TestData = new Dictionary<string, SecurityTestDataUnit[]>();
            // all columns that were read except Entity and Dto - the columns with profile names
            // first we read columns and fill dictionary object with keys - profile names and empty values
            // then iterate rows and fill dictionary with values

            var rowsCount = dataTable.Rows.Count;
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.ColumnName != _entityColumnName && column.ColumnName != _dtoColumnName)
                {
                    TestData.Add(column.ColumnName, new SecurityTestDataUnit[rowsCount]);
                }
            }

            // iterate created previously dictionary where keys - profile names
            // and then iterate dataTable to fill dictionary with data
            foreach (var testData in TestData)
            {
                var rowIndex = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    // key - profile name
                    // value - test units data Array
                    testData.Value[rowIndex] = CreateDataUnitFromDataRow(row, testData.Key);
                    rowIndex++;
                }
            }
        }

        /// <summary>
        /// Creates one data unit, that will be used by test methods
        /// </summary>
        /// <param name="row"><see cref="DataRow"/> current data table row</param>
        /// <param name="profileName">Name of the security profile - one of the data columns</param>
        /// <returns>Data unit, that contains tested type and expected access level to test</returns>
        private static SecurityTestDataUnit CreateDataUnitFromDataRow(DataRow row, string profileName)
        {
            var entityType = GetTypeFromString(row[_entityColumnName].ToString());
            var dtoType = GetTypeFromString(row[_dtoColumnName].ToString());
            var accessLevel = GetAccessLevelFromString(row[profileName].ToString());

            return new SecurityTestDataUnit
            {
                EntityType = entityType,
                DtoType = dtoType,
                ExpectedAccessLevel = accessLevel
            };
        }

        /// <summary>
        /// Converts type name to Type
        /// </summary>
        /// <param name="typeName">Name of the type, for example Organization or EmployeeDto</param>
        /// <returns>Type or null if there is type name is less then 3 symbols (this filters some "service" symbols like ""</returns>
        private static Type GetTypeFromString(string typeName)
        {
            // absent entity or dto can be marked as "-" or any other symbol
            // we check length of entity or dto name to be sure that this is valid name
            if (typeName.Length > 2)
            {
                return TestReflectionHelper.GetTypeByName(typeName);
            }

            return null;
        }

        /// <summary>
        /// Converts string representation of access level to <see cref="ExpectedAccessLevel"/>
        /// </summary>
        /// <param name="accessLevelName">String representation of access level: 
        /// N -  No
        /// R -  Read (read check without rls check)
        /// RO - Read own
        /// RA - Read all
        /// W  - Write (write check without rls check)
        /// WO - Write own
        /// WA - Write all
        /// S  - Skip this test
        /// </param>
        /// <returns>Expected access level or Exception</returns>
        private static ExpectedAccessLevel GetAccessLevelFromString(string accessLevelName)
        {
            if (_accessLevels.TryGetValue(accessLevelName, out var accessLevel))
            {
                return accessLevel;
            }

            throw new DataException($"Access level {accessLevelName} is not supported. " +
                $"Check, that Excel file defines only the following names: {string.Join(",", _accessLevels.Keys)}");
        }
    }
}
