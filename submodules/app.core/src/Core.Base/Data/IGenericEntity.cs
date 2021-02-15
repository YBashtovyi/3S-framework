using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Base.Data
{
    public interface IGenericEntity<TId> where TId: struct
    {
        TId Id { get; set; }
    }
}
