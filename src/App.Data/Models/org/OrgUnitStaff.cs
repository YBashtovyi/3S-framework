using Core.Data.Models.Org;
using Core.Security;

namespace App.Data.Models
{
    [MainEntity(nameof(OrgUnitStaff))]
    public class OrgUnitStaff: BaseOrgUnitStaff
    {
        public OrgUnitPosition OrgUnitPosition { get; set; }

        public Employee Employee { get; set; }
    }
}
