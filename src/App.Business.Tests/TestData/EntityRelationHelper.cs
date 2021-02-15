using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Models;

namespace App.Business.Tests.TestData
{
    public static class EntityRelationHelper
    {
        public static EntityRelation CreateNew()
        {
            var entityRelation = new EntityRelation
            {
                Id = Guid.NewGuid(),
                Caption = "",
                EntityId = Guid.NewGuid(),
                EntityName = "Main test entity name",
                RelatedEntityId = Guid.NewGuid(),
                RelatedEntityName = "Related test entity name"
            };

            return entityRelation;
        }

        public static EntityRelation CreateNew(Guid entityId, string entityName, Guid relatedEntityId, string relatedEntityName)
        {
            var entityRelation = new EntityRelation
            {
                Id = Guid.NewGuid(),
                Caption = entityName + " " + relatedEntityName,
                EntityId = entityId,
                EntityName = entityName,
                RelatedEntityId = relatedEntityId,
                RelatedEntityName = relatedEntityName
            };

            return entityRelation;
        }
    }
}
