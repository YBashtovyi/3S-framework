using Dtm.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DocumentTemplates.Extensions
{
    public static class GenericExtensions
    {
        public static void SetOwner<T>(this T entity, Guid? ownerId) where T : class, IOwnedEntity
        {
            if (ownerId != null)
            {
                entity.OwnerId = ownerId;
            }
        }
    }
}
