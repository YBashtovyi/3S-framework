using App.Data.Dto.Cdn;
using App.Data.Models;
using App.Data.Models.cdn;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Contexts
{
    public partial class AppDbContext
    {
        public DbSet<Position> Position { get; set; }
        public DbSet<PositionListDto> PositionListDtos { get; set; }
        public DbSet<PositionDto> PositionDtos { get; set; }

        public DbSet<ConstructionObjectExPropertyDictionary> ConstructionObjectExPropertyDictionary { get; set; }
        public DbSet<ConstructionObjectExPropertyDictionaryListDto> ConstructionObjectExPropertyDictionaryListDtos { get; set; }
        public DbSet<ConstructionObjectExPropertyDictionaryDetailsDto> ConstructionObjectExPropertyDictionaryDetailsDtos { get; set; }
        public DbSet<ConstructionObjectExPropertyDictionaryEditDto> ConstructionObjectExPropertyDictionaryEditDtos { get; set; }
        public DbSet<ConstructionObjectTypeOfObjectListDto> ConstructionObjectTypeOfObjectListDtos { get; set; }

        public DbSet<ConstructionObjectExtendedProperty> ConstructionObjectExtendedProperty { get; set; }
        public DbSet<ConstructionObjectExtendedPropertyDto> ConstructionObjectExtendedPropertyDtos { get; set; }
        public DbSet<ConstructionObjectExtendedPropertyListDto> ConstructionObjectExtendedPropertyListDtos { get; set; }

        public DbSet<OrgUnitExtendedProperty> OrgUnitExtendedProperty { get; set; }
        public DbSet<OrgUnitExtendedPropertyListDto> OrgUnitExtendedPropertyListDtos { get; set; }

        public DbSet<PersonExtendedProperty> PersonExtendedProperty { get; set; }
        public DbSet<PersonExtendedPropertyListDto> PersonExtendedPropertyListDtos { get; set; }

        public DbSet<TypeOfProjectWork> TypeOfProjectWork { get; set; }
        public DbSet<TypeOfObjectWorkListDto> TypeOfObjectWorkListDtos { get; set; }

        public DbSet<WorkSubType> WorkSubType { get; set; }
        public DbSet<WorkSubTypeListDto> WorkSubTypeListDtos { get; set; }
        public DbSet<WorkSubTypeEditDto> WorkSubTypeEditDtos { get; set; }
        public DbSet<WorkSubTypeDetailsDto> WorkSubTypeDetailsDtos { get; set; }

        private void BuildCdnModels(ModelBuilder builder)
        {
            builder.Entity<PositionListDto>().HasNoKey();
            builder.Entity<PositionDto>().HasNoKey();

            builder.Entity<ConstructionObjectExPropertyDictionaryListDto>().HasNoKey();
            builder.Entity<ConstructionObjectExPropertyDictionaryDetailsDto>().HasNoKey();
            builder.Entity<ConstructionObjectExPropertyDictionaryEditDto>().HasNoKey();
            builder.Entity<ConstructionObjectTypeOfObjectListDto>().HasNoKey();

            builder.Entity<ConstructionObjectExtendedPropertyDto>().HasNoKey();
            builder.Entity<ConstructionObjectExtendedPropertyListDto>().HasNoKey();

            builder.Entity<OrgUnitExtendedPropertyListDto>().HasNoKey();
            builder.Entity<PersonExtendedPropertyListDto>().HasNoKey();

            builder.Entity<TypeOfObjectWorkListDto>().HasNoKey();

            builder.Entity<WorkSubTypeListDto>().HasNoKey();
            builder.Entity<WorkSubTypeEditDto>().HasNoKey();
            builder.Entity<WorkSubTypeDetailsDto>().HasNoKey();
        }
    }
}
