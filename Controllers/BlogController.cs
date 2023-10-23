using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebAPI.Models;
using System.IO;

namespace BlogWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet(Name = "GetAllArticles")]
        public ActionResult Get()
        {
            return _blogRepository.Get();
        }

        [HttpGet("articles/{name}", Name = "GetArticlesByName")]
        [HttpGet("theme/{theme}", Name = "GetArticlesByTheme")]
        [HttpGet("tag/{tag}", Name = "GetArticlesByTag")]
        public ActionResult Get(string name = "", string theme = "", string tag = "")
        {
            if (name != string.Empty && theme == string.Empty && tag == string.Empty)
                return _blogRepository.GetArticlesByName(name);
            else if (name == string.Empty && theme != string.Empty && tag == string.Empty)
                return _blogRepository.GetArticlesByTheme(theme);
            else if (tag != string.Empty && name == string.Empty && theme == string.Empty)
                return _blogRepository.GetArticlesByTag(tag);

            return new NotFoundResult();
        }

        [HttpGet("{id}", Name = "GetArticle")]
        public IActionResult Get(int Id)
        {
            Article article = _blogRepository.Get(Id);

            if (article == null)
            {
                return NotFound();
            }

            return new JsonResult(article);
        }

        [HttpPost]
        public IActionResult Create([FromBody] PostArticle article)
        {
            if (article == null)
            {
                return BadRequest();
            }


            //_blogRepository.Create(article);
            //return CreatedAtRoute("GetArticle", new { id = article.Id }, article);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int Id, [FromBody] Article updatedArticle)
        {
            if (updatedArticle == null || updatedArticle.Id != Id)
            {
                return BadRequest();
            }

            var article = _blogRepository.Get(Id);
            if (article == null)
            {
                return NotFound();
            }

            _blogRepository.Update(updatedArticle);
            return RedirectToRoute("GetAllArticles");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var deletedArticle = _blogRepository.Delete(Id);

            if (deletedArticle == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedArticle);
        }       
    }
}
