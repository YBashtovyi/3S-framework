using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Models.System
{
    [Table("SysCryptoSignFieldSetting")]
    public abstract class BaseCryptoSignFieldSetting: CoreEntity
    {
        // name of class
        public virtual string EntityName { get; set; }
        // field name as it is defined in a class
        public virtual string FieldName { get; set; }
        // it is possible that for signing the alternate name will be used
        public virtual string SignFieldName { get; set; }
    }
}
