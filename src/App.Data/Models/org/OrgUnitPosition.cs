using Core.Data.Models.Org;
using Core.Security;

namespace App.Data.Models
{
    [MainEntity(nameof(OrgUnitPosition))]
    public class OrgUnitPosition: BaseOrgUnitPosition
    {
        public OrgUnit OrgUnit { get; set; }

        public Position Position { get; set; }
    }
}
