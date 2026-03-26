using EventSystemManager.Data.Classes;
using EventSystemManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerEventSystem.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await categoryService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}