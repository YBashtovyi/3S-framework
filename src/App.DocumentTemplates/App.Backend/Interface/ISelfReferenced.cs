using System;

namespace Dtm.Common.Interface
{
    public interface ISelfReferenced<T>
    {
        string Caption { get; set; }
        Guid? ParentId { get; set; }
           T Parent { get; set; }     

    }
}
