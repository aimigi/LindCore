using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LindCore.Manager.Controllers
{
    public class HomeController : Controller
    {
        ManagerContext _context;
        public IActionResult Index(ManagerContext context)
        {
            _context = context;
            var user = _context.WebManageUsers.ToList();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
