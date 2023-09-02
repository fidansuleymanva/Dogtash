using Fundamental.Application.DTOs.AdminDTOs;
using Fundamental.Domain.Entities;
using Fundamental.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dogtas.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly MainContext _mainContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> usersManager, SignInManager<AppUser> signInManager, MainContext mainContext, RoleManager<IdentityRole> roleManager)
        {
            _mainContext = mainContext;
            _userManager = usersManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginDto adminLoginDto)
        {

            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(adminLoginDto.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is incorrent!");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, adminLoginDto.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is incorret!");
                return View();
            }


            return RedirectToAction("Index", "dashboard");


        }


        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("login", "account");

        }


        public async Task<IActionResult> CreateRole()
        {
            IdentityRole role1 = new IdentityRole("SuperAdmin");
            IdentityRole role2 = new IdentityRole("Admin");

            await _roleManager.CreateAsync(role1);
            await _roleManager.CreateAsync(role2);
            return Ok("Created");
        }

        public async Task<IActionResult> AddRole()
        {
            AppUser user = new AppUser
            {
                UserName = "Admin"
            };
            await _userManager.CreateAsync(user, "Admin123");
            var result = await _userManager.AddToRoleAsync(user, "SuperAdmin");
            return Ok(result);
        }
    }
}
