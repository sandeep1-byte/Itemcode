using Item_Code_management_System.Models;
using Item_Code_management_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Item_Code_management_System.Controllers
{
    public class UserItemMappingController : Controller
    {
        private readonly ApplicationDbContext context;

        public UserItemMappingController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> CreateUserMapping()
        {
            var itemCodes = await context.Products.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.ItemCode
            }).ToListAsync();

            var userMappingDto = new UserCodeMappingDto
            {
                AvailableItemCodes = itemCodes
            };

            return View(userMappingDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserMapping(UserCodeMappingDto userMappingDto)
        {
            var existingMapping = await context.ItemCodeMappings.FirstOrDefaultAsync(m => m.UserItemCode == userMappingDto.UserItemCode);

            if (existingMapping != null)
            {
                TempData["ErrorMessage"] = "User item code already in use.";
                userMappingDto.AvailableItemCodes = await context.Products.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.ItemCode
                }).ToListAsync();

                return View(userMappingDto);
            }

            try
            {
                var userMapping = new UserItemCodeMapping
                {
                    ItemCodeId = userMappingDto.ItemCodeId,
                    UserItemCode = userMappingDto.UserItemCode,
                    UserPrice = userMappingDto.UserPrice
                };

                context.ItemCodeMappings.Add(userMapping);
                await context.SaveChangesAsync();

                TempData["SuccessMessage"] = "User mapping added successfully!";
                return RedirectToAction("UserMappingList", "UserItemMapping");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
                userMappingDto.AvailableItemCodes = await context.Products.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.ItemCode
                }).ToListAsync();

                return View(userMappingDto);
            }
            if (!ModelState.IsValid)
            {
                userMappingDto.AvailableItemCodes = await context.Products.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.ItemCode
                }).ToListAsync();

                TempData["ErrorMessage"] = "Please correctly fill the form.";
                return View(userMappingDto);
            }
        }

        public async Task<IActionResult> UserMappingList()
        {
            // Fetch the list of UserItemCodeMappings
            var userItemCodeMappings = await context.ItemCodeMappings
                .Include(u => u.Product) // Include related Product data
                .ToListAsync();

            // Map to UserCodeMappingDto
            var userCodeMappings = userItemCodeMappings.Select(u => new UserCodeMappingDto
            {
                Id = u.Id,
                ItemCodeId = u.ItemCodeId,
                UserItemCode = u.UserItemCode,
                UserPrice = u.UserPrice,
                ItemCode = u.Product.ItemCode // Assuming the Product has an ItemCode property
            }).ToList();

            return View(userCodeMappings);
        }

        // Auto-search for ItemCode from the Product table
        [HttpGet]
        public JsonResult GetItemCodes(string term)
        {
            var itemCodes = context.Products
                                    .Where(p => p.ItemCode.Contains(term))
                                    .Select(p => new { id = p.Id, text = p.ItemCode })
                                    .ToList();

            return Json(itemCodes);
        }

    }
}
