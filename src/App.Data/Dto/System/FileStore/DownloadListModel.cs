using System.Collections.Generic;

namespace App.Data.Dto.System
{
    public class DownloadListModel
    {
        public double TimeZoneOffsetMinutes { get; set; }
        public Dictionary<string, string> ParamList { get; set; }
        public string OrderBy { get; set; }
        public Dictionary<string, string> Columns { get; set; }
    }
}
