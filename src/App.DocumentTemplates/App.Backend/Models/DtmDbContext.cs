using Core.Data.Extensions;
using Core.Models;
using Core.Services;
using Microsoft.EntityFrameworkCore;

namespace App.DocumentTemplates.Models
{
    public class DtmDbContext : BaseDbContext
    {
        public DbSet<TemplateDocument> TemplateDocuments { get; set; }
        public DbSet<DocumentTemplate> DocumentTemplates { get; set; }
        public DbSet<DocumentTemplateElement> DocumentTemplateElements { get; set; }
        public DbSet<DocumentTemplateElementValue> DocumentTemplateElementValues { get; set; }
        public DbSet<DocumentTemplateElementValueTree> DocumentTemplateElementValueTrees { get; set; }
        public DbSet<DocumentControlType> DocumentControlTypes { get; set; }
        public DbSet<DocumentData> DocumentData { get; set; }
        public DbSet<DocumentTemplatePreset> DocumentTemplatePresets { get; set; }
        public DbSet<DocumentTemplatePresetValue> DocumentTemplatePresetValues { get; set; }     
        //public DbSet<Report4110> Report4110 { get; set; }
        //public DbSet<ReportCabinetKT> ReportCabinetKT { get; set; }

        public DtmDbContext(DbContextOptions<DtmDbContext> options)
            : base(options)
        {
        }

        public DtmDbContext(DbContextOptions<DtmDbContext> options, IAuditEntityEntryChangesTracker auditTracker) : base(options, auditTracker) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DocumentData>().HasOne(x => x.Document).WithMany(d => d.DocumentDataList).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DocumentTemplate>()
                .HasIndex(p => new { p.Code, p.OwnerId}).IsUnique();
            //builder.Entity<DocTemplateElementValuesDbModel>().HasOne(x => x.Parent).WithMany(d => d.TemplateElementValues).OnDelete(DeleteBehavior.Cascade);
            //builder.Ignore<Report4110>();
            //builder.Entity<DocTemplatesDbModel>().HasOne(x => x.Parent).WithMany(m => m.DocTemplates).OnDelete(DeleteBehavior.Restrict);
            //builder.Entity<DocumentDbModel>().HasOne(x => x.Parent).WithMany(m => m.Documents).OnDelete(DeleteBehavior.Restrict);

            var useSnakeCase = true; // TODO: move to configuration
            if (useSnakeCase)
            {
                builder.UseSnakeCaseNaming();
            }
        }
    }
}
