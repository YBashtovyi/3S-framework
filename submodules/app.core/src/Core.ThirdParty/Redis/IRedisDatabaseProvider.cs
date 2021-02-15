using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace Core.ThirdParty.Redis
{
    public interface IRedisdatabaseProvider
    {
        IDatabase GetDatabase();
    }
}
