using System;
using System.ComponentModel.DataAnnotations;
using Core.Data.Models.Org;
using Core.Security;

namespace App.Data.Models
{
    /// <summary>
    /// Class with represents department model
    /// </summary>
    [MainEntity(nameof(Department))]
    [RlsRight(nameof(OrgUnit), nameof(Id))]
    public class Department: BaseDepartment
    {
        #region IDerivedEntity
        public override Type BaseType => typeof(OrgUnit);
        #endregion

        /// <summary>
        /// State of the department <see cref="BaseEnumRecord"/>
        /// </summary>
        [StringLength(50, MinimumLength = 1)]
        public string DepartmentState { get; set; }

        public OrgUnit Parent { get; set; }
    }
}
