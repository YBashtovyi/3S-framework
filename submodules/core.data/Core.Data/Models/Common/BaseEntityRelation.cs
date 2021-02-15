using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Models.Common
{
    /// <summary>
    /// Relation between main and related entities
    /// </summary>
    [Table("CmnEntityRelation")]
    [Display(Name = "Відповідність між сутностями")]
    public abstract class BaseEntityRelation: CoreEntity, ICaption
    {
        public string Caption { get; set; }
        /// <summary>
        /// Contains link on main table in this relation 
        /// </summary>
        public virtual Guid EntityId { get; set; }

        /// <summary>
        /// Contains main entity table name
        /// </summary>
        public virtual string EntityName { get; set; }

        /// <summary>
        /// Contains link on relate table in this relation 
        /// </summary>
        public virtual Guid RelatedEntityId { get; set; }

        /// <summary>
        /// Contains related entity table name
        /// </summary>
        public virtual string RelatedEntityName { get; set; }

        /// <summary>
        /// Contains relation type
        /// </summary>
        public virtual string RelationType { get; set; }
    }
}
