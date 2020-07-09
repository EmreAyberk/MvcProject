using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerPlantMvcApplication.Data;

namespace PowerPlantMvcApplication.Controllers
{
    public class ElectrometersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ElectrometersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _dbContext.Electrometers.Include("PowerPlantUnit").ToListAsync();
            
            return View(list);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            var ppUnitList = await _dbContext.PowerPlantUnits.ToListAsync();
            ViewData["PowerPlantUnitList"] = ppUnitList;
            
            if (id == null){return View(new Electrometer());}
            var entity = await _dbContext.Electrometers.FirstOrDefaultAsync(e => e.Id == id);

            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Electrometer input)
        {
            var entity = await _dbContext.Electrometers.FirstOrDefaultAsync(e => e.Id == input.Id);
            if (entity == null)
            {
                entity = new Electrometer();
                await _dbContext.Electrometers.AddAsync(entity);
            }

            entity.Name = input.Name;
            entity.Value = input.Value;
            entity.PowerPlantUnitId = input.PowerPlantUnitId;
            
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