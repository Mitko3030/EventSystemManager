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
        private readonly IWebHostEnvironment webHostEnvironment;

        public EventsController(EventService eventService, IWebHostEnvironment webHostEnvironment)
        {
            this.eventService = eventService;
            this.webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create(Event model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(model);
            }

            
            if (imageFile != null && imageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images", "events");

                
                Directory.CreateDirectory(uploadsFolder);

                
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                
                model.ImageUrl = "/images/events/" + uniqueFileName;
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