using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface IQueryConditionsBuilder
    {
        FormattableString GetQueryConditionsString(Type type, IDictionary<string, string> parameters, string queryAlias);
    }
}
