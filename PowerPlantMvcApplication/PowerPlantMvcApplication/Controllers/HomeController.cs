using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using PowerPlantMvcApplication.Data;
using PowerPlantMvcApplication.Models;
using PowerPlantMvcApplication.Models.Dto;

namespace PowerPlantMvcApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var ppUnitList = _dbContext.PowerPlantUnits.ToList();
            var electrometerList = _dbContext.Electrometers.Include("PowerPlantUnit").ToList();
            var valueArray = new List<int>();
            var capacityarray = new List<int>();
            foreach (var em in electrometerList)
            {
                valueArray.Add(Convert.ToInt32(em.Value));
                foreach (var unit in ppUnitList)
                {
                    if (em.PowerPlantUnitId == unit.Id)
                    {
                        var unitEms = electrometerList.Where(e => e.PowerPlantUnitId == unit.Id).Count();
                        capacityarray.Add(Convert.ToInt32(unit.Capacity)/unitEms);
                        break;
                    }
                }
            }

            var model = new HomeIndexDto
            {
                Labels = electrometerList.Select(e => e.Name).ToArray(),
                Values = valueArray.ToArray(),
                Capacity = capacityarray.ToArray(),
                SearchDto = new HomeIndexDto()
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}