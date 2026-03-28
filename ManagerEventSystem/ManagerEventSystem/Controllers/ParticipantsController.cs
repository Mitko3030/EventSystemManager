using EventSystemManager.Data.Classes; 
using EventSystemManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerEventSystem.Controllers
{
    [Authorize]
    public class ParticipantsController : Controller
    {
        private readonly ParticipantService participantService;

        public ParticipantsController(ParticipantService participantService)
        {
            this.participantService = participantService;
        }

        public async Task<IActionResult> Index()
        {
            var participants = await participantService.GetAllAsync();
            return View(participants);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Participant model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await participantService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await participantService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}