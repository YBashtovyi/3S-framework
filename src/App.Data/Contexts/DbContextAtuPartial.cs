using App.Data.Dto.Atu;
using App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Contexts
{
    public partial class AppDbContext
    {
        public DbSet<Country> Country { get; set; }
        public DbSet<Coordinate> Coordinate { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Street> Street { get; set; }
        public DbSet<CityDistrictStreetsMap> CityDistrictStreetsMap { get; set; }
        public DbSet<CityRegionMap> CityRegionMap { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<PostOffice> PostOffice { get; set; }
        public DbSet<PostOfficeByDistrict> PostOfficeByDistrict { get; set; }
        public DbSet<PostIndex> PostIndex { get; set; }

        public DbSet<RegionListDto> RegionListDtos { get; set; }

        public DbSet<CityListDto> CityListDtos { get; set; }

        private void BuildAtuModels(ModelBuilder builder)
        {
            builder.Entity<RegionListDto>().HasNoKey();

            builder.Entity<CityDistrictStreetsMap>().HasKey(p => new {p.CityId, p.DistrictId, p.StreetId});
        }
    }
}
