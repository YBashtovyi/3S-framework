using System;

namespace Core.Base.Data
{
    public interface IDocument : ICoreEntity
    {
        string RegNumber { get; set; }
        DateTime RegDate { get; set; }
        string DocType { get; set; }
        string DocState { get; set; }
        string Description { get; set; }
        Guid? ParentId { get; set; }
    }
}
