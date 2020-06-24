using System.ComponentModel.DataAnnotations;

namespace PowerPlantMvcApplication.Data
{
    public class PowerPlant
    {
        [Key]
        public long Id { get; set; }
        
        public string Name { get; set; }
    }
}