namespace PowerPlantMvcApplication.Models.Dto
{
    public class HomeIndexDto
    {
        public string[] Labels { get; set; }
        public int[] Values { get; set; }
        public int[] Capacity { get; set; }
        public HomeIndexDto SearchDto { get; set; }
    }
}