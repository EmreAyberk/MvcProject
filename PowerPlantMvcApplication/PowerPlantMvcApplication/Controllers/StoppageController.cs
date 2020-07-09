using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerPlantMvcApplication.Data;

namespace PowerPlantMvcApplication.Controllers
{
    [Authorize]
    public class StoppageController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public StoppageController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _dbContext.Stoppages.ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
                return View(new Stoppage());
            var entity = await _dbContext.Stoppages.FirstOrDefaultAsync(e => e.Id == id);
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Stoppage input)
        {
            var entity = await _dbContext.Stoppages.FirstOrDefaultAsync(e => e.Id == input.Id);
            if (entity == null)
            {
                entity = new Stoppage();
                await _dbContext.Stoppages.AddAsync(entity);
            }
            entity.Reason = input.Reason;
            entity.StartDate = input.StartDate;
            entity.EndDate = input.EndDate;
            entity.PowerPlantId = input.PowerPlantId;
            entity.PowerPlantUnitId = input.PowerPlantUnitId;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            var entity = await _dbContext.Stoppages.FirstOrDefaultAsync(e => e.Id == id);
            if (entity != null)
            {
                _dbContext.Stoppages.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }

            return Ok(id);
        }
    }
}