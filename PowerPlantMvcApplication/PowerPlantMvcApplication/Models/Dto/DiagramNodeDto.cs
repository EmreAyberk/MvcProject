using System.Collections.Generic;

namespace PowerPlantMvcApplication.Models.Dto
{
    public class DiagramNodeDto
    {
        public string name { get; set; }
        public List<DiagramNodeDto> children { get; set; }
    }
}