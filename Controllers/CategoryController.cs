﻿using Item_Code_management_System.Models;
using Item_Code_management_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Item_Code_management_System.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext context;

        public CategoryController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Category/CreateCategory
        public IActionResult CreateCategory()
        {
            return View();
        }

        // POST: Category/CreateCategory
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                // Check if the category already exists in the database
                bool categoryExists = this.context.Categories
                    .Any(c => c.CategoryName.ToLower() == category.CategoryName.ToLower()); // Compare in lowercase

                if (categoryExists)
                {
                    // Add error to ModelState if the category already exists
                    ModelState.AddModelError("CategoryName", "This category already exists. Please enter a different category name.");
                    return View(category); // Return the view with the current model to show the error
                }

                // Add new category to the database
                this.context.Categories.Add(category);
                this.context.SaveChanges();

                // Set a success message
                TempData["msg"] = "Category added successfully!";
                return RedirectToAction("CreateCategory");
            }

            return View(category); // Return the view with the current model if ModelState is invalid
        }



        public IActionResult CategoryList()
        {
            var categories = this.context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Delete(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("CategoryList", "Category"); // Redirect if the category doesn't exist
            }

            context.Categories.Remove(category); // Remove the category from the database
            context.SaveChanges(); // Save changes

            return RedirectToAction("CategoryList", "Category"); // Redirect to the category list
        }
        public IActionResult Edit(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();//404
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,CategoryName")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                context.Update(category);
                context.SaveChanges();
                TempData["msg"] = "Category updated successfully!";
                return RedirectToAction("CategoryList", "Category");
            }
            return View(category);
        }
    }
}
