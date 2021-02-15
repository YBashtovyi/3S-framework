using Core.Data.Models.Org;
using Core.Security;

namespace App.Data.Models
{
    [MainEntity(nameof(OrgUnitAtuAddress))]
    public class OrgUnitAtuAddress: BaseOrgUnitAtuAddress
    {
        public OrgUnit OrgUnit { get; set; }

        public Address AtuAddress { get; set; }
    }
}
