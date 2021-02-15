using App.Data.Dto.Prj;
using App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Contexts
{
    public partial class AppDbContext
    {
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectListDto> ProjectListDtos { get; set; }
        public DbSet<ProjectDetailsDto> ProjectDetailsDtos { get; set; }
        public DbSet<ProjectEditDto> ProjectEditDtos { get; set; }
        public DbSet<ProjectParticipantEmployeeListDto> ProjectParticipantEmployeeListDtos { get; set; }
        public DbSet<ProjectListExcelDto> ProjectListExcelDtos { get; set; }

        public DbSet<ProjectConstructionObject> ProjectConstructionObject { get; set; }

        public DbSet<ProjectParticipant> ProjectParticipants { get; set; }
        public DbSet<ProjectParticipantListDto> ProjectParticipantListDtos { get; set; }
        public DbSet<ProjectParticipantDetailsDto> ProjectParticipantDetailsDtos { get; set; }
        public DbSet<ProjectParticipantEditDto> ProjectParticipantEditDtos { get; set; }

        public DbSet<ProjectContract> ProjectContract { get; set; }
        public DbSet<ProjectContractListDto> ProjectContractListDtos { get; set; }
        public DbSet<ProjectContractEditDto> ProjectContractEditDtos { get; set; }
        public DbSet<ProjectContractDetailsDto> ProjectContractDetailsDtos { get; set; }
        public DbSet<ProjectContractAddAgreementListDto> ProjectContractAddAgreementListDtos { get; set; }

        public DbSet<ProjectAdditionalAgreement> ProjectAdditionAgreement { get; set; }
        public DbSet<ProjectAdditionalAgreementListDto> ProjectAdditionalAgreementListDtos { get; set; }
        public DbSet<ProjectAdditionalAgreementEditDto> ProjectAdditionalAgreementEditDtos { get; set; }
        public DbSet<ProjectAdditionalAgreementDetailsDto> ProjectAdditionalAgreementDetailsDtos { get; set; }

        public DbSet<ProjectWorkSchedule> ProjectWorkSchedule { get; set; }
        public DbSet<ProjectWorkScheduleListDto> ProjectWorkScheduleListDtos { get; set; }
        public DbSet<ProjectWorkScheduleEditDto> ProjectWorkScheduleEditDtos { get; set; }
        public DbSet<ProjectWorkScheduleDetailsDto> ProjectWorkScheduleDetailsDtos { get; set; }

        public DbSet<ProjectWorkScheduleStage> ProjectWorkScheduleStage { get; set; }
        public DbSet<ProjectWorkScheduleStageListDto> ProjectWorkScheduleStageListDtos { get; set; }
        public DbSet<ProjectWorkScheduleStageEditDto> ProjectWorkScheduleStageEditDtos { get; set; }
        public DbSet<ProjectWorkScheduleStageDetailsDto> ProjectWorkScheduleStageDetailsDtos { get; set; }

        public DbSet<ProjectWorkScheduleSubType> ProjectWorkScheduleType { get; set; }
        public DbSet<ProjectWorkScheduleSubTypeListDto> ProjectWorkScheduleTypeListDtos { get; set; }
        public DbSet<ProjectWorkScheduleSubTypeEditDto> ProjectWorkScheduleTypeEditDtos { get; set; }
        public DbSet<ProjectWorkScheduleSubTypeDetailsDto> ProjectWorkScheduleTypeDetailsDtos { get; set; }

        public DbSet<ProjectPhotoReport> ProjectPhotoReport { get; set; }
        public DbSet<ProjectPhotoReportListDto> ProjectPhotoReportListDtos { get; set; }
        public DbSet<ProjectPhotoReportEditDto> ProjectPhotoReportEditDtos { get; set; }
        public DbSet<ProjectPhotoReportDetailsDto> ProjectPhotoReportDetailsDtos { get; set; }

        private void BuildProjectModels(ModelBuilder builder)
        {
            builder.Entity<ProjectListDto>().HasNoKey();
            builder.Entity<ProjectDetailsDto>().HasNoKey();
            builder.Entity<ProjectEditDto>().HasNoKey();
            builder.Entity<ProjectParticipantEmployeeListDto>().HasNoKey();
            builder.Entity<ProjectListExcelDto>().HasNoKey();

            builder.Entity<ProjectParticipantListDto>().HasNoKey();
            builder.Entity<ProjectParticipantEditDto>().HasNoKey();
            builder.Entity<ProjectParticipantDetailsDto>().HasNoKey();

            builder.Entity<ProjectContractListDto>().HasNoKey();
            builder.Entity<ProjectContractEditDto>().HasNoKey();
            builder.Entity<ProjectContractDetailsDto>().HasNoKey();
            builder.Entity<ProjectContractAddAgreementListDto>().HasNoKey();

            builder.Entity<ProjectAdditionalAgreementListDto>().HasNoKey();
            builder.Entity<ProjectAdditionalAgreementEditDto>().HasNoKey();
            builder.Entity<ProjectAdditionalAgreementDetailsDto>().HasNoKey();

            builder.Entity<ProjectWorkScheduleListDto>().HasNoKey();
            builder.Entity<ProjectWorkScheduleEditDto>().HasNoKey();
            builder.Entity<ProjectWorkScheduleDetailsDto>().HasNoKey();

            builder.Entity<ProjectWorkScheduleStageListDto>().HasNoKey();
            builder.Entity<ProjectWorkScheduleStageEditDto>().HasNoKey();
            builder.Entity<ProjectWorkScheduleStageDetailsDto>().HasNoKey();

            builder.Entity<ProjectWorkScheduleSubTypeListDto>().HasNoKey();
            builder.Entity<ProjectWorkScheduleSubTypeEditDto>().HasNoKey();
            builder.Entity<ProjectWorkScheduleSubTypeDetailsDto>().HasNoKey();

            builder.Entity<ProjectPhotoReportListDto>().HasNoKey();
            builder.Entity<ProjectPhotoReportEditDto>().HasNoKey();
            builder.Entity<ProjectPhotoReportDetailsDto>().HasNoKey();

            builder.Entity<Project>().HasIndex(p => p.Code).IsUnique();
        }
    }
}
