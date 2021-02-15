using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Common.Enums;

namespace Core.Base.Data
{
    [Display(Name = "Збережені файли")]
    [Table("CmnFileStore")]
    public abstract class BaseFileStore : CoreEntity
    {
        // file extension
        [MaxLength(5)]
        public virtual string FileType { get; set; }
        // file can be stored at several destinations
        public virtual FileStoreDestinationType FileStoreDestinationType { get; set; }
        // file relates to this entity
        [MaxLength(50)]
        public virtual string EntityName { get; set; }
        // file relates to this entity
        [Required]
        public virtual Guid EntityId { get; set; }
        // file path on server
        [MaxLength(200)]
        public virtual string FilePath { get; set; }
        // short file name like "Some text.docx"
        [MaxLength(100)]
        public virtual string FileName { get; set; }
        // from header, if uploaded from form
        [MaxLength(50)]
        public virtual string ContentType { get; set; }
        // in bytes
        public virtual int FileSize { get; set; }
        // comment
        [MaxLength(500)]
        public virtual string Description { get; set; }

        [MaxLength(50)]
        [Required]
        public string TypeOfAttachedFile { get; set; }

        public Guid OwnerId { get; set; }
    }
}
