using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PowerPlantMvcApplication.Data;
using PowerPlantMvcApplication.Models;
using PowerPlantMvcApplication.Models.Dto;

namespace PowerPlantMvcApplication.Controllers
{
    [Authorize]
    public class DiagramController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public DiagramController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}