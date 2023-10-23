using BlogWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlogWebAPI
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _context;

        public BlogRepository(BlogDbContext context)
        {
            _context = context;
        }

        public void Create(Article article)
        {
            _context.Articles.Add(article);
            _context.SaveChanges();
        }

        public Article Delete(int id)
        {
            Article article = Get(id);

            if (article != null)
            {
                _context.Articles.Remove(article);
                _context.SaveChanges();
            }

            return article;
        }

        public ActionResult Get()
        {
            var data = _context.Articles
                .Include(a => a.Author)
                .Include(a => a.Theme)
                .Include(a => a.Tags);
            
            return new JsonResult(data);
        }

        public Article Get(int id)
        {
            var article = _context.Articles.Find(id);
            _context.Authors.Where(a => a.Id == article.AuthorFK).Load();
            _context.Themes.Where(t => t.Id == article.ThemeFK).Load();
            _context.Entry(article).Collection(a => a.Tags).Load();

            return article;
        }

        public ActionResult GetArticlesByTheme(string theme)
        {
            var data = _context.Articles
                .Where(a => a.Theme.Name == theme)
                .Include(a => a.Author)
                .Include(a => a.Theme)
                .Include(a => a.Tags);

            return new JsonResult(data);
        }

        public ActionResult GetArticlesByName(string name)
        {
            name = name.ToLower();

            var data = _context.Articles
                .Where(a => a.Name.ToLower().Contains(name))
                .Include(a => a.Author)
                .Include(a => a.Theme)
                .Include(a => a.Tags);

            return new JsonResult(data);
        }
        
        public ActionResult GetArticlesByTag(string tagName)
        {
            Tag tag = default;

            foreach (var t in _context.Tags)
            {
                if (t.Name == tagName)
                {
                    tag = t;
                    break;
                }
            }

            var data = _context.Articles
                .Where(a => a.Tags.Contains(tag))
                .Include(a => a.Author)
                .Include(a => a.Theme)
                .Include(a => a.Tags);

            return new JsonResult(data);
        }

        public void Update(Article updatedArticle)
        {
            Article currentArticle = Get(updatedArticle.Id);
            currentArticle.Name = updatedArticle.Name;
            currentArticle.Date = updatedArticle.Date;
            currentArticle.ThemeFK = updatedArticle.ThemeFK;
            currentArticle.Theme = updatedArticle.Theme;
            currentArticle.AuthorFK = updatedArticle.AuthorFK;
            currentArticle.Author = updatedArticle.Author;
            currentArticle.ImgUrl = updatedArticle.ImgUrl;
            currentArticle.Text = updatedArticle.Text;

            _context.Articles.Update(currentArticle);
            _context.SaveChanges();
        }

    }
}
