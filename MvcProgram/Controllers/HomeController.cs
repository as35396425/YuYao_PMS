using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcProgram.Models;

namespace MvcProgram.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Test()
    {

        var modelList = new List<Product>();
        for (int i = 0; i < 3; i++)
        {
            var model = new Product();
            model.Name = "楊于曜" ;
            model.price = i * 10 ;
            modelList.Add(model) ; 
        }

        ViewBag.List =modelList ;  
        ViewData["List"] = modelList ; 
        return View(modelList);
    }
    
    [HttpPost]
    public IActionResult Verify(User model)
    {

        
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
