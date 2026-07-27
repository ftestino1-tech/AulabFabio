namespace Blog.Web.Repositories;

using System;
using System.Collections.Generic;
using Blog.Web.Data;
using Blog.Web.Models.Domain;

public class TagRepository : ITagRepository
{
    private readonly BlogDbContext _blogDbContext;

    public TagRepository(BlogDbContext blogDbContext)
    {
        this._blogDbContext = blogDbContext;
    }

    public Tag Add(Tag tag)
    {
        _blogDbContext.Tags.Add(tag);
        _blogDbContext.SaveChanges();
        return tag;
    }

    public Tag Delete(Guid id)
    {
        var tag = _blogDbContext.Tags.Find(id);
       
        if (tag != null)
        {
            _blogDbContext.Tags.Remove(tag);
            _blogDbContext.SaveChanges();

               return tag;
        }

        return null;
    }

    public List<Tag> GetAll()
    {
        return _blogDbContext.Tags.ToList();
    }

    public Tag GetByID(Guid id)
    {
        return _blogDbContext.Tags.FirstOrDefault(t => t.Id == id);
    }

    public Tag Update(Tag tag)
    {
        var existingTag = _blogDbContext.Tags.Find(tag.Id);

        if (existingTag != null)
        {
            existingTag.Name = tag.Name;
            existingTag.DisplayName = tag.DisplayName;

            _blogDbContext.SaveChanges();

            return tag;
        }

        return null;

    }
}