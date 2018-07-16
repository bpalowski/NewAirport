using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Airport.Models;
namespace Airport.Controllers
{
    public class HomeController : Controller
    {
         [HttpGet("/")]
        public ActionResult Index()
        {
          return View();
        }
    }
}
