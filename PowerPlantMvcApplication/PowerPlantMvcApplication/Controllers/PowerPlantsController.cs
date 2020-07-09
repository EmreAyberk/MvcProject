using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerPlantMvcApplication.Data;
using PowerPlantMvcApplication.Models.Dto;

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

        public async Task<IActionResult> Diagram(long? id)
        {
            var powerPlant = await _dbContext.PowerPlants.Where(p => p.Id == id).FirstOrDefaultAsync();
            var ppUnitList = await _dbContext.PowerPlantUnits.Where(p => p.PowerPlantId == id).ToListAsync();
            var unitIds = ppUnitList.Select(e => e.Id).ToArray();
            var electrometerList = await _dbContext.Electrometers.Where(e => unitIds.Contains(e.PowerPlantUnitId.Value)).ToListAsync();
            
            var diagramData = new DiagramNodeDto()
            {
                name = powerPlant.Name,
                children = ppUnitList.Select(u=>new DiagramNodeDto{
                    name = u.Name,
                    children = electrometerList.Where(e=>e.PowerPlantUnitId == u.Id).Select(e=>new DiagramNodeDto
                    {
                        name = e.Name
                    }).ToList()
                }).ToList()
            };
            return View(diagramData);
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