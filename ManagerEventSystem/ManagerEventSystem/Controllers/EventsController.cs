using EventSystemManager.Data.Classes;
using EventSystemManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManagerEventSystem.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly EventService eventService;

        public EventsController(EventService eventService)
        {
            this.eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await eventService.GetAllAsync();
            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(model);
            }

            await eventService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await eventService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdownsAsync()
        {
            var categories = await eventService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var venues = await eventService.GetAllVenuesAsync();
            ViewBag.Venues = new SelectList(venues, "Id", "Name");
        }
    }
}