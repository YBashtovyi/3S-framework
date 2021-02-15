using System;
using System.Collections.Generic;

namespace App.Business
{
    public static class Constants
    {
        public const string Iso86Format = "yyyy-MM-ddTHH:mm:ssZ";
        public const string Iso8601DateFormat = "yyyy-MM-dd";
        public const string ExternalAuthenticationMethod = "external";
        public const string AccessTokenAudience = "{0}resources";
        public const string DefaultHashAlgorithm = "SHA256";
        public const string DefaultDepartmentName = "Інша філія";

        public static readonly TimeSpan DefaultCookieTimeSpan = TimeSpan.FromHours(10);
        public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromMinutes(5);


        public static class RlsType
        {
            public const string OrgUnit = "OrgUnit";
            public const string User = "User";
            public const string AtuObject = "AtuObject";
        }

        public static class DaysOfTheWeek
        {
            public const string Monday = "Mon";
            public const string Tuesday = "Tue";
            public const string Wednesday = "Wed";
            public const string Thursday = "Thu";
            public const string Friday = "Fri";
            public const string Sunday = "Sat";
            public const string Saturday = "Sun";
        }

        public static class Gender
        {
            public const string Male = "Male";
            public const string Female = "Female";
            public const string NotSpecified = "NotSpecified";
        }

        public static class IdentityDocument
        {
            public const string Passport = "Passport";
            public const string TaxNumber = "TaxNumber";
            public const string NotSpecified = "NotSpecified";
        }

        public static class ObjectStatus
        {
            public const string Active = "Active";
            public const string NotActive = "NotActive";
            public const string Project = "Project";
        }

    }
}
