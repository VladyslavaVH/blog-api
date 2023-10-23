using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebAPI
{
    public interface IBlogRepository
    {
        ActionResult Get();
        Article Get(int id);
        ActionResult GetArticlesByName(string name);
        ActionResult GetArticlesByTheme(string theme);
        ActionResult GetArticlesByTag(string tagName);
        void Create(Article article);
        void Update(Article article);
        Article Delete(int id);
    }
}
