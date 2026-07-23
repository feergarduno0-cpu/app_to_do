using Microsoft.AspNetCore.Mvc;
using app_to_do.Models;
using app_to_do.Services;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Welcome()
        {
            try
            {
                // Instanciar el cliente del servicio WCF agregado
                DogServiceReference.DogServiceClient client = new DogServiceReference.DogServiceClient();

                // Consumir el método que obtiene la URL del perrito de forma asíncrona
                var urlPerrito = await client.ObtenerPerritoDelDiaAsync();

                // Enviar la URL a la vista usando ViewBag
                ViewBag.UrlPerrito = urlPerrito;
            }
            catch
            {
                // En caso de error de conexión con el WCF, se define como nulo
                ViewBag.UrlPerrito = null;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}