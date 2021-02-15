using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace Core.Base.Administration
{
    public class RowLevelSecurityData
    {
        public IList<RowLevelSecurityItem> OrgUnit { get; set; } = new List<RowLevelSecurityItem>();
        public IList<RowLevelSecurityItem> AtuObject { get; set; } = new List<RowLevelSecurityItem>();
        public IList<RowLevelSecurityItem> User { get; set; } = new List<RowLevelSecurityItem>();
    }

    public class RowLevelSecurityItem
    {
        public RowLevelSecurityItem()
        {
            AccessLevel = CrudOperation.None;
        }

        public Guid Id { get; set; }
        public string Els { get; set; }

        /// <summary>
        /// Rls User or Role
        /// </summary>
        [NotMapped, JsonIgnore]
        public string RlsOwner { get; set; }
        
        [NotMapped, JsonIgnore]
        public CrudOperation AccessLevel { get; set; }
    }
}
