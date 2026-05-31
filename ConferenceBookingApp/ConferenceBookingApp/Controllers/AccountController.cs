using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConferenceBookingApp.Controllers
{
    public class AccountController : Controller
    {
        // 1. Wyświetlanie formularza logowania
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // 2. Obsługa wysłania formularza
        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            string role = "";

            // 1. Sprawdzamy czy to ADMIN (tutaj wpisz swoje nowe hasło!)
            if (login == "admin" && password == "admin123!")
            {
                role = "Admin";
            }
            // 2. Sprawdzamy czy to ZWYKŁY UŻYTKOWNIK (do przeglądania)
            else if (login == "gosc" && password == "gosc123")
            {
                role = "User";
            }

            // Jeśli login i hasło pasowały do kogoś:
            if (!string.IsNullOrEmpty(role))
            {

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, login),
            new Claim(ClaimTypes.Role, role) // Przypisujemy rolę: Admin lub User
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Nieprawidłowy login lub hasło!";
            return View();
        }

        // 3. Wylogowanie
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}