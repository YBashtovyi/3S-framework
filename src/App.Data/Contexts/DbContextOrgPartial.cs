using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using App.Data.Dto.Administration;
using App.Data.Dto.Common;
using App.Data.Dto.Org;
using App.Data.Enums;
using App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Contexts
{
    public partial class AppDbContext
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<DepartmentListDto> DepartmentListDtos { get; set; }
        public DbSet<DepartmentEditDto> DepartmentEditDtos { get; set; }
        public DbSet<DepartmentDetailsDto> DepartmentDetailsDtos { get; set; }


        public DbSet<Employee> Employee { get; set; }
        public DbSet<OrgEmployeeSimpleDto> EmployeeSimpleDtos { get; set; }
        public DbSet<OrgEmployeeListDto> OrgEmployeeListDtos { get; set; }
        public DbSet<OrgEmployeeDto> OrgEmployeeDtos { get; set; }


        public DbSet<OrgUnit> OrgUnit { get; set; }
        public DbSet<OrgUnitListDto> OrgUnitListDtos { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<OrganizationListDto> OrganizationListDtos { get; set; }
        public DbSet<OrganizationDetailsDto> OrganizationDetailsDtos { get; set; }
        public DbSet<OrganizationEditDto> OrganizationEditDtos { get; set; }

        public DbSet<OrgUnitAtuAddress> OrgUnitAtuAddress { get; set; }
        
        public DbSet<OrgUnitPosition> OrgUnitPosition { get; set; }
        public DbSet<OrgUnitPositionListDto> OrgUnitPositionListDtos { get; set; }
        public DbSet<OrgUnitPositionEditDto> OrgUnitPositionEditDtos { get; set; }
        public DbSet<OrgUnitPositionDetailsDto> OrgUnitPositionDetailsDtos { get; set; }

        public DbSet<OrgUnitStaff> OrgUnitStaff { get; set; }
        public DbSet<OrgUnitStaffListDto> OrgUnitStaffListDtos { get; set; }
        public DbSet<OrgUnitStaffEditDto> OrgUnitStaffEditDtos { get; set; }
        public DbSet<OrgUnitStaffDetailsDto> OrgUnitStaffDetailsDtos { get; set; }


        private void BuildOrgModels(ModelBuilder builder)
        {
            builder.Entity<Organization>().HasIndex(p => p.Code).IsUnique();

            builder.Entity<OrgUnitListDto>().HasNoKey();

            builder.Entity<OrganizationListDto>().HasNoKey();
            builder.Entity<OrganizationDetailsDto>().HasNoKey();
            builder.Entity<OrganizationEditDto>().HasNoKey();

            builder.Entity<OrgUnitPositionListDto>().HasNoKey();
            builder.Entity<OrgUnitPositionEditDto>().HasNoKey();
            builder.Entity<OrgUnitPositionDetailsDto>().HasNoKey();

            builder.Entity<OrgUnitStaffListDto>().HasNoKey();
            builder.Entity<OrgUnitStaffEditDto>().HasNoKey();
            builder.Entity<OrgUnitStaffDetailsDto>().HasNoKey();

            builder.Entity<OrgEmployeeSimpleDto>().HasNoKey();
            builder.Entity<OrgEmployeeListDto>().HasNoKey();
            builder.Entity<OrgEmployeeDto>().HasNoKey();

            builder.Entity<DepartmentListDto>().HasNoKey();
            builder.Entity<DepartmentEditDto>().HasNoKey();
            builder.Entity<DepartmentDetailsDto>().HasNoKey();
        }
    }
}
