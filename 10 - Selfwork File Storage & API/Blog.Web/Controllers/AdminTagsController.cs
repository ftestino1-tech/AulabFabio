using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models.ViewModels;
using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Repositories;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {

        //private readonly BlogDbContext _blogDbContext;

        //public AdminTagsController(BlogDbContext blogDbContext)
        //{
        //    this._blogDbContext = blogDbContext;
        //}

        private readonly ITagRepository _tagRepository;

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }

        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }   


        [HttpGet]
        public IActionResult List()
        {
            //var tags = _blogDbContext.Tags.ToList();
            var tags = _tagRepository.GetAll();
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
            
            //_blogDbContext.Tags.Add(tag);
            //_blogDbContext.SaveChanges();
            _tagRepository.Add(tag);

            TempData["SuccessMessage"] = "Creazione avvenuta con successo!";

            //return View("Add");
            return RedirectToAction("List");
        }


        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            // var tag = _blogDbContext.Tags.FirstOrDefault(x => x.Id == id);
            var tag = _tagRepository.GetByID(id);

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
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = _tagRepository.Update(tag);

            if (updatedTag != null)
            {
                TempData["SuccessMessage"] = "Modifica avvenuta con successo!";
                return RedirectToAction("List");
            }
            TempData["ErrorMessage"] = "Errore nella modifica!";
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = _tagRepository.Delete(editTagRequest.Id);

            if (deletedTag != null)
            {
                TempData["SuccessMessage"] = "Cancellazione avvenuta con successo!";
                return RedirectToAction("List");
            }   

            TempData["ErrorMessage"] = "Errore nella cancellazione!";
            
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
            

    }
}