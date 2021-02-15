using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Dto.Administration.NotMapped
{
    public class TokenInfo
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string user_id { get; set; }
    }
}
