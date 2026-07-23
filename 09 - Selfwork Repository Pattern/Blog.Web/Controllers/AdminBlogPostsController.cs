using Microsoft.AspNetCore.Mvc;
using Blog.Web.Repositories;
using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Blog.Web.Models.Domain;
using System.IO.Compression;


namespace Blog.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;
        

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            var blogPosts = blogPostRepository.GetAll();
            return View(blogPosts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var tags = tagRepository.GetAll();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.DisplayName
                })
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddBlogPostRequest addBlogPostRequest)
        {
            
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading, 
                PageTitle = addBlogPostRequest.PageTitle, 
                Content = addBlogPostRequest.Content, 
                ShortDescription = addBlogPostRequest.ShortDescription, 
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle, 
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author, 
                Visible = addBlogPostRequest.Visible, 
                Tags = new List<Tag>()
            };

            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTagId, out var tagGuid))
                {
                    var existingTag = tagRepository.GetByID(tagGuid);

                    if (existingTag != null)
                    {
                        blogPost.Tags.Add(existingTag); 
                    }
                }
            }

            blogPostRepository.Add(blogPost);

            TempData["SuccessMessage"] = "Creazione avvenuta con successo!";
            
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var blogPost = blogPostRepository.GetById(id);

            if (blogPost == null)
            {
                return NotFound(); 
            }

            var model = new EditBlogPostRequest
            {
                Id = blogPost.Id, 
                Heading = blogPost.Heading, 
                PageTitle = blogPost.PageTitle, 
                Content = blogPost.Content, 
                ShortDescription = blogPost.ShortDescription, 
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle, 
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author, 
                Visible = blogPost.Visible
            };

            var selectedTags = new List<Tag>(); 

            if (Request.SelectedTags != null && Request.SelectedTags.Any())
            {
                
            }

            var tags = tagRepository.GetAll();

            model.Tags = tags.Select(x => new SelectListItem
            {
                Text = x.Name, 
                Value = x.Id.ToString()
            });

            model.SelectedTags = blogPost.Tags
                .Select(x => x.Id.ToString())
                .ToArray(); 

            return View(model); 
        }

        public IActionResult Edit(EditBlogPostRequest editBlogPostRequest)
        {

            var blogPost = new BlogPost
            {
                Id = editBlogPostRequest.Id, 
                Heading = editBlogPostRequest.Heading, 
                PageTitle = editBlogPostRequest.PageTitle, 
                Content = editBlogPostRequest.Content, 
                ShortDescription = editBlogPostRequest.ShortDescription, 
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle, 
                PublishedDate = editBlogPostRequest.PublishedDate,
                Author = editBlogPostRequest.Author, 
                Visible = editBlogPostRequest.Visible
            };

            var selectedTags = new List<Tag>(); 

            if (editBlogPostRequest.SelectedTags != null && editBlogPostRequest.SelectedTags.Any())
            {
                foreach (var tagIdString in editBlogPostRequest.SelectedTags)
                {
                    if (Guid.TryParse(tagIdString, out var tagId))
                    {
                        var foundTag = tagRepository.GetByID(tagId); 

                        if (foundTag != null)
                        {
                            selectedTags.Add(foundTag);
                        }
                    }
                }
            }

           blogPost.Tags = selectedTags;

           var updateBlog = blogPostRepository.Update(blogPost); 

           if (updateBlog != null)
            {
                return RedirectToAction("List", new { id = editBlogPostRequest.Id });
            }

            return View(editBlogPostRequest); 
        }

        [HttpPost]
        public IActionResult Delete(EditBlogPostRequest editBlogPostRequest)
        {
            var deleteBlog = blogPostRepository.Delete(editBlogPostRequest.Id);

            if (deleteBlog != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editBlogPostRequest.Id});
        }
    }
}   