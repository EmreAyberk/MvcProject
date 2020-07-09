using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerPlantMvcApplication.Data
{
    public class Stoppage
    {
        [Key] public long Id { get; set; }
        
        [ForeignKey(nameof(PowerPlantId))]
        public PowerPlant PowerPlant { get; set; }
        public long? PowerPlantId { get; set; }
        
        [ForeignKey(nameof(PowerPlantUnitId))]
        public PowerPlantUnit PowerPlantUnit { get; set; }
        public long? PowerPlantUnitId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
    }
}