using System;
using Core.Base.Data;

namespace Core.Data.Ehealth.Dto
{
    /// <summary>
    /// Relation between main and related eHealth entities
    /// </summary>
    public abstract class BaseEhealthEntityRelationDto: BaseDto
    {        
        /// <summary>
        /// Contains link on main table in this relation 
        /// </summary>
        public virtual Guid MainEntityId { get; set; }

        /// <summary>
        /// Contains main entity table name
        /// </summary>
        public virtual string MainEntityName { get; set; }

        /// <summary>
        /// Contains link on relate table in this relation 
        /// </summary>
        public virtual Guid RelatedEntityId { get; set; }

        /// <summary>
        /// Contains related entity table name
        /// </summary>
        public virtual string RelatedEntityName { get; set; }

        /// <summary>
        /// Arbitrary name for mapping link assignment and fast filtering
        /// </summary>
        public virtual string RelationType { get; set; }
    }
}
