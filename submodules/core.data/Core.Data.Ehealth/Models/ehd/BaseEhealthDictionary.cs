using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Ehealth.Models
{
    /// <summary>
    /// Common eHealth dictionary is used in many eHealth entities
    /// </summary>
    [Display(Name = "Загальний довідник eHealth")]
    [Table("EhdDictionary")]
    public abstract class BaseEhealthDictionary: BaseEntity
    {
        /// <summary>
        /// Dictionary name (dictionary type), for example:
        /// eHealth/ICPC2/reasons, COUNTRY, KVEDS etc
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Is active in eHealth (can be deactivated)
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Code, is unique within one dictionary name
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// Value of the dictionary (display name), for example:
        /// Інші профілактичні процедури or РУМУНІЯ
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// OwnRecord is a sign that determines if record is ours
        /// </summary>
        public virtual bool IsOwn { get; set; }

        /// <summary>
        /// Hidden, shows if record is hidden on the ui
        /// </summary>
        public virtual bool Hidden { get; set; }

    }
}
