using Core.Common;
using System;

namespace Core.Services.Repositories
{
    public interface ICommonRepository: ICommonGenericRepository<Guid>, IApplicationModels
    {
    }
}
