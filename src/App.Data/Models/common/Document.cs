using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Base.Data;

namespace App.Data.Models
{
    [Table("Cmn"+nameof(Document))]
    public class Document: BaseDocument
    {
    }
}
