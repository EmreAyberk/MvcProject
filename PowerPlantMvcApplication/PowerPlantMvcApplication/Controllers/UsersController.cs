using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PowerPlantMvcApplication.Data;

namespace PowerPlantMvcApplication.Controllers
{
    public class UsersController: Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public UsersController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var list = await _dbContext.Users.ToListAsync();
            
            return View(list);
        }
        public async Task<IActionResult> Edit()
        {
            return View(new IdentityUser());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string eMail, string password)
        {
            var result = await _userManager.CreateAsync(new IdentityUser
            {
                Email = eMail,
                UserName = eMail
            },password);

            if (result.Succeeded)
            {
                await _dbContext.SaveChangesAsync();
                TempData["Success"] = "1";
                return RedirectToAction("Index");
            }
            foreach (var identityError in result.Errors)
            {
                ModelState.AddModelError("",identityError.Description);
            }
            return View();
        }
        public async Task<IActionResult> Delete(string id)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(u=>u.Id==id);
            if (entity != null)
            {
                _dbContext.Users.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            return Ok(new {id=id});
        }
    }
}