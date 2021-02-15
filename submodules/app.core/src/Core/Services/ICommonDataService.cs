using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Base.Data;
using Core.Common;

namespace Core.Services.Data
{
    public interface ICommonDataService: ICommonGenericDataService<Guid>, IApplicationModels
    {
        /// <summary>
        /// Detaches all entities from the context
        /// </summary>
        void ClearContext();

    }
}
