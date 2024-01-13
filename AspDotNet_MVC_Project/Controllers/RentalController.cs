using AspDotNet_MVC_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Controllers
{

    public class RentalController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RentalDbContext _context;

        public RentalController(RentalDbContext context){
            _context = context;
        }

        public IActionResult CarList()
        {

          var cars = _context.Cars.ToList();
          return View(cars);
            
        }
        public ActionResult CreateCar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            return RedirectToAction("CarList");

        }
        public ActionResult DeleteCar(int id)
        {
            return View(_context.Cars.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCar(int id, Car car)
        {
            Car car1 = _context.Cars.FirstOrDefault(x => x.Id == id);
            _context.Cars.Remove(car1);
            _context.SaveChanges();
            return RedirectToAction("CarList");

        }

        public ActionResult DetailsCar(int id)
        {
            return View(_context.Cars.FirstOrDefault(x => x.Id == id));
        }

        public ActionResult EditCar(int id)
        {
            return View(_context.Cars.FirstOrDefault(x => x.Id == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar(int id, Car car)
        {
            Car car1 = _context.Cars.FirstOrDefault(x => x.Id == id);
            car1.Make = car.Make;
            car1.Model = car.Model;
            car1.MfYear = car.MfYear;
            car1.Price = car.Price;
            _context.SaveChanges();

            return RedirectToAction("CarList");
        }

        public IActionResult UserList()
        {
            return View(_context.Users);
        }

      
    }
}
