using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common
{
    public interface IApplicationModels
    {
        IEnumerable<Type> GetApplicationModels(bool withPrimaryKeysOnly);
    }
}
