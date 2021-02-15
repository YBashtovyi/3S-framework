using System;

namespace Core.Base.Data
{
    public interface IDerivedEntity
    {
        //string BaseEntity { get; set; }
        Type BaseType { get; }
    }
}
