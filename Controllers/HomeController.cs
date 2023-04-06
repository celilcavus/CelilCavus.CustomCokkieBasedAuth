using System.Security.Claims;
using CelilCavus.Data.Contexts;
using CelilCavus.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelilCavus.Controllers
{
    public class HomeController : Controller
    {

        private readonly CokkieContext _context;

        public HomeController(CokkieContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> SignIn()
        {
            return View(new UserSignInModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            var user = _context.ApplicationUsers.SingleOrDefault(x => x.userName == model.UserName && x.password == model.Password);

            if (user != null)
            {
                var role = _context.ApplicationRoles.Where(x => x.ApplicationUserRoles.Any(x => x.UserId == user.Id)).Select(x => x.Definition).ToList();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                };
                foreach (var item in role)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.Remember
                };

                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

                return RedirectToAction("Index", "Home");

            }
            ModelState.AddModelError("", "Kullanici Adi Veya Şifre Hatalidir");
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Member")]
        public IActionResult Member()
        {
            return View();
        }
        public async Task<IActionResult> LogOut(UserSignInModel model)
        {   
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
    }
}