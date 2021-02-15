using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Models.Common
{
    //[AuditInclude]
    //[AuditDisplay(name: "Повідомлення")]
    [Display(Name = "Повідомлення")]
    [Table("CmnMessage")]
    public abstract class BaseMessage : CoreEntity
    {
        [Display(Name = "Посилання на пов’язане повідомлення")]
        public virtual Guid? ParentId { get; set; }

        [Display(Name = "Message entity Id")]
        public virtual Guid? EntityId { get; set; }
        [Display(Name = "Message entity name")]
        public virtual string EntityName { get; set; }

        [Required(ErrorMessage = "Заповніть поле")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата повідомлення")]
        public virtual DateTime MsgDate { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        [Display(Name = "Статус повідомлення")]
        public virtual Guid MsgStatusId { get; set; } // EnumRecord code - MsgStatus
        
        [Display(Name = "Тип повіомлення")]
        public virtual Guid MsgTypeId { get; set; } // EnumRecord -  MsgType 

        [Display(Name = "Повідомлення продивленно (так/ні)")]
        [DefaultValue(false)]
        public virtual bool MsgViewed { get; set; } 
        
        [Display(Name = "Відправвник")]
        public virtual Guid SenderId { get; set; }

        [Display(Name = "Отримувач")]
        public virtual Guid? ReceiverId { get; set; }

        [Display(Name = "Посада отримувача")]
        public virtual string ReceiverPositionType { get; set; }

        [StringLength(100)]
        [Display(Name = "Заголовок")]
        public virtual string Header { get; set; }

        [StringLength(4000)]
        [Display(Name = "Текст повідомлення")]
        public virtual string MsgText { get; set; }
    }
}
