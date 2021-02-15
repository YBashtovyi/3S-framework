using System;
using Core.Base.Data;

namespace Dtm.Common.Interface
{
	public interface IOwnedEntity : IGenericEntity<Guid>
    {
		Guid? OwnerId { get; set; }
	}
}
