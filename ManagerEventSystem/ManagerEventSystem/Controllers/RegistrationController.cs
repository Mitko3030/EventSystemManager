using EventSystemManager.Data.Classes;
using EventSystemManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManagerEventSystem.Controllers
{
    [Authorize]
    public class RegistrationsController : Controller
    {
        private readonly RegistrationService registrationService;

        public RegistrationsController(RegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }

        public async Task<IActionResult> Index()
        {
            var registrations = await registrationService.GetAllAsync();
            return View(registrations);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // NEW: Load events and participants into dropdowns
            var events = await registrationService.GetAllEventsAsync();
            ViewBag.Events = new SelectList(events, "Id", "Title");

            var participants = await registrationService.GetAllParticipantsAsync();
            ViewBag.Participants = new SelectList(participants, "Id", "FirstName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Registration model)
        {
            if (!ModelState.IsValid)
            {
                // NEW: Reload dropdowns if validation fails
                var events = await registrationService.GetAllEventsAsync();
                ViewBag.Events = new SelectList(events, "Id", "Title");

                var participants = await registrationService.GetAllParticipantsAsync();
                ViewBag.Participants = new SelectList(participants, "Id", "FirstName");

                return View(model);
            }

            await registrationService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await registrationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}