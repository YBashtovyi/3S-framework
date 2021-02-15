using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    [Display(Name = "Дані про зберігання документів в архіві")]
    [Table("EhdArchive")]
    public abstract class BaseEhealthArchive: BaseEntity
    {
        /// <summary>
        /// The date when paper documents were transferred to archive
        /// </summary>
        public virtual DateTime? Date { get; set; }

        /// <summary>
        /// The address of building where paper documents are
        /// </summary>
        public virtual string Place { get; set; }

        /// <summary>
        /// Ehealth id of entity, that owns paper documents
        /// </summary>
        public virtual Guid? EntityId { get; set; }
    }
}
