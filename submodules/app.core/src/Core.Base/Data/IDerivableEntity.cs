using System;

namespace Core.Base.Data
{
    public interface IDerivableEntity : ICoreEntity
    {
        string DerivedEntity { get; set; }
    }
}
