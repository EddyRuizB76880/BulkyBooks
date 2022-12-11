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

        public IActionResult Edit(int? id) {
            Category category = GetCategory(id);
            if (category == null){
                return NotFound();
            }
            else{
                return View(category);
            }
        }

        public IActionResult Delete(int? id) {
            Category category = GetCategory(id);
            if (category == null){
                return NotFound();
            }
            else{
                return View(category);
            }
        }


        [HttpPost]
        //Avoid cross site request forgery attack
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category newCategory)
        {
           
            if (IsValidInput(newCategory))
            {
                //Add recently submitted category
                _db.Categories.Add(newCategory);
                //Commit changes to db
                _db.SaveChanges();
                //TempData stores data that expires after one use. Use it to display one-time alerts.
                //It is a mapping. To learn how to retrieve its data, check _Notifications, in the Shared folder.
                TempData["task_completed"] = ("Category successfully created");
                //Go back to index page
                //Note: Index is invoked because it is defined in this controller.
                //In other circumstances, it might be necessary to specify controller too, in the second parameter.
                return RedirectToAction("Index");
            }

            return View(newCategory);
        }

        [HttpPost]
        //Avoid cross site request forgery attack
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category updatedCategory)
        {
       
            if (IsValidInput(updatedCategory))
            {
                //Update category
                _db.Categories.Update(updatedCategory);
                //Commit changes to db
                _db.SaveChanges();
                TempData["task_completed"] = ("Category successfully updated");
                //Go back to index page
                //Note: Index is invoked because it is defined in this controller.
                //In other circumstances, it might be necessary to specify controller too, in the second parameter.
                return RedirectToAction("Index");
            }

            return View(updatedCategory);
        }
        //The ActionName method allows us to change how the method will be invoked from the form in the view
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //A Controller can't have 2 methods that have the same name and receive the same parameters.
        public IActionResult DeletePOST(int? id){
            
            Category category = GetCategory(id);
            if (category != null) {
                _db.Categories.Remove(category);
                _db.SaveChanges();
                TempData["task_completed"]=("Category successfully deleted");
                return RedirectToAction("Index");
            }

            return NotFound();
        }

            private Boolean IsValidInput(Category category) { 
      
            //Suppose there is a rule that says that dOrder can't be the same as the category's name
            if (category.name == category.DisplayOrder.ToString()){   
                //AddModelError allows us to customize the error message the page shows.
                //This is a map; first value is a key and second is the value.
                //If the key's name matches a model's attribute, then the error message will be displayed on
                //the asp-validation-for space that corresponds to the matching attribute on the form and on the.
                //validation summary. If not, it will only be displayed on the summary
                ModelState.AddModelError("displayorder", "Display order and name can't have the same value");
            }

            //We need to make sure that incoming model is valid according to 
            //sql table's rules like not null. For that, we use ModelState

            return ModelState.IsValid;
        }

        private Category GetCategory(int? id) {
            if (id == null || id == 0)
            {
                return null;
            }
            //Other methods to retrieve a single row from db
            //SingleOrDefault: Throws exception if there is more than one matchingrow
            //FirstOrDefault: Throws an exception if there is no matching row
            var category = _db.Categories.Find(id);
            return category;
        }

    }
}
