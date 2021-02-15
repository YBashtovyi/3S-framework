using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Eq.Models
{
    /// <summary>
    /// Contains the <c>Id</c> and <c>Name</c> of the tables for which the schedule is created.
    /// </summary>
    [Table("EqScheduleResource")]
    public abstract class BaseScheduleResource: CoreEntity
    {
        /// <summary>
        /// Id of the table entity for which the schedule is created.
        /// </summary>
        public virtual Guid EntityId { get; set; }

        /// <summary>
        /// Table name.
        /// </summary>
        [MaxLength(256)]
        public virtual string EntityName { get; set; }
    }
}
