using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base.Data;

namespace Core.Data.Mis.Models
{
    [Table("MisEncounter")]
    public abstract class BaseEncounter: BaseEntity
    {
        public Guid EpisodeId { get; set; }
    }
}
