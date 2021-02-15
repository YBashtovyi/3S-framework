using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Base.Administration;

namespace Core.Administration.Helpers
{
    public static class CrudOperationHelper
    {
        public static bool ParseCrudOperation(string crud, out CrudOperation accessLevel)
        {
            accessLevel = 0;
            var accessLevels = crud.ToArray();
            foreach (var c in accessLevels)
            {
                if (!Enum.TryParse(c.ToString(), out CrudOperation level))
                {
                    continue;
                }

                accessLevel |= level;
            }

            return accessLevel != 0;
        }

        /// <summary>
        /// Parse crud and set <see cref="CrudOperation"/>
        /// </summary>
        /// <param name="rlsList"></param>
        public static void ParseCrudOperationRls(IEnumerable<RowLevelSecurityItem> rlsList)
        {
            foreach (var rowLevelSecurityItem in rlsList)
            {
                if (!ParseCrudOperation(rowLevelSecurityItem.Els, out var accessLevel))
                {
                    continue;
                }

                rowLevelSecurityItem.AccessLevel = accessLevel;
            }
        }

        /// <summary>
        /// Parse crud and set <see cref="CrudOperation"/>
        /// </summary>
        /// <param name="rlsData"></param>
        public static void ParseCrudOperationRls(RowLevelSecurityData rlsData)
        {
            ParseCrudOperationRls(rlsData.OrgUnit);
            ParseCrudOperationRls(rlsData.User);
            ParseCrudOperationRls(rlsData.AtuObject);
        }
    }
}
