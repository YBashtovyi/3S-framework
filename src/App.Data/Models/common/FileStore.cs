using Core.Base.Data;
using Core.Security;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Models
{
    [MainEntity(nameof(FileStore))]
    public class FileStore: BaseFileStore
    {
        public OrgUnit Owner { get; set; }
    }
}
