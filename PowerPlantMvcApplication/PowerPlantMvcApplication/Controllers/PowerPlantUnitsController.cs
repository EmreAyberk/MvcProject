using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerPlantMvcApplication.Data;

namespace PowerPlantMvcApplication.Controllers
{
    public class PowerPlantUnitsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public PowerPlantUnitsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _dbContext.PowerPlantUnits.Include("PowerPlant").ToListAsync();

            return View(list);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            var ppList = await _dbContext.PowerPlants.ToListAsync();
            ViewData["PowerPlantList"] = ppList;

            if (id == null) return View(new PowerPlantUnit());
            var entity = await _dbContext.PowerPlantUnits.FirstOrDefaultAsync(u => u.Id == id);
            
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PowerPlantUnit input)
        {
            var entity = await _dbContext.PowerPlantUnits.FirstOrDefaultAsync(u => u.Id == input.Id);
            {
                entity = new PowerPlantUnit();
                await _dbContext.PowerPlantUnits.AddAsync(entity);
            }

            if(entity.Capacity<0)
            entity.Name = input.Name;
            entity.Capacity = input.Capacity;
            entity.PowerPlantId = input.PowerPlantId;

            await _dbContext.SaveChangesAsync();
            TempData["Success"] = "1";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            var entity = await _dbContext.Electrometers.FirstOrDefaultAsync(e => e.Id == id);
            if (entity != null)
            {
                _dbContext.Electrometers.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            return Ok(id);
        }
    }
}