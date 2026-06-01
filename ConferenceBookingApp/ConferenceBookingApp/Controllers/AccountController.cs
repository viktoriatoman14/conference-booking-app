using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConferenceBookingApp.Controllers
{
    public class AccountController : Controller
    {
        // Wyświetlanie formularza logowania
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // wysłanie formularza
        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            string role = "";

            // admin
            if (login == "admin" && password == "admin123!")
            {
                role = "Admin";
            }
            // uzytkownik(gosc)
            else if (login == "gosc" && password == "gosc123")
            {
                role = "User";
            }

            //login i hasło pasowały
            if (!string.IsNullOrEmpty(role))
            {

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, login),
            new Claim(ClaimTypes.Role, role) //Admin lub User
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Nieprawidłowy login lub hasło!";
            return View();
        }

        // wylogowanie
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}