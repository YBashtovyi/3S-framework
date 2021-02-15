using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Security;

namespace App.Data.Models
{
    [Table("PrjParticipant")]
    public class ProjectParticipant: CoreEntity
    {
        [RequiredNonDefault]
        public Guid ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        /// <summary>
        /// Code of the <see cref="EnumRecord"/>, group - ProjectRoles
        /// </summary>
        [Required, MaxLength(50), Display(Name = "Проект будівництва")]
        public string ProjectRole { get; set; }

        /// <summary>
        /// Id of the <see cref="Organization"/>
        /// </summary>
        [RequiredNonDefault, Display(Name = "Контрагент")]
        public Guid ParticipantId { get; set; }
        [ForeignKey("ParticipantId")]
        public Organization Participant { get; set; }

        /// <summary>
        /// Id of the <see cref="Employee"/>
        /// </summary>
        [RequiredNonDefault, Display(Name = "Відповідальна особа")]
        public Guid ResponsiblePersonId { get; set; }
        [ForeignKey("ResponsiblePersonId")]
        public Employee ResponsiblePerson { get; set; }

        [MaxLength(250), Display(Name = "Коментар")]
        public string Description { get; set; }
    }
}
