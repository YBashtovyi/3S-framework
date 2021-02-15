using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Base.Data
{
    public abstract class CoreEntity: ICoreEntity
    {
        [Key]
        public virtual Guid Id { get; set; }
        public virtual RecordState RecordState { get; set; } = RecordState.Default;
        
        public virtual Guid ModifiedBy { get; set; }
        public virtual DateTime? ModifiedOn { get; set; }
        public virtual Guid CreatedBy { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }
}
