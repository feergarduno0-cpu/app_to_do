using Microsoft.AspNetCore.Mvc;
using app_to_do.Models;
using app_to_do.Services;

namespace app_to_do.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController()
        {
            _authService = new AuthService();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool esValido = _authService.ValidarCredenciales(model);

            if (esValido)
            {
                return RedirectToAction("Welcome");
            }

            return RedirectToAction("Error");
        }

        [HttpGet]
        public IActionResult Welcome()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}