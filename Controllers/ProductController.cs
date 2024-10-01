using Item_Code_management_System.Models;
using Item_Code_management_System.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

public class ProductController : Controller
{
    private readonly ApplicationDbContext context;

    public ProductController(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IActionResult CreateProduct()
    {
        var categories = context.Categories.Select(c => new SelectListItem
    {
     Value = c.Id.ToString(),
      Text = c.CategoryName
    })
    .ToList();

        var productDto = new ProductDto
        {
            Categories = categories
        };

        return View(productDto);
    }

    [HttpPost]
    public IActionResult CreateProduct(ProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            productDto.Categories = context.Categories.Select(c => new SelectListItem
         {
              Value = c.Id.ToString(),
                          Text = c.CategoryName
         }).ToList();

            TempData["ErrorMessage"] = "Please correctly fill the form.";
            return View(productDto);
        }

        // Check if the item code already exists in the database
        var existingProduct = context.Products.FirstOrDefault(p => p.ItemCode == productDto.ItemCode);
        if (existingProduct != null)
        {
            TempData["ErrorMessage"] = "Item code already in use.";
            productDto.Categories = context.Categories .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }).ToList();

            return View(productDto);
        }

        // Create a new Product object and populate it with data from the DTO
        Product product = new Product()
        {
            ItemCode = productDto.ItemCode,
            Description = productDto.Description,
            Price = productDto.Price,
            Unit = productDto.Unit,
            CategoryId = productDto.CategoryId // Foreign key for item category
        };

        // Add the new product to the database
        context.Products.Add(product);
        context.SaveChanges();  

        // Set a success message in TempData and redirect to the product listing action
        TempData["SuccessMessage"] = "Product added successfully!";
        return RedirectToAction("ProductList", "Product"); // Adjust the redirect action as needed
    }
    public IActionResult ProductList()
    {
        var products = this.context.Products.ToList(); // Fetching all products from the database
        return View(products); // Returning the product list to the view
    }
    public IActionResult Delete(int id)
    {
        var product = context.Products.Find(id);
        if (product == null)
        {
            return RedirectToAction("ProductList", "Product"); // Redirect if the product doesn't exist
        }

        context.Products.Remove(product); // Remove the product from the database
        context.SaveChanges(); // Save changes

        return RedirectToAction("ProductList", "Product"); // Redirect to the product list
    }

    public IActionResult Edit(int id)
    {
        var product = context.Products.Find(id);
        if (product == null)
        {
            return NotFound(); // 404 if the product doesn't exist
        }

        var productDto = new ProductDto
        {
            Id = product.Id,
            ItemCode = product.ItemCode,
            Description = product.Description,
            Price = product.Price,
            Unit = product.Unit,
            CategoryId = product.CategoryId,
            Categories = context.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }).ToList()
        };

        return View(productDto); // Returning the product data with categories
    }

    [HttpPost]
    public IActionResult Edit(int id, ProductDto productDto)
    {
        if (id != productDto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound(); // If product doesn't exist
            }

            // Updating product properties from the DTO
            product.ItemCode = productDto.ItemCode;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.Unit = productDto.Unit;
            product.CategoryId = productDto.CategoryId;

            context.Update(product); // Update the product
            context.SaveChanges(); // Save the changes to the database
            TempData["msg"] = "Product updated successfully!";
            return RedirectToAction("ProductList", "Product"); // Redirect to the product list
        }

        productDto.Categories = context.Categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.CategoryName
        }).ToList();

        return View(productDto);
    }
    }
