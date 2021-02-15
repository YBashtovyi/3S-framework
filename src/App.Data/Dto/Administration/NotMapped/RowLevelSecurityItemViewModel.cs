using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Administration;
using Core.Security;

namespace App.Data.Dto.Administration.NotMapped
{
    /// <summary>
    /// The class is used to display detailed information RLS on the front end
    /// </summary>
    public class RowLevelSecurityItemViewModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Organization name, user full name, etc. which refers to detailed information of RLS
        /// </summary>
        [NotMapped]
        public string Name { get; set; }

        /// <summary>
        /// Table name of the RLS (OrgUnit, User or etc)
        /// </summary>
        [NotMapped]
        public string DerivedEntity { get; set; }

        /// <summary>
        /// Data access (RLS)
        /// </summary>
        public CrudOperation AccessLevel { get; set; } 
    }
}
