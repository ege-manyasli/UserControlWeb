using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using UserControlWeb.Context;
using UserControlWeb.Models;

namespace UserControlWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContext _db;
        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public JsonResult _UserList()
        {
            return Json(_db.Users.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult CreateUser(Users user)
        {
        try
        {
        
            _db.Users.Add(user);
            _db.SaveChanges();


                return RedirectToAction("Index", "Home");
        }
            catch (Exception ex)
            {
        
                return BadRequest(ex.Message);
            }
        }

        

            [HttpPost]
        public IActionResult EditUser(Users user)
        {
            var existingUser = _db.Users.FirstOrDefault(u => u.UserId == user.UserId);

            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Surname = user.Surname;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;
                existingUser.Password = user.Password;

                _db.SaveChanges();

                TempData["ShowAlert"] = true;

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult EditUser(int userId)
        {

            return View(_db.Users.FirstOrDefault(u => u.UserId == userId));
        }



        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            var existingUser = _db.Users.FirstOrDefault(u => u.UserId == userId);

            if (existingUser != null)
            {
                _db.Users.Remove(existingUser);
                _db.SaveChanges();

                return Json(new { success = true, message = "Kullanýcý baþarýyla silindi." });
            }

            return Json(new { success = false, message = "Belirtilen kullanýcý bulunamadý." });
        }

        public IActionResult HomePage()
        {
            return View();
        }



    }
}
