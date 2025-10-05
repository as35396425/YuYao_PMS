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
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MvcProgram.Controllers
{
    public class UserController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<applicationUser> _userManager;
        SignInManager<applicationUser> _signInManager;
        IConfiguration _config;
        public UserController(UserManager<applicationUser> userManager, SignInManager<applicationUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;

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
            
            if (_userManager.FindByNameAsync(user.UserName).Result != null)
                return BadRequest("帳號已存在");


            var result = await _userManager.CreateAsync(indentityUser, user.Password);
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

            var isLogin = await _userManager.CheckPasswordAsync(model, user.Password);
            if (!isLogin)
            {
                return BadRequest("密碼錯誤");
            }
            var UID = _userManager.FindByNameAsync(user.UserName).Result.Id;
            var token = GenerateJwtToken(user , UID);
            var bearer = new JwtSecurityTokenHandler().WriteToken(token);

            Response.Cookies.Append("jwt", bearer, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.Now.AddMinutes(30)
            });

            return Ok(new
            {
                bearer,
                expiration = token.ValidTo
            });
            return Redirect("/Product/index");


        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //await _signInManager.SignOutAsync
            //var exp = User.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;
            Response.Cookies.Delete("jwt");

            //await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            return Redirect("/User/Login");
        }

         public JwtSecurityToken  GenerateJwtToken(User user , string id)
        {
            var JWT = _config.GetSection("JWT");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT["Key"]));
            var credentials = new SigningCredentials(securityKey,JWT["alg"]);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name ,user.UserName),
                new Claim(type : "UID" , id),
            };
            var token = new JwtSecurityToken(
                //issuer: "localhost",
                //audience: "localhost",
                claims: claims,
                expires: DateTime.Now.AddMinutes(JWT.GetValue<int>("ExpInMin")),
                signingCredentials: credentials
            );

            return token;
        }
    }
}