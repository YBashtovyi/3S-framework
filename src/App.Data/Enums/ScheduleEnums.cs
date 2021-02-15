using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Enums
{
    public enum ScheduleRepeat
    {
        EveryWeek,
        EveryOddDay
    }

    public enum ScheduleSlotType
    {
        RecordOnReception,
        LiveQueue,
        WithoutQueue,
        Reservation,
        Сancellation,
        Duty
    }

    public enum ScheduleSlotState
    {
        Open,
        Canceled,
        Appointed
    }

    public enum SchedulePropertyType
    {
        Break,
        Vocation
    }
}
