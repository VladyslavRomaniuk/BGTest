using BgLocal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BGLocalWeb.Controllers {
    [Authorize]
    public class AccountController : Controller {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index() {
            var user = await _userManager.GetUserAsync(User);

            if (user == null) {
                return NotFound();
            }

            return View(user);
        }
    }
}
