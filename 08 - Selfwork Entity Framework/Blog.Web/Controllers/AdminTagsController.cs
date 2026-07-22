using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models.ViewModels;
using Blog.Web.Data;
using Blog.Web.Models.Domain;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {

        private readonly BlogDbContext _blogDbContext;

        public AdminTagsController(BlogDbContext blogDbContext)
        {
            this._blogDbContext = blogDbContext;
        }

        [HttpGet]
        public IActionResult List()
        {
            var tags = _blogDbContext.Tags.ToList();
            return View(tags);
        }

        // GET: /AdminTags/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult SubmitTag(AddTagRequest addTagRequest)     
        {
            //var name = addTagRequest.Name;
            //var displayName = addTagRequest.DisplayName;

            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            
            _blogDbContext.Tags.Add(tag);
            _blogDbContext.SaveChanges();

            TempData["SuccessMessage"] = "Creazione avvenuta con successo!";

            //return View("Add");
            return RedirectToAction("List");
        }


        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = _blogDbContext.Tags.FirstOrDefault(x => x.Id == id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }   

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var existingTag = _blogDbContext.Tags.Find(editTagRequest.Id);

            if (existingTag != null)
            {
                existingTag.Name = editTagRequest.Name;
                existingTag.DisplayName = editTagRequest.DisplayName;

                _blogDbContext.SaveChanges();

                TempData["SuccessMessage"] = "Modifica avvenuta con successo!";

                return RedirectToAction("List");
            }

            TempData["ErrorMessage"] = "Errore nella modifica!";

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var tag = _blogDbContext.Tags.Find(editTagRequest.Id);

            if (tag != null)
            {
                _blogDbContext.Tags.Remove(tag);
                _blogDbContext.SaveChanges();

                TempData["SuccessMessage"] = "Cancellazione avvenuta con successo!";    

                return RedirectToAction("List");
            }

            TempData["ErrorMessage"] = "Errore nella cancellazione!";
            
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
            

    }
}