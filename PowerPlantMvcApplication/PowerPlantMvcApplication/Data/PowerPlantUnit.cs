using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerPlantMvcApplication.Data
{
    public class PowerPlantUnit
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(PowerPlantId))] 
        public PowerPlant PowerPlant { get; set; }
        public long? PowerPlantId { get; set; }
        public long Capacity { get; set; }
    }
}