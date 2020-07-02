using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerPlantMvcApplication.Data
{
    public class Electrometer
    {
        [Key] public long Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(PowerPlantId))]
        public PowerPlant PowerPlant { get; set; }
        public long? PowerPlantId { get; set; }
        public long Value { get; set; }
    }
}