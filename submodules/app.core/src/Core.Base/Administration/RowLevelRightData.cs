using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Base.Administration
{
    /// <summary>
    /// Class represents row level right data for property of a class
    /// For example, the data for OrganizationId of Employee class
    /// </summary>
    public class RowLevelRightData
    {
        /// <summary>
        /// Name of the field, that represents the entity, for example OrganizationId in Employee class
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///
        /// </summary>
        public CrudOperation AccessLevel { get; set; }
        /// <summary>
        /// List of ids for type that Name property references to, for example list of OrganizationId
        /// </summary>
        public List<string> Entities { get; set; }

        public RowLevelRightData(string name, CrudOperation accessLevel, List<string> entities)
        {
            Name = name;
            AccessLevel = accessLevel;
            Entities = entities;
        }
    }
}
