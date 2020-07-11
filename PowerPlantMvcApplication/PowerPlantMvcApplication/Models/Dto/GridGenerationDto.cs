using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PowerPlantMvcApplication.Models.Dto
{
    public class GridGenerationDto
    {
        public string powerplant { get; set; }
        public string unit { get; set; }
        public long? unitValue { get; set; }
        public long? electrometerValue { get; set; }
        public string electrometerName { get; set; }
        public bool isOverLoad { get; set; }
    }
}