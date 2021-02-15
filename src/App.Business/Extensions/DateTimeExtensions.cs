using System;

namespace App.Business.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetFullYears(this DateTime dateTime)
        {
            var zeroTime = new DateTime(1, 1, 1);
            var currentDate = DateTime.UtcNow;

            var dateDifference = currentDate - dateTime;
            var years = (zeroTime + dateDifference).Year - 1;

            return years;
        }
    }
}
