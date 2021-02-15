using System;

namespace Core.Base.Data
{
    public interface IEntity: IGenericEntity<Guid>, IRecordState
    {
    }
}
