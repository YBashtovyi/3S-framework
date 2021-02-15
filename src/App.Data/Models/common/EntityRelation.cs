using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Data.Models.Common;
using Core.Security;

namespace App.Data.Models
{
    /// <inheritdoc/>
    [MainEntity(nameof(EntityRelation))]
    [Table("Cmn"+nameof(EntityRelation))]
    public class EntityRelation: BaseEntityRelation
    {
        [MaxLength(100)]
        public override string EntityName { get => base.EntityName; set => base.EntityName = value; }

        [MaxLength(100)]
        public override string RelatedEntityName { get => base.RelatedEntityName; set => base.RelatedEntityName = value; }

        [MaxLength(50)]
        public override string RelationType { get => base.RelationType; set => base.RelationType = value; }
    }
}
