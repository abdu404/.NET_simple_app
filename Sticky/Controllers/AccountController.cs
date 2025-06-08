using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sticky.Data; 
using Sticky.Models;
using Sticky.Models.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Sticky.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("", "Username already exists.");
                    return View(model);
                }

                var user = new User
                {
                    Username = model.Username
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

                _context.Users.Add(user);
                _context.SaveChanges();

                
                return RedirectToAction("Login");
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.Username == model.Username);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(model);
                }

                var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

                if (result == PasswordVerificationResult.Success)
                  {
                    var claims = new List<Claim>
                       {
                       new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                       new Claim(ClaimTypes.Name, user.Username)
                       };

                      var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                      var principal = new ClaimsPrincipal(identity);

                      var authProperties = new AuthenticationProperties
                       {
                        IsPersistent = true
                       };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        authProperties
                     );

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Register");
                }
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

    }
}
