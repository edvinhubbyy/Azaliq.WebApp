using Azaliq.Data;
using Azaliq.Services.Core;
using Azaliq.Services.Core.Contracts;
using Azaliq.ViewModels.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Azaliq.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class StoreController : BaseController
    {
        private readonly IStoreService _storeService;
        private readonly ApplicationDbContext _context;

        public StoreController(IStoreService storeService, ApplicationDbContext context)
        {
            _storeService = storeService;
            _context = context;
        }

        // GET: /Store
        [HttpGet]
        [AllowAnonymous] // Allow anonymous access to the index page
        public async Task<IActionResult> Index()
        {
            var stores = await _storeService.GetAllAsync();
            return View(stores);
        }

        // GET: /Store/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Store/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStoreLocationInputModel inputModel)
        {
            if (!ModelState.IsValid)
                return View(inputModel);

            bool addressExists = await _storeService.AddressExistsAsync(inputModel.Address);
            if (addressExists)
            {
                ModelState.AddModelError(nameof(inputModel.Address), "A store with this address already exists.");
                return View(inputModel);
            }

            await _storeService.AddAsync(inputModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Store/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var store = await _storeService.GetByIdAsync(id);
            if (store == null)
                return NotFound();

            var editModel = new EditStoreLocationInputModel
            {
                Id = store.Id,
                Name = store.Name,
                GoogleMapsUrl = store.GoogleMapsUrl,
                Address = store.Address,
                Phone = store.Phone
            };

            return View(editModel);
        }

        // POST: /Store/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditStoreLocationInputModel inputModel)
        {
            if (!ModelState.IsValid)
                return View(inputModel);

            bool addressExists = await _storeService.AddressExistsAsync(inputModel.Address, inputModel.Id);
            if (addressExists)
            {
                ModelState.AddModelError(nameof(inputModel.Address), "A store with this address already exists.");
                return View(inputModel);
            }

            bool updated = await _storeService.UpdateAsync(inputModel);
            if (!updated)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Store/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var store = await _storeService.GetByIdAsync(id);
            if (store == null)
                return NotFound();

            return View(store);
        }

        // POST: /Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleted = await _storeService.DeleteAsync(id);
            if (!deleted)
            {
                ModelState.AddModelError("", "Unable to delete store.");
                var store = await _storeService.GetByIdAsync(id);
                return View(store);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Store/Map/5
        [AllowAnonymous]
        public async Task<IActionResult> Map(int id)
        {
            var store = await _storeService.GetByIdAsync(id);
            if (store == null)
                return NotFound();

            return View(store);
        }
    }
}
