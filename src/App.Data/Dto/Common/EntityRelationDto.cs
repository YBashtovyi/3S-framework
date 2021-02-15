using App.Data.Models;
using Core.Data.Dto.Common;
using Core.Security;

namespace App.Data.Dto.Common
{
    /// <inheritdoc/>
    [MainEntity(nameof(EntityRelation))]
    public class EntityRelationDto : BaseEntityRelationDto
    {
        public string RelationType { get; set; }
    }
}
