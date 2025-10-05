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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace MvcProgram.Controllers
{
    public class JWTController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<applicationUser> _userManager;
        SignInManager<applicationUser> _signInManager;
        IConfiguration _configuration;
        public JWTController(UserManager<applicationUser> userManager, SignInManager<applicationUser> signInManager , IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
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
            return BadRequest(GenerateJwtToken(user.UserName));

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
        
        public string GenerateJwtToken(string userName)
        {   
            var JWTsetting = _configuration.GetSection("JWT");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTsetting["Key"]));
            var credentials = new SigningCredentials(securityKey, JWTsetting["alg"]);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: JWTsetting["Issuer"],
                audience: JWTsetting["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(JWTsetting["ExpireMinutes"] ?? "30")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}