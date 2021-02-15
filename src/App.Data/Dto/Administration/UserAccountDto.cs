using System;
using System.Collections.Generic;
using System.Text;
using Core.Base.Data;

namespace App.Data.Dto.Administration
{
    public class UserAccountDto: CoreDto
    {
        public Guid UserId { get; set; }

        public string AuthProvider { get; set; }

        public string AccountId { get; set; }
    }
}
