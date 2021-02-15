using App.Data.Dto.Administration;
using App.Data.Dto.Common;
using App.Data.Dto.Prj;
using App.Data.Models;
using Core.Base.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Contexts
{
    public partial class AppDbContext
    {
        public DbSet<ExtendedProperty> ExtendedProperty { get; set; }
        public DbSet<EntityExtendedPropertyValue> EntityExtendedPropertyValue { get; set; }
        public DbSet<EntityExtendedPropertyValueDto> EntityExtendedPropertyValueDtos { get; set; }
        public DbSet<EntityRelation> EntityRelations { get; set; }
        public DbSet<EntityRelationDto> EntityRelationDtos { get; set; }

        public DbSet<EnumRecord> EnumRecord { get; set; }
        public DbSet<EnumRecordDto> EnumRecordDtos { get; set; }
        public DbSet<EnumRecordListDto> EnumRecordListDtos { get; set; }

        public DbSet<SimpleDataDto> SimpleDataDtos { get; set; }

        public DbSet<PrintedFormTemplate> PrintedFormTemplates { get; set; }
        public DbSet<PrintedFormTemplateDto> PrintedFormTemplateDtos { get; set; }

        #region notifications
        public DbSet<Notification> Notification { get; set; }
        public DbSet<NotificationReceiver> NotificationReceiver { get; set; }

        public DbSet<NotificationDetailDto> NotificationDetailDtos { get; set; }
        public DbSet<NotificationEditDto> NotificationEditDtos { get; set; }
        public DbSet<NotificationByAuthorListDto> NotificationByAuthorListDtos { get; set; }
        public DbSet<NotificationByReceiverListDto> NotificationByReceiverListDtos { get; set; }
        public DbSet<NotificationReceiverEditDto> NotificationReceiverEditDtos { get; set; }
        public DbSet<NotificationReceiverListDto> NotificationReceiverListDtos { get; set; }
        #endregion

        #region Person

        public DbSet<Person> Person { get; set; }
        public DbSet<PersonListDto> PersonListDtos { get; set; }
        public DbSet<PersonDetailsDto> PersonDetailsDtos { get; set; }
        public DbSet<PersonEditDto> PersonEditDtos { get; set; }

        #endregion

        public DbSet<ConstructionObject> ConstructionObjects { get; set; }
        public DbSet<ConstructionObjectListDto> ConstructionObjectListDtos { get; set; }
        public DbSet<ConstructionObjectDetailsDto> ConstructionObjectDetailsDtos { get; set; }
        public DbSet<ConstructionObjectEditDto> ConstructionObjectEditDtos { get; set; }
        public DbSet<ProjectConstructionObjectListDto> ProjectConstructionObjectListDtos { get; set; }

        public DbSet<Document> Document { get; set; }
        public DbSet<DocumentListDto> DocumentListDtos { get; set; }


        private void BuildCommonModels(ModelBuilder builder)
        {
            // entity relation table will be used frequently
            // common search case: find all related entities for entity by id
            // and then my entity name to exclude cases with two different entity types having the same id
            // (the chance is almost 0, because we use guids but still)
            // then we get related entity id and related entity name as a result
            // query example (similar case with join instead of where):
            // select RelatedEntityId, RelatedEntityName from EntityRelation where EntityId = 'guid here' and EntityName = 'name here'
            // the similar case will be with related entity id
            // we create two indexes on entity id and related entity id with further columns needed as included
            // the entity names should be short enough so duplicating this data won't affect performance
            //builder.Entity<EntityRelation>()
            //    .HasIndex(t => t.EntityId)
            //    .IncludeProperties("EntityName", "RelatedEntityId", "RelatedEntityName");
            //builder.Entity<EntityRelation>()
            //    .HasIndex(t => t.RelatedEntityId)
            //    .IncludeProperties("RelatedEntityName", "EntityId", "EntityName");

            builder.Entity<EnumRecordDto>().HasNoKey();
            builder.Entity<EnumRecordListDto>().HasNoKey();
            builder.Entity<SimpleDataDto>().HasNoKey();
            builder.Entity<EntityRelationDto>().HasNoKey();
            builder.Entity<EntityExtendedPropertyValueDto>().HasNoKey();
            builder.Entity<PrintedFormTemplateDto>().HasNoKey();

            builder.Entity<NotificationDetailDto>().HasNoKey();
            builder.Entity<NotificationEditDto>().HasNoKey();
            builder.Entity<NotificationByAuthorListDto>().HasNoKey();
            builder.Entity<NotificationByReceiverListDto>().HasNoKey();
            builder.Entity<NotificationReceiverEditDto>().HasNoKey();
            builder.Entity<NotificationReceiverListDto>().HasNoKey();

            builder.Entity<PersonListDto>().HasNoKey();
            builder.Entity<PersonEditDto>().HasNoKey();
            builder.Entity<PersonDetailsDto>().HasNoKey();

            builder.Entity<ConstructionObjectListDto>().HasNoKey();
            builder.Entity<ConstructionObjectEditDto>().HasNoKey();
            builder.Entity<ConstructionObjectDetailsDto>().HasNoKey();
            builder.Entity<ProjectConstructionObjectListDto>().HasNoKey();

            builder.Entity<DocumentListDto>().HasNoKey();

            #region indexes
            builder.Entity<EntityExtendedPropertyValue>().HasIndex(b => b.EntityId);
            builder.Entity<Notification>().HasIndex(b => b.OneSignalId);
            #endregion

            #region delete behavior: restrict
            builder.Entity<EntityExtendedPropertyValue>().HasOne(x => x.Property).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Notification>().HasOne(x => x.Type).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Notification>().HasOne(x => x.State).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Notification>().HasOne(x => x.Organization).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<NotificationReceiver>().HasOne(x => x.Notification).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<NotificationReceiver>().HasOne(x => x.Receiver).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<NotificationReceiver>().HasOne(x => x.Organization).WithMany().OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
