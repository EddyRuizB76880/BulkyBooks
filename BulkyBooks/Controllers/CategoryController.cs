using BulkyBooks.Data;
using BulkyBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBooks.Controllers
{
    //Remember the structure of a .NET MVC url:
    //server/controller/method/?id
    public class CategoryController : Controller
    {
        private ApplicationDBContext _db;
        //Here, dependency injection happens because of line #12 on the Program.cs file
        //No initialization is needed.
        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }

        //This method will invoke if url is: localhost/Category/Index
        public IActionResult Index()
        {
            //Retrieve from the Categories table
            IEnumerable<Category> categories = _db.Categories;

            //The returned view will be the Index.cshtml file found on the View/Category folder.
            //Pass categories to View.
            return View(categories);
        }

        public IActionResult Create() 
        {
            return View();
        }


        [HttpPost]
        //Avoid cross site request forgery attack
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category newCategory)
        {
            //Suppose there is a rule that says that dOrder can't be the same as the name
            if (newCategory.name == newCategory.DisplayOrder.ToString()) 
            {   //AddModelError allows us to customize the error message the page shows.
                //This is a map; first value is a key and second is the value.
                //If the key's name matches a model's attribute, then the error message will be displayed on
                //the asp-validation-for space that corresponds the matching attribute on the form and on the.
                //validation summary. If not, it will only be displayed on the summary
                ModelState.AddModelError("displayorder","Display order and name can't have the same value");
            }
            //We need to make sure that incoming model is valid according to 
            //sql table's rules like not null. For that, we use ModelState
            if (ModelState.IsValid)
            {
                //Add recently submitted category
                _db.Categories.Add(newCategory);
                //Commit changes to db
                _db.SaveChanges();
                //Go back to index page
                //Note: Index is invoked because it is defined in this controller.
                //In other circumstances, it might be necessary to specify controller too, in the second parameter.
                return RedirectToAction("Index");
            }

            return View(newCategory);
        }

    }
}
