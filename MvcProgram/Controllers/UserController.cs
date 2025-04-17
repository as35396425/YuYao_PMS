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
        private ProductContext _context ; 
       // private readonly UserManager<IdentityUser> _userManager;

        public UserController(ProductContext context){
            _context = context ; 
             
        }
        
        [HttpGet]
        public IActionResult register(){
            return View() ; 
        }

        [HttpPost]
        public async Task<IActionResult> register(User user){
            
            
            var model = _context.User.Find(user.UserName) ; 
                
            
            //var indentityUser = new IdentityUser { UserName = user.UserName };

            //var result = await _userManager.CreateAsync(indentityUser, user.Password);
            //var model = _context.User.Where(m => user.UserName == m.UserName && user.Password == m.Password ).FirstOrDefault();



            if (model != null){
                return Content($"{model.UserName}已經存在") ; 
            }

            _context.Add(user);    
            await _context.SaveChangesAsync();
            
            return Ok($"{model.UserName}成功註冊") ; 
            
        
        }
                [HttpGet]
        public IActionResult Login(){
            



            return View() ; 
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user){
            
            var model = _context.User.Where(m => user.UserName == m.UserName && user.Password == m.Password ).FirstOrDefault();
            
            if (model == null){
                return Content("用戶不存在或密碼錯誤");
            }
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name ,model.UserName),
                new Claim(type : "Age" , model.age.ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(claims ,  CookieAuthenticationDefaults.AuthenticationScheme); 



            var authProperties = new AuthenticationProperties{
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            
            return Ok(user) ; 
        

        }
    }
}