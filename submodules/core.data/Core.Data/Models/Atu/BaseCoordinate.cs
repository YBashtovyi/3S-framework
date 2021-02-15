using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Models.Cdn
{
    [Display(Name = "Довідник координат")]
    [Table("AtuCoordinate")]
    public abstract class BaseCoordinate : CoreEntity
    {
        public virtual string Latitude { get; set; }
        public virtual string Longitude { get; set; }
    }
}
