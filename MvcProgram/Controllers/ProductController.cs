using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcProgram.Models;


namespace MvcProgram.Controllers
{
    public class ProductController : Controller
    {
        private ProductContext _context ; 
        public ProductController(ProductContext context){
            _context = context ; 
        }
        public async Task<IActionResult> index(){
            var db = await _context.Product.ToListAsync();


            return View(db) ; 
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet]
        public  IActionResult create(){
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(Models.Product model){
            _context.Add(model) ; 
            await _context.SaveChangesAsync();
            return RedirectToAction("index") ; 
        }

        [HttpGet]
        public IActionResult edit(string? id){
            
            var model = _context.Product.Where(m => m.Name == id).FirstOrDefault();

            return View(model) ; 
        }
        [HttpPost]
        public IActionResult edit(Models.Product model){
            

            var data = _context.Product.Where(m => m.Name == model.Name).FirstOrDefault();
            if(data != null ){
                data.Name = model.Name ; 
                data.price = model.price ; 
                _context.SaveChangesAsync() ; 
                return RedirectToAction("Index") ; 
            }

            return View() ; 
        }
        
        [HttpGet]
        public IActionResult delete(string? id){
            var model = _context.Product.Find(id);
            
            return View(model) ; 
        }

        [HttpPost]
        public async Task<IActionResult> delete(Models.Product model){
            

            var data = _context.Product.Where(m => m.Name == model.Name).FirstOrDefault();
            if(data != null ){
                _context.Remove(data) ;  
                await _context.SaveChangesAsync() ; 
                return RedirectToAction("Index") ; 
            }
            
            return RedirectToAction($"delete/{model.Name}") ; 
        }


        public IActionResult detail(string? id){
            var model = _context.Product.Find(id) ; 




            return View(model);
        }
    }
}