using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcProgram.Models;
using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MvcProgram.Controllers
{
    public class UserController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<applicationUser> _userManager;
        SignInManager<applicationUser> _signInManager;

        public UserController(UserManager<applicationUser> userManager, SignInManager<applicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpGet]
        public IActionResult register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> register(User user)
        {

            // var model = _context.User.Find(user.UserName) ; 

            applicationUser indentityUser = new applicationUser
            {
                UserName = user.UserName,
                age = user.age,
                Address = user.Address,
                BirthDay = user.BirthDay
            };

            var result = await _userManager.CreateAsync(indentityUser, user.Password);
            //var model = _context.User.Where(m => user.UserName == m.UserName && user.Password == m.Password ).FirstOrDefault();

            //_context.Add(user);    
            //await _context.SaveChangesAsync();

            if (result.Succeeded)
                return Ok("註冊成功");
            else
                return BadRequest(result.Errors);


        }
        [HttpGet]
        public IActionResult Login()
        {

            //aaaaaaaa As6295638


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {

            var model = await _userManager.FindByNameAsync(user.UserName);
            

            // if (model == null)
            // {
            //     return BadRequest(model.UserName);
            // }

            var isLogin = await _userManager.CheckPasswordAsync(model, user.Password);
            if (!isLogin)
            {
                return BadRequest("密碼錯誤");
            }

            var result = await _signInManager.PasswordSignInAsync(model, user.Password, isPersistent: true, lockoutOnFailure: false);
            await _signInManager.SignInAsync(model, isPersistent: false);

            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name ,model.UserName),
                new Claim(type : "Age" , model.age.ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);



            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            var username = await _userManager.GetUserAsync(User);
            //return Ok(User.Identity.Name);
            return Redirect("/Product/index");


        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/User/Login");
        }
    }
}