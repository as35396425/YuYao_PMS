using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using MvcProgram.Models;


namespace MvcProgram.Controllers
{
    public class ProductController : Controller
    {
        private IdentityContext _context ; 
        private readonly IProductService  _service;
        private readonly UserManager<applicationUser> _userManager;
        public ProductController(IdentityContext context , IProductService  service ,UserManager<applicationUser> userManager){
            _context = context ; 
            _service = service ; 
            _userManager = userManager;
            
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> index()
        {

            if(User.Identity.IsAuthenticated == false)
                return Unauthorized("請登入");
            var id = User.FindFirst("UID")?.Value;
            //var model = await _userManager.GetUserAsync(User);
            var db = await _context.Products.Where(m => m.UID == id).ToListAsync();
            return View(db);

        }


       
        [HttpGet]
        public  IActionResult create(){
            // if(User.Identity.IsAuthenticated == false)
            //     return Unauthorized("請登入");
            return View();
        }

        [HttpPost]
         [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]  
        public async Task<IActionResult> create(Models.Product model)
        {
            if (User.Identity.IsAuthenticated == false)
                return Unauthorized("請登入");

            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        [HttpGet]
        public  IActionResult createToUser(){
            
            return View();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> createToUser(Models.Product model)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return BadRequest("請登入");
            }

            string userID = User.FindFirst("UID")?.Value;
            model.UID = userID;
            //var Identity = User.Identity;
            //var user = await _userManager.GetUserAsync();
            //Debug.WriteLine(User.Identity.Name);

            //model.UID = user.Name;
            _context.Add(model);

            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult edit(int? id) {

            string userID = User.FindFirst("UID")?.Value;
            //string userID = _userManager.GetUserId(User).ToString();

            var model = _context.Products.Where(m => m.ProductID == id).FirstOrDefault();

            if (model.UID != userID)
                return BadRequest($"沒有權限{userID}");

            return View(model);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> edit(Models.Product model)
        {
            if (User.Identity.IsAuthenticated == false)
                return Unauthorized("請登入");

            string userID = User.FindFirst("UID")?.Value;
            
            //Console.WriteLine(model.Name);
            var data = _context.Products.Find(model.ProductID);
            if (data.UID != userID)
                return BadRequest("沒有權限");


            if (data != null)
            {
                data.Name = model.Name;
                data.price = model.price;
                data.Description = model.Description;
                _context.Update(data);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction($"index");
            }

        }
        
        [HttpGet]
        public IActionResult delete(int? id){
            var model = _context.Products.Find(id);
            string userID = User.FindFirst("UID")?.Value;
            if (model.UID != userID)
                return BadRequest($"沒有權限{userID}");

            return View(model) ; 
        }

        [HttpPost]
        public async Task<IActionResult> delete(Models.Product model){
            

            var data = _context.Products.Find(model.ProductID);
            if(data.UID != User.FindFirst("UID")?.Value)
                return BadRequest("沒有權限");
                
            if (data != null)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction($"delete/{model.ProductID}");
            }
        }


        public async Task<IActionResult> detail(int? id)
        {
            var model = await _service.GetFormAsync(id) ; 
            return View(model);
        }

    }
}