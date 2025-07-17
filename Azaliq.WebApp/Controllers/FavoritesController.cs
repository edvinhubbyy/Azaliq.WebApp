using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Azaliq.WebApp.Controllers
{
    public class FavoritesController : BaseController
    {
        private readonly IFavoriteService _favoriteService;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoritesController(IFavoriteService favoriteService, UserManager<ApplicationUser> userManager)
        {
            _favoriteService = favoriteService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var favorites = await _favoriteService.GetFavoritesAsync(userId);
            return View(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId)
        {
            var userId = _userManager.GetUserId(User);
            if (await _favoriteService.IsFavoriteAsync(userId, productId))
                await _favoriteService.RemoveFromFavoritesAsync(userId, productId);
            else
                await _favoriteService.AddToFavoritesAsync(userId, productId);

            return RedirectToAction("Index", "Product");
        }
    }

}
