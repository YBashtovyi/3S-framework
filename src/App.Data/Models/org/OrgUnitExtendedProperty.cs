using Core.Data.Models.Org;
using Core.Security;

namespace App.Data.Models
{
    [MainEntity(nameof(OrgUnitExtendedProperty))]
    public class OrgUnitExtendedProperty: BaseOrgUnitExtendedProperty
    {
        public OrgUnit OrgUnit { get; set; }
    }
}
