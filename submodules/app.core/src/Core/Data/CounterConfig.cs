using System;
using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.Data
{
    public class CounterConfig
    {
        [Required]
        public string CounterName { get; set; }
        public Guid EntityId { get; set; }
        [Required]
        public string EntityName { get; set; }
        public RegNumberCounterType CounterType => RegNumberCounterType.Increment;
        public NumberCounterPattern Pattern => NumberCounterPattern.Number;
    }
}
