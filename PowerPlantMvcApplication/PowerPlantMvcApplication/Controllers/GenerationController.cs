using System.Collections.Generic;
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
    public class GenerationController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public GenerationController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _dbContext.PowerPlantUnits.Include("PowerPlant").ToListAsync();

            return View(list);
        }
        public async Task<IActionResult> Grid(long? id)
        {
            
            var pp = await _dbContext.PowerPlants.Where(p => p.Id == id).FirstOrDefaultAsync();
            var ppUnitList = await _dbContext.PowerPlantUnits.Where(p => p.PowerPlantId == id).ToListAsync();
            var unitIds = ppUnitList.Select(e => e.Id).ToArray();
            var electrometerList = await _dbContext.Electrometers.Where(e => unitIds.Contains(e.PowerPlantUnitId.Value)).ToListAsync();

            var gridData = new List<GridGenerationDto>();

            foreach (var electrometer in electrometerList)
            {
                gridData.Add(new GridGenerationDto()
                {
                    powerplant = pp.Name,
                    unit = ppUnitList.Where(u=>u.Id == electrometer.PowerPlantUnitId).Single().Name,
                    unitValue = ppUnitList.Where(u=>u.Id == electrometer.PowerPlantUnitId).Single().Capacity,
                    electrometerName= electrometer.Name,
                    electrometerValue= electrometer.Value,
                    isOverLoad = electrometer.Value>ppUnitList.Where(u=>u.Id == electrometer.PowerPlantUnitId).Single().Capacity
                });
            }
            
            return View(gridData);
        }
/*
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
                return View(new Generation());
            var entity = await _dbContext.Generations.FirstOrDefaultAsync(e => e.Id == id);
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Generation input)
        {
            var entity = await _dbContext.Generations.FirstOrDefaultAsync(e => e.Id == input.Id);
            if (entity == null)
            {
                entity = new Generation();
                await _dbContext.Generations.AddAsync(entity);
            }
            entity.Reason = input.Reason;
            entity.StartDate = input.StartDate;
            entity.EndDate = input.EndDate;
            entity.PowerPlantId = input.PowerPlantId;
            entity.PowerPlantUnitId = input.PowerPlantUnitId;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
*/
        public async Task<IActionResult> Delete(long id)
        {
            var entity = await _dbContext.Generations.FirstOrDefaultAsync(e => e.Id == id);
            if (entity != null)
            {
                _dbContext.Generations.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }

            return Ok(id);
        }
    }
}