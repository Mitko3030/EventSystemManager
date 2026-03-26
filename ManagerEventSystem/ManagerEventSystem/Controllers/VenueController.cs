using EventSystemManager.Data.Classes;
using EventSystemManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerEventSystem.Controllers
{
    [Authorize]
    public class VenuesController : Controller
    {
        private readonly VenueService venueService;

        public VenuesController(VenueService venueService)
        {
            this.venueService = venueService;
        }

        public async Task<IActionResult> Index()
        {
            var venues = await venueService.GetAllAsync();
            return View(venues);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Venue model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await venueService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await venueService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}