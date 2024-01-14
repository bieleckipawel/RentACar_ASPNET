using AspDotNet_MVC_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarRentalApp.Controllers
{

    public class RentalController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RentalDbContext _context;

        public RentalController(UserManager<User> userManager, RentalDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [Authorize(Roles = "Admin, User")]
        public IActionResult CarList()
        {

          var cars = _context.Cars.ToList();
          return View(cars);
            
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CreateCar()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            return RedirectToAction("CarList");

        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCar(int id)
        {
            return View(_context.Cars.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult EditCar(int id)
        {
            return View(_context.Cars.FirstOrDefault(x => x.Id == id));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult UserList()
        {
            return View(_context.Users);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await _context.Users.FindAsync(id);
            return View(user);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _context.Users.FindAsync(id);
            return View(user);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        //creating user broke this
        public async Task<IActionResult> UserEdit(User user, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var userToUpdate = await _userManager.FindByIdAsync(user.Id);
                userToUpdate.Email = user.Email;
                userToUpdate.UserName = user.Email;
                userToUpdate.IsVerified = user.IsVerified;
                var updateResult = await _userManager.UpdateAsync(userToUpdate);
                var currentRoles = await _userManager.GetRolesAsync(userToUpdate);
                var rolesToAdd = selectedRoles.Except(currentRoles).ToList();
                var rolesToRemove = currentRoles.Except(selectedRoles).ToList();
                if (rolesToAdd.Contains("Admin"))
                {
                    userToUpdate.IsVerified = true;
                    await _userManager.UpdateAsync(userToUpdate);
                }
                await _userManager.AddToRolesAsync(userToUpdate, rolesToAdd);
                await _userManager.RemoveFromRolesAsync(userToUpdate, rolesToRemove);
                return RedirectToAction("UserList");
            }
            else return View(user);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult UserCreate()
        {
            var user = new User();
            return View(user);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreate(User model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email, Name=model.Name, IsVerified = model.IsVerified};
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRolesAsync(user, selectedRoles);
                return RedirectToAction("UserList");
            }
            return View(model);
        }
        public async Task<IActionResult> RentalList()
        {
            IQueryable<Rental> query = _context.Rentals.Include(r => r.Car).Include(r => r.Customer);

            if (!User.IsInRole("Admin"))
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                query = query.Where(r => r.CustomerId == currentUserId);
            }

            var rentals = await query.ToListAsync();
            return View(rentals);
        }

        public async Task<IActionResult> RentalCreate()
        {
            var cars = _context.Cars.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Make} {c.Model}"
            }).ToList();

            ViewBag.CarId = new SelectList(cars, "Value", "Text");

            if (User.IsInRole("Admin"))
            {
                var customers = await _userManager.Users.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.Name
                }).ToListAsync();

                ViewBag.CustomerId = new SelectList(customers, "Value", "Text");
            }

            return View(new Rental());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RentalCreate(Rental rental)
        {
            ModelState.Remove("Car");
            ModelState.Remove("Customer");
            if (rental.ReturnDate <= rental.RentalDate)
            {
                ModelState.AddModelError("ReturnDate", "Return Date must be greater than Rental Date.");
            }
            bool isCarAlreadyRented = await _context.Rentals.AnyAsync(r =>
                r.CarId == rental.CarId &&
                rental.RentalDate < r.ReturnDate &&
                rental.ReturnDate > r.RentalDate);
            if (isCarAlreadyRented)
            {
                ModelState.AddModelError("CarId", "This car is already rented during the selected period.");
            }

            if (ModelState.IsValid)
            {
                var car = await _context.Cars.FindAsync(rental.CarId);
                var user = await _userManager.FindByIdAsync(rental.CustomerId);
                if (!user.IsVerified)
                {
                    TempData["RentalMessage"] = "Rent is successful, but you will need to confirm your data.";
                }
                else
                {
                    TempData["RentalMessage"] = "Thank you for your rent.";
                }
                var rentalDays = (rental.ReturnDate - rental.RentalDate).Days;
                rental.TotalPrice = rentalDays * car.Price;
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(RentalList));
            }
            var cars = _context.Cars.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Make} {c.Model}"
            }).ToList();

            ViewBag.CarId = new SelectList(cars, "Value", "Text");

            if (User.IsInRole("Admin"))
            {
                var customers = await _userManager.Users.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.Name
                }).ToListAsync();

                ViewBag.CustomerId = new SelectList(customers, "Value", "Text");
            }
            return View();
        }
        public async Task<IActionResult> RentalDelete(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.Id == id);
            return View(rental);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RentalDeleteConfirm(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            _context.Rentals.Remove(rental); 
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(RentalList));
        }


    }
}