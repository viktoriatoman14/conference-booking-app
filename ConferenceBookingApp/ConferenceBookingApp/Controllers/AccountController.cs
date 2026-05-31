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
            // Tutaj definiujesz dane logowania na sztywno. Możesz je zmienić na dowolne!
            if (login == "admin" && password == "admin123")
            {
                // Tworzymy "tożsamość" użytkownika
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Zapisujemy ciasteczko w przeglądarce użytkownika
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Po udanym logowaniu wracamy na stronę główną
                return RedirectToAction("Index", "Home");
            }

            // Jeśli dane są złe, pokazujemy błąd
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