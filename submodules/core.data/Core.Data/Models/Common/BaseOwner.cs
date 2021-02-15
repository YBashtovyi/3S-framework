using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;
using Core.Common.Enums;

namespace Core.Data.Models.Common
{
	[Table("Owner")]
	public abstract class BaseOwner : CoreEntity
	{
		public virtual string Description { get; set; }
	    public virtual string SubjectTypeCode { get; set; } //TODO : ?
        public virtual bool IsSync { get; set; }
		public virtual Language DefaultLanguage { get; set; }
	}
}
