using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcProgram.Models;

namespace MvcProgram.Controllers;

public class HelperController : Controller
{
    public IActionResult index()
    {
        return View();

    }

    [HttpPost]
    public IActionResult Login(IFormCollection obj)
    {
        ViewBag.username = obj["username"];
        ViewBag.password = obj["password"];



        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
       

        return View();
    }

    [HttpPost]
    public IActionResult Register(Models.User model)
    {
       
        return View(model);

    }
    [HttpGet]
    public IActionResult Register()
    {
       

        return View();
    }
}