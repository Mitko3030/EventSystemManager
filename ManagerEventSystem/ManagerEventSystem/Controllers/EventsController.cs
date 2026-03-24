using EventSystemManager.Data;
using EventSystemManager.Data.Classes;
using EventSystemManager.Services;
using EventSystemManager.Services.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await eventService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}