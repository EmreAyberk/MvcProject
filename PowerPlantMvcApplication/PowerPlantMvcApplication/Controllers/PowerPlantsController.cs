using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerPlantMvcApplication.Data;

namespace PowerPlantMvcApplication.Controllers
{
    [Authorize]
    public class PowerPlantsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public PowerPlantsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _dbContext.PowerPlants.ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
                return View(new PowerPlant());
            var entity = await _dbContext.PowerPlants.FirstOrDefaultAsync(e => e.Id == id);
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PowerPlant input)
        {
            var entity = await _dbContext.PowerPlants.FirstOrDefaultAsync(e => e.Id == input.Id);
            if (entity == null)
            {
                entity = new PowerPlant();
                await _dbContext.PowerPlants.AddAsync(entity);
            }
            entity.Name = input.Name;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            var entity = await _dbContext.PowerPlants.FirstOrDefaultAsync(e => e.Id == id);
            if (entity != null)
            {
                _dbContext.PowerPlants.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }

            return Ok(id);
        }
    }
}