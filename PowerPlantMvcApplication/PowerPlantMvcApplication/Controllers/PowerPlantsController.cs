using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerPlantMvcApplication.Data;

namespace PowerPlantMvcApplication.Controllers
{
    [Authorize]
    public class PowerPlantsController: Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public PowerPlantsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var list =await _dbContext.PowerPlants.ToListAsync();
            return View(list);
        }
    }
}