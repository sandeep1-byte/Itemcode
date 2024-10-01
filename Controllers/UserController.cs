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
                return Json(new { success = false, message = "Please correctly fill the form." });
            }

            // Check if the email already exists
            var existingUser = context.Users.FirstOrDefault(u => u.Email == userDto.Email);
            if (existingUser != null)
            {
                return Json(new { success = false, message = "Email already in use." });
            }

            User user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password // Consider hashing this password before saving for security reasons.
            };

            context.Users.Add(user);
            context.SaveChanges();

            return Json(new { success = true, message = "User registered successfully!" });
        }


        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInDto userDto)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == userDto.Email && u.Password == userDto.Password);

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Please correctly fill the form." });
            }

            if (user == null)
            {
                return Json(new { success = false, message = "Invalid login attempt." });
            }

            // Log the user in (implement your authentication logic here)

            // Store necessary data in TempData to access in the view
            TempData["UserId"] = user.Id; // Store user ID
            TempData["UserName"] = user.Name; // Store user name or other data as needed

            return Json(new { success = true, message = "You have signed in successfully!", userId = user.Id, userName = user.Name });
            return RedirectToAction("Index", "DashBoard");
        }
    }
}
