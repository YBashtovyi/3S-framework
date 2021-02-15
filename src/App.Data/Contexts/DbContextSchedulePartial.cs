using System.Collections.Generic;
using App.Data.Dto.ElectronicQueue;
using App.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace App.Data.Contexts
{
    public partial class AppDbContext
    {
        public DbSet<ScheduleSetting> ScheduleSettings { get; set; }
        public DbSet<ScheduleSettingProperty> ScheduleProperties { get; set; }
        public DbSet<ScheduleResource> ScheduleResources { get; set; }
        public DbSet<ScheduleSlot> ScheduleSlots { get; set; }
        public DbSet<ScheduleTime> ScheduleTimes { get; set; }

        public DbSet<ScheduleSettingDto> ScheduleSettingDtos { get; set; }

        private void BuildScheduleModels(ModelBuilder builder)
        {
            builder.Entity<ScheduleSettingDto>().HasNoKey();
        }
    }
}
