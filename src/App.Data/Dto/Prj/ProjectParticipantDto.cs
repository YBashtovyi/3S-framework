using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Models;
using Core.Base.Data;
using Core.Common.Attributes;
using Core.Common.Enums;

namespace App.Data.Dto.Prj
{
    public class ProjectParticipantDto: CoreDto
    {
        [CaseFilter(CaseFilterOperation.Equals)]
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Code of the <see cref="EnumRecord"/>, group - ProjectRole
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ProjectRole { get; set; }

        /// <summary>
        /// Id of the <see cref="Organization"/>
        /// </summary>
        public Guid ParticipantId { get; set; }

        /// <summary>
        /// Id of the <see cref="Employee"/>
        /// </summary>
        public Guid ResponsiblePersonId { get; set; }

        [CaseFilter(CaseFilterOperation.Contains)]
        public string Description { get; set; }
    }

    public class ProjectParticipantListDto: ProjectParticipantDto, IPagingCounted
    {
        public int TotalRecordCount { get; set; }

        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Name of the <see cref="EnumRecord"/>
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ProjectRoleName { get; set; }

        /// <summary>
        /// Name of the <see cref="Organization"/>
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParticipantName { get; set; }

        /// <summary>
        /// Caption of the <see cref="Person"/>
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ResponsiblePersonFullName { get; set; }
    }

    public class ProjectParticipantEditDto: ProjectParticipantDto
    {
        /// <summary>
        /// Name of the <see cref="Project"/>
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ProjectName { get; set; }

        /// <summary>
        /// Name of the <see cref="Organization"/>
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParticipantName { get; set; }

        /// <summary>
        /// Caption of the <see cref="Person"/>
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ResponsiblePersonFullName { get; set; }
    }

    public class ProjectParticipantDetailsDto: ProjectParticipantDto
    {
        /// <summary>
        /// Name of the <see cref="Project"/>
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ProjectName { get; set; }

        /// <summary>
        /// Name of the <see cref="EnumRecord"/>
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ProjectRoleName { get; set; }

        /// <summary>
        /// Name of the <see cref="Organization"/>
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ParticipantName { get; set; }

        /// <summary>
        /// Caption of the <see cref="Person"/>
        /// </summary>
        [CaseFilter(CaseFilterOperation.Contains)]
        public string ResponsiblePersonFullName { get; set; }
    }
}
