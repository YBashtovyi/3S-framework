using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace App.DocumentTemplates.ViewModels
{
    public class DocElementVm
    {
        public DocDataVm Panel { get; set; }
        public IEnumerable<DocElementVm> Nested { get; set; } = Enumerable.Empty<DocElementVm>();
        public bool IsEmpty { get; set; }
    }
}
