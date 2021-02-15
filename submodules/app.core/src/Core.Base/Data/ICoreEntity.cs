using System;

namespace Core.Base.Data
{
    public interface ICoreEntity: IEntity
    {
        Guid ModifiedBy { get; set; }
        DateTime? ModifiedOn { get; set; }
        Guid CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
    }
}
