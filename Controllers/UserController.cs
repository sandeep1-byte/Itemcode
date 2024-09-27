using Item_Code_management_System.Models;
using Item_Code_management_System.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Item_Code_management_System.Controllers
{
    public class UserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext context;

        public UserController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SignUpDto userDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correctly Fill form.";
                return View(userDto);
            }

            // Check if the email already exists
            var existingUser = context.Users.FirstOrDefault(u => u.Email == userDto.Email);
            if (existingUser != null)
            {
                TempData["ErrorMessage"] = "Email already in use.";
                return View(userDto);
            }

            User user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password // Consider hashing this password before saving for security reasons.
            };

            context.Users.Add(user);
            context.SaveChanges();

            TempData["SuccessMessage"] = "User registered successfully!";
            return RedirectToAction("SignIn", "User");
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInDto userDto)
        {

            var user = context.Users.FirstOrDefault(u => u.Email == userDto.Email && u.Password == userDto.Password);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Invalid login attempt.";
                return View(userDto);
            }
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correctly Fill form.";
                return View(userDto);
            }

            // Log the user in (implement your authentication logic here)

            TempData["SuccessMessage"] = "You have signed in successfully!";
            return RedirectToAction("Index", "DashBoard");
        }
    }
}
