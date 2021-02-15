using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Models;
using Core.Base.Data;
using Core.Security;

namespace App.Data.Dto.Common
{
    [MainEntity(nameof(EntityExtendedPropertyValue))]
    public class EntityExtendedPropertyValueDto: BaseDto
    {
        public Guid EntityId { get; set; }
        public Guid PropertyId { get; set; }
        public Guid PropertyTypeId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyTypeName { get; set; }

        public string Value { get; set; }
    }
}
