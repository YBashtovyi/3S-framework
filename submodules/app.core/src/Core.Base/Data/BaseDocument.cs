using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Base.Data
{
    [Display(Name = "Документ")]
    public abstract class BaseDocument : CoreEntity, IDocument, IDerivableEntity
    {
        public string DerivedEntity { get; set; }

        [Required, MaxLength(100), Display(Name = "Реєстраційний номер документу")]
        public virtual string RegNumber { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата реєстрації документу")]
        public virtual DateTime RegDate { get; set; }

        [Required, MaxLength(50)]
        public virtual string DocType { get; set; }
        
        [Required, MaxLength(50)]
        public virtual string DocState { get; set; }

        [MaxLength(500), Display(Name = "Будь яка додаткова інформація")]
        public virtual string Description { get; set; }

        public virtual Guid? ParentId { get; set; }
    }
}
