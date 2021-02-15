using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Dto.ElectronicQueue;
using App.Data.Models;

namespace App.Business.Tests.TestData
{
    public static class ScheduleHelper
    {
        public static ScheduleResource CreateScheduleResource()
        {
            var resource = new ScheduleResource
            {
                Id = Guid.NewGuid(),
                EntityId = Guid.NewGuid(),
                EntityName = "EntityTest"
            };

            return resource;
        }

        public static ScheduleSetting CreateScheduleSetting()
        {
            var setting = new ScheduleSetting
            {
                WorkDateFrom = DateTime.Now,
                WorkDateTo = DateTime.Now,
                ScheduleRepeatId = Guid.NewGuid(),
                Day1 = false,
                Day2 = false,
                Day3 = false,
                Day4 = false,
                Day5 = false,
                Day6 = false,
                Day7 = false,
                IsFullDay = false,
                WorkTimeFrom = new TimeSpan(10, 10, 10),
                WorkTimeTo = new TimeSpan(12, 12, 12),
                SlotDuration = new TimeSpan(1, 0, 0),
                BreakBetweenSlots = new TimeSpan(1, 0, 0)
            };

            return setting;
        }

        public static ScheduleSettingProperty CreateScheduleSettingProperty()
        {
            var settingProperty = new ScheduleSettingProperty
            {
                Value = "json"
            };

            return settingProperty;
        }

        public static ScheduleTime CreateScheduleTime()
        {
            var time = new ScheduleTime
            {
                IsWeekend = false,
                ScheduleDate = DateTime.Now,
                WorkTimeFrom = new TimeSpan(10, 10, 10),
                WorkTimeTo = new TimeSpan(12, 12, 12),
                WorkTimeDuration = new TimeSpan(1, 0, 0),
                BreakBetweenSlots = new TimeSpan(1, 0, 0)
            };

            return time;
        }

        public static ScheduleSlot CreateScheduleSlot()
        {
            var slot = new ScheduleSlot
            {
                Date = DateTime.Now,
                TimeFrom = new TimeSpan(10, 10, 10),
                TimeTo = new TimeSpan(12, 12, 12),
            };

            return slot;
        }
    }
}
