using Microsoft.AspNetCore.Mvc;
using MongoBackend.Models;
using MongoDB.Bson;
using System.Diagnostics;

namespace MongoBackend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DatabaseHelper.Database db = new DatabaseHelper.Database();

            if (db.getUsers().Count != 0)
            {
                ViewBag.UserList = db.getUsers();
                return View("Index");
            }
            else

            return View();
        }
        public IActionResult insertUser(string txtNombre, string txtDirec, string txtTel,string txtCorreo)
        {

            DatabaseHelper.Database db = new DatabaseHelper.Database();

            db.insertUser(new User()
            {
                name = txtNombre,
                email = txtCorreo,
                phone = txtTel,
                address = txtDirec,
                dateIn = DateTime.Now
            });
            return RedirectToAction("Index", "Home");

        }

        public IActionResult updateUser(string txtId,string txtNombre, string txtDirec, string txtTel, string txtCorreo)
        {
            User user = new User()
            {
                _id = ObjectId.Parse(txtId),
                name = txtNombre,
                address = txtDirec,
                phone = txtTel,
                email = txtCorreo

            };

            DatabaseHelper.Database db = new DatabaseHelper.Database();

            db.updateUser(user);
            return RedirectToAction("Index", "Home");

        }

        public IActionResult goUpdateUser(string txtIndex)
        {
            DatabaseHelper.Database db = new DatabaseHelper.Database();
            User user = db.getUser(txtIndex);
           ViewBag.User=user;
            return View("Index2","Home");
        }


        public IActionResult deleteUser(string txtindex)
        {
            DatabaseHelper.Database database = new DatabaseHelper.Database();
            database.deleteUser(txtindex);
            return RedirectToAction("Index","Home");
        }
        public IActionResult CancelView()
        {
            return RedirectToAction("Index", "Home");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}