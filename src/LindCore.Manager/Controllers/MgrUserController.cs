using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LindCore.Manager.Entities;
using Microsoft.Extensions.Logging;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LindCore.Manager.Controllers
{
    public class MgrUserController : Controller
    {

        ManagerContext _context;
        public MgrUserController(ManagerContext context)
        {
            _context = context;

        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            return View(_context.WebManageUsers.ToList());
        }
        public IActionResult Detail(int id)
        {
            return View(_context.WebManageUsers.FirstOrDefault(i => i.Id == id));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(WebManageUsers user)
        {
            user.AddTime = DateTime.Now;
            user.Mobile = string.Empty;
            _context.WebManageUsers.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
