using Core.Data.Models.Atu;
using Core.Security;

namespace App.Data.Models
{
    [MainEntity(nameof(Address))]
    public class Address: BaseAddress
    {
        public Street Street { get; set; }
    }
}
