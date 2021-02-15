using System;
using Core.Base.Data;

namespace Core.Data.Common.Dto
{
    /// <summary>
    /// Relation between main and related entities
    /// </summary>
    public abstract class BaseEntityRelationDto: BaseDto
    {        
        /// <summary>
        /// Contains link on main class in this relation 
        /// </summary>
        public virtual Guid EntityId { get; set; }

        /// <summary>
        /// Contains main entity class name
        /// </summary>
        public virtual string EntityName { get; set; }

        /// <summary>
        /// Contains link on relate class in this relation 
        /// </summary>
        public virtual Guid RelatedEntityId { get; set; }

        /// <summary>
        /// Contains related entity class name
        /// </summary>
        public virtual string RelatedEntityName { get; set; }
    }
}
