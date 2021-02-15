using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.DocumentTemplates.ReportModels
{
    [NotMapped]
    public class Report4110
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }

        public int CountAll { get; set; }
        public int gp { get; set; }
        public int cp { get; set; }
        public int kss { get; set; }
        public int etc { get; set; }    

    }
}
