using App.Data.Dto.Administration;
using App.Data.Dto.System;
using App.Data.Models;
using Core.Data;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Contexts
{
    public partial class AppDbContext
    {
        public DbSet<ApplicationSetting> ApplicationSetting { get; set; }
        public DbSet<NumberCounter> NumberCounter { get; set; }

        public DbSet<FileStore> FileStore { get; set; }
        public DbSet<FileStoreDto> FileStoreDtos { get; set; }
        //public DbSet<FileStoreExtendedDto> FileStoreExtendedDtos { get; set; }
        public DbSet<FileStoreFullDto> FileStoreFullDtos { get; set; }
        //public DbSet<FileEmbeddedDto> FileEmbeddedDtos { get; set; }

        public DbSet<CryptoSignFieldSetting> CryptoSignFieldSetting { get; set; }
        public DbSet<CryptoSignFieldSettingDto> CryptoSignFieldSettingDtos { get; set; }

        public DbSet<SysEvaluatedValue> SysEvaluatedValue { get; set; }
        public DbSet<SysEvaluatedValueDto> SysEvaluatedValueDto { get; set; }

        public DbSet<PendingChangeDto> PendingChangeDtos { get; set; }
        public DbSet<PendingChangeDetailDto> PendingChangeDetailDtos { get; set; }

        private void BuildSystemModels(ModelBuilder builder)
        {
            builder.Entity<CryptoSignFieldSetting>()
                .HasAlternateKey(ent => new { ent.EntityName, ent.FieldName });

            builder.Entity<FileStoreDto>().HasNoKey();
            //builder.Entity<FileStoreExtendedDto>().HasNoKey();
            builder.Entity<FileStoreFullDto>().HasNoKey();
            //builder.Entity<FileEmbeddedDto>().HasNoKey();
            builder.Entity<CryptoSignFieldSettingDto>().HasNoKey();
            builder.Entity<PendingChangeDto>().HasNoKey();
            builder.Entity<PendingChangeDetailDto>().HasNoKey();
            builder.Entity<SysEvaluatedValueDto>().HasNoKey();

            #region indexes
            builder.Entity<SysEvaluatedValue>().HasIndex(x => new { x.EntityId, x.EntityName, x.ValueName }).IsUnique();
            #endregion

        }
    }
}
