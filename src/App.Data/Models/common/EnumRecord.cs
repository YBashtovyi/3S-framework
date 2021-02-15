using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Security;

namespace App.Data.Models
{
    [MainEntity(nameof(EnumRecord))]
    [Table("Cmn" + nameof(EnumRecord))]
    public class EnumRecord: BaseEnumRecord
    {
        [Display(Name = "Група переліку (укр.)"), MaxLength(50)]
        public string GroupName { get; set; }

        [Display(Name = "Порядковий номер елемента переліку")]
        public byte ItemNumber { get; set; }
    }
}
