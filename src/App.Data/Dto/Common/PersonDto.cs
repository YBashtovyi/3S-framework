using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Models;
using Core.Base.Data;
using Core.Data.Dto.Common;
using Core.Security;

namespace App.Data.Dto.Common
{
    [MainEntity(nameof(Person))]
    public class PersonDetailsDto: BasePersonDto
    {
        public string GenderName { get; set; }
        public string IdentityDocumentName { get; set; }
    }

    [MainEntity(nameof(Person))]
    public class PersonListDto: BasePersonDto, IPagingCounted
    {
        public DateTime CreatedOn { get; set; }
        public string GenderName { get; set; }
        public string IdentityDocumentName { get; set; }
        public int TotalRecordCount { get; set; }
    }

    [MainEntity(nameof(Person))]
    public class PersonEditDto: BasePersonDto
    {
    }
}
