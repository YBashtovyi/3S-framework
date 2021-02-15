using System;
using System.ComponentModel.DataAnnotations;
using Core.Data.Models.Org;
using Core.Security;

namespace App.Data.Models
{
    /// <inheritdoc cref="BaseOrganization" />
    [MainEntity(nameof(Organization))]
    [RlsRight(nameof(Organization), nameof(Id))]
    public class Organization: BaseOrganization
    {
        #region IDerivedEntity
        public override Type BaseType => typeof(OrgUnit);
        #endregion

        [Required ,MaxLength(50)]
        public string OrganizationCategory { get; set; }

        [Required ,MaxLength(50)]
        public string OrgState { get; set; }
    }
}
